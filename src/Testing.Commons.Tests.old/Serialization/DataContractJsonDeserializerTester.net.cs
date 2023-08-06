using System.Runtime.Serialization;
using NUnit.Framework;
using Testing.Commons.Serialization;
using Testing.Commons.Tests.Serialization.Subjects;

namespace Testing.Commons.Tests.Serialization
{
	[TestFixture]
	public class DataContractJsonDeserializerTester
	{
		[Test]
		public void Deserialize_SerializationRepresentation_DeserializedObject()
		{
			var subject = new DataContractJsonDeserializer();
			var deserialized = subject.Deserialize<Serializable>(Serializable.DataContractJsonString("s", 3m));

				Assert.That(deserialized.D, Is.EqualTo(3m));
				Assert.That(deserialized.S, Is.EqualTo("s"));
		}

		[Test]
		public void Deserialize_InvalidSerializationRepresentation_Extepcion()
		{
			var subject = new DataContractJsonDeserializer();

			Assert.That(() => subject.Deserialize<Serializable>("invalid"), Throws.InstanceOf<SerializationException>());
		}
	}
}
