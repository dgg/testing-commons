using System;
using NUnit.Framework;
using Testing.Commons.Serialization;
using Testing.Commons.Tests.Serialization.Subjects;

namespace Testing.Commons.Tests.Serialization
{
	[TestFixture]
	public class RoundtripXmlSerializerTester
	{
		[Test]
		public void Serialize_SerializableType_AStringRepresentationOfSerializedObject()
		{
			using (var subject = new XmlRoundtripSerializer<Serializable>())
			{
				string representation = subject.Serialize(new Serializable { S = "s", D = 3m });

				Assert.That(representation, Is.StringEnding("<S>s</S><D>3</D></Serializable>"));
			}
		}

		[Test]
		public void Serialize_NonSerializableType_Exception()
		{
			using (var subject = new XmlRoundtripSerializer<NonSerializable>())
			{
				Assert.That(() => subject.Serialize(new NonSerializable("s")), Throws.InstanceOf<InvalidOperationException>());
			}
		}

		[Test]
		public void Deserialize_BeforeSerializing_Exception()
		{
			using (var subject = new XmlRoundtripSerializer<Serializable>())
			{
				Assert.That(() => subject.Deserialize(), Throws.InstanceOf<InvalidOperationException>());
			}
		}

		[Test]
		public void Deserialize_AfterSerializingSerializableType_InitialObject()
		{
			using (var subject = new XmlRoundtripSerializer<Serializable>())
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
