using System;
using System.Text;
using System.Web.Script.Serialization;

namespace Testing.Commons.Serialization
{
	internal class RoundtripJsonSerializer<T> : IDisposable
	{
		private readonly JavaScriptSerializer _serializer;
		private readonly StringBuilder _sb;

		public RoundtripJsonSerializer(params JavaScriptConverter[] converters)
		{
			_serializer = new JavaScriptSerializer();
			_serializer.RegisterConverters(converters);

			_sb = new StringBuilder();
		}

		public string Serialize(T toSerialize)
		{
			_serializer.Serialize(toSerialize, _sb);
			return _sb.ToString();
		}

		public T Deserialize()
		{
			T deserialized = _serializer.Deserialize<T>(_sb.ToString());
			return deserialized;
		}

		public void Dispose() { }
	}
}