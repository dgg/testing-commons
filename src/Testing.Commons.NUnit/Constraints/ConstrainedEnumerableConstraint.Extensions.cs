using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

public partial class Iz : Is
{
	/// <summary>
	/// Builds an instance of <see cref="ConstrainedEnumerableConstraint"/> with the provided constraints.
	/// </summary>
	/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
	/// <returns>Instance built.</returns>
	public static ConstrainedEnumerableConstraint Constrained(params Constraint[] constraints)
	{
		return new ConstrainedEnumerableConstraint(constraints);
	}
}
