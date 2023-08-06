using NUnit.Framework;
using Testing.Commons.Serialization;

namespace Testing.Commons.Tests.Serialization
{
	[TestFixture]
	public class JsonStringExtensionsTester
	{
		[Test]
		public void Jsonify_NullString_NullJsonString()
		{
			Assert.That(((string)null).Jsonify(), Is.Null);
		}

		[Test]
		public void Jsonify_EmptyString_EmptyJsonString()
		{
			Assert.That(string.Empty.Jsonify(), Is.Empty);
		}

		[Test]
		public void Jsonify_SomeShortcutJsonString_ProperJsonString()
		{
			string json = "{'str' : 'value', 'number': 42}".Jsonify();
			Assert.That(json, Is.EqualTo("{\"str\" : \"value\", \"number\": 42}"));
		}
	}
}