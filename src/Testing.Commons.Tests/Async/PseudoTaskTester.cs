using NSubstitute;
using NUnit.Framework;
using Testing.Commons.Async;
using Testing.Commons.Tests.Async.Support;

namespace Testing.Commons.Tests.Async
{
	[TestFixture]
	public class PseudoTaskTester
	{
		[Test]
		public void StubbingAsyncInterface_IsPossibleWithPseudoTask()
		{
			string stuff = "echo";
			var provider = Substitute.For<IProviderStuff>();

			var subject = new AsyncConsumer(provider);
			provider.LongRunningStuff().Returns(PseudoTask.Create(stuff));

			Assert.That(subject.EchoStuff(), Is.EqualTo("echoecho"));
		}
	}
}