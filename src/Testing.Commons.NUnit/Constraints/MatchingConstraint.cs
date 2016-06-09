using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public class MatchingConstraint : Constraint
	{
		public MatchingConstraint(object expected) { }

		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			return new ConstraintResult(this, null, true);
		}
	}

	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="MatchingConstraint"/> to match the provided expected object.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="expected">The object to match the actual value against.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Expected(this Must.MatchEntryPoint entry, object expected)
		{
			return new MatchingConstraint(expected);
		}
	}
}