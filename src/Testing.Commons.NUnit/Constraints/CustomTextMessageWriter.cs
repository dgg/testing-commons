using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	internal class CustomTextMessageWriter : TextMessageWriter
	{
		public static readonly string ActualConnector = "->";

		public static void WriteActualConnector(MessageWriter writer)
		{
			writer.WriteConnector(ActualConnector);
		}
	}
}
