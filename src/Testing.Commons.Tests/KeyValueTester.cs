using System.Collections.Generic;
using NUnit.Framework;

namespace Testing.Commons.Tests
{
	[TestFixture]
	public partial class KeyValueTester
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
	}
}
