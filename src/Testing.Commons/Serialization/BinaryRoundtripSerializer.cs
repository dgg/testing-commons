using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Testing.Commons.Serialization
{
	/// <summary>
	/// Allows testing a single cycle of serialization and deserialization using binary serialization.
	/// </summary>
	/// <remarks>It uses <see cref="BinaryFormatter"/> to perform the serialization and deserialization.</remarks>
	/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
	public class BinaryRoundtripSerializer<T> : IRoundTripSerializer<T>
	{
		private readonly MemoryStream _stream;
		
		/// <summary>
		/// Creates an instance of <see cref="BinaryRoundtripSerializer{T}"/>
		/// </summary>
		public BinaryRoundtripSerializer()
		{
			_stream = new MemoryStream();
		}

		/// <summary>
		/// Serializes the specified object using binary serialization, writting the output to the returned string.
		/// </summary>
		/// <param name="toSerialize">Object to be binary-serialized.</param>
		/// <returns>The string representation of the serialized object.</returns>
		public string Serialize(T toSerialize)
		{
			BinaryFormatter outFormatter = new BinaryFormatter();
			outFormatter.Serialize(_stream, toSerialize);

			_stream.Flush();
			_stream.Seek(0, SeekOrigin.Begin);

			string serialized = new StreamReader(_stream).ReadToEnd();
			return serialized;
		}

		/// <summary>
		/// Deserializes the previously serialized object (using <see cref="Serialize"/>) using binary serialization.
		/// </summary>
		/// <remarks>This method must be called after <see cref="Serialize"/> as it is temporaly coupled to it.</remarks>
		/// <returns>The deserialized object.</returns>
		public T Deserialize()
		{
			BinaryFormatter inFormatter = new BinaryFormatter();

			_stream.Seek(0, SeekOrigin.Begin);

			T deserialized = (T)inFormatter.Deserialize(_stream);

			return deserialized;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>Closes and disposes internal serialization stream.</remarks>
		public void Dispose()
		{
			_stream.Close();
			_stream.Dispose();
		}
	}
}
