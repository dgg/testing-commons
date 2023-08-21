using System.Collections;
using NUnit.Framework.Internal;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Constraints.Support;

namespace Testing.Commons.NUnit.Tests.Constraints;

[TestFixture]
public class EnumerableTallyConstraintTester : ConstraintTesterBase
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
#pragma warning disable NUnit2022
		IEnumerable<int> e = new[] { 1, 2, 3 };
		Assert.That(e, Has.Length.EqualTo(3));
#pragma warning restore NUnit2022
	}

	[Test]
	public void Enumerable_FromList_MustUseCount()
	{
#pragma warning disable NUnit2022
		IEnumerable<int> e = new List<int> { 1, 2, 3 };
		Assert.That(e, Has.Count.EqualTo(3));
#pragma warning restore NUnit2022
	}

	[Test]
	public void Enumereble_FromLinq_CannotUseLengthOrCount()
	{
#pragma warning disable NUnit2022
		IEnumerable<int> e = new[] { 1, 2, 3 }.Where(i => i <= 2);
		TestDelegate assertion = () => Assert.That(e, Has.Length.EqualTo(2));
		Assert.That(assertion, Throws.ArgumentException.With.Message.Contain("Length"));

		assertion = () => Assert.That(e, Has.Count.EqualTo(2));
		Assert.That(assertion, Throws.ArgumentException.With.Message.Contain("Count"));
#pragma warning restore NUnit2022
	}

	#endregion

	#region ApplyTo

	[Test]
	public void ApplyTo_NotAnEnumerable_False()
	{
		var subject = new EnumerableTallyConstraint(Is.AtLeast(0));
		Assert.That(matches(subject, 3), Is.False);
	}

	[Test]
	public void ApplyTo_NullEnumerable_False()
	{
		var subject = new EnumerableTallyConstraint(Is.AtLeast(0));
		Assert.That(matches(subject, (IEnumerable)null), Is.False);
	}

	[Test]
	public void ApplyTo_EnumerableWithNotMatchingCount_False()
	{
		IEnumerable e = new[] { 1, 2, 3 }.Where(i => i <= 2);
		var subject = new EnumerableTallyConstraint(Is.GreaterThan(4));
		Assert.That(matches(subject, e), Is.False);
	}

	[Test]
	public void ApplyTo_EnumerableWithMatchingCount_True()
	{
		IEnumerable e = new[] { 1, 2, 3 }.Where(i => i <= 2);
		var subject = new EnumerableTallyConstraint(Is.EqualTo(2));
		Assert.That(matches(subject, e), Is.True);
	}

	#endregion

	#region WriteMessageTo

	[Test]
	public void WriteMessageTo_NotAnEnumerable_ExpectedEnumerable_ActualContainsTypeAndValue()
	{
		var subject = new EnumerableTallyConstraint(Is.AtLeast(0));
		Assert.That(getMessage(subject, ConsoleColor.Black), Does
			.StartWith(TextMessageWriter.Pfx_Expected + "instance of <System.Collections.IEnumerable>").And
			.Contains(TextMessageWriter.Pfx_Actual + "instance of <System.ConsoleColor>").And
			.Contains("Black"));
	}

	[Test]
	public void WriteMessageTo_NullEnumerable_ExpectedContainsIEnumerable_ActualConstainsNull()
	{
		var subject = new EnumerableTallyConstraint(Is.AtLeast(0));
		Assert.That(getMessage(subject, (IEnumerable)null), Does.
			StartWith(TextMessageWriter.Pfx_Expected + "instance of <System.Collections.IEnumerable>").And
			.Contains(TextMessageWriter.Pfx_Actual + "null"));
	}

	[Test]
	public void WriteMessageTo_EnumerableWithNotMatchingCount_ExpectedContainsCount_ActualContainsCollectionValues()
	{
		IEnumerable e = new[] { '1', '2', '3' }.Where(i => i <= '2');
		var subject = new EnumerableTallyConstraint(Is.GreaterThan(4));
		Assert.That(getMessage(subject, e), Does
			.StartWith(TextMessageWriter.Pfx_Expected + "number of elements greater than 4").And
			.Contains(TextMessageWriter.Pfx_Actual + "2 -> < '1', '2' >"));
	}

	#endregion

	[Test]
	public void CanBeNewedUp()
	{
		IEnumerable e = new[] { 1, 2, 3 }.Where(i => i <= 2);
		Assert.That(e, new EnumerableTallyConstraint(Is.LessThan(3)));
	}

	[Test]
	public void CanBeCreatedWithExtension()
	{
		IEnumerable e = new[] { 1, 2, 3 }.Where(i => i <= 2);
		Assert.That(e, Haz.Tally(Is.LessThan(3)));
	}
}
