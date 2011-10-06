using System;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public class MatchingConstraint : ExpectedConstraint
	{
		public MatchingConstraint(object expected) : base(expected) { }

		/// <summary>
		/// Write the constraint description to a MessageWriter.
		/// </summary>
		/// <param name="writer">The writer on which the description is displayed.</param>
		public override void WriteDescriptionTo(MessageWriter writer)
		{
			string results = _writer.GetFormattedResults();
			string resultWithLeadingAndWithoutTrailing = Environment.NewLine +
				results.Remove(results.Length - 2, 2);

			writer.Write(resultWithLeadingAndWithoutTrailing);
		}
	}
}
