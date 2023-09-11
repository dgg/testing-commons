using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Testing.Commons.Serialization;

/// <summary>
/// Allows testing a single cycle of serialization and deserialization using XML serialization.
/// </summary>
/// <remarks>It uses <see cref="XmlSerializer"/> to perform the serialization and deserialization.</remarks>
/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
public class XmlRoundtripSerializer<T> : IRoundtripSerializer<T>
{
	private readonly MemoryStream _stream;
	private bool disposedValue;

	/// <summary>
	/// Creates an instance of <see cref="XmlRoundtripSerializer{T}"/>
	/// </summary>
	public XmlRoundtripSerializer()
	{
		_stream = new MemoryStream();
	}

	/// <summary>
	/// Serializes the specified object using XML serialization, writting the output to the returned string.
	/// </summary>
	/// <param name="toSerialize">Object to be data XML-serialized.</param>
	/// <returns>The string representation of the serialized object.</returns>
	public string Serialize(T toSerialize)
	{
		var serializer = new XmlSerializer(typeof(T));
		serializer.Serialize(_stream, toSerialize);

		var sb = new StringBuilder();
		using XmlWriter xw = XmlWriter.Create(sb);
		serializer.Serialize(xw, toSerialize);
		xw.Flush();
		_stream.Flush();
		_stream.Seek(0, SeekOrigin.Begin);

		return sb.ToString();
	}

	/// <summary>
	/// Deserializes the previously serialized object (using <see cref="Serialize"/>) using XML serialization.
	/// </summary>
	/// <remarks>This method must be called after <see cref="Serialize"/> as it is temporaly coupled to it.</remarks>
	/// <returns>The deserialized object.</returns>
	public T Deserialize()
	{
		var deserializer = new XmlSerializer(typeof(T));
		_stream.Seek(0, SeekOrigin.Begin);
		using XmlReader xr = XmlReader.Create(_stream);
		T deserialized = (T)(deserializer.Deserialize(xr) ?? throw new SerializationException(Resources.Exceptions.CannotReadObject));
		return deserialized;
	}

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
	/// </summary>
	/// <remarks>Closes and disposes internal serialization stream.</remarks>
	/// <param name="disposing">Indicates whether the method call comes from a <see cref="IDisposable.Dispose"/> method (value <c>true</c>)
	/// or from a finalizer (value <c>false</c>)</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				_stream.Close();
				_stream.Dispose();
			}

			disposedValue = true;
		}
	}

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
	/// </summary>
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
