using System.Runtime.Serialization;
using Testing.Commons.Serialization;
using Testing.Commons.Tests.Serialization.Subjects;

namespace Testing.Commons.Tests.Serialization;

[TestFixture]
public class DataContractDeserializerTester
{
	[Test]
	public void Deserialize_SerializationRepresentation_DeserializedObject()
	{
		var subject = new DataContractDeserializer();
		var deserialized = subject.Deserialize<Serializable>(Serializable.DataContractString("s", 3m));

		Assert.That(deserialized.D, Is.EqualTo(3m));
		Assert.That(deserialized.S, Is.EqualTo("s"));
	}

	[Test]
	public void Deserialize_InvalidSerializationRepresentation_Extepcion()
	{
		var subject = new DataContractDeserializer();

		Assert.That(() => subject.Deserialize<Serializable>("invalid"), Throws.InstanceOf<SerializationException>());
	}
}
