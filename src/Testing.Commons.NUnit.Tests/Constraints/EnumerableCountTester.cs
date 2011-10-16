using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class EnumerableCountContraintTester : ConstraintTesterBase
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
			Assert.That(assertion, Throws.ArgumentException.With.Message.StringContaining("Length"));

			assertion = () => Assert.That(e, Has.Count.EqualTo(2));
			Assert.That(assertion, Throws.ArgumentException.With.Message.StringContaining("Count"));
		}

		#endregion

		#region Matches

		[Test]
		public void Matches_NotAnEnumerable_False()
		{
			var subject = new EnumerableCountConstraint(null);
			Assert.That(subject.Matches(3), Is.False);
		}

		[Test]
		public void Matches_NullEnumerable_False()
		{
			var subject = new EnumerableCountConstraint(null);
			Assert.That(subject.Matches((IEnumerable)null), Is.False);
		}

		[Test]
		public void Matches_EnumerableWithNotMatchingCount_False()
		{
			IEnumerable e = new[] { 1, 2, 3 }.Where(i => i <= 2);
			var subject = new EnumerableCountConstraint(Is.GreaterThan(4));
			Assert.That(subject.Matches(e), Is.False);
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_NotAnEnumerable_ExpectedContainsIEnumerable()
		{
			var subject = new EnumerableCountConstraint(null);
			Assert.That(GetMessage(subject, 3),
				Is.StringStarting(TextMessageWriter.Pfx_Expected + "instance of <System.Collections.IEnumerable>"));
		}

		[Test]
		public void WriteMessageTo_NotAnEnumerable_ActualContainsTypeAndValue()
		{
			var subject = new EnumerableCountConstraint(null);
			Assert.That(GetMessage(subject, PlatformID.MacOSX), Is
				.StringContaining(TextMessageWriter.Pfx_Actual + "instance of <System.PlatformID>").And
				.StringContaining("MacOSX"));
		}

		[Test]
		public void WriteMessageTo_NullEnumerable_ExpectedContainsIEnumerable()
		{
			var subject = new EnumerableCountConstraint(null);
			Assert.That(GetMessage(subject, (IEnumerable)null),
				Is.StringStarting(TextMessageWriter.Pfx_Expected + "instance of <System.Collections.IEnumerable>"));
		}

		[Test]
		public void WriteMessageTo_NullEnumerable_ActualConstainsNull()
		{
			var subject = new EnumerableCountConstraint(null);
			Assert.That(GetMessage(subject, (IEnumerable)null),
				Is.StringContaining(TextMessageWriter.Pfx_Actual + "null"));
		}

		[Test]
		public void WriteMessageTo_EnumerableWithNotMatchingCount_ExpectedContainsCountExpectation()
		{
			IEnumerable e = new[] { 1, 2, 3 }.Where(i => i <= 2);
			var subject = new EnumerableCountConstraint(Is.GreaterThan(4));
			Assert.That(GetMessage(subject, e), Is.StringStarting(TextMessageWriter.Pfx_Expected + "number of elements greater than 4"));
		}

		[Test]
		public void WriteMessageTo_EnumerableWithNotMatchingCount_ActualContainsCollectionValues()
		{
			IEnumerable e = new[] { '1', '2', '3' }.Where(i => i <= '2');
			var subject = new EnumerableCountConstraint(Is.GreaterThan(4));
			Assert.That(GetMessage(subject, e), Is.StringContaining(TextMessageWriter.Pfx_Actual + "2 -> < '1', '2' >"));
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
			Assert.That(e, Must.Have.Count(Is.LessThan(1)));
		}
	}
}
