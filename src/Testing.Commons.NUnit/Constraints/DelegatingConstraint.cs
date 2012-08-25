using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Provides a strongly-typed base class to implement custom constraints that
	/// delegate matching and message formatting to another instance of <see cref="Constraint"/>
	/// </summary>
	/// <typeparam name="T">Type of the value to be asserted.</typeparam>
	public abstract class DelegatingConstraint<T> : Constraint
	{
		/// <summary>
		/// Instance of <see cref="Constraint"/> that will be in charge of matching and message formatting.
		/// </summary>
		protected Constraint Delegate { get; set; }

		/// <summary>
		/// Test whether the constraint is satisfied by a given value
		/// </summary>
		/// <param name="current">The value to be tested</param>
		/// <returns>True for success, false for failure</returns>
		public override bool Matches(object current)
		{
			return matches((T)current);
		}

		/// <summary>
		/// Test whether the constraint is satisfied by <paramref name="current"/>.
		/// </summary>
		/// <param name="current">The value to be tested.</param>
		/// <returns>True for success, false for failure.</returns>
		protected abstract bool matches(T current);

		/// <summary>
		/// Write the constraint description to a MessageWriter.
		/// </summary>
		/// <param name="writer">The writer on which the description is displayed</param>
		public override void WriteDescriptionTo(MessageWriter writer)
		{
			Delegate.WriteDescriptionTo(writer);
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
			Delegate.WriteActualValueTo(writer);
		}

		/// <summary>
		/// Write the failure message to the MessageWriter provided
		/// as an argument. The default implementation simply passes
		/// the constraint and the actual value to the writer, which
		/// then displays the constraint description and the value.
		/// Constraints that need to provide additional details,
		/// such as where the error occured can override this.
		/// </summary>
		/// <param name="writer">The MessageWriter on which to display the message</param>
		public override void WriteMessageTo(MessageWriter writer)
		{
			Delegate.WriteMessageTo(writer);
		}
	}
}
