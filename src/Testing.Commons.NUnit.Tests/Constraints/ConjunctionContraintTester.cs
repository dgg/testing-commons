using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.NUnit.Tests.Subjects;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class ConjunctionContraintTester : ConstraintTesterBase
	{
		#region Matches

		[Test]
		public void Matches_PassingConstraint_True()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(1),
				Is.GreaterThan(5));

			Assert.That(subject.Matches(6), Is.True);
		}

		[Test]
		public void Matches_FailingConstraint_False()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(1),
				Is.GreaterThan(5));

			Assert.That(subject.Matches(4), Is.False);
		}

		[Test]
		public void Matches_NoConstraints_True()
		{
			var subject = new ConjunctionConstraint();

			Assert.That(subject.Matches(new object()), Is.True);
		}

		[Test]
		public void Matches_EmptyConstraints_True()
		{
			var subject = new ConjunctionConstraint(new Constraint[0]);

			Assert.That(subject.Matches(new object()), Is.True);
		}

		[Test]
		public void Matches_AllNullContraints_True()
		{
			var subject = new ConjunctionConstraint(new Constraint[] { null, null });

			Assert.That(subject.Matches(new object()), Is.True);
		}

		[Test]
		public void Matches_SingleFailingConstraints_False()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(5));

			Assert.That(subject.Matches(4), Is.False);
		}

		[Test]
		public void Matches_SinglePassingConstraints_True()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(5));

			Assert.That(subject.Matches(6), Is.True);
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_FailingConstraint_ContainsConjuctionOfConstraints()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(1),
				Is.GreaterThan(5));

			Assert.That(GetMessage(subject, 4), Is.StringStarting(TextMessageWriter.Pfx_Expected + "greater than 1 and greater than 5"));
		}

		[Test]
		public void WriteMessageTo_SingleFailingConstraint_ContainsConstraints()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(5));

			Assert.That(GetMessage(subject, 4), Is.StringStarting(TextMessageWriter.Pfx_Expected + "greater than 5"));
		}

		[Test]
		public void WriteMessageTo_FailingConstraint_ContainsSpecificOffender()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(1),
				Is.GreaterThan(5));

			Assert.That(GetMessage(subject, 4), Is.StringContaining("Specifically: greater than 5"));
		}

		[Test]
		public void WriteMessageTo_SingleFailingConstraint_ContainsTheOffender()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(5));

			Assert.That(GetMessage(subject, 4), Is.StringContaining("Specifically: greater than 5"));
		}

		[Test]
		public void WriteMessageTo_FailingConstraint_ActualContainsActual()
		{
			var subject = new ConjunctionConstraint(
				Is.GreaterThan(1),
				Is.GreaterThan(5));

			Assert.That(GetMessage(subject, 4),
				//Is.Empty
				Is.StringContaining(TextMessageWriter.Pfx_Actual + "4")
			);
		}

		[Test]
		public void WriteMessageTo_FailingConstraintThatEvaluatesAMember_ActualConstainsActualAndMember()
		{
			var customer = new FlatCustomer { Name = "name", PhoneNumber = "123456" };
			var subject = Must.Satisfy.Conjunction(
				Must.Have.Property<FlatCustomer>(c => c.Name, Is.StringContaining("me")),
				Must.Have.Property<FlatCustomer>(c => c.PhoneNumber, Is.StringContaining("-")),
				Is.Not.Null);

			Assert.That(GetMessage(subject, customer), Is.StringContaining(typeof(FlatCustomer).Name).And
				.StringContaining("123456"));
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			Assert.That(4, new ConjunctionConstraint(
				Is.GreaterThan(1),
				Is.GreaterThan(3)));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			Assert.That(4, Must.Satisfy.Conjunction(
				Is.GreaterThan(1),
				Is.GreaterThan(3)));
		}
	}
}