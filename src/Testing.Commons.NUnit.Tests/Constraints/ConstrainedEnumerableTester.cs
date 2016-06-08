using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class ConstrainedEnumerableTester : Support.ConstraintTesterBase
	{
		#region ApplyTo

		[Test]
		public void ApplyTo_SubjectHasLessItemsThanConstraintsProvided_Failure()
		{
			var subject = new ConstrainedEnumerable(Is.EqualTo(1), Is.EqualTo(2));

			Assert.That(matches(subject, new[] { 1 }), Is.False);
		}

		[Test]
		public void ApplyTo_SubjectHasMoreItemsThanConstraintsProvided_Failure()
		{
			var subject = new ConstrainedEnumerable(Is.EqualTo(1));

			Assert.That(matches(subject, new[] { 1, 2 }), Is.False);
		}

		[Test]
		public void Apply_FirstItemDoesNotMatchConstraint_False()
		{
			var subject = new ConstrainedEnumerable(Is.GreaterThan(0), Is.EqualTo(2));

			Assert.That(matches(subject, new[] { -1, 2 }), Is.False);
		}

		[Test]
		public void ApplyTo_FirstItemDoesNotMatchConstraint_SubsequentConstraintsNotEvaluated()
		{
			Constraint failing = Substitute.For<Constraint>(),
				notEvaluated = Substitute.For<Constraint>();

			var subject = new ConstrainedEnumerable(failing, notEvaluated);
			failing.ApplyTo(-1).Returns(new ConstraintResult(failing, -1, false));

			subject.ApplyTo(new[] { -1, 2 });

			notEvaluated.DidNotReceive().ApplyTo(Arg.Any<int>());
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_SubjectHasLessItemsThanConstraintsProvided_ExpectedIsLengthOfSubjectAndActualIsNumberOfConstraints()
		{
			var subject = new ConstrainedEnumerable(Is.EqualTo(1), Is.EqualTo(2));

			Assert.That(getMessage(subject, new[] { 1 }), 
				Does.Contain(TextMessageWriter.Pfx_Expected + 1).And
				.Contain(TextMessageWriter.Pfx_Actual + 2));
		}

		[Test]
		public void WriteMessageTo_SubjectHasMoreItemsThanConstraintsProvided_ExpectedIsLengthOfSubjectAndActualIsNumberOfConstraints()
		{
			var subject = new ConstrainedEnumerable(Is.EqualTo(1));

			Assert.That(getMessage(subject, new[] { 1, 2 }),
				Does.Contain(TextMessageWriter.Pfx_Expected + 2).And
				.Contain(TextMessageWriter.Pfx_Actual + 1));
		}

		[Test]
		public void WriteMessageTo_FirstItemDoesNotMatchConstraint_DescriptionContainsIndexOfFailingItem_ExpectedIsValueOfFailingConstraint_ActualIsValueOfOffendingItem()
		{
			var subject = new ConstrainedEnumerable(Is.GreaterThan(0), Is.EqualTo(2));

			Assert.That(getMessage(subject, new[] { -1, 2 }), Does.Contain("# 0").And
				.Contain(TextMessageWriter.Pfx_Expected + "greater than 0").And
				.Contain(TextMessageWriter.Pfx_Actual + "-1"));
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			Assert.That(new[] { 1, 2 }, new ConstrainedEnumerable(Is.EqualTo(1), Is.LessThan(3)));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			Assert.That(new[] { 1, 2 }, Must.Be.Constrained(Is.EqualTo(1), Is.LessThan(3)));
		}
	}

	public class ConstrainedEnumerable : Constraint
	{
		private readonly Constraint[] _constraints;

		/// <summary>
		/// Builds an instance with the provided constraints.
		/// </summary>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		public ConstrainedEnumerable(params Constraint[] constraints) : this(constraints.AsEnumerable()) { }

		/// <summary>
		/// Builds an instance with the provided constraints.
		/// </summary>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		public ConstrainedEnumerable(IEnumerable<Constraint> constraints)
		{
			_constraints = constraints.ToArray();
		}

		private Constraint _inner;
		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			// it is ok to iterate the collection as most of the times it will be a small controlled collection
			var collection = (actual as IEnumerable).Cast<object>().ToArray();

			ConstraintResult result = sameNumberOfElementsAndConstraints(collection, _constraints.Length);
			if (result.IsSuccess)
			{
				for (int i = 0; i < collection.Length && result.IsSuccess; i++)
				{
					_inner = new IndexedConstraint(collection, i, _constraints[i]);
					result = _inner.ApplyTo(collection.GetValue(i));
				}	
			}

			return result;
		}

		public override string Description => _inner.Description;

		private ConstraintResult sameNumberOfElementsAndConstraints(Array current, int numberOfConstraints)
		{
			_inner = new MatchingLength(current, current.Length);
			var result = _inner.ApplyTo(numberOfConstraints);
			return result;
		}

		/// <summary>
		/// Checks that the count of the enumerable and the provided constraints is the same. 
		/// </summary>
		class MatchingLength : EqualConstraint
		{
			private readonly Array _items;

			public MatchingLength(Array items, object expected)
				: base(expected)
			{
				_items = items;
			}

			public override ConstraintResult ApplyTo<TActual>(TActual actual)
			{
				return new MatchingLengthResult(_items, this, base.ApplyTo(actual));
			}

			class MatchingLengthResult : ConstraintResult
			{
				private readonly Array _items;

				public MatchingLengthResult(Array items, IConstraint constraint, ConstraintResult result) : base(constraint, result.ActualValue, result.IsSuccess)
				{
					_items = items;
				}

				public override void WriteMessageTo(MessageWriter writer)
				{
					writer.WriteMessageLine(0, "{0} has a different number of items than constraints are provided.", getCollectionValue(writer));
					base.WriteMessageTo(writer);
				}

				private string getCollectionValue(MessageWriter writer)
				{
					writer.WriteActualValue(_items);
					var sb = writer.GetStringBuilder();
					string items = sb.ToString();
					sb.Remove(0, sb.Length);
					return items;
				}
			}
		}

		/// <summary>
		/// Encapsulates a contraint applied to an enumerable element to enhance the feedback in case of failure by providing the index of the element that provoked the failure.
		/// </summary>
		class IndexedConstraint : Constraint
		{
			private readonly Array _items;
			private readonly int _index;
			private readonly IConstraint _constraint;
			public IndexedConstraint(Array items, int index, IResolveConstraint constraint)
			{
				_items = items;
				_index = index;
				_constraint = constraint.Resolve();
			}

			public override ConstraintResult ApplyTo<TActual>(TActual actual)
			{
				return new IndexedResult(_items, _index, _constraint, _constraint.ApplyTo(actual));
			}

			class IndexedResult : ConstraintResult
			{
				private readonly Array _items;
				private readonly int _index;

				public IndexedResult(Array items, int index, IConstraint constraint, ConstraintResult result) : base(constraint, result.ActualValue, result.IsSuccess)
				{
					_items = items;
					_index = index;
				}

				public override void WriteMessageTo(MessageWriter writer)
				{
					string collectionValue = getCollectionValue(writer);
					writer.WriteMessageLine(0, "Element # {0} from {1} failed to satisfy constraint.", _index, collectionValue);
					base.WriteMessageTo(writer);
				}

				private string getCollectionValue(MessageWriter writer)
				{
					writer.WriteActualValue(_items);
					var sb = writer.GetStringBuilder();
					string items = sb.ToString();
					sb.Remove(0, sb.Length);
					return items;
				}
			}
		}
	}

	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="ConstrainedEnumerable"/> with the provided constraints.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		/// <returns>Instance built.</returns>
		public static ConstrainedEnumerable Constrained(this Must.BeEntryPoint entry, params Constraint[] constraints)
		{
			return new ConstrainedEnumerable(constraints);
		}

		
	}

	
}