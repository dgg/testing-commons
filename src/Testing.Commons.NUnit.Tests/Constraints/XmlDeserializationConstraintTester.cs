using NUnit.Framework;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Subjects;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class XmlDeserializationConstraintTester : ConstraintTesterBase
	{
		#region Matches

		[Test]
		public void Matches_MatchingDeserialized_True()
		{
			var matching = Serializable.XmlString("s", 3m);
			var subject = new DeserializationConstraint<Serializable>(
				new XmlDeserializer(),
				Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m));

			Assert.That(subject.Matches(matching), Is.True);
		}

		[Test]
		public void Matches_NonSerialized_False()
		{
			var nonSerializable = "<notSerializable />";
			var subject = new DeserializationConstraint<Serializable>(
				new XmlDeserializer(), Is.Not.Null);

			Assert.That(subject.Matches(nonSerializable), Is.False);
		}

		[Test]
		public void Matches_NonMatching_False()
		{
			var nonMatching = Serializable.XmlString("s", 3m);
			var subject = new DeserializationConstraint<Serializable>(
				new XmlDeserializer(),
				Has.Property("S").EqualTo("sS")
					.And.Property("D").EqualTo(3m));

			Assert.That(subject.Matches(nonMatching), Is.False);
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_NonSerialized_ExpectedContainsConstraintExpectations()
		{
			var nonSerializable = "<notSerializable />";
			var subject = new DeserializationConstraint<Serializable>(
				new XmlDeserializer(), Is.Not.Null);

			Assert.That(GetMessage(subject, nonSerializable), Is.StringStarting(TextMessageWriter.Pfx_Expected + "Deserialized object not null"));
		}

		[Test]
		public void WriteMessageTo_NonSerialized_ActualContainsExpectationsErrorPlusObject()
		{
			var nonSerializable = "<notSerializable />";
			var subject = new DeserializationConstraint<Serializable>(
				new XmlDeserializer(), Is.Not.Null);

			Assert.That(GetMessage(subject, nonSerializable), Is.StringContaining(
				TextMessageWriter.Pfx_Actual + "Could not deserialize object"));
		}

		[Test]
		public void WriteMessageTo_NonMatching_ActualContainsOffendingValue()
		{
			var nonMatching = Serializable.XmlString("s", 3m);
			var subject = new DeserializationConstraint<Serializable>(
				new XmlDeserializer(),
				Has.Property("S").EqualTo("sS")
					.And.Property("D").EqualTo(3m));

			Assert.That(GetMessage(subject, nonMatching), Is.StringContaining(TextMessageWriter.Pfx_Actual + "\"s\""));
		}

		[Test]
		public void WriteMessageTo_NonMatching_ActualContainsActualObject()
		{
			var nonMatching = Serializable.XmlString("s", 3m);
			var subject = new DeserializationConstraint<Serializable>(
				new XmlDeserializer(),
				Has.Property("S").EqualTo("sS")
					.And.Property("D").EqualTo(3m));

			Assert.That(GetMessage(subject, nonMatching), Is.StringContaining(" -> <" + typeof(Serializable).FullName + ">"));
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			Assert.That(Serializable.XmlString("s", 3m),
			            new DeserializationConstraint<Serializable>(
			            	new XmlDeserializer(),
			            	Has.Property("S").EqualTo("s")
			            		.And.Property("D").EqualTo(3m)));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			Assert.That(Serializable.XmlString("s", 3m),
			            Must.Be.XmlDeserializable<Serializable>(
			            	Has.Property("S").EqualTo("s")
			            		.And.Property("D").EqualTo(3m)));
		}
	}
}