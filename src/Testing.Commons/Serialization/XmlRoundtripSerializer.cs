using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Testing.Commons.Serialization
{
	public class XmlRoundtripSerializer<T> : IRoundTripSerializer<T>
	{
		private readonly MemoryStream _stream;

		public XmlRoundtripSerializer()
		{
			_stream = new MemoryStream();
		}

		public string Serialize(T toSerialize)
		{
			var serializer = new XmlSerializer(typeof(T));
			serializer.Serialize(_stream, toSerialize);

			StringBuilder sb = new StringBuilder();
			XmlWriter xw = XmlWriter.Create(sb);
			serializer.Serialize(xw, toSerialize);
			xw.Flush();
			_stream.Flush();
			_stream.Seek(0, SeekOrigin.Begin);

			return sb.ToString();
		}

		public T Deserialize()
		{
			var deserializer = new XmlSerializer(typeof(T));
			_stream.Seek(0, SeekOrigin.Begin);
			T deserialized = (T)deserializer.Deserialize(_stream);
			return deserialized;
		}

		public void Dispose()
		{
			_stream.Close();
			_stream.Dispose();
		}
	}
}
