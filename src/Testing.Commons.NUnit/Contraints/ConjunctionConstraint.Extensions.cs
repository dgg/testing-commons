using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Custom constraints.
/// </summary>
public partial class Satisfies : Is
{
	/// <summary>
	/// Builds an instance of <see cref="ConjunctionConstraint"/> that allows joining multiple constraints
	/// while reporting the specific constraint that failed.
	/// </summary>
	/// <param name="constraints">The list of constraints to evaluate.</param>
	/// <returns>Instance built.</returns>
	public static Constraint Conjunction(params Constraint[] constraints)
	{
		return new ConjunctionConstraint(constraints);
	}
}
