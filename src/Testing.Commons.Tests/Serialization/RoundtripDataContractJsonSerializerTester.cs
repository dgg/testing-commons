using System.Runtime.Serialization;
using NUnit.Framework;
using Testing.Commons.Serialization;
using Testing.Commons.Tests.Serialization.Subjects;

namespace Testing.Commons.Tests.Serialization
{
	[TestFixture]
	public class RoundtripDataContractJsonSerializerTester
	{
		[Test]
		public void Serialize_SerializableType_AStringRepresentationOfSerializedObject()
		{
			using (var subject = new DataContractJsonRoundtripSerializer<Serializable>())
			{
				string representation = subject.Serialize(new Serializable { S = "s", D = 3m });

				Assert.That(representation, Is.StringContaining("__BackingField")
					.And.StringContaining(":3")
					.And.StringContaining(":\"s\""));
			}
		}

		[Test]
		public void Serialize_NonSerializableType_Exception()
		{
			using (var subject = new DataContractJsonRoundtripSerializer<NonSerializable>())
			{
				Assert.That(() => subject.Serialize(new NonSerializable("s")), Throws.InstanceOf<InvalidDataContractException>());
			}
		}

		[Test]
		public void Deserialize_BeforeSerializing_Exception()
		{
			using (var subject = new DataContractJsonRoundtripSerializer<Serializable>())
			{
				Assert.That(() => subject.Deserialize(), Throws.InstanceOf<SerializationException>());
			}
		}

		[Test]
		public void Deserialize_AfterSerializingSerializableType_InitialObject()
		{
			using (var subject = new DataContractJsonRoundtripSerializer<Serializable>())
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