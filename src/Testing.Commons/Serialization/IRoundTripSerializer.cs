namespace Testing.Commons.Serialization;

/// <summary>
/// Allows testing a single cycle of serialization and deserialization.
/// </summary>
/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
public interface IRoundtripSerializer<T> : IDisposable
{
	/// <summary>
	/// Serializes the specified object, writting the output to the returned string.
	/// </summary>
	/// <param name="toSerialize">Object to be serialized.</param>
	/// <returns>The string representation of the serialized object.</returns>
	string Serialize(T toSerialize);

	/// <summary>
	/// Deserializes the previously serialized object (using <see cref="Serialize"/>).
	/// </summary>
	/// <remarks>This method must be called after <see cref="Serialize"/> as it is temporaly coupled to it.</remarks>
	/// <returns>The deserialized object.</returns>
	T Deserialize();
}
