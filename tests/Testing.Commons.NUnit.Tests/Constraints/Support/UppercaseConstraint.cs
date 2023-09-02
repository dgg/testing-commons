using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Tests.Constraints.Support;
internal class UppercaseConstraint : Constraint
{
	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{
		return new ConstraintResult(this, actual, match(actual));
	}

	private bool match(object current)
	{
		var c = (char)current;
		return char.IsUpper(c);
	}

	public override string Description => "An uppercase character";
}
