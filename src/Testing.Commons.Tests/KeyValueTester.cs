using System.Collections.Generic;
using NUnit.Framework;
using Testing.Commons.Time;
using Testing.Commons.Web;

namespace Testing.Commons.Tests
{
	[TestFixture]
	public class KeyValueTester
	{
		[Test]
		public void New_TakesAdvantageOfTypeInference()
		{
			var pair = KeyValue.New(1, 3m);

			Assert.That(pair, Is.EqualTo(new KeyValuePair<int, decimal>(1, 3m)));
		}

		[Test]
		public void Pair_StringAndObjectPair()
		{
			var value = new object();
			var pair = KeyValue.Pair("key", value);

			Assert.That(pair, Is.EqualTo(new KeyValuePair<string, object>("key", value)));
		}

		[Test]
		public void Pair_HelpsToSpecifyProfileTesterStubs()
		{
			var provider = new ProfileTestProvider();

			// it does save keystrokes
			provider.StubValues(
				KeyValue.Pair("key1", 3),
				KeyValue.Pair("key2", 13.Hours()));

			// generics make it more verbose
			provider.StubValues(
				new KeyValuePair<string, object>("key1", 3),
				new KeyValuePair<string, object>("key2", 13.Hours()));
		}
	}
}