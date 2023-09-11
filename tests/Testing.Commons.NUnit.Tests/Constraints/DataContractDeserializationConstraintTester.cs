using NUnit.Framework.Internal;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Constraints.Support;
using Testing.Commons.NUnit.Tests.Constraints.Subjects;
using Testing.Commons.Serialization;

using Iz = Testing.Commons.NUnit.Constraints.Iz;

namespace Testing.Commons.NUnit.Tests.Constraints;

[TestFixture]
public class DataContractDeserializationConstraintTester : ConstraintTesterBase
{
	#region ApplyTo

	[Test]
	public void ApplyTo_MatchingDeserialized_True()
	{
		var matching = Serializable.DataContractString("s", 3m);
		var subject = new DeserializationConstraint<Serializable>(
			new DataContractDeserializer(),
			Has.Property("S").EqualTo("s")
				.And.Property("D").EqualTo(3m));

		Assert.That(matches(subject, matching), Is.True);
	}

	[Test]
	public void ApplyTo_NonSerialized_False()
	{
		var nonSerializable = "<notSerializable />";
		var subject = new DeserializationConstraint<Serializable>(
			new DataContractDeserializer(), Is.Not.Null);

		Assert.That(matches(subject, nonSerializable), Is.False);
	}

	[Test]
	public void ApplyTo_NonMatching_False()
	{
		var nonMatching = Serializable.DataContractString("s", 3m);
		var subject = new DeserializationConstraint<Serializable>(
			new DataContractDeserializer(),
			Has.Property("S").EqualTo("sS")
				.And.Property("D").EqualTo(3m));

		Assert.That(matches(subject, nonMatching), Is.False);
	}

	#endregion

	#region WriteMessageTo

	[Test]
	public void WriteMessageTo_NonSerialized_ExpectedContainsConstraintExpectations_ActualContainsExpectationsErrorPlusObject()
	{
		var nonSerializable = "<notSerializable />";
		var subject = new DeserializationConstraint<Serializable>(
			new DataContractDeserializer(), Is.Not.Null);

		Assert.That(getMessage(subject, nonSerializable), Does
			.StartWith(TextMessageWriter.Pfx_Expected + "Deserialized object not null").And
			.Contains(TextMessageWriter.Pfx_Actual + "Could not deserialize object"));
	}

	[Test]
	public void WriteMessageTo_NonMatching_ActualContainsOffendingValueAmdActualObject()
	{
		var nonMatching = Serializable.DataContractString("s", 3m);
		var subject = new DeserializationConstraint<Serializable>(
			new DataContractDeserializer(),
			Has.Property("S").EqualTo("sS")
				.And.Property("D").EqualTo(3m));

		Assert.That(getMessage(subject, nonMatching), Does.Contain(TextMessageWriter.Pfx_Actual + "\"s\"").And
			.Contains(" -> <" + typeof(Serializable).FullName + ">"));
	}

	#endregion

	[Test]
	public void CanBeNewedUp()
	{
		Assert.That(Serializable.DataContractString("s", 3m),
			new DeserializationConstraint<Serializable>(
				new DataContractDeserializer(),
				Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m)));
	}

	[Test]
	public void CanBeCreatedWithExtension()
	{
		Assert.That(Serializable.DataContractString("s", 3m),
			Iz.DataContractDeserializable<Serializable>(
				Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m)));
	}
}
