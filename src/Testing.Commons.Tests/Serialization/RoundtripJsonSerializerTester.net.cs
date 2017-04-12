using NUnit.Framework;
using Testing.Commons.Serialization;
using Testing.Commons.Tests.Serialization.Subjects;

namespace Testing.Commons.Tests.Serialization
{
	[TestFixture]
	public class RoundtripJsonSerializerTester
	{
		[Test]
		public void Serialize_SerializableType_AStringRepresentationOfSerializedObject()
		{
			using (var subject = new JsonRoundtripSerializer<Serializable>())
			{
				string representation = subject.Serialize(new Serializable { S = "s", D = 3m });

				Assert.That(representation, Is.EqualTo("{\"S\":\"s\",\"D\":3}"));
			}
		}

		[Test]
		public void Deserialize_BeforeSerializing_Null()
		{
			using (var subject = new JsonRoundtripSerializer<Serializable>())
			{
				Assert.That(subject.Deserialize(), Is.Null);
			}
		}

		[Test]
		public void Deserialize_AfterSerializingSerializableType_InitialObject()
		{
			using (var subject = new JsonRoundtripSerializer<Serializable>())
			{
				var serialized = new Serializable { S = "s", D = 3m };
				subject.Serialize(serialized);

				Serializable deserialized = subject.Deserialize();

				Assert.That(deserialized, Is.Not.SameAs(serialized)
					.And.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m));
			}
		}
	}
}
