using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints;

public partial class Iz : Is
{
	/// <summary>
	/// Builds an instance of <see cref="DeserializationConstraint{T}"/> that allows checking the deserialization of an object.
	/// </summary>
	/// <param name="deserializer">Deserializer used to deserialize the tested value.</param>
	/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
	/// <returns>Instance built.</returns>
	public static Constraint Deserializable<T>(IDeserializer deserializer, Constraint constraintOverDeserialized)
	{
		return new DeserializationConstraint<T>(deserializer, constraintOverDeserialized);
	}

	/// <summary>
	/// Builds an instance of <see cref="DeserializationConstraint{T}"/> that allows checking the XML deserialization of an object.
	/// </summary>
	/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
	/// <returns>Instance built.</returns>
	public static Constraint XmlDeserializable<T>(Constraint constraintOverDeserialized)
	{
		return new DeserializationConstraint<T>(new XmlDeserializer(), constraintOverDeserialized);
	}

	/// <summary>
	/// Builds an instance of <see cref="DeserializationConstraint{T}"/> that allows checking the data contract deserialization of an object.
	/// </summary>
	/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
	/// <returns>Instance built.</returns>
	public static Constraint DataContractDeserializable<T>(Constraint constraintOverDeserialized)
	{
		return new DeserializationConstraint<T>(new DataContractDeserializer(), constraintOverDeserialized);
	}
}
