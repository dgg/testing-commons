using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints.Support;

internal static class MessageWriterExtensions
{
	public const string ActualConnector = "->";

	public static void WriteActualConnector(this MessageWriter writer)
	{
		writer.WriteConnector(ActualConnector);
	}

	public static void WritePredicate(this MessageWriter writer, string predicate)
	{
		writer.Write(predicate);
		writer.Write(" ");
	}

	public static void WriteConnector(this MessageWriter writer, string connector)
	{
		writer.Write(" ");
		writer.Write(connector);
		writer.Write(" ");
	}
}
