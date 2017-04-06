using System.Web.Script.Serialization;

namespace Testing.Commons.Serialization
{
	/// <summary>
	/// Allows testing a single cycle of deserialization using JSON serialization.
	/// </summary>
	/// <remarks>It uses <see cref="JavaScriptSerializer"/> to perform the deserialization.</remarks>
	public class JsonDeserializer : IDeserializer
	{
		private readonly JavaScriptSerializer _serializer;
		
		/// <summary>
		/// Creates an instance of <see cref="JsonDeserializer"/>.
		/// </summary>
		/// <param name="converters">An array that contains the custom converters to be registered.</param>
		public JsonDeserializer(params JavaScriptConverter[] converters)
		{
			_serializer = new JavaScriptSerializer();
			_serializer.RegisterConverters(converters);
		}

		/// <summary>
		/// Deserializes the object represented by <paramref name="toDeserialize"/> using JSON serialization.
		/// </summary>
		/// <param name="toDeserialize">String representation of the serialized object to be JSON-deserialized.</param>
		/// <typeparam name="T">Type to be deserialized.</typeparam>
		/// <returns>The deserialized object.</returns>
		public T Deserialize<T>(string toDeserialize)
		{
			T deserialized = _serializer.Deserialize<T>(toDeserialize);
			return deserialized;
		}
	}
}
