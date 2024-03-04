using NUnit.Framework.Constraints;
using NUnit.Framework;

namespace Testing.Commons.NUnit.Constraints;

public partial class Iz : Is
{
	/// <summary>
	/// Builds an instance of <see cref="JsonEqualConstraint"/> that allows assertions using
	/// compact JSON strings.
	/// </summary>
	/// <remarks>A compact JSON string notation uses single quotes for names and string values instead
	/// of double quotes, removing the need to escape such double quotes.
	/// <para>A non-compact JSON string uses the canonical double quote style for names an string values.</para>
	/// </remarks>
	/// <param name="expected">The expected value in JSON compact notation.</param>
	/// <returns>Instance built.</returns>
	public static Constraint Json(string expected)
	{
		return new JsonEqualConstraint(expected);
	}
}
