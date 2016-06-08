using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class EnumerableCountConstraintTester
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

		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			return new ConstraintResult(this, actual, true);
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