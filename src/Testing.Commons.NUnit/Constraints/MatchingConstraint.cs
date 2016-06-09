using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public class MatchingConstraint : Constraint
	{
		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			return new ConstraintResult(this, null, true);
		}
	}
}