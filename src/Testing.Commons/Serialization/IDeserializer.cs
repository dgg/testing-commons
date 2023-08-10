namespace Testing.Commons.Serialization;

/// <summary>
/// Allows testing a single cycle of deserialization.
/// </summary>
public interface IDeserializer
{
	/// <summary>
	/// Deserializes the object represented by <paramref name="toDeserialize"/>.
	/// </summary>
	/// <param name="toDeserialize">String representation of the serialized object to be deserialized.</param>
	/// <typeparam name="T">Type to be deserialized.</typeparam>
	/// <returns>The deserialized object.</returns>
	T Deserialize<T>(string toDeserialize);
}
