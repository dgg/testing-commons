using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Testing.Commons.Serialization
{
	/// <summary>
	/// Allows testing a single cycle of deserialization using data contract JSON serialization.
	/// </summary>
	/// <remarks>It uses <see cref="DataContractJsonSerializer"/> to perform the deserialization.</remarks>
	public class DataContractJsonDeserializer : IDeserializer
	{
		private readonly int _maxItemsInObjectGraph;
		private readonly bool _ignoreExtensionDataObject, _alwaysEmitTypeInformation;
		private readonly IDataContractSurrogate _dataContractSurrogate;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataContractJsonDeserializer"/> class.
		/// </summary>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize. The default is <c>4</c>.</param>
		/// <param name="ignoreExtensionDataObject">true to ignore the <see cref="IExtensibleDataObject"/> interface upon serialization and ignore unexpected data upon deserialization; otherwise, false. The default is false.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="IDataContractSurrogate"/> to customize the serialization process.</param>
		/// <param name="alwaysEmitTypeInformation">true to emit type information; otherwise, false. The default is false.</param>
		public DataContractJsonDeserializer(int maxItemsInObjectGraph = 4, bool ignoreExtensionDataObject = false, IDataContractSurrogate dataContractSurrogate = null, bool alwaysEmitTypeInformation = false)
		{
			_alwaysEmitTypeInformation = alwaysEmitTypeInformation;
			_ignoreExtensionDataObject = ignoreExtensionDataObject;
			_dataContractSurrogate = dataContractSurrogate;
			_maxItemsInObjectGraph = maxItemsInObjectGraph;
		}

		/// <summary>
		/// Deserializes the object represented by <paramref name="toDeserialize"/> using data contract serialization.
		/// </summary>
		/// <param name="toDeserialize">String representation of the serialized object to be data contract-deserialized.</param>
		/// <typeparam name="T">Type to be deserialized.</typeparam>
		/// <returns>The deserialized object.</returns>
		public T Deserialize<T>(string toDeserialize)
		{
			var serializer = new DataContractJsonSerializer(
				typeof(T),
				new[] { typeof(T) },
				_maxItemsInObjectGraph,
				_ignoreExtensionDataObject,
				_dataContractSurrogate,
				_alwaysEmitTypeInformation);

			using (var ms = new MemoryStream(Encoding.Default.GetBytes(toDeserialize)))
			{
				try
				{
					ms.Seek(0, SeekOrigin.Begin);
					T deserialized = (T)serializer.ReadObject(ms);
					return deserialized;
				}
				finally
				{
#if NET
					ms.Close();
#endif
				}
			}
		}
	}
}