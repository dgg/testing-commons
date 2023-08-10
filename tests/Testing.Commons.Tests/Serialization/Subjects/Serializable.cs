using System.Runtime.Serialization;

namespace Testing.Commons.Tests.Serialization.Subjects;

[DataContract]
public partial class Serializable
{
	[DataMember]
	public string S { get; set; }
	[DataMember]
	public decimal D { get; set; }

	public static string DataContractString(string s, decimal d)
	{
		return string.Format(
			"<Serializable xmlns=\"http://schemas.datacontract.org/2004/07/Testing.Commons.Tests.Serialization.Subjects\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><D>{1}</D><S>{0}</S></Serializable>",
			s, d);
	}

	public static string XmlString(string s, decimal d)
	{
		return string.Format("<?xml version=\"1.0\" encoding=\"utf-16\"?><Serializable xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><S>{0}</S><D>{1}</D></Serializable>",
			s, d);
	}
	public static string JsonString(string s, decimal d)
	{
		return string.Format("{{\"S\":\"{0}\",\"D\":{1}}}", s, d);
	}

	public static string DataContractJsonString(string s, decimal d)
	{
		return string.Format(
			"{{\"D\":{0},\"S\":\"{1}\"}}",
			d, s);
	}
}
