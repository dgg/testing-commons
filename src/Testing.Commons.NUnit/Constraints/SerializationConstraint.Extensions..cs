using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints;

public partial class Iz : Is
{
	/// <summary>
	/// Builds an instance of <see cref="SerializationConstraint{T}"/> that allows checking the serialization/deserialization of an object.
	/// </summary>
	/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
	/// <param name="serializer"></param>
	/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
	/// <returns>Instance built.</returns>
	public static Constraint Serializable<T>(IRoundtripSerializer<T> serializer, Constraint constraintOverDeserialized)
	{
		return new SerializationConstraint<T>(serializer, constraintOverDeserialized);
	}

	/// <summary>
	/// Builds an instance of <see cref="SerializationConstraint{T}"/> that allows checking the XML serialization/deserialization of an object.
	/// </summary>
	/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
	/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
	/// <returns>Instance built.</returns>
	public static new Constraint XmlSerializable<T>(Constraint constraintOverDeserialized)
	{
		return new SerializationConstraint<T>(new XmlRoundtripSerializer<T>(), constraintOverDeserialized);
	}

	/// <summary>
	/// Builds an instance of <see cref="SerializationConstraint{T}"/> that allows checking the data contract serialization/deserialization of an object.
	/// </summary>
	/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
	/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
	/// <returns>Instance built.</returns>
	public static Constraint DataContractSerializable<T>(Constraint constraintOverDeserialized)
	{
		return new SerializationConstraint<T>(new DataContractRoundtripSerializer<T>(), constraintOverDeserialized);
	}
}
