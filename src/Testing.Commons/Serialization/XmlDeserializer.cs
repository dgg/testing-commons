using System.IO;
using System.Xml.Serialization;

namespace Testing.Commons.Serialization
{
	public class XmlDeserializer : IDeserializer
	{
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