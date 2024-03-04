using ExpectedObjects;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;

namespace Testing.Commons.NUnit.Constraints.Support;

/// <summary>
/// Adapter that allows writing an instance of <see cref="EqualityResult"/> to a <see cref="MessageWriter"/>.
/// </summary>
internal class WritableEqualityResult : EqualityResult
{
	public WritableEqualityResult(string member, object expected, object actual) : base(false, member, expected, actual) { }

	/// <summary>
	/// Writes the member that did not match.
	/// </summary>
	public void WriteOffendingMember(MessageWriter writer)
	{
		writer.WriteMessageLine(0, Member);
	}

	/// <summary>
	/// Writes the value of the actual object.
	/// </summary>
	public void WriteActual(MessageWriter writer)
	{
		if (Actual is IMissingMember)
		{
			writer.Write("member was missing");
		}
		else if (Actual is IMissingElement)
		{
			writer.Write("element was missing");
		}
		else if (Actual is IUnexpectedElement element)
		{
			writer.WriteActualValue(element.Element);
		}
		else
		{
			writer.WriteActualValue(Actual);
		}
	}

	/// <summary>
	/// Writes the value of the expected object.
	/// </summary>
	public string WriteExpected()
	{
		using MessageWriter writer = new TextMessageWriter();
		if (Actual is IUnexpectedElement)
		{
			writer.Write("nothing");
		}
		else
		{
			writer.WriteValue(Expected);
		}
		return writer.ToString();
	}
}
