using System;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public class ComparableImplementationConstraint<T> : Constraint
	{
		private readonly T _strictlyLessThan;
		private readonly T _strictlyGreaterThan;
		private readonly T _equal;
		ChainedConstraints _rules;

		public ComparableImplementationConstraint(T strictlyLessThan, T strictlyGreaterThan) : this(strictlyLessThan, strictlyGreaterThan, default(T)) { }

		public ComparableImplementationConstraint(T strictlyLessThan, T strictlyGreaterThan, T equal)
		{
			_strictlyLessThan = strictlyLessThan;
			_strictlyGreaterThan = strictlyGreaterThan;
			_equal = equal;
		}

		public override bool Matches(object actual)
		{
			T actualOrEqual = ReferenceEquals(_equal, default(T)) ? (T)actual : _equal;

			_rules = new ChainedConstraints(
				() => ComparableConstraint<T>.EqualTo(actualOrEqual),
				() => ComparableConstraint<T>.LessThanOrEqual(actualOrEqual),
				() => ComparableConstraint<T>.GreaterThanOrEqual(actualOrEqual),
				() => ComparableConstraint<T>.GreaterThan(_strictlyLessThan),
				() => ComparableConstraint<T>.GreaterThanOrEqual(_strictlyLessThan),
				() => ComparableConstraint<T>.LessThan(_strictlyGreaterThan),
				() => ComparableConstraint<T>.LessThanOrEqual(_strictlyGreaterThan)
				);
			return _rules.Evaluate(actual);
		}


		public override void WriteDescriptionTo(MessageWriter writer) { }

		public override void WriteActualValueTo(MessageWriter writer) { }

		public override void WriteMessageTo(MessageWriter writer)
		{
			writer.WriteLine("A type that implements IComparable<{0}> contract.", typeof(T).Name);
			_rules.Offender.WriteMessageTo(writer);
		}
	}
}
