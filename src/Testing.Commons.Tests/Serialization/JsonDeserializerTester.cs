using NUnit.Framework;
using Testing.Commons.Serialization;
using Testing.Commons.Tests.Serialization.Subjects;

namespace Testing.Commons.Tests.Serialization
{
	[TestFixture]
	public class JsonDeserializerTester
	{
		[Test]
		public void Deserialize_SerializationRepresentation_DeserializedObject()
		{
			var subject = new JsonDeserializer();
			var deserialized = subject.Deserialize<Serializable>(Serializable.JsonString("s", 3m));

				Assert.That(deserialized.D, Is.EqualTo(3m));
				Assert.That(deserialized.S, Is.EqualTo("s"));
		}

		[Test]
		public void Deserialize_InvalidSerializationRepresentation_Extepcion()
		{
			var subject = new JsonDeserializer();

			Assert.That(() => subject.Deserialize<Serializable>("invalid"), Throws.ArgumentException);
		}
	}
}
