using System.Runtime.Serialization;

namespace Testing.Commons.Serialization;

/// <summary>
/// Allows testing a single cycle of serialization and deserialization using data contract serialization.
/// </summary>
/// <remarks>It uses <see cref="DataContractSerializer"/> to perform the serialization and deserialization.</remarks>
/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
public class DataContractRoundtripSerializer<T> : IRoundtripSerializer<T>
{
	private readonly MemoryStream _stream;
	private readonly StreamReader _reader;
	private readonly DataContractSerializer _serializer;
	private bool disposedValue;

	/// <summary>
	/// Creates an instance of <see cref="DataContractRoundtripSerializer{T}"/>
	/// </summary>
	public DataContractRoundtripSerializer()
	{
		_stream = new MemoryStream();
		_reader = new StreamReader(_stream);

		_serializer = new DataContractSerializer(typeof(T));
	}

	/// <summary>
	/// Serializes the specified object using data contract serialization, writting the output to the returned string.
	/// </summary>
	/// <param name="toSerialize">Object to be data contract-serialized.</param>
	/// <returns>The string representation of the serialized object.</returns>
	public string Serialize(T toSerialize)
	{
		_serializer.WriteObject(_stream, toSerialize);

		_stream.Flush();
		_stream.Seek(0, SeekOrigin.Begin);
		string serialized = _reader.ReadToEnd();
		return serialized;
	}

	/// <summary>
	/// Deserializes the previously serialized object (using <see cref="Serialize"/>) using data contract serialization.
	/// </summary>
	/// <remarks>This method must be called after <see cref="Serialize"/> as it is temporaly coupled to it.</remarks>
	/// <returns>The deserialized object.</returns>
	public T Deserialize()
	{
		_stream.Seek(0, SeekOrigin.Begin);

		T deserialized = (T)(_serializer.ReadObject(_stream) ?? throw new SerializationException(Resources.Exceptions.CannotReadObject));

		return deserialized;
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				_reader.Close();
				_reader.Dispose();
				_stream.Close();
				_stream.Dispose();
			}

			disposedValue = true;
		}
	}

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
	/// </summary>
	/// <remarks>Closes and disposes internal serialization stream.</remarks>
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
