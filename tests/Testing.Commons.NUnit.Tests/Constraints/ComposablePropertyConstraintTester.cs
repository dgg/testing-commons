using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Constraints.Support;
using Testing.Commons.NUnit.Tests.Subjects;

namespace Testing.Commons.NUnit.Tests.Constraints;

[TestFixture]
public class ComposablePropertyConstraintTester : ConstraintTesterBase
{
	#region ApplyTo

	[Test]
	public void ApplyTo_PassingConstraint_Success()
	{
		var subject = new ComposablePropertyConstraint(
			nameof(FlatCustomer.Name),
			Is.Empty);

		Assert.That(matches(subject, new FlatCustomer()), Is.True);

		Assert.That(getMessage(subject, new FlatCustomer()), Is.Empty);
	}


	[Test]
	public void ApplyTo_FailingConstraint_Failure()
	{
		var subject = new ComposablePropertyConstraint(
			nameof(FlatCustomer.Name),
			Is.EqualTo("lol"));

		Assert.That(matches(subject, new FlatCustomer()), Is.False);
	}

	#endregion

	#region WriteMessageTo

	[Test]
	public void WriteMessageTo_FailingConstraint_ContainsMember()
	{
		var subject = new ComposablePropertyConstraint(
			nameof(FlatCustomer.Name),
			Is.EqualTo("lol"));

		Assert.That(getMessage(subject, new FlatCustomer()), Does.StartWith(
			TextMessageWriter.Pfx_Expected + "property Name"));
	}


	[Test]
	public void WriteMessageTo_FailingConstraint_ContainsSpecificMessage()
	{
		var subject = new ComposablePropertyConstraint(
			nameof(FlatCustomer.Name),
			Is.EqualTo("lol"));

		Assert.That(getMessage(subject, new FlatCustomer()), Does.Contain("equal to \"lol\""));
	}


	[Test]
	public void WriteMessageTo_FailingConstraint_ActualContainsActual()
	{
		var subject = new ComposablePropertyConstraint(
			nameof(FlatCustomer.Name),
			Is.EqualTo("lol"));

		Assert.That(getMessage(subject, new FlatCustomer { Name = "Bob" }),
			Does.Contain(TextMessageWriter.Pfx_Actual + "\"Bob\"")
		);
	}

	#endregion
}
