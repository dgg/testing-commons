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
	public class NoPropertyChangingConstraintTester : ConstraintTesterBase
	{
		#region ApplyTo

		[Test]
		public void ApplyTo_SetterDoesNotRaiseEvent_True()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			var subject = new NoPropertyChangingConstraint<IRaisingSubject>(raising);

			Assert.That(matches(subject, () => raising.I = 3), Is.True);
		}

		[Test]
		public void ApplyTo_SetterRaisesEvent_False()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();

			raising
				.When(r => r.I = Arg.Any<int>())
				.Do(ci => raising.PropertyChanging += Raise.Event<PropertyChangingEventHandler>(raising, new PropertyChangingEventArgs("anything")));

			var subject = new NoPropertyChangingConstraint<IRaisingSubject>(raising);
			Assert.That(matches(subject, () => raising.I = 3), Is.False);
		}

		#endregion

		#region WriteDescriptionTo

		[Test]
		public void WriteDescriptionTo_RaisesEvent_ExpectationWithEvent_ActualWithEventNotRaising()
		{
			IRaisingSubject raising = Substitute.For<IRaisingSubject>();
			raising
				.When(r => r.I = Arg.Any<int>())
				.Do(ci => raising.PropertyChanging += Raise.Event<PropertyChangingEventHandler>(raising, new PropertyChangingEventArgs("anything")));

			var subject = new NoPropertyChangingConstraint<IRaisingSubject>(raising);

			Assert.That(getMessage(subject, () => raising.I = 3),
				Does.StartWith(TextMessageWriter.Pfx_Expected + "event 'PropertyChanging' not raised").And
					.Contain(TextMessageWriter.Pfx_Actual + "event 'PropertyChanging' raised"));
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			var raising = Substitute.For<IRaisingSubject>();
			raising
				.When(r => r.I = Arg.Any<int>())
				.Do(_ => { });

			Assert.That(() => raising.I = 3, new NoPropertyChangingConstraint<IRaisingSubject>(raising));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			var raising = Substitute.For<IRaisingSubject>();
			raising.When(r => r.I = Arg.Any<int>())
				.Do(_ => { });

			Assert.That(() => raising.I = 3, Must.Not.Raise.PropertyChanging(raising));
		}
	}
}