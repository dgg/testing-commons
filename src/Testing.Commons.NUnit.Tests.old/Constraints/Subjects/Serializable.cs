using System;
using System.Runtime.Serialization;

namespace Testing.Commons.NUnit.Tests.Constraints.Subjects
{
	[DataContract]
	public partial class Serializable
	{
		[DataMember]
		public string S { get; set; }
		[DataMember]
		public decimal D { get; set; }

		public static string DataContractString(string s, decimal d)
		{
			return string.Format("<Serializable xmlns=\"http://schemas.datacontract.org/2004/07/Testing.Commons.NUnit.Tests.Constraints.Subjects\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><D>{1}</D><S>{0}</S></Serializable>",
				s, d);
		}

		public static string XmlString(string s, decimal d)
		{
			return $"<?xml version=\"1.0\" encoding=\"utf-16\"?><Serializable xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><S>{s}</S><D>{d}</D></Serializable>";
		}
		public static string JsonString(string s, decimal d)
		{
			return $"{{\"S\":\"{s}\",\"D\":{d}}}";
		}

		public static string DataContractJsonString(string s, decimal d)
		{
			return $"{{\"D\":{d},\"S\":\"{s}\"}}";
		}
	}
}