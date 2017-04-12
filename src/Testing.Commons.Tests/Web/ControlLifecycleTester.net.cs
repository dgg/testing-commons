using System;
using System.Reflection;
using NUnit.Framework;
using Testing.Commons.Tests.Web.Subjects;
using Testing.Commons.Web;

namespace Testing.Commons.Tests.Web
{
	[TestFixture]
	public class ControlLifecycleTester
	{
		#region Control.Load

		[Test]
		public void Fake_Load_NoArguments_StepCalledWithNull()
		{
			var subject = new ControlSpy();

			ControlLifecycle.Fake(subject, s => s.Load += null);

			Assert.That(subject.LastStep.StepName, Is.EqualTo("OnLoad"));
			Assert.That(subject.LastStep.StepArguments, Is.EqualTo(new object[] {null}));
		}

		[Test]
		public void Fake_Load_CustomArguments_StepCalledWithCustomArguments()
		{
			var subject = new ControlSpy();
			EventArgs custom = new EventArgs();
			ControlLifecycle.Fake(subject, s => s.Load += null, custom);

			Assert.That(subject.LastStep.StepName, Is.EqualTo("OnLoad"));
			Assert.That(subject.LastStep.StepArguments[0], Is.SameAs(custom));
		}

		[Test]
		public void Fake_Load_MoreArguments_Exception()
		{
			var subject = new ControlSpy();

			Assert.That(() => ControlLifecycle.Fake(subject, s => s.Load += null, "notEventArgs", 1),
			            Throws.InstanceOf<TargetParameterCountException>());
		}

		[Test]
		public void Fake_Load_WrongArgumentType_Exception()
		{
			var subject = new ControlSpy();

			Assert.That(() => ControlLifecycle.Fake(subject, s => s.Load += null, "notEventArgs"),
			            Throws.ArgumentException);
		}

		#endregion

		#region Control.BubbleEvent

		[Test]
		public void Call_BubbleEvent_NoArguments_StepCalledWithNull()
		{
			var subject = new ControlSpy();

			ControlLifecycle.Call(subject, "OnBubbleEvent");

			Assert.That(subject.LastStep.StepName, Is.EqualTo("OnBubbleEvent"));
			Assert.That(subject.LastStep.StepArguments, Is.EqualTo(new object[] {null, null}));
		}

		[Test]
		public void Call_BubbleEvent_CustomArguments_StepCalledWithCustomArguments()
		{
			object sender = new object();
			EventArgs args = new EventArgs();

			var subject = new ControlSpy();
			ControlLifecycle.Call(subject, "OnBubbleEvent", sender, args);

			Assert.That(subject.LastStep.StepName, Is.EqualTo("OnBubbleEvent"));
			Assert.That(subject.LastStep.StepArguments[0], Is.SameAs(sender));
			Assert.That(subject.LastStep.StepArguments[1], Is.SameAs(args));
		}

		[Test]
		public void Call_BubbleEvent_LessArguments_Exception()
		{
			var subject = new ControlSpy();

			Assert.That(() => ControlLifecycle.Call(subject, "OnBubbleEvent", "anObject"),
			            Throws.InstanceOf<TargetParameterCountException>());
		}

		[Test]
		public void Call_BubbleEvent_MoreArguments_Exception()
		{
			var subject = new ControlSpy();

			Assert.That(() => ControlLifecycle.Call(subject, "OnBubbleEvent", 1, EventArgs.Empty, TimeSpan.Zero),
			            Throws.InstanceOf<TargetParameterCountException>());
		}

		[Test]
		public void Call_BubbleEvent_WrongArgumentType_Exception()
		{
			var subject = new ControlSpy();

			Assert.That(() => ControlLifecycle.Call(subject, "OnBubbleEvent", 1, "notEventArgs"),
			            Throws.ArgumentException);
		}

		#endregion
	}
}
