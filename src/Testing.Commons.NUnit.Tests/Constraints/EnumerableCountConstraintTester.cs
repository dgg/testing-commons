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
		private readonly Constraint _constraint;

		public EnumerableCountConstraint(Constraint constraint)
		{
			_constraint = constraint;
		}

		private Constraint _inner;
		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			var result = new ConstraintResult(this, actual, true);
			_inner = new TypeRevealingConstraint(typeof(IEnumerable));
			result = _inner.ApplyTo(actual);
			if (result.IsSuccess)
			{
				
			}
			return result;
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