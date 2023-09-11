using NSubstitute;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Constraints.Support;

using Iz = Testing.Commons.NUnit.Constraints.Iz;

namespace Testing.Commons.NUnit.Tests.Constraints;

[TestFixture]
public class ConstrainedEnumerableConstraintTester : ConstraintTesterBase
{
	#region ApplyTo

	[Test]
	public void ApplyTo_SubjectHasLessItemsThanConstraintsProvided_Failure()
	{
		var subject = new ConstrainedEnumerableConstraint(Is.EqualTo(1), Is.EqualTo(2));

		Assert.That(matches(subject, new[] { 1 }), Is.False);
	}

	[Test]
	public void ApplyTo_SubjectHasMoreItemsThanConstraintsProvided_Failure()
	{
		var subject = new ConstrainedEnumerableConstraint(Is.EqualTo(1));

		Assert.That(matches(subject, new[] { 1, 2 }), Is.False);
	}

	[Test]
	public void Apply_FirstItemDoesNotMatchConstraint_False()
	{
		var subject = new ConstrainedEnumerableConstraint(Is.GreaterThan(0), Is.EqualTo(2));

		Assert.That(matches(subject, new[] { -1, 2 }), Is.False);
	}

	[Test]
	public void ApplyTo_FirstItemDoesNotMatchConstraint_SubsequentConstraintsNotEvaluated()
	{
		Constraint failing = Substitute.For<Constraint>(),
			notEvaluated = Substitute.For<Constraint>();

		var subject = new ConstrainedEnumerableConstraint(failing, notEvaluated);
		failing.ApplyTo(-1).Returns(new ConstraintResult(null, null, false));

		subject.ApplyTo(new[] { -1, 2 });

		notEvaluated.DidNotReceive().ApplyTo(Arg.Any<int>());
	}

	[Test]
	public void ApplyTo_SecondItemDoesNotMatchConstraint_False()
	{
		var subject = new ConstrainedEnumerableConstraint(Is.EqualTo(1), Is.EqualTo(2), Is.EqualTo(3));

		Assert.That(matches(subject, new[] { 1, -2, 3 }), Is.False);
	}

	[Test]
	public void apply_SecondItemDoesNotMatchConstraint_SubsequentConstraintsNotEvaluated()
	{
		Constraint
			passing = Substitute.For<Constraint>(),
			failing = Substitute.For<Constraint>(),
			notEvaluated = Substitute.For<Constraint>();

		var subject = new ConstrainedEnumerableConstraint(failing, notEvaluated);
		passing.ApplyTo(1).Returns(new ConstraintResult(null, null, false));
		failing.ApplyTo(-2).Returns(new ConstraintResult(null, null, false));

		subject.ApplyTo(new[] { 1, -2, 3 });

		notEvaluated.DidNotReceive().ApplyTo(Arg.Any<int>());
	}

	#endregion

	#region WriteMessageTo

	[Test]
	public void WriteMessageTo_SubjectHasLessItemsThanConstraintsProvided_ExpectedIsLengthOfSubjectAndActualIsNumberOfConstraints()
	{
		var subject = new ConstrainedEnumerableConstraint(Is.EqualTo(1), Is.EqualTo(2));

		Assert.That(getMessage(subject, new[] { 1 }),
			Does.Contain(TextMessageWriter.Pfx_Expected + 1).And
			.Contains(TextMessageWriter.Pfx_Actual + 2));
	}

	[Test]
	public void WriteMessageTo_SubjectHasMoreItemsThanConstraintsProvided_ExpectedIsLengthOfSubjectAndActualIsNumberOfConstraints()
	{
		var subject = new ConstrainedEnumerableConstraint(Is.EqualTo(1));

		Assert.That(getMessage(subject, new[] { 1, 2 }),
			Does.Contain(TextMessageWriter.Pfx_Expected + 2).And
			.Contains(TextMessageWriter.Pfx_Actual + 1));
	}

	[Test]
	public void WriteMessageTo_FirstItemDoesNotMatchConstraint_DescriptionContainsIndexOfFailingItem_ExpectedIsOfFailingConstraint_ActualIsValueOfOffendingItem()
	{
		var subject = new ConstrainedEnumerableConstraint(Is.GreaterThan(0), Is.EqualTo(2));

		Assert.That(getMessage(subject, new[] { -1, 2 }), Does.Contain("# 0").And
			.Contain(TextMessageWriter.Pfx_Expected + "greater than 0").And
			.Contains(TextMessageWriter.Pfx_Actual + "-1"));
	}

	[Test]
	public void WriteMessageTo_SecondItemDoesNotMatchConstraint_DescriptionContainsIndexOfFailingItem_ExpectedIsOfFailingConstraint_ActualIsValueOfOffendingItem()
	{
		var subject = new ConstrainedEnumerableConstraint(Is.EqualTo('1'), Is.GreaterThan('3'));

		Assert.That(getMessage(subject, new[] { '1', '2' }), Does.Contain("# 1").And
			.Contain(TextMessageWriter.Pfx_Expected + "greater than '3'").And
			.Contains(TextMessageWriter.Pfx_Actual + "'2'"));
	}

	#endregion

	[Test]
	public void CanBeNewedUp()
	{
		Assert.That(new[] { 1, 2 }, new ConstrainedEnumerableConstraint(Is.EqualTo(1), Is.LessThan(3)));
	}

	[Test]
	public void CanBeCreatedWithExtension()
	{
		Assert.That(new[] { 1, 2 }, Iz.Constrained(Is.EqualTo(1), Is.LessThan(3)));
	}
}
