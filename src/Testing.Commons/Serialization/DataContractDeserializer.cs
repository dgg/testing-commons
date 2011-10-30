using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Testing.Commons.Serialization
{
	public class DataContractDeserializer : IDeserializer
	{
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
					xr.Close();
					sr.Close();
				}
			}
		}
	}
}