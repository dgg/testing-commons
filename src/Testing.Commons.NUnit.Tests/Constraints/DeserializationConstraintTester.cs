using NUnit.Framework;
using NUnit.Framework.Constraints;
using Rhino.Mocks;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Subjects;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class DeserializationConstraintTester : ConstraintTesterBase
	{
		#region Matches

		[Test]
		public void Matches_AppliesConstraintToDeserialized()
		{
			string serializationRepresentation = "representation";
			var deserialized = new Serializable { D = 3m, S = "s" };

			var deserializer = MockRepository.GenerateStub<IDeserializer>();
			var constraint = MockRepository.GeneratePartialMock<Constraint>();
			deserializer.Stub(s => s.Deserialize<Serializable>(serializationRepresentation)).Return(deserialized);

			var subject = new DeserializationConstraint<Serializable>(deserializer, constraint);
			subject.Matches(serializationRepresentation);

			constraint.AssertWasCalled(c => c.Matches(deserialized));
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			string serializationRepresentation = "representation";
			var deserialized = new Serializable { D = 3m, S = "s" };

			var deserializer = MockRepository.GenerateStub<IDeserializer>();
			deserializer.Stub(s => s.Deserialize<Serializable>(serializationRepresentation)).Return(deserialized);

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

			var deserializer = MockRepository.GenerateStub<IDeserializer>();
			deserializer.Stub(s => s.Deserialize<Serializable>(serializationRepresentation)).Return(deserialized);

			Assert.That(serializationRepresentation,
				Must.Be.Deserializable<Serializable>(deserializer, 
					Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m)));
		}

	}
}
