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
	public class PropertyChangingConstraintTester : ConstraintTesterBase
	{
		#region ApplyTo

		[Test]
		public void ApplyTo_SetterDoesNotRaiseEvent_False()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			var subject = new PropertyChangingConstraint<IRaisingSubject>(raising, r => r.I);

			Assert.That(matches(subject, () => raising.I = 3), Is.False);
		}

		[Test]
		public void ApplyTo_WrongPropertyName_False()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			raising.When(r => r.I = Arg.Any<int>())
				.Do(i => raising.PropertyChanging += Raise.Event<PropertyChangingEventHandler>(raising, new PropertyChangingEventArgs("Wrong")));

			var subject = new PropertyChangingConstraint<IRaisingSubject>(raising, r => r.I);
			Assert.That(matches(subject, () => raising.I = 3), Is.False);
		}

		[Test]
		public void ApplyTo_RightPropertyName_True()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			raising.When(r => r.I = Arg.Any<int>())
				.Do(i => raising.PropertyChanging += Raise.Event<PropertyChangingEventHandler>(raising, new PropertyChangingEventArgs("I")));

			var subject = new PropertyChangingConstraint<IRaisingSubject>(raising, r => r.I);
			Assert.That(matches(subject, () => raising.I = 3), Is.True);
		}

		#endregion

		#region WriteDescriptionTo

		[Test]
		public void WriteDescriptionTo_SetterDoesNotRaiseEvent_ExpectationWithEvent_PropertyName_ActualWithEventNotRaised()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			var subject = new PropertyChangingConstraint<IRaisingSubject>(raising, r => r.I);

			Assert.That(getMessage(subject, () => raising.I = 3), 
				Does.StartWith(TextMessageWriter.Pfx_Expected + "raise event 'PropertyChanging'").And
				.Contains("PropertyName equal to \"I\"").And
				.Contains(TextMessageWriter.Pfx_Actual + "event 'PropertyChanging' not raised")
				);
		}

		[Test]
		public void WriteDescriptionTo_WrongPropertyName_ActualWithOffendingValue()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			raising.When(r => r.I = Arg.Any<int>())
				.Do(i => raising.PropertyChanging += Raise.Event<PropertyChangingEventHandler>(raising, new PropertyChangingEventArgs("Wrong")));

			var subject = new PropertyChangingConstraint<IRaisingSubject>(raising, r => r.I);
			Assert.That(getMessage(subject, () => raising.I = 3), Does
				.Contain(TextMessageWriter.Pfx_Actual + "\"Wrong\""));
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			raising.When(r => r.I = Arg.Any<int>())
				.Do(i => raising.PropertyChanging += Raise.Event<PropertyChangingEventHandler>(raising, new PropertyChangingEventArgs("I")));

			Assert.That(() => raising.I = 3, new PropertyChangingConstraint<IRaisingSubject>(raising, r => r.I));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			raising.When(r => r.I = Arg.Any<int>())
				.Do(i => raising.PropertyChanging += Raise.Event<PropertyChangingEventHandler>(raising, new PropertyChangingEventArgs("I")));

			Assert.That(() => raising.I = 3, Must.Raise.PropertyChanging(raising, r => r.I));
		}

		[Test]
		public void AllowsPropertyChanging_ToBeDifferentFromTheMemberName()
		{
			var raising = Substitute.For<IRaisingSubject>();
			raising.When(r => r.I = Arg.Any<int>())
				.Do(ci => raising.PropertyChanging += Raise.Event<PropertyChangingEventHandler>(raising,
					new PropertyChangingEventArgs("somethingElse")));

			Assert.That(() => raising.I = 3, Must.Raise.PropertyChanging(raising, Is.EqualTo("somethingElse")));
		}
	}
}