using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using Testing.Commons.NUnit.Constraints.Support;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class EnumerableCountConstraintTester : Support.ConstraintTesterBase
	{
		#region Exploratory

		[Test]
		public void Array_MustUseLength()
		{
			Assert.That(new[] { 1, 2, 3 }, Has.Length.EqualTo(3));
		}

		[Test]
		public void List_MustUseCount()
		{
			Assert.That(new List<int> { 1, 2, 3 }, Has.Count.EqualTo(3));
		}

		[Test]
		public void Enumerable_FromArray_MustUseLength()
		{
			IEnumerable<int> e = new[] { 1, 2, 3 };
			Assert.That(e, Has.Length.EqualTo(3));
		}

		[Test]
		public void Enumerable_FromList_MustUseCount()
		{
			IEnumerable<int> e = new List<int> { 1, 2, 3 };
			Assert.That(e, Has.Count.EqualTo(3));
		}

		[Test]
		public void Enumereble_FromLinq_CannotUseLengthOrCount()
		{
			IEnumerable<int> e = new[] { 1, 2, 3 }.Where(i => i <= 2);

			TestDelegate assertion = () => Assert.That(e, Has.Length.EqualTo(2));
			Assert.That(assertion, Throws.ArgumentException.With.Message.Contain("Length"));

			assertion = () => Assert.That(e, Has.Count.EqualTo(2));
			Assert.That(assertion, Throws.ArgumentException.With.Message.Contain("Count"));
		}

		#endregion

		#region ApplyTo

		[Test]
		public void ApplyTo_NotAnEnumerable_False()
		{
			var subject = new EnumerableCountConstraint(null);
			Assert.That(matches(subject, 3), Is.False);
		}

		[Test]
		public void ApplyTo_NullEnumerable_False()
		{
			var subject = new EnumerableCountConstraint(null);
			Assert.That(matches(subject, (IEnumerable)null), Is.False);
		}

		[Test]
		public void ApplyTo_EnumerableWithNotMatchingCount_False()
		{
			IEnumerable e = new[] { 1, 2, 3 }.Where(i => i <= 2);
			var subject = new EnumerableCountConstraint(Is.GreaterThan(4));
			Assert.That(matches(subject, e), Is.False);
		}

		[Test]
		public void ApplyTo_EnumerableWithMatchingCount_True()
		{
			IEnumerable e = new[] { 1, 2, 3 }.Where(i => i <= 2);
			var subject = new EnumerableCountConstraint(Is.EqualTo(2));
			Assert.That(matches(subject, e), Is.True);
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_NotAnEnumerable_ExpectedEnumerable_ActualContainsTypeAndValue()
		{
			var subject = new EnumerableCountConstraint(null);
			Assert.That(getMessage(subject, PlatformID.MacOSX), Does
				.StartWith(TextMessageWriter.Pfx_Expected + "instance of <System.Collections.IEnumerable>").And
				.Contains(TextMessageWriter.Pfx_Actual + "instance of <System.PlatformID>").And
				.Contains("MacOSX"));
		}

		[Test]
		public void WriteMessageTo_NullEnumerable_ExpectedContainsIEnumerable_ActualConstainsNull()
		{
			var subject = new EnumerableCountConstraint(null);
			Assert.That(getMessage(subject, (IEnumerable)null), Does.
				StartWith(TextMessageWriter.Pfx_Expected + "instance of <System.Collections.IEnumerable>").And
				.Contains(TextMessageWriter.Pfx_Actual + "null"));
		}

		[Test]
		public void WriteMessageTo_EnumerableWithNotMatchingCount_ExpectedContainsCount_ActualContainsCollectionValues()
		{
			IEnumerable e = new[] { '1', '2', '3' }.Where(i => i <= '2');
			var subject = new EnumerableCountConstraint(Is.GreaterThan(4));
			Assert.That(getMessage(subject, e), Does
				.StartWith(TextMessageWriter.Pfx_Expected + "number of elements greater than 4").And
				.Contains(TextMessageWriter.Pfx_Actual + "2 -> < '1', '2' >"));
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			IEnumerable e = new[] { 1, 2, 3 }.Where(i => i <= 2);
			Assert.That(e, new EnumerableCountConstraint(Is.LessThan(3)));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			IEnumerable e = new[] { 1, 2, 3 }.Where(i => i <= 2);
			Assert.That(e, Must.Have.Count(Is.LessThan(3)));
		}
	}

	public class EnumerableCountConstraint : Constraint
	{
		private readonly Constraint _countConstraint;

		public EnumerableCountConstraint(Constraint countConstraint)
		{
			_countConstraint = countConstraint;
		}

		private Constraint _inner;
		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			var result = new ConstraintResult(this, actual, true);
			_inner = new TypeRevealingConstraint(typeof(IEnumerable));
			result = _inner.ApplyTo(actual);
			if (result.IsSuccess)
			{
				var collection = (IEnumerable)actual;
				// ReSharper disable PossibleMultipleEnumeration
				ushort count = calculateCount(collection);
				_inner = new CountConstraint(_countConstraint, collection);
				// ReSharper restore PossibleMultipleEnumeration
				result = _inner.ApplyTo(count);
			}
			return result;
		}

		private ushort calculateCount(IEnumerable current)
		{
			ushort num = 0;
			IEnumerator enumerator = current.GetEnumerator();
			while (enumerator.MoveNext())
			{
				num++;
			}
			return num;
		}


		/// <summary>
		/// Used to test that an object is of the same type provided or derived from it and extend the information given for the actual failing value.
		/// </summary>
		/// <remarks></remarks>
		internal class TypeRevealingConstraint : InstanceOfTypeConstraint
		{
			public TypeRevealingConstraint(Type type) : base(type) { }

			public override ConstraintResult ApplyTo<TActual>(TActual actual)
			{
				return new TypeRevealingResult(this, actual, base.ApplyTo(actual));
			}

			class TypeRevealingResult : ConstraintResult
			{
				private readonly object _actual;

				public TypeRevealingResult(IConstraint constraint, object actual, ConstraintResult result)
					: base(constraint, result.ActualValue, result.IsSuccess)
				{
					_actual = actual;
				}

				public override void WriteActualValueTo(MessageWriter writer)
				{
					if (ActualValue != null)
					{
						writer.WritePredicate("instance of");
						// ActualValue cannot be used since it contains the type itself
						writer.WriteActualValue(_actual.GetType());
						writer.WriteActualConnector();
						writer.WriteValue(_actual);
					}
					else
					{
						writer.WriteActualValue(null);
					}
				}
			}
		}

		/// <summary>
		/// Wraps a constraint when used on the number of elements of an enumerable.
		/// </summary>
		internal class CountConstraint : Constraint
		{
			private readonly Constraint _decoree;
			private readonly IEnumerable _enumerable;

			public CountConstraint(Constraint decoree, IEnumerable enumerable)
			{
				_decoree = decoree;
				_enumerable = enumerable;
			}

			public override ConstraintResult ApplyTo<TActual>(TActual actual)
			{
				// actual is the count of the enumerable
				return new CountResult(this, _enumerable, _decoree.ApplyTo(actual));
			}

			public override string Description => "number of elements " + _decoree.Description;

			class CountResult : ConstraintResult
			{
				private readonly IEnumerable _collection;
				private readonly ConstraintResult _result;

				public CountResult(IConstraint constraint, IEnumerable collection, ConstraintResult result) : base(constraint, result.ActualValue, result.IsSuccess)
				{
					_collection = collection;
					_result = result;
				}

				public override void WriteActualValueTo(MessageWriter writer)
				{
					_result.WriteActualValueTo(writer);
					writer.WriteActualConnector();
					writer.WriteActualValue(_collection.Cast<object>().ToArray());
				}
			}
		}
	}

	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="EnumerableCountConstraint"/> that allows asserting on the number of elements of any instance of <see cref="System.Collections.IEnumerable"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="countConstraint">The constraint to be applied to the element count.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Count(this Must.HaveEntryPoint entry, Constraint countConstraint)
		{
			return new EnumerableCountConstraint(countConstraint);
		}
	}
}