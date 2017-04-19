using System.IO;
using System.Xml;

namespace Testing.Commons.Serialization
{
	internal static class IO
	{
		public static void Finalize(this StringReader sr)
		{
#if NET
			sr.Close();
#endif
		}

		public static void Finalize(this XmlReader xr)
		{
#if NET
			xr.Close();
#endif
		}

		public static void Finalize(this Stream stream)
		{
#if NET
			stream.Close();
#endif
		}
	}
}