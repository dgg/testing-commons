using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
			var _collection = (actual as IEnumerable).Cast<object>().ToArray();
			return sameNumberOfElementsAndConstraints(_collection, _constraints.Length);
		}

		private ConstraintResult sameNumberOfElementsAndConstraints(Array current, int numberOfConstraints)
		{
			_inner = new MatchingLength(current, current.Length);
			return _inner.ApplyTo(numberOfConstraints);
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
				return new MatchingLengthResult(this, _items, base.ApplyTo(actual));
			}

			class MatchingLengthResult : ConstraintResult
			{
				private readonly Array _items;

				public MatchingLengthResult(IConstraint constraint, Array items, ConstraintResult result) : base(constraint, result.ActualValue, result.IsSuccess)
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