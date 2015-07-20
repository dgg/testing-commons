using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Allows joining multiple constraints while reporting the specific constraint that failed.
	/// </summary>
	public class ConjunctionConstraint : Constraint
	{
		private readonly IEnumerable<Constraint> _constraints;
		/// <summary>
		/// Prefix added when writing the constraint
		/// </summary>
		public static readonly string Pfx_Specific = "\tSpecifically: ";

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
			_constraints = (constraints ?? new Constraint[0])
				.Where(c => c != null);
		}

		private Constraint _failing;
		/// <summary>
		/// Test whether the constraint is satisfied by a given value.
		/// </summary>
		/// <param name="current">The value to be tested</param>
		/// <returns>True for success, false for failure</returns>
		public override bool Matches(object current)
		{
			actual = current;

			bool matches = true;
			foreach (var constraint in _constraints)
			{
				matches = constraint.Matches(current);
				if (!matches)
				{
					_failing = constraint;
					break;
				}
			}
			return matches;
		}

		/// <summary>
		/// Write the constraint description to a MessageWriter.
		/// </summary>
		/// <param name="writer">The writer on which the description is displayed.</param>
		public override void WriteDescriptionTo(MessageWriter writer)
		{
			writeConjuction(writer);
			writer.WriteLine();
			writer.Write(Pfx_Specific);
			_failing.WriteDescriptionTo(writer);
		}

		private void writeConjuction(MessageWriter writer)
		{
			Constraint aggregate = _constraints.Aggregate((c1, c2) => c1 & c2);
			aggregate.WriteDescriptionTo(writer);
		}

		/// <summary>
		/// Write the actual value for a failing constraint test to a MessageWriter.
		/// The default implementation simply writesthe raw value of actual, leaving it to the writer to
		/// perform any formatting.
		/// </summary>
		/// <param name="writer">The writer on which the actual value is displayed</param>
		public override void WriteActualValueTo(MessageWriter writer)
		{
			base.WriteActualValueTo(writer);
			writer.WriteLine();
			writer.Write(Pfx_Specific);
			_failing.WriteActualValueTo(writer);
		}
	}
}