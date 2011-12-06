using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Allows checking whether a type properly implements comparison operators against the same type.
	/// </summary>
	/// <typeparam name="T">Type of objects to compare.</typeparam>
	public class ImplementsComparisonConstraint<T> : Constraint
	{
		private readonly T _strictlyLessThan;
		private readonly T _strictlyGreaterThan;
		ChainedConstraints _rules;

		/// <summary>
		/// Initializes a new instance of the <see cref="ImplementsComparisonConstraint{T}"/> class when type implements comparison operators against the same type.
		/// </summary>
		/// <param name="strictlyLessThan">An instance of <typeparamref name="T"/> that is strictly less than the value tested.</param>
		/// <param name="strictlyGreaterThan">An instance of <typeparamref name="T"/> that is strictly greater than the value tested.</param>
		public ImplementsComparisonConstraint(T strictlyLessThan, T strictlyGreaterThan)
		{
			_strictlyLessThan = strictlyLessThan;
			_strictlyGreaterThan = strictlyGreaterThan;
		}

		/// <summary>
		/// Test whether the constraint is satisfied by a given value.
		/// </summary>
		/// <param name="current">The value to be tested.</param>
		/// <returns>True for success, false for failure.</returns>
		public override bool Matches(object current)
		{
			actual = current;
			T equalOrActual = (T)actual;
			_rules = new ChainedConstraints(
				() => ComparisonConstraint<T>.LessThanOrEqual(equalOrActual),
				() => ComparisonConstraint<T>.GreaterThanOrEqual(equalOrActual),
				() => ComparisonConstraint<T>.GreaterThan(_strictlyLessThan),
				() => ComparisonConstraint<T>.GreaterThanOrEqual(_strictlyLessThan),
				() => ComparisonConstraint<T>.LessThan(_strictlyGreaterThan),
				() => ComparisonConstraint<T>.LessThanOrEqual(_strictlyGreaterThan),
				ComparisonConstraint<T>.GreaterThanNull,
				() => ComparisonConstraint<T>.NullAllwaysLessThan(_strictlyLessThan)
				);

			return _rules.Evaluate(actual);
		}

		/// <summary>
		/// Write the constraint description to a MessageWriter.
		/// </summary>
		/// <param name="writer">The writer on which the description is displayed.</param>
		public override void WriteDescriptionTo(MessageWriter writer) { }

		/// <summary>
		/// Write the actual value for a failing constraint test to a
		/// MessageWriter. The default implementation simply writes
		/// the raw value of actual, leaving it to the writer to
		/// perform any formatting.
		/// </summary>
		/// <param name="writer">The writer on which the actual value is displayed</param>
		public override void WriteActualValueTo(MessageWriter writer) { }

		/// <summary>
		/// Write the failure message to the MessageWriter provided
		/// as an argument. The default implementation simply passes
		/// the constraint and the actual value to the writer, which
		/// then displays the constraint description and the value.
		/// 
		/// Constraints that need to provide additional details,
		/// such as where the error occured can override this.
		/// </summary>
		/// <param name="writer">The MessageWriter on which to display the message</param>
		public override void WriteMessageTo(MessageWriter writer)
		{
			writer.WriteLine("A type that implements comparison operators to <{0}>.", typeof(T).Name);
			_rules.Offender.WriteMessageTo(writer);
		}
	}

	/// <summary>
	/// Allows checking whether a type properly implements comparison operators against another type.
	/// </summary>
	/// <typeparam name="T">Type of objects to compare, usually at the left hand side of the operator.</typeparam>
	/// <typeparam name="U">Type of objects to compare at the right hand side of the operator.</typeparam>
	public class ImplementsComparisonConstraint<T, U> : Constraint
	{
		private readonly U _equal, _strictlyLessThan, _strictlyGreaterThan;
		ChainedConstraints _rules;


		/// <summary>
		/// Initializes a new instance of the <see cref="ImplementsComparisonConstraint{T}"/> class when type implements comparison operators against another type.
		/// </summary>
		/// <param name="strictlyLessThan">An instance of <typeparamref name="U"/> that is strictly less than the value tested.</param>
		/// <param name="strictlyGreaterThan">An instance of <typeparamref name="U"/> that is strictly greater than the value tested.</param>
		/// <param name="equal">An instance of <typeparamref name="U"/> that has the same value as the value tested.</param>
		public ImplementsComparisonConstraint(U strictlyLessThan, U strictlyGreaterThan, U equal)
		{
			_equal = equal;
			_strictlyLessThan = strictlyLessThan;
			_strictlyGreaterThan = strictlyGreaterThan;
		}

		/// <summary>
		/// Test whether the constraint is satisfied by a given value.
		/// </summary>
		/// <param name="current">The value to be tested.</param>
		/// <returns>True for success, false for failure.</returns>
		public override bool Matches(object current)
		{
			actual = current;
			_rules = new ChainedConstraints(
				() => ComparisonConstraint<T, U>.LessThanOrEqual(_equal),
				() => ComparisonConstraint<T, U>.GreaterThanOrEqual(_equal),
				() => ComparisonConstraint<T, U>.GreaterThan(_strictlyLessThan),
				() => ComparisonConstraint<T, U>.GreaterThanOrEqual(_strictlyLessThan),
				() => ComparisonConstraint<T, U>.LessThan(_strictlyGreaterThan),
				() => ComparisonConstraint<T, U>.LessThanOrEqual(_strictlyGreaterThan),
				ComparisonConstraint<T, U>.GreaterThanNull,
				() => ComparisonConstraint<T, U>.NullAlwaysLessThan(_strictlyLessThan)
				);
			return _rules.Evaluate(actual);
		}
		
		/// <summary>
		/// Write the constraint description to a MessageWriter.
		/// </summary>
		/// <param name="writer">The writer on which the description is displayed.</param>
		public override void WriteDescriptionTo(MessageWriter writer) { }

		/// <summary>
		/// Write the actual value for a failing constraint test to a
		/// MessageWriter. The default implementation simply writes
		/// the raw value of actual, leaving it to the writer to
		/// perform any formatting.
		/// </summary>
		/// <param name="writer">The writer on which the actual value is displayed</param>
		public override void WriteActualValueTo(MessageWriter writer) { }

		/// <summary>
		/// Write the failure message to the MessageWriter provided
		/// as an argument. The default implementation simply passes
		/// the constraint and the actual value to the writer, which
		/// then displays the constraint description and the value.
		/// 
		/// Constraints that need to provide additional details,
		/// such as where the error occured can override this.
		/// </summary>
		/// <param name="writer">The MessageWriter on which to display the message</param>
		public override void WriteMessageTo(MessageWriter writer)
		{
			writer.WriteLine("A type that implements comparison operators to <{0}>.", typeof(U).Name);
			_rules.Offender.WriteMessageTo(writer);
		}
	}
}