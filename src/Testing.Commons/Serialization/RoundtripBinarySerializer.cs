using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Testing.Commons.Serialization
{
	public class RoundtripBinarySerializer<T> : IDisposable
	{
		private readonly MemoryStream _stream;

		public RoundtripBinarySerializer()
		{
			_stream = new MemoryStream();
		}

		public string Serialize(T toSerialize)
		{
			BinaryFormatter outFormatter = new BinaryFormatter();
			outFormatter.Serialize(_stream, toSerialize);

			_stream.Flush();
			_stream.Seek(0, SeekOrigin.Begin);

			string serialized = new StreamReader(_stream).ReadToEnd();
			return serialized;
		}

		public T Deserialize()
		{
			BinaryFormatter inFormatter = new BinaryFormatter();

			_stream.Seek(0, SeekOrigin.Begin);

			T deserialized = (T)inFormatter.Deserialize(_stream);

			return deserialized;
		}

		public void Dispose()
		{
			_stream.Close();
			_stream.Dispose();
		}
	}
}
