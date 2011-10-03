using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;
using Testing.Commons.NUnit.Tests.Subjects;

namespace Testing.Commons.NUnit.Tests
{
	public class ArrangingTestTester : ArrangingTest<string>
	{
		private static int COUNTER;

		protected override string initSubject()
		{
			COUNTER++;
			return "subject";
		}

		[Test]
		public void Subject_IsInitialized()
		{
			Assert.That(Subject, Is.EqualTo("subject"));
		}

		[Test]
		public void ArrangingMethod_InvokedBeforeThisTest() { }

		[Test]
		public void ArrangingMethod_AndBeforeThisTest() { }

		[TestFixtureTearDown]
		public void AfterAllTestAreRun()
		{
			Assert.That(COUNTER, Is.EqualTo(3), "run for every test in the fixture");
		}
	}

	public class UsefulWhenDealingWithSubjectsWithMockableDependencies : ArrangingTest<ISubjectWithDependencies>
	{
		// dependencies go into fields for test to access them
		private IDependencyOne _one;
		private IDependencyTwo _two;

		protected override ISubjectWithDependencies initSubject()
		{
			// dependencies are initialized inside the arrange method
			_one = MockRepository.GenerateStub<IDependencyOne>();
			_two = MockRepository.GenerateStub<IDependencyTwo>();

			return new SubjectWithDependencies(_one, _two);
		}

		[Test]
		public void DoSomethingWithOne_InvokesDependencyOne()
		{
			// Act
			Subject.DoSomethingWithOne();

			// Assert
			_one.AssertWasCalled(o => o.DoSomething());
		}

		[Test]
		public void DoSomethingWithOne_InvokesDependencyTwo()
		{
			// Act
			Subject.DoSomethingWithTwo();

			// Assert
			_two.AssertWasCalled(o => o.DoSomethingElse());
		}

		[Test]
		public void DoSomethingWithBoth_InvokesDependencyTwo()
		{
			// Act
			Subject.DosomethingWithBoth();

			// Assert
			_one.AssertWasCalled(o => o.DoSomething());
			_two.AssertWasCalled(o => o.DoSomethingElse());
		}

		[Test]
		public void IndividualTests_CanOverrideDependencyBehavior()
		{
			// init the subject in a custom method
			initSubjectWith(MockRepository.GenerateStrictMock<IDependencyTwo>());

			// no expectation on a strict mock makes the mock throw

			Assert.That(() => Subject.DoSomethingWithTwo(), Throws.InstanceOf<ExpectationViolationException>());
		}

		private void initSubjectWith(IDependencyTwo dependencyTwo)
		{
			Subject = new SubjectWithDependencies(_one, dependencyTwo);
		}
	}
}
