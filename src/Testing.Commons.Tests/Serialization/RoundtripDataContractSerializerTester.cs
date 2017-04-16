using System.Runtime.Serialization;
using System.Xml;
using NUnit.Framework;
using Testing.Commons.Serialization;
using Testing.Commons.Tests.Serialization.Subjects;

namespace Testing.Commons.Tests.Serialization
{
	[TestFixture]
	public class RoundtripDataContractSerializerTester
	{
		[Test]
		public void Serialize_SerializableType_AStringRepresentationOfSerializedObject()
		{
			using (var subject = new DataContractRoundtripSerializer<Serializable>())
			{
				string representation = subject.Serialize(new Serializable { S = "s", D = 3m });

				Assert.That(representation, Does.Contain("S").And.Contain("D")
					.And.Contain(">3<")
					.And.Contain(">s<"));
			}
		}

		[Test]
		public void Serialize_NonSerializableType_Exception()
		{
			using (var subject = new DataContractRoundtripSerializer<NonSerializable>())
			{
				Assert.That(() => subject.Serialize(new NonSerializable("s")), Throws.InstanceOf<InvalidDataContractException>());
			}
		}

		[Test]
		public void Deserialize_BeforeSerializing_Exception()
		{
			using (var subject = new DataContractRoundtripSerializer<Serializable>())
			{
				Assert.That(() => subject.Deserialize(), Throws.InstanceOf<XmlException>());
			}
		}

		[Test]
		public void Deserialize_AfterSerializingSerializableType_InitialObject()
		{
			using (var subject = new DataContractRoundtripSerializer<Serializable>())
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
