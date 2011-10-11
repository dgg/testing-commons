using NUnit.Framework;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.NUnit.Tests.Subjects;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class LambdaPropertyConstraintTester : ConstraintTesterBase
	{
		#region Matches

		[Test]
		public void Matches_PassingConstraint_True()
		{
			var subject = new LambdaPropertyConstraint<FlatCustomer>(c => c.Name, Is.StringStarting("N"));
			Assert.That(subject.Matches(new FlatCustomer {Name = "Name"}), Is.True);
		}

		[Test]
		public void Matches_FailingConstraint_False()
		{
			var subject = new LambdaPropertyConstraint<FlatCustomer>(c => c.Name, Is.StringStarting("n"));
			Assert.That(subject.Matches(new FlatCustomer {Name = "Name"}), Is.False);
		}

		[Test]
		public void Matches_NotAProperty_Exception()
		{
			Assert.That(() => new LambdaPropertyConstraint<FlatCustomer>(c => c.ToString(), Is.Not.Null),
			            Throws.ArgumentException.With.Property("ParamName").EqualTo("property"));
		}

		[Test]
		public void Matches_PropertyNotfound_Exception()
		{
			var subject = new LambdaPropertyConstraint<FlatCustomer>(c => c.Name.Length, Is.EqualTo(5));
			Assert.That(() => subject.Matches(new FlatCustomer {Name = "12345"}),
			            Throws.ArgumentException
			            	.With.Message.StringContaining("Length"));
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_FailingConstraint_ExpectedContainsProperty()
		{
			var subject = new LambdaPropertyConstraint<FlatCustomer>(c => c.Name, Is.StringStarting("v"));
			Assert.That(GetMessage(subject, new FlatCustomer {Name = "Value"}), Is.StringStarting(TextMessageWriter.Pfx_Expected + "property Name"));
		}

		[Test]
		public void WriteMessageTo_FailingConstraint_ExpectedContainsConstraintDescription()
		{
			var subject = new LambdaPropertyConstraint<FlatCustomer>(c => c.Name, Is.StringStarting("n"));
			Assert.That(GetMessage(subject, new FlatCustomer { Name = "Value" }), Is.StringContaining("starting with \"n\""));
		}

		[Test]
		public void WriteMessageTo_FailingConstraint_ActualContainsPropretyValue()
		{
			var subject = new LambdaPropertyConstraint<FlatCustomer>(c => c.Name, Is.StringStarting("n"));
			Assert.That(GetMessage(subject, new FlatCustomer { Name = "Value" }), 
				//Is.Empty
				Is.StringContaining(TextMessageWriter.Pfx_Actual + "\"Value\"")
			);
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			Assert.That(new{ Name = "123" }, new LambdaPropertyConstraint<FlatCustomer>(a => a.Name, Has.Length.EqualTo(3)));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			Assert.That(new { Name = "123" }, Must.Have.Property<FlatCustomer>(a => a.Name, Has.Length.EqualTo(3)));
		}
	}
}
