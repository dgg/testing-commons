using System.IO;
using System.Xml.Serialization;

namespace Testing.Commons.Serialization
{
	/// <summary>
	/// Allows testing a single cycle of deserialization using XML serialization.
	/// </summary>
	/// <remarks>It uses <see cref="XmlSerializer"/> to perform the deserialization.</remarks>
	public class XmlDeserializer : IDeserializer
	{
		/// <summary>
		/// Deserializes the object represented by <paramref name="toDeserialize"/> using XML serialization.
		/// </summary>
		/// <param name="toDeserialize">String representation of the serialized object to be XML-deserialized.</param>
		/// <typeparam name="T">Type to be deserialized.</typeparam>
		/// <returns>The deserialized object.</returns>
		public T Deserialize<T>(string toDeserialize)
		{
			var serializer = new XmlSerializer(typeof(T));
			using (StringReader sr = new StringReader(toDeserialize))
			{
				try
				{
					T deserialized = (T)serializer.Deserialize(sr);
					return deserialized;
				}
				finally
				{
					sr.Close();	
				}
			}
		}
	}
}