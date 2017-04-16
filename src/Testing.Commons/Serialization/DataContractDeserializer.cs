using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Testing.Commons.Serialization
{
	/// <summary>
	/// Allows testing a single cycle of deserialization using data contract serialization.
	/// </summary>
	/// <remarks>It uses <see cref="DataContractSerializer"/> to perform the deserialization.</remarks>
	public class DataContractDeserializer : IDeserializer
	{
		/// <summary>
		/// Deserializes the object represented by <paramref name="toDeserialize"/> using data contract serialization.
		/// </summary>
		/// <param name="toDeserialize">String representation of the serialized object to be data contract-deserialized.</param>
		/// <typeparam name="T">Type to be deserialized.</typeparam>
		/// <returns>The deserialized object.</returns>
		public T Deserialize<T>(string toDeserialize)
		{
			using (StringReader sr = new StringReader(toDeserialize))
			{
				XmlReader xr = XmlReader.Create(sr);
				try
				{
					var serializer = new DataContractSerializer(typeof(T));
					T deserialized = (T)serializer.ReadObject(xr);
					return deserialized;
				}
				finally
				{
#if NET
					xr.Close();
					sr.Close();
#endif
				}
			}
		}
	}
}