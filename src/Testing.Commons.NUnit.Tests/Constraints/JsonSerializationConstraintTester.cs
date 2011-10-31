using NUnit.Framework;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Subjects;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class JsonSerializationConstraintTester : ConstraintTesterBase
	{
		#region Matches

		[Test]
		public void Matches_MatchingSerializable_True()
		{
			var serializable = new Serializable {D = 3m, S = "s"};
			var subject = new SerializationConstraint<Serializable>(new JsonRoundtripSerializer<Serializable>(), 
				Is.Not.SameAs(serializable)
					.And.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m));

			Assert.That(subject.Matches(serializable), Is.True);
		}

		[Test]
		public void Matches_NonSerializable_False()
		{
			var nonSerializable = new NonSerializable("s");
			var subject = new SerializationConstraint<NonSerializable>(new JsonRoundtripSerializer<NonSerializable>(), Is.Not.Null);

			Assert.That(subject.Matches(nonSerializable), Is.False);
		}

		[Test]
		public void Matches_NonMatchingSerializable_False()
		{
			var serializable = new Serializable {D = 3m, S = "s"};
			var subject = new SerializationConstraint<Serializable>(new JsonRoundtripSerializer<Serializable>(),
				Is.Not.SameAs(serializable)
					.And.Property("S").EqualTo("sS")
					.And.Property("D").EqualTo(3m));

			Assert.That(subject.Matches(serializable), Is.False);
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_NonSerializable_ExpectedContainsConstraintExpectations()
		{
			var nonSerializable = new NonSerializable("s");
			var subject = new SerializationConstraint<NonSerializable>(new JsonRoundtripSerializer<NonSerializable>(), Is.Not.Null);

			Assert.That(GetMessage(subject, nonSerializable), Is.StringStarting(TextMessageWriter.Pfx_Expected + "Deserialized object not null"));
		}

		[Test]
		public void WriteMessageTo_NonSerializable_ActualContainsExpectationsErrorPlusObject()
		{
			var nonSerializable = new NonSerializable("s");
			var subject = new SerializationConstraint<NonSerializable>(new JsonRoundtripSerializer<NonSerializable>(), Is.Not.Null);

			Assert.That(GetMessage(subject, nonSerializable), Is.StringContaining(
				TextMessageWriter.Pfx_Actual + "Could not serialize/deserialize object"));
		}

		[Test]
		public void WriteMessageTo_NonMatchingSerializable_ActualContainsOffendingValue()
		{
			var serializable = new Serializable { D = 3m, S = "s" };
			var subject = new SerializationConstraint<Serializable>(new JsonRoundtripSerializer<Serializable>(),
				Is.Not.SameAs(serializable)
					.And.Property("S").EqualTo("sS")
					.And.Property("D").EqualTo(3m));

			Assert.That(GetMessage(subject, serializable), Is.StringContaining(TextMessageWriter.Pfx_Actual + "\"s\""));
		}

		[Test]
		public void WriteMessageTo_NonMatchingSerializable_ActualContainsActualObject()
		{
			var serializable = new Serializable { D = 3m, S = "s" };
			var subject = new SerializationConstraint<Serializable>(new JsonRoundtripSerializer<Serializable>(),
				Is.Not.SameAs(serializable)
					.And.Property("S").EqualTo("sS")
					.And.Property("D").EqualTo(3m));

			Assert.That(GetMessage(subject, serializable), Is.StringContaining(" -> <" + typeof(Serializable).FullName + ">"));
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			Assert.That(new Serializable { D = 3m, S = "s" },
				new SerializationConstraint<Serializable>(new JsonRoundtripSerializer<Serializable>(),
					Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m)));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			Assert.That(new Serializable { D = 3m, S = "s" },
				Must.Be.JsonSerializable<Serializable>(
					Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m)));
		}
	}
}
