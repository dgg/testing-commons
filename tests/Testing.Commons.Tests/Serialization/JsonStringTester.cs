using Testing.Commons.Serialization;

namespace Testing.Commons.Tests.Serialization;

[TestFixture]
public class JsonStringTester
{
	[Test]
	public void Ctor_EmptyString_Exception()
	{
		Assert.That(() => new JsonString(string.Empty), Throws.ArgumentException);
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
