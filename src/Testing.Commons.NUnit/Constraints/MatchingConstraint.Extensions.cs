using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

public partial class Haz : Has
{
	/// <summary>
	/// Builds an instance of <see cref="MatchingConstraint"/> to match the provided expected object.
	/// </summary>
	/// <param name="expected">The object to match the actual value against.</param>
	/// <returns>Instance built.</returns>
	public static Constraint Match(object expected)
	{
		return new MatchingConstraint(expected);
	}
}
