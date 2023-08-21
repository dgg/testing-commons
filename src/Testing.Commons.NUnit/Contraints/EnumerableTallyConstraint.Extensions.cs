using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

public partial class Haz : Has
{
	/// <summary>
	/// Builds an instance of <see cref="EnumerableTallyConstraint"/> that allows asserting on the number of elements of any instance of <see cref="System.Collections.IEnumerable"/>.
	/// </summary>
	/// <param name="countConstraint">The constraint to be applied to the element count.</param>
	/// <returns>Instance built.</returns>
	public static Constraint Tally(Constraint countConstraint)
	{
		return new EnumerableTallyConstraint(countConstraint);
	}
}
