using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Constraints.Support;
using Testing.Commons.Serialization;

using Iz = Testing.Commons.NUnit.Constraints.Iz;

namespace Testing.Commons.NUnit.Tests.Constraints;

[TestFixture]
public class JsonEqualConstraintTester : ConstraintTesterBase
{
	#region ApplyTo

	[Test]
	public void ApplyTo_SameProperJson_True()
	{
		string properJson = "{\"prop\"=\"value\"}";
		var subject = new JsonEqualConstraint("{'prop'='value'}");

		Assert.That(matches(subject, properJson), Is.True);
	}

	[Test]
	public void Matches_SameJsonified_False()
	{
		string jsonified = "{'prop'='value'}";
		var subject = new JsonEqualConstraint("{'prop'='value'}");

		Assert.That(matches(subject, jsonified), Is.False);
	}

	[Test]
	public void Matches_NotSame_False()
	{
		string notSame = "{\"abc\"=123}";
		var subject = new JsonEqualConstraint("{'prop'='value'}");

		Assert.That(matches(subject, notSame), Is.False);
	}

	#endregion

	#region WriteMessageTo

	[Test]
	public void WriteMessageTo_DifferentJson_DelegateToEquals()
	{
		string expected = "{'prop'='value'}",
		   actual = "{\"abcd\"=\"12345\"}";
		var subject = new JsonEqualConstraint(expected);
		var equals = new EqualConstraint(expected.Jsonify());

		Assert.That(getMessage(subject, actual),
			Is.EqualTo(getMessage(equals, actual)));
	}

	#endregion

	[Test]
	public void CanBeNewedUp()
	{
		var actual = "{\"prop\"=\"value\"}";
		Assert.That(actual, new JsonEqualConstraint("{'prop'='value'}"));
	}

	[Test]
	public void CanBeCreatedWithExtension()
	{
		var actual = "{\"prop\"=\"value\"}";
		Assert.That(actual, Iz.Json("{'prop'='value'}"));
	}

	[Test]
	public void EqualsCanBeUsed_WithAComparer()
	{
		var actual = "{\"prop\"=\"value\"}";
		Assert.That(actual, Is.EqualTo("{'prop'='value'}").Using(JsonString.Comparer));
	}

	[Test]
	public void CanBeUsed_WithUsing()
	{
		var actual = "{\"prop\"=\"value\"}";
		Assert.That(actual, Is.EqualTo("{'prop'='value'}").Using(JsonString.Comparer));
	}

	[Test]
	public void CanBeUsed_WithExtension()
	{
		var actual = "{\"prop\"=\"value\"}";
		Assert.That(actual, Is.EqualTo("{'prop'='value'}").AsJson());
	}
}
