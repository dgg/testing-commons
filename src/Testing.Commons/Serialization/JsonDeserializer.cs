using System.Web.Script.Serialization;

namespace Testing.Commons.Serialization
{
	public class JsonDeserializer : IDeserializer
	{
		private readonly JavaScriptSerializer _serializer;

		public JsonDeserializer(params JavaScriptConverter[] converters)
		{
			_serializer = new JavaScriptSerializer();
			_serializer.RegisterConverters(converters);
		}

		public T Deserialize<T>(string toDeserialize)
		{
			T deserialized = _serializer.Deserialize<T>(toDeserialize);
			return deserialized;
		}
	}
}
