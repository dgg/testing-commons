using NUnit.Framework;
using Testing.Commons.Serialization;

namespace Testing.Commons.Tests.Serialization
{
	[TestFixture]
	public class JsonStringTester
	{
		[Test]
		public void Ctor_NullString_NullJsonString()
		{
			var subject = new JsonString(null);

			Assert.That(subject.ToString(), Is.Null);

			string @null = subject;
			Assert.That(@null, Is.Null);
		}

		[Test]
		public void Ctor_EmptyString_EmptyJsonString()
		{
			var subject = new JsonString(string.Empty);

			Assert.That(subject.ToString(), Is.Empty);

			string @null = subject;
			Assert.That(@null, Is.Empty);
		}

		[Test]
		public void ToString_SomeShortcutJsonString_ProperJsonString()
		{
			var subject = new JsonString("{'str' : 'value', 'number': 42}");

			string json = subject.ToString();
			Assert.That(json, Is.EqualTo("{\"str\" : \"value\", \"number\": 42}"));
		}

		[Test]
		public void StringConversion_SomeShortcutJsonString_ProperJsonString()
		{
			var subject = new JsonString("{'str' : 'value', 'number': 42}");

			string json = subject;
			Assert.That(json, Is.EqualTo("{\"str\" : \"value\", \"number\": 42}"));
		}
	}
}