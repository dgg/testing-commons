using System.Text;
using System.Web.Script.Serialization;

namespace Testing.Commons.Serialization
{
	/// <summary>
	/// Allows testing a single cycle of serialization and deserialization using JSON serialization.
	/// </summary>
	/// <remarks>It uses <see cref="JavaScriptSerializer"/> to perform the serialization and deserialization.</remarks>
	/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
	public class JsonRoundtripSerializer<T> : IRoundTripSerializer<T>
	{
		private readonly JavaScriptSerializer _serializer;
		private readonly StringBuilder _sb;

		/// <summary>
		/// Creates an instance of <see cref="JsonRoundtripSerializer{T}"/>
		/// </summary>
		/// <param name="converters">An array that contains the custom converters to be registered.</param>
		public JsonRoundtripSerializer(params JavaScriptConverter[] converters)
		{
			_serializer = new JavaScriptSerializer();
			_serializer.RegisterConverters(converters);

			_sb = new StringBuilder();
		}

		/// <summary>
		/// Serializes the specified object using JSON serialization, writting the output to the returned string.
		/// </summary>
		/// <param name="toSerialize">Object to be data JSON-serialized.</param>
		/// <returns>The string representation of the serialized object.</returns>
		public string Serialize(T toSerialize)
		{
			_serializer.Serialize(toSerialize, _sb);
			return _sb.ToString();
		}

		/// <summary>
		/// Deserializes the previously serialized object (using <see cref="Serialize"/>) using JSON serialization.
		/// </summary>
		/// <remarks>This method must be called after <see cref="Serialize"/> as it is temporaly coupled to it.</remarks>
		/// <returns>The deserialized object.</returns>
		public T Deserialize()
		{
			T deserialized = _serializer.Deserialize<T>(_sb.ToString());
			return deserialized;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() { }
	}
}