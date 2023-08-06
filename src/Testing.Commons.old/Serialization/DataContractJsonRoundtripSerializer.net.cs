using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Testing.Commons.Serialization
{
	/// <summary>
	/// Allows testing a single cycle of serialization and deserialization using data contract JSON serialization.
	/// </summary>
	/// <remarks>It uses <see cref="DataContractJsonSerializer"/> to perform the serialization and deserialization.</remarks>
	/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
	public class DataContractJsonRoundtripSerializer<T> : IRoundtripSerializer<T>
	{
		private readonly MemoryStream _stream;
		private readonly DataContractJsonSerializer _serializer;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataContractJsonRoundtripSerializer{T}"/> class.
		/// </summary>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize. The default is <c>4</c>.</param>
		/// <param name="ignoreExtensionDataObject">true to ignore the <see cref="IExtensibleDataObject"/> interface upon serialization and ignore unexpected data upon deserialization; otherwise, false. The default is false.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="IDataContractSurrogate"/> to customize the serialization process.</param>
		/// <param name="alwaysEmitTypeInformation">true to emit type information; otherwise, false. The default is false.</param>
		public DataContractJsonRoundtripSerializer(int maxItemsInObjectGraph = 4, bool ignoreExtensionDataObject = false, IDataContractSurrogate dataContractSurrogate = null, bool alwaysEmitTypeInformation = false)
		{
			_stream = new MemoryStream();
			_serializer = new DataContractJsonSerializer(
				typeof(T),
				new[] { typeof(T) },
				maxItemsInObjectGraph,
				ignoreExtensionDataObject,
				dataContractSurrogate,
				alwaysEmitTypeInformation); ;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>Closes and disposes internal serialization stream.</remarks>
		public void Dispose()
		{
			_stream.Finalize();
			_stream.Dispose();
		}

		/// <summary>
		/// Serializes the specified object using data contract JSON serialization, writting the output to the returned string.
		/// </summary>
		/// <param name="toSerialize">Object to be JSON data contract-serialized.</param>
		/// <returns>The string representation of the serialized object.</returns>
		public string Serialize(T toSerialize)
		{
			_serializer.WriteObject(_stream, toSerialize);

			_stream.Flush();
			_stream.Seek(0, SeekOrigin.Begin);
			string serialized = new StreamReader(_stream).ReadToEnd();
			return serialized;
		}

		/// <summary>
		/// Deserializes the previously serialized object (using <see cref="Serialize"/>) using data contract JSON serialization.
		/// </summary>
		/// <remarks>This method must be called after <see cref="Serialize"/> as it is temporaly coupled to it.</remarks>
		/// <returns>The deserialized object.</returns>
		public T Deserialize()
		{
			_stream.Seek(0, SeekOrigin.Begin);

			T deserialized = (T)_serializer.ReadObject(_stream);

			return deserialized;
		}
	}
}