using System.ComponentModel;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Constraints.Support;
using Testing.Commons.NUnit.Tests.Subjects;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class PropertyChangedConstraintTester : ConstraintTesterBase
	{
		#region ApplyTo

		[Test]
		public void ApplyTo_SetterDoesNotRaiseEvent_False()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			var subject = new PropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);

			Assert.That(matches(subject, () => raising.I = 3), Is.False);
		}

		[Test]
		public void ApplyTo_WrongPropertyName_False()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();

			raising
				.When(r => r.I = Arg.Any<int>())
				.Do(ci => raising.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(raising, new PropertyChangedEventArgs("Wrong")));

			var subject = new PropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);
			Assert.That(matches(subject, () => raising.I = 3), Is.False);
		}

		[Test]
		public void ApplyTo_RightPropertyName_True()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			raising
				.When(r => r.I = Arg.Any<int>())
				.Do(ci => raising.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(raising, new PropertyChangedEventArgs("I")));

			var subject = new PropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);
			Assert.That(matches(subject, () => raising.I = 3), Is.True);
		}

		#endregion

		#region WriteDescriptionTo

		[Test]
		public void WriteDescriptionTo_SetterDoesNotRaiseEvent_ExpectationWithEvent_PropertyName_ActualWithEventNotRaised()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			var subject = new PropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);

			Assert.That(getMessage(subject, () => raising.I = 3),
				Does.StartWith(TextMessageWriter.Pfx_Expected + "raise event 'PropertyChanged'").And
				.Contain("PropertyName equal to \"I\"").And
				.Contain(TextMessageWriter.Pfx_Actual + "event 'PropertyChanged' not raised"));
		}

		[Test]
		public void WriteDescriptionTo_WrongPropertyName_ActualWithOffendingValue()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			raising
				.When(r => r.I = Arg.Any<int>())
				.Do(ci => raising.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(raising, new PropertyChangedEventArgs("Wrong")));

			var subject = new PropertyChangedConstraint<IRaisingSubject>(raising, r => r.I);
			Assert.That(getMessage(subject, () => raising.I = 3),
				Does.Contain(TextMessageWriter.Pfx_Actual + "\"Wrong\""));
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			var raising = Substitute.For<IRaisingSubject>();
			raising
				.When(r => r.I = Arg.Any<int>())
				.Do(ci => raising.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(raising, new PropertyChangedEventArgs("I")));

			Assert.That(() => raising.I = 3, new PropertyChangedConstraint<IRaisingSubject>(raising, r => r.I));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			var raising = Substitute.For<IRaisingSubject>();
			raising.When(r => r.I = Arg.Any<int>())
				.Do(ci => raising.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(raising, new PropertyChangedEventArgs("I")));

			Assert.That(() => raising.I = 3, Must.Raise.PropertyChanged(raising, r => r.I));
		}

		[Test]
		public void AllowsPropertyChanged_ToBeDifferentFromTheMemberName()
		{
			var raising = Substitute.For<IRaisingSubject>();
			raising.When(r => r.I = Arg.Any<int>())
				.Do(ci => raising.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(raising,
					new PropertyChangedEventArgs("somethingElse")));

			Assert.That(() => raising.I = 3, Must.Raise.PropertyChanged(raising, Is.EqualTo("somethingElse")));
		}
	}
}