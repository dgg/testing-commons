using System.ComponentModel;
using NUnit.Framework;
using Rhino.Mocks;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.NUnit.Tests.Subjects;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class RaisesPropertyChangedConstraintTester : ConstraintTesterBase
	{
		#region Matches

		[Test]
		public void Matches_SetterDoesNotRaiseEvent_False()
		{
			IRaisingSubject raising = MockRepository.GenerateMock<IRaisingSubject>();
			var subject = new RaisesPropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);

			Assert.That(subject.Matches(() => raising.I = 3), Is.False);
		}

		[Test]
		public void Matches_WrongPropertyName_False()
		{
			IRaisingSubject raising = MockRepository.GenerateMock<IRaisingSubject>();
			raising.Stub(r => r.I = Arg<int>.Is.Anything)
				.WhenCalled(i => raising.Raise(r => r.PropertyChanged += null,
				                               raising, new PropertyChangedEventArgs("Wrong")));

			var subject = new RaisesPropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);
			Assert.That(subject.Matches(() => raising.I = 3), Is.False);
		}

		[Test]
		public void Matches_RightPropertyName_True()
		{
			IRaisingSubject raising = MockRepository.GenerateMock<IRaisingSubject>();
			raising.Stub(r => r.I = Arg<int>.Is.Anything)
				.WhenCalled(i => raising.Raise(r => r.PropertyChanged += null,
				                               raising, new PropertyChangedEventArgs("I")));

			var subject = new RaisesPropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);
			Assert.That(subject.Matches(() => raising.I = 3), Is.True);
		}

		#endregion

		#region WriteDescriptionTo

		[Test]
		public void WriteDescriptionTo_SetterDoesNotRaiseEvent_ExpectationWithEvent()
		{
			IRaisingSubject raising = MockRepository.GenerateMock<IRaisingSubject>();
			var subject = new RaisesPropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);

			Assert.That(GetMessage(subject, () => raising.I = 3), Is.StringStarting(TextMessageWriter.Pfx_Expected + "raise event 'PropertyChanged'"));
		}

		[Test]
		public void WriteDescriptionTo_SetterDoesNotRaiseEvent_ExpectationWithPropertyName()
		{
			IRaisingSubject raising = MockRepository.GenerateMock<IRaisingSubject>();
			var subject = new RaisesPropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);

			Assert.That(GetMessage(subject, () => raising.I = 3), Is.StringContaining("PropertyName equal to \"I\""));
		}

		[Test]
		public void WriteDescriptionTo_SetterDoesNotRaiseEvent_ActualWithEventNotRaised()
		{
			IRaisingSubject raising = MockRepository.GenerateMock<IRaisingSubject>();
			var subject = new RaisesPropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);

			Assert.That(GetMessage(subject, () => raising.I = 3), Is.StringContaining(TextMessageWriter.Pfx_Actual + "event 'PropertyChanged' not raised"));
		}

		[Test]
		public void WriteDescriptionTo_WrongPropertyName_ActualWithOffendingValue()
		{
			IRaisingSubject raising = MockRepository.GenerateMock<IRaisingSubject>();
			raising.Stub(r => r.I = Arg<int>.Is.Anything)
				.WhenCalled(i => raising.Raise(r => r.PropertyChanged += null,
					raising, new PropertyChangedEventArgs("Wrong")));

			var subject = new RaisesPropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);
			Assert.That(GetMessage(subject, () => raising.I = 3), Is.StringContaining(TextMessageWriter.Pfx_Actual + "\"Wrong\""));
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			IRaisingSubject raising = MockRepository.GenerateMock<IRaisingSubject>();
			raising.Stub(r => r.I = Arg<int>.Is.Anything)
				.WhenCalled(i => raising.Raise(r => r.PropertyChanged += null,
					raising, new PropertyChangedEventArgs("I")));

			Assert.That(() => raising.I = 3, new RaisesPropertyChangedConstraint<IRaisingSubject>(raising, r => r.I));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			IRaisingSubject raising = MockRepository.GenerateMock<IRaisingSubject>();
			raising.Stub(r => r.I = Arg<int>.Is.Anything)
				.WhenCalled(i => raising.Raise(r => r.PropertyChanged += null,
					raising, new PropertyChangedEventArgs("I")));

			Assert.That(() => raising.I = 3, Must.Raise.PropertyChanged(raising, r => r.I));
		}
	}
}
