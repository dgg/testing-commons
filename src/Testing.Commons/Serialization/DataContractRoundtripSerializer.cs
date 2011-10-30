using System.IO;
using System.Runtime.Serialization;

namespace Testing.Commons.Serialization
{
	public class DataContractRoundtripSerializer<T> : IRoundTripSerializer<T>
	{
		private readonly MemoryStream _stream;
		private readonly DataContractSerializer _serializer;

		public DataContractRoundtripSerializer()
		{
			_stream = new MemoryStream();
			_serializer = new DataContractSerializer(typeof(T));
		}

		public string Serialize(T toSerialize)
		{
			_serializer.WriteObject(_stream, toSerialize);

			_stream.Flush();
			_stream.Seek(0, SeekOrigin.Begin);
			string serialized = new StreamReader(_stream).ReadToEnd();
			return serialized;
		}

		public T Deserialize()
		{
			_stream.Seek(0, SeekOrigin.Begin);

			T deserialized = (T)_serializer.ReadObject(_stream);

			return deserialized;
		}

		public void Dispose()
		{
			_stream.Close();
			_stream.Dispose();
		}
	}
}
