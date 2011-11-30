using System;
using NUnit.Framework;
using Testing.Commons.NUnit.Constraints.Support;

namespace Testing.Commons.NUnit.Constraints
{
	internal class ComparisonConstraint<T> : ContractConstraint<T>
	{
		private readonly Func<T, T, bool> _operation;

		internal ComparisonConstraint(T expected, Func<T, T, bool> operation, string messageConnector)
			: base(expected, Is.True, messageConnector)
		{
			_operation = operation;
		}

		public override bool Matches(object current)
		{
			actual = current;
			T comparable = (T)actual;
			return _inner.Matches(_operation(comparable, _expected));
		}

		public static ComparisonConstraint<T> GreaterThan(T expected)
		{
			return new ComparisonConstraint<T>(expected, Operator<T>.GreaterThan, " must be > (greater than) ");
		}

		public static ComparisonConstraint<T> GreaterThanNull()
		{
			Type t = typeof(T);
			return (t.IsValueType) ? new AlwaysMatching() : GreaterThan(default(T));
		}

		public static ComparisonConstraint<T> GreaterThanOrEqual(T expected)
		{
			return new ComparisonConstraint<T>(expected, Operator<T>.GreaterThanOrEqual, " must be >= (greater than or equal to) ");
		}

		public static ComparisonConstraint<T> LessThan(T expected)
		{
			return new ComparisonConstraint<T>(expected, Operator<T>.LessThan, " must be < (less than) ");
		}

		public static ComparisonConstraint<T> LessThanNull()
		{
			Type t = typeof(T);
			return (t.IsValueType) ? new AlwaysMatching() : LessThan(default(T));
		}

		public static ComparisonConstraint<T> LessThanOrEqual(T expected)
		{
			return new ComparisonConstraint<T>(expected, Operator<T>.LessThanOrEqual, " must be <= (less than or equal to) ");
		}

		class AlwaysMatching : ComparisonConstraint<T>
		{
			internal AlwaysMatching() : base(default(T), null, string.Empty) { }
			public override bool Matches(object current) { return true; }
		}
	}

	internal class ComparisonConstraint<T, U> : ContractConstraint<U>
	{
		private readonly Func<T, U, bool> _operation;

		internal ComparisonConstraint(U expected, Func<T, U, bool> operation, string messageConnector)
			: base(expected, Is.True, messageConnector)
		{
			_operation = operation;
		}

		public override bool Matches(object current)
		{
			actual = current;
			T comparable = (T)actual;
			return _inner.Matches(_operation(comparable, _expected));
		}

		public static ComparisonConstraint<T, U> GreaterThan(U expected)
		{
			return new ComparisonConstraint<T, U>(expected, Operator<T, U>.GreaterThan, " must be > (greater than) ");
		}

		public static ComparisonConstraint<T, U> GreaterThanNull()
		{
			Type t = typeof(U);
			return (t.IsValueType) ? new AlwaysMatching() : GreaterThan(default(U));
		}

		public static ComparisonConstraint<T, U> GreaterThanOrEqual(U expected)
		{
			return new ComparisonConstraint<T, U>(expected, Operator<T, U>.GreaterThanOrEqual, " must be >= (greater than or equal to) ");
		}

		public static ComparisonConstraint<T, U> LessThan(U expected)
		{
			return new ComparisonConstraint<T, U>(expected, Operator<T, U>.LessThan, " must be < (less than) ");
		}

		public static ComparisonConstraint<T, U> LessThanNull()
		{
			Type t = typeof(U);
			return (t.IsValueType) ? new AlwaysMatching() : LessThan(default(U));
		}

		public static ComparisonConstraint<T, U> LessThanOrEqual(U expected)
		{
			return new ComparisonConstraint<T, U>(expected, Operator<T, U>.LessThanOrEqual, " must be <= (less than or equal to) ");
		}

		class AlwaysMatching : ComparisonConstraint<T, U>
		{
			internal AlwaysMatching() : base(default(U), null, string.Empty) { }
			public override bool Matches(object current) { return true; }
		}
	}
}