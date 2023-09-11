using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Allows joining multiple constraints while reporting the specific constraint that failed.
/// </summary>
public class ConjunctionConstraint : Constraint
{
	internal static string Pfx_Specific { get; } = "\tSpecifically: ";

	private readonly IEnumerable<Constraint> _constraints;

	/// <summary>
	/// Creates the instance of the constraint.
	/// </summary>
	/// <param name="constraints">The list of constraints to evaluate.</param>
	public ConjunctionConstraint(params Constraint[] constraints) : this(constraints.AsEnumerable()) { }

	/// <summary>
	/// Creates the instance of the constraint.
	/// </summary>
	/// <param name="constraints">The list of constraints to evaluate.</param>
	public ConjunctionConstraint(IEnumerable<Constraint> constraints)
	{
		_constraints = (constraints ?? Array.Empty<Constraint>())
			.Where(c => c != null);
	}

	private Constraint _beingMatched = default!;

	/// <summary>
	/// Applies the constraint to an actual value, returning a ConstraintResult.
	/// </summary>
	/// <param name="actual">The value to be tested</param>
	/// <returns>A ConstraintResult</returns>
	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{
		foreach (var constraint in _constraints)
		{
			ConstraintResult result = constraint.ApplyTo(actual);
			if (!result.IsSuccess)
			{
				_beingMatched = constraint;
				return new ConjuctionConstraintResult(this, actual, result);
			}
		}
		return new ConstraintResult(this, actual, true);
	}

	/// <summary>
	/// The Description of what this constraint tests, for
	/// use in messages and in the ConstraintResult.
	/// </summary>
	public override string Description
	{
		get
		{
			Constraint aggregate = _constraints.Aggregate((c1, c2) => c1 & c2);
			var sb = new StringBuilder(aggregate.Description);
			sb.AppendLine();
			sb.Append(Pfx_Specific);
			sb.Append(_beingMatched?.Description);
			return sb.ToString();
		}
		protected set { }
	}

	internal class ConjuctionConstraintResult : ConstraintResult
	{
		private readonly object _actual;
		private readonly ConstraintResult _failing;

		public ConjuctionConstraintResult(IConstraint constraint, object actual, ConstraintResult failing) : base(constraint, failing.ActualValue, failing.Status)
		{
			_actual = actual;
			_failing = failing;
		}

		/// <summary>
		/// Write the actual value for a failing constraint test to a
		/// MessageWriter. The default implementation simply writes
		/// the raw value of actual, leaving it to the writer to
		/// perform any formatting.
		/// </summary>
		/// <param name="writer">The writer on which the actual value is displayed</param>
		public override void WriteActualValueTo(MessageWriter writer)
		{
			writer.WriteActualValue(_actual);
			writer.WriteLine();
			writer.Write(Pfx_Specific);
			_failing.WriteActualValueTo(writer);
		}
	}
}
