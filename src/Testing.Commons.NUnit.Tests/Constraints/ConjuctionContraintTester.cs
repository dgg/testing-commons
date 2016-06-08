using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.NUnit.Tests.Subjects;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class ConjuctionContraintTester : ConstraintTesterBase
	{
		#region ApplyTo

		[Test]
		public void ApplyTo_PassingConstraint_Success()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(1),
				Is.GreaterThan(5));

			Assert.That(matches(subject, 6), Is.True);
		}

		[Test]
		public void ApplyTo_FailingConstraint_Failure()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(1),
				Is.GreaterThan(5));

			Assert.That(matches(subject, 4), Is.False);
		}

		[Test]
		public void ApplyTo_NoConstraints_Success()
		{
			var subject = new ConjunctionConstraint();

			Assert.That(matches(subject, new object()), Is.True);
		}

		[Test]
		public void ApplyTo_EmptyConstraints_Success()
		{
			var subject = new ConjunctionConstraint(new Constraint[0]);

			Assert.That(matches(subject, new object()), Is.True);
		}

		[Test]
		public void ApplyTo_AllNullContraints_Success()
		{
			var subject = new ConjunctionConstraint(null, null);

			Assert.That(matches(subject, new object()), Is.True);
		}

		[Test]
		public void ApplyTo_SingleFailingConstraints_Failure()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(5));

			Assert.That(matches(subject, 4), Is.False);
		}

		[Test]
		public void ApplyTo_SinglePassingConstraints_Success()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(5));

			Assert.That(matches(subject, 6), Is.True);
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_FailingConstraint_ContainsConjuctionOfConstraints()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(1),
				Is.GreaterThan(5));

			Assert.That(getMessage(subject, 4), Does.StartWith(
				TextMessageWriter.Pfx_Expected + "greater than 1 and greater than 5"));
		}

		[Test]
		public void WriteMessageTo_SingleFailingConstraint_ContainsConstraints()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(5));

			Assert.That(getMessage(subject, 4), Does.StartWith(TextMessageWriter.Pfx_Expected + "greater than 5"));
		}

		[Test]
		public void WriteMessageTo_FailingConstraint_ContainsSpecificOffender()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(1),
				Is.GreaterThan(5));

			Assert.That(getMessage(subject, 4), Does.Contain("Specifically: greater than 5"));
		}

		[Test]
		public void WriteMessageTo_SingleFailingConstraint_ContainsTheOffender()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(5));

			Assert.That(getMessage(subject, 4), Does.Contain("Specifically: greater than 5"));
		}

		[Test]
		public void WriteMessageTo_FailingConstraint_ActualContainsActual()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(1),
				Is.GreaterThan(5));

			Assert.That(getMessage(subject, 4),
				Does.Contain(TextMessageWriter.Pfx_Actual + "4")
			);
		}

		[Test]
		public void WriteMessageTo_FailingConstraintThatEvaluatesAMember_ActualConstainsActualAndMember()
		{
			var customer = new FlatCustomer { Name = "name", PhoneNumber = "123456" };
			var nameConstraint = Must.Have.Property(nameof(FlatCustomer.Name), Is.EqualTo("name"));
			var subject = Must.Satisfy.Conjunction(
				Must.Have.Property(nameof(FlatCustomer.Name), Does.Contain("me")),
				Must.Have.Property(nameof(FlatCustomer.PhoneNumber), Does.Contain("-")),
				Is.Not.Null);

			Assert.That(customer, nameConstraint);
			Assert.That(getMessage(subject, customer), Does.Contain(typeof(FlatCustomer).Name).And
				.Contains("123456"));
		}

		#endregion
	}
}