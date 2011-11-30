using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public class ImplementsComparisonConstraint<T> : Constraint
	{
		private readonly T _strictlyLessThan;
		private readonly T _strictlyGreaterThan;
		ChainedConstraints _rules;

		public ImplementsComparisonConstraint(T strictlyLessThan, T strictlyGreaterThan)
		{
			_strictlyLessThan = strictlyLessThan;
			_strictlyGreaterThan = strictlyGreaterThan;
		}

		public override bool Matches(object actual)
		{
			T equalOrActual = (T)actual;

			_rules = new ChainedConstraints(
				() => ComparisonConstraint<T>.LessThanOrEqual(equalOrActual),
				() => ComparisonConstraint<T>.GreaterThanOrEqual(equalOrActual),
				() => ComparisonConstraint<T>.GreaterThan(_strictlyLessThan),
				() => ComparisonConstraint<T>.GreaterThanOrEqual(_strictlyLessThan),
				() => ComparisonConstraint<T>.LessThan(_strictlyGreaterThan),
				() => ComparisonConstraint<T>.LessThanOrEqual(_strictlyGreaterThan),
				ComparisonConstraint<T>.LessThanNull,
				ComparisonConstraint<T>.GreaterThanNull
				);
			return _rules.Evaluate(actual);
		}


		public override void WriteDescriptionTo(MessageWriter writer) { }

		public override void WriteActualValueTo(MessageWriter writer) { }

		public override void WriteMessageTo(MessageWriter writer)
		{
			writer.WriteLine("A type that implements comparison operators to <{0}>.", typeof(T).Name);
			_rules.Offender.WriteMessageTo(writer);
		}
	}

	public class ImplementsComparisonConstraint<T, U> : Constraint
	{
		private readonly U _equal, _strictlyLessThan, _strictlyGreaterThan;
		ChainedConstraints _rules;

		public ImplementsComparisonConstraint(U strictlyLessThan, U strictlyGreaterThan, U equal)
		{
			_equal = equal;
			_strictlyLessThan = strictlyLessThan;
			_strictlyGreaterThan = strictlyGreaterThan;
		}

		public override bool Matches(object actual)
		{
			_rules = new ChainedConstraints(
				() => ComparisonConstraint<T, U>.LessThanOrEqual(_equal),
				() => ComparisonConstraint<T, U>.GreaterThanOrEqual(_equal),
				() => ComparisonConstraint<T, U>.GreaterThan(_strictlyLessThan),
				() => ComparisonConstraint<T, U>.GreaterThanOrEqual(_strictlyLessThan),
				() => ComparisonConstraint<T, U>.LessThan(_strictlyGreaterThan),
				() => ComparisonConstraint<T, U>.LessThanOrEqual(_strictlyGreaterThan),
				ComparisonConstraint<T, U>.LessThanNull,
				ComparisonConstraint<T, U>.GreaterThanNull
				);
			return _rules.Evaluate(actual);
		}


		public override void WriteDescriptionTo(MessageWriter writer) { }

		public override void WriteActualValueTo(MessageWriter writer) { }

		public override void WriteMessageTo(MessageWriter writer)
		{
			writer.WriteLine("A type that implements comparison operators to <{0}>.", typeof(U).Name);
			_rules.Offender.WriteMessageTo(writer);
		}
	}
}