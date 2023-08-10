using Testing.Commons.Serialization;

namespace Testing.Commons.Tests.Serialization
{
	[TestFixture]
	public class JsonStringExtensionsTester
	{
		[Test]
		public void Jsonify_EmptyString_Exception()
		{
			Assert.That(() => string.Empty.Jsonify(), Throws.ArgumentException);
		}

		[Test]
		public void Jsonify_SomeShortcutJsonString_ProperJsonString()
		{
			string json = "{'str' : 'value', 'number': 42}".Jsonify();
			Assert.That(json, Is.EqualTo("{\"str\" : \"value\", \"number\": 42}"));
		}
	}
}
