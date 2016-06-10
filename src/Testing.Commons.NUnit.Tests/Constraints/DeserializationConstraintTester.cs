using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Subjects;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class DeserializationConstraintTester : Support.ConstraintTesterBase
	{
		#region ApplyTo

		[Test]
		public void ApplyTo_AppliesConstraintToDeserialized()
		{
			string serializationRepresentation = "representation";
			var deserialized = new Serializable { D = 3m, S = "s" };

			var deserializer = Substitute.For<IDeserializer>();
			var constraint = Substitute.For<Constraint>();
			deserializer.Deserialize<Serializable>(serializationRepresentation).Returns(deserialized);

			var subject = new DeserializationConstraint<Serializable>(deserializer, constraint);
			subject.ApplyTo(serializationRepresentation);

			constraint.Received().ApplyTo(deserialized);
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			string serializationRepresentation = "representation";
			var deserialized = new Serializable { D = 3m, S = "s" };

			var deserializer = Substitute.For<IDeserializer>();
			deserializer.Deserialize<Serializable>(serializationRepresentation).Returns(deserialized);

			Assert.That(serializationRepresentation,
				new DeserializationConstraint<Serializable>(deserializer,
					Has.Property("S").EqualTo("s")
						.And.Property("D").EqualTo(3m)));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			string serializationRepresentation = "representation";
			var deserialized = new Serializable { D = 3m, S = "s" };

			var deserializer = Substitute.For<IDeserializer>();
			deserializer.Deserialize<Serializable>(serializationRepresentation).Returns(deserialized);

			Assert.That(serializationRepresentation,
				Must.Be.Deserializable<Serializable>(deserializer,
					Has.Property("S").EqualTo("s")
						.And.Property("D").EqualTo(3m)));
		}
	}
}