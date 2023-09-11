using System.Linq;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Constraints.Support;
using Testing.Commons.NUnit.Tests.Constraints.Subjects;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class SerializationConstraintTester : ConstraintTesterBase
	{
		#region ApplyTo

		[Test]
		public void ApplyTo_AsksDeserializerToDeserializeWhatWasSerialized()
		{
			var serializable = new Serializable { D = 3m, S = "s" };

			var serializer = Substitute.For<IRoundtripSerializer<Serializable>>();

			var subject = new SerializationConstraint<Serializable>(serializer, Is.Null);
			subject.ApplyTo(serializable);

			ICall[] receivedCalls = serializer.ReceivedCalls().ToArray();

			// first call: .Serialize(serializable)
			Assert.That(receivedCalls[0].GetMethodInfo().Name, Is.EqualTo("Serialize"));
			Assert.That(receivedCalls[0].GetArguments()[0], Is.SameAs(serializable));

			// second call: Deserialize()
			Assert.That(receivedCalls[1].GetMethodInfo().Name, Is.EqualTo("Deserialize"));

			// third call: Dispose()
			Assert.That(receivedCalls[2].GetMethodInfo().Name, Is.EqualTo("Dispose"));
		}

		[Test]
		public void ApplyTo_AppliesConstraintToDeserialized()
		{
			var serializable = new Serializable { D = 3m, S = "s" };
			var deserialized = new Serializable();

			var serializer = Substitute.For<IRoundtripSerializer<Serializable>>();
			var constraint = Substitute.For<Constraint>();
			serializer.Deserialize().Returns(deserialized);

			var subject = new SerializationConstraint<Serializable>(serializer, constraint);
			subject.ApplyTo(serializable);

			constraint.Received().ApplyTo(deserialized);
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			var serializable = new Serializable { D = 3m, S = "s" };

			var serializer = Substitute.For<IRoundtripSerializer<Serializable>>();
			serializer.Deserialize().Returns(serializable);

			Assert.That(serializable,
				new SerializationConstraint<Serializable>(serializer,
					Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m)));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			var serializable = new Serializable { D = 3m, S = "s" };

			var serializer = Substitute.For<IRoundtripSerializer<Serializable>>();
			serializer.Deserialize().Returns(serializable);

			Assert.That(serializable,
				Must.Be.Serializable(serializer,
					Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m)));
		}
	}
}