using ExpectedObjects;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Allows matching partial expected objects.
	/// </summary>
	/// <remarks>It is a useful feature when it is easier to create a object with the same shape and a subset of the values as the actual object.</remarks>
	public class MatchingConstraint : Constraint
	{
		private readonly ExpectedObject _expected;
		private readonly ExposingWriter _writer;
		private WritableEqualityResult _exposed;

		/// <summary>
		/// Creates the instance of the constraint.
		/// </summary>
		/// <param name="expected">The object to match the actual value against.</param>
		public MatchingConstraint(object expected)
		{
			_writer = new ExposingWriter(new ShouldWriter());
			_expected = expected.ToExpectedObject().Configure(ctx =>
			{
				ctx.IgnoreTypes();
				ctx.SetWriter(_writer);
			});
		}

		/// <summary>
		/// Test whether the constraint is satisfied by a given value.
		/// </summary>
		/// <param name="current">The value to be tested</param>
		/// <returns>True for success, false for failure</returns>
		public override bool Matches(object current)
		{
			actual = current;
			bool matched = _expected.Equals(actual);
			if (!matched)
			{
				_writer.GetFormattedResults();
				_exposed = _writer.Exposed;
			}
			return matched;
		}

		/// <summary>
		/// Write the constraint description to a MessageWriter.
		/// </summary>
		/// <param name="writer">The writer on which the description is displayed.</param>
		public override void WriteDescriptionTo(MessageWriter writer)
		{
			_exposed.WriteExpected(writer);
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
			_exposed.WriteActual(writer);
		}

		/// <summary>
		/// Write the failure message to the MessageWriter provided
		/// as an argument. The default implementation simply passes
		/// the constraint and the actual value to the writer, which
		/// then displays the constraint description and the value.
		/// </summary>
		/// <param name="writer">The MessageWriter on which to display the message</param>
		public override void WriteMessageTo(MessageWriter writer)
		{
			_exposed.WriteOffendingMember(writer);
			base.WriteMessageTo(writer);
		}
	}
}
