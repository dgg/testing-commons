using NUnit.Framework.Internal;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Constraints.Support;
using Testing.Commons.NUnit.Tests.Constraints.Subjects;
using Testing.Commons.Serialization;

using Iz = Testing.Commons.NUnit.Constraints.Iz;

namespace Testing.Commons.NUnit.Tests.Constraints;

[TestFixture]
public class XmlSerializationConstraintTester : ConstraintTesterBase
{
	#region ApplyTo

	[Test]
	public void ApplyTo_MatchingSerializable_True()
	{
		var serializable = new Serializable { D = 3m, S = "s" };
		var subject = new SerializationConstraint<Serializable>(new XmlRoundtripSerializer<Serializable>(),
			Is.Not.SameAs(serializable)
				.And.Property("S").EqualTo("s")
				.And.Property("D").EqualTo(3m));

		Assert.That(matches(subject, serializable), Is.True);
	}

	[Test]
	public void ApplyTo_NonSerializable_False()
	{
		var nonSerializable = new NonSerializable("s");
		var subject = new SerializationConstraint<NonSerializable>(new XmlRoundtripSerializer<NonSerializable>(), Is.Not.Null);

		Assert.That(matches(subject, nonSerializable), Is.False);
	}

	[Test]
	public void ApplyTo_NonMatchingSerializable_False()
	{
		var serializable = new Serializable { D = 3m, S = "s" };
		var subject = new SerializationConstraint<Serializable>(new XmlRoundtripSerializer<Serializable>(),
			Is.Not.SameAs(serializable)
				.And.Property("S").EqualTo("sS")
				.And.Property("D").EqualTo(3m));

		Assert.That(matches(subject, serializable), Is.False);
	}

	#endregion

	#region WriteMessageTo

	[Test]
	public void WriteMessageTo_NonSerializable_ExpectedContainsConstraintExpectations_ActualContainsExpectationsErrorPlusObject()
	{
		var nonSerializable = new NonSerializable("s");
		var subject = new SerializationConstraint<NonSerializable>(new XmlRoundtripSerializer<NonSerializable>(), Is.Not.Null);

		Assert.That(getMessage(subject, nonSerializable), Does
			.StartWith(TextMessageWriter.Pfx_Expected + "Deserialized object not null").And
			.Contain(TextMessageWriter.Pfx_Actual + "Could not serialize/deserialize object"));
	}

	[Test]
	public void WriteMessageTo_NonMatchingSerializable_ActualContainsOffendingValueAndActualObject()
	{
		var serializable = new Serializable { D = 3m, S = "s" };
		var subject = new SerializationConstraint<Serializable>(new XmlRoundtripSerializer<Serializable>(),
			Is.Not.SameAs(serializable)
				.And.Property("S").EqualTo("sS")
				.And.Property("D").EqualTo(3m));

		Assert.That(getMessage(subject, serializable), Does.Contain(TextMessageWriter.Pfx_Actual + "\"s\"").And
			.Contain(" -> <" + typeof(Serializable).FullName + ">"));
	}

	#endregion

	[Test]
	public void CanBeNewedUp()
	{
		Assert.That(new Serializable { D = 3m, S = "s" },
			new SerializationConstraint<Serializable>(new XmlRoundtripSerializer<Serializable>(),
				Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m)));
	}

	[Test]
	public void CanBeCreatedWithExtension()
	{
		Assert.That(new Serializable { D = 3m, S = "s" },
			Iz.XmlSerializable<Serializable>(
				Has.Property("S").EqualTo("s")
					.And.Property("D").EqualTo(3m)));
	}
}
