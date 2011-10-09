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
			_exposed.WriteExpected(writer);
		}

		public override void WriteActualValueTo(MessageWriter writer)
		{
			_exposed.WriteActual(writer);
		}

		public override void WriteMessageTo(MessageWriter writer)
		{
			_exposed.WriteMember(writer);
			base.WriteMessageTo(writer);
		}
	}
}
