using NUnit.Framework;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.NUnit.Tests.Subjects.Comparisons;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class ImplementsEquatableConstraintTester : ConstraintTesterBase
	{
		#region Matches

		[Test]
		public void Matches_CorrectImplementationReference_True()
		{
			string eq = "eq", notEq = "notEq";
			var targetSubject = new EquatableSubject<string>("target")
				.Setup(eq, true)
				.Setup(notEq, false)
				.Setup(null, false);

			var subject = new ImplementsEquatableConstraint<string>(eq, notEq);

			Assert.That(subject.Matches(targetSubject), Is.True);
		}

		[Test]
		public void Matches_CorrectImplementationValue_True()
		{
			int eq = 20, notEq = 10;
			var target = new EquatableSubject<int>("target")
				.Setup(eq, true)
				.Setup(notEq, false);
			// no need to setup null comparison as the comparer target is a value type

			var subject = new ImplementsEquatableConstraint<int>(eq, notEq);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_CorrectImplementationSelf_True()
		{
			EquatableSubject eq = new EquatableSubject("eq"), notEq = new EquatableSubject("notEq");
			var target = new EquatableSubject("target");
			target.Setup(target, true)
				.Setup(eq, true)
				.Setup(notEq, false)
				.Setup(null, false);

			var subject = new ImplementsEquatableConstraint<EquatableSubject>(eq, notEq);

			Assert.That(subject.Matches(target), Is.True);
		}

		#endregion

		[Test]
		public void CanBeNewedUp_EquatableToSelf()
		{
			EquatableSubject eq = new EquatableSubject("eq"), notEq = new EquatableSubject("notEq");
			var target = new EquatableSubject("target");
			target.Setup(target, true)
				.Setup(eq, true)
				.Setup(notEq, false);

			Assert.That(target, new ImplementsEquatableConstraint<EquatableSubject>(eq, notEq));
		}

		[Test]
		public void CanBeCreatedWithExtension_EquatableToSelf()
		{
			EquatableSubject eq = new EquatableSubject("eq"), notEq = new EquatableSubject("notEq");
			var target = new EquatableSubject("target");
			target.Setup(target, true)
				.Setup(eq, true)
				.Setup(notEq, false);

			Assert.That(target, Must.Satisfy.EquatableSpecificationAgainst(eq, notEq));
		}
	}
}