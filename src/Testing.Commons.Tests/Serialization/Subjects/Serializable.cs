using System;

namespace Testing.Commons.Tests.Serialization.Subjects
{
	public partial class Serializable
	{
		public string S { get; set; }
		public decimal D { get; set; }

		public static string DataContractString(string s, decimal d)
		{
			return string.Format(
				"<Serializable xmlns=\"http://schemas.datacontract.org/2004/07/Testing.Commons.Tests.Serialization.Subjects\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><_x003C_D_x003E_k__BackingField>{1}</_x003C_D_x003E_k__BackingField><_x003C_S_x003E_k__BackingField>{0}</_x003C_S_x003E_k__BackingField></Serializable>",
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
				"{{\"<D>k__BackingField\":{0},\"<S>k__BackingField\":\"{1}\"}}",
				d, s);
		}
	}
}
