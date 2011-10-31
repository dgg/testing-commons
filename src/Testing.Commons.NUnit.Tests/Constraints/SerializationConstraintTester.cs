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
	public class SerializationConstraintTester : ConstraintTesterBase
	{
		#region Matches

		[Test]
		public void Matches_AsksDeserializerToDeserializeWhatWasSerialized()
		{
			var serializable = new Serializable { D = 3m, S = "s" };

			var serializer = MockRepository.GenerateStrictMock<IRoundtripSerializer<Serializable>>();
			using (serializer.GetMockRepository().Ordered())
			{
				serializer.Expect(s => s.Serialize(serializable)).Return(null);
				serializer.Expect(s => s.Deserialize()).Return(null);
				serializer.Expect(s => s.Dispose());
			}

			var subject = new SerializationConstraint<Serializable>(serializer, Is.Null);
			subject.Matches(serializable);

			serializer.VerifyAllExpectations();
		}

		[Test]
		public void Matches_AppliesConstraintToDeserialized()
		{
			var serializable = new Serializable { D = 3m, S = "s" };
			var deserialized = new Serializable();

			var serializer = MockRepository.GenerateStub<IRoundtripSerializer<Serializable>>();
			var constraint = MockRepository.GeneratePartialMock<Constraint>();
			serializer.Stub(s => s.Deserialize()).Return(deserialized);

			var subject = new SerializationConstraint<Serializable>(serializer, constraint);
			subject.Matches(serializable);

			constraint.AssertWasCalled(c => c.Matches(deserialized));
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			var serializable = new Serializable { D = 3m, S = "s" };

			var serializer = MockRepository.GenerateStub<IRoundtripSerializer<Serializable>>();
			serializer.Stub(s => s.Deserialize()).Return(serializable);

			Assert.That(serializable,
				new SerializationConstraint<Serializable>(serializer,
					Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m)));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			var serializable = new Serializable { D = 3m, S = "s" };

			var serializer = MockRepository.GenerateStub<IRoundtripSerializer<Serializable>>();
			serializer.Stub(s => s.Deserialize()).Return(serializable);

			Assert.That(serializable,
				Must.Be.Serializable(serializer, 
					Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m)));
		}

	}
}
