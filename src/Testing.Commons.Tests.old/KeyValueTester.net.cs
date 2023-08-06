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
