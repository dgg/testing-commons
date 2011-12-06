using System;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Allows checking whether a type properly implements <see cref="IComparable{T}"/>.
	/// </summary>
	/// <typeparam name="T">Type of objects to compare.</typeparam>
	public class ImplementsComparableConstraint<T> : Constraint
	{
		private readonly T _strictlyLessThan;
		private readonly T _strictlyGreaterThan;
		private readonly T _equal;
		ChainedConstraints _rules;

		/// <summary>
		/// Initializes a new instance of the <see cref="ImplementsComparableConstraint{T}"/> class when type implements <see cref="IComparable{T}"/> to the same type.
		/// </summary>
		/// <param name="strictlyLessThan">An instance of <typeparamref name="T"/> that is strictly less than the value tested.</param>
		/// <param name="strictlyGreaterThan">An instance of <typeparamref name="T"/> that is strictly greater than the value tested.</param>
		public ImplementsComparableConstraint(T strictlyLessThan, T strictlyGreaterThan) : this(strictlyLessThan, strictlyGreaterThan, default(T)) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImplementsComparableConstraint{T}"/> class when type implements <see cref="IComparable{T}"/> to another type.
		/// </summary>
		/// <param name="strictlyLessThan">An instance of <typeparamref name="T"/> that is strictly less than the value tested.</param>
		/// <param name="strictlyGreaterThan">An instance of <typeparamref name="T"/> that is strictly greater than the value tested.</param>
		/// <param name="equal">An instance of <typeparamref name="T"/> that has the same value as the value tested.</param>
		public ImplementsComparableConstraint(T strictlyLessThan, T strictlyGreaterThan, T equal)
		{
			_strictlyLessThan = strictlyLessThan;
			_strictlyGreaterThan = strictlyGreaterThan;
			_equal = equal;
		}

		/// <summary>
		/// Test whether the constraint is satisfied by a given value.
		/// </summary>
		/// <param name="current">The value to be tested.</param>
		/// <returns>True for success, false for failure.</returns>
		public override bool Matches(object current)
		{
			actual = current;
			T actualOrEqual = ReferenceEquals(_equal, default(T)) ? (T)actual : _equal;

			_rules = new ChainedConstraints(
				() => ComparableConstraint<T>.EqualTo(actualOrEqual),
				() => ComparableConstraint<T>.LessThanOrEqual(actualOrEqual),
				() => ComparableConstraint<T>.GreaterThanOrEqual(actualOrEqual),
				() => ComparableConstraint<T>.GreaterThan(_strictlyLessThan),
				() => ComparableConstraint<T>.GreaterThanOrEqual(_strictlyLessThan),
				() => ComparableConstraint<T>.LessThan(_strictlyGreaterThan),
				() => ComparableConstraint<T>.LessThanOrEqual(_strictlyGreaterThan),
				ComparableConstraint<T>.LessThanNull
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
			writer.WriteLine("A type that implements IComparable<{0}> contract.", typeof(T).Name);
			_rules.Offender.WriteMessageTo(writer);
		}
	}
}
