using System.Text.Json;
using System.Text.Json.Nodes;
using NUnit.Framework.Internal;
using Testing.Commons.Serialization;
using Testing.Commons.Tests.Serialization.Subjects;

namespace Testing.Commons.Tests.Serialization;

[TestFixture]
public class JsonConverterExtensionsTester
{
	[Test]
	public void WriteToString_NoOptions_JsonAsWrittenByConverter()
	{
		var subject = new CustomDoublingConverter();
		var doubling = new Doubling(2);

		string json = subject.WriteToString(doubling);

		Assert.That(json, Is.EqualTo("4"), "by default, numbers are numbers");
	}

	[Test]
	public void WriteToString_CustomOptions_OptionsAppliedToSerialization()
	{
		var subject = new CustomDoublingConverter();
		var doubling = new Doubling(2);
		var customOptions = new JsonSerializerOptions
		{
			NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.WriteAsString,
		};

		string json = subject.WriteToString(doubling, customOptions);

		Assert.That(json, Is.EqualTo("\"4\""), "with the provided custom options, numbers are written as strings :-O");
	}

	[Test]
	public void ReadFromString_NoOptions_JsonAsReadByConverter()
	{
		string json = "4";
		var subject = new CustomDoublingConverter();

		Doubling read = subject.ReadFromString(json);

		Assert.That(read.Value, Is.EqualTo(2), "by default, numbers are numbers");
	}

	[Test]
	public void WriteToString_CustomOptions_OptionsAppliedToDeserialization()
	{
		var subject = new CustomDoublingConverter();
		string json = "\"4\"";
		var customOptions = new JsonSerializerOptions
		{
			NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
		};

		Doubling read = subject.ReadFromString(json, customOptions);

		Assert.That(read.Value, Is.EqualTo(2), "with the provided custom options, numbers are read as strings :-O");
	}

	[Test]
	public void ParseObject_ObjectParseable_Parsed()
	{
		string json = """
			{ "a": 1, "b": false}
		""";

		JsonObject obj = json.ParseObject();

		Assert.That(obj, Has.Count.EqualTo(2), "two props");
	}

	[Test]
	public void ParseObject_ObjectParseable_ParsedPropsAreCAseInsensitiveForEasyTesting()
	{
		string json = """
			{ "a": 1, "b": false}
		""";

		JsonObject obj = json.ParseObject();

		Assert.That(obj.ContainsKey("A"), Is.True);
		Assert.That(obj["B"]!.GetValue<bool>(), Is.False);
	}

	[Test]
	public void ParseObject_Null_Exception()
	{
		string json = "null";

		Assert.That(() => json.ParseObject(), Throws.InvalidOperationException);
	}
	
	[Test]
	public void ParseObject_NotAnObject_Exception()
	{
		string json = "[1, false]";

		Assert.That(()=> json.ParseObject(), Throws.InvalidOperationException);
	}
}