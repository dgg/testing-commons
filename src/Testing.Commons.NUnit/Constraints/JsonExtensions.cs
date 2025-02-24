using System.Diagnostics.CodeAnalysis;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Extensions for assertions involving compact JSON strings.
/// </summary>
/// <remarks>A compact JSON string notation uses single quotes for names and string values instead
/// of double quotes, removing the need to escape such double quotes.
/// <para>An extended JSON string uses the canonical double quote style for names an string values.</para>
/// </remarks>
public static class JsonExtensions
{
	/// <summary>
	/// Flags the constraint to use the supplied compact JSON string when performing equality.
	/// </summary>
	/// <param name="constraint">The constraint to modify.</param>
	/// <returns>The modified constraint.</returns>
	/// <example><code>Assert.That("{\"prop\"=\"value\"}", Is.EqualTo("{'prop'='value'}").AsJson())</code></example>
	public static EqualUsingConstraint<string> AsJson([NotNull] this EqualStringConstraint constraint)
	{
		return constraint.Using(JsonString.Comparer);
	}
}
