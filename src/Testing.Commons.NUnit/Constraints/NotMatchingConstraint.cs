using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public class NotMatchingConstraint : ExpectedConstraint
	{
		public NotMatchingConstraint(object expected) : base(expected) { }

		/// <summary>
		/// Write the constraint description to a MessageWriter.
		/// </summary>
		/// <param name="writer">The writer on which the description is displayed.</param>
		public override void WriteDescriptionTo(MessageWriter writer)
		{
			writer.Write("matching ");
			writer.WriteValue(_expected);
		}
	}
}