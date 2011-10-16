using System.Collections;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Allows asserting on the number of elements of any instance of <see cref="IEnumerable"/>.
	/// </summary>
	/// <remarks>When evaluating linq queries NUnit does not provide a way of aserting on the element count.</remarks>
	public class EnumerableCountConstraint : Constraint
	{
		private readonly Constraint _countConstraint;
		private readonly TypeRevealingConstraint _enumerable;

		/// <summary>
		/// Creates the instance of the constraint.
		/// </summary>
		/// <param name="countConstraint">The constraint to be applied to the element count.</param>
		public EnumerableCountConstraint(Constraint countConstraint)
		{
			_countConstraint = countConstraint;
			_enumerable = new TypeRevealingConstraint(typeof(IEnumerable));
		}

		Constraint _beingMatched;
		/// <summary>
		/// Test whether the constraint is satisfied by a given value.
		/// </summary>
		/// <param name="current">The value to be tested</param>
		/// <returns>True for success, false for failure</returns>
		public override bool Matches(object current)
		{
			actual = current;
			_beingMatched = _enumerable;
			bool matched = _beingMatched.Matches(current);
			if (matched)
			{
				int count = calculateCount((IEnumerable)current);
				_beingMatched = new CountConstraint(_countConstraint, (IEnumerable)actual);
				matched = _beingMatched.Matches(count);
			}

			return matched;
		}

		private int calculateCount(IEnumerable current)
		{
			int num = 0;
			IEnumerator enumerator = current.GetEnumerator();
			while (enumerator.MoveNext())
			{
				num++;
			}
			return num;
		}

		/// <summary>
		/// Write the constraint description to a MessageWriter.
		/// </summary>
		/// <param name="writer">The writer on which the description is displayed.</param>
		public override void WriteDescriptionTo(MessageWriter writer)
		{
			_beingMatched.WriteDescriptionTo(writer);
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
			_beingMatched.WriteActualValueTo(writer);
		}
	}
}