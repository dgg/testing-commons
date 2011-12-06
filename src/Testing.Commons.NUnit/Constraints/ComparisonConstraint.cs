using System;
using NUnit.Framework;
using Testing.Commons.NUnit.Constraints.Support;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Checks the result of a comparison operator on the same type when the first operand is not null and provides construction methods for better legibility.
	/// </summary>
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

		public static ComparisonConstraint<T> NullAllwaysLessThan(T expected)
		{
			Type t = typeof(T);
			return (t.IsValueType) ?
				new AlwaysMatching() :
				(ComparisonConstraint<T>)new NullComparisonConstraint(expected, Operator<T>.LessThan, " must be < (less than) ");
		}

		public static ComparisonConstraint<T> LessThanOrEqual(T expected)
		{
			return new ComparisonConstraint<T>(expected, Operator<T>.LessThanOrEqual, " must be <= (less than or equal to) ");
		}

		/// <summary>
		/// Always matches, used when the type is a value type and no comparison to NULL need to be performed
		/// </summary>
		class AlwaysMatching : ComparisonConstraint<T>
		{
			internal AlwaysMatching() : base(default(T), null, string.Empty) { }
			public override bool Matches(object current) { return true; }
		}

		/// <summary>
		/// Checks a comparison operator when the first operand is NULL.
		/// </summary>
		class NullComparisonConstraint : ComparisonConstraint<T>
		{
			private new readonly Func<T, T, bool> _operation;

			internal NullComparisonConstraint(T expected, Func<T, T, bool> operation, string messageConnector)
				: base(expected, operation, messageConnector)
			{
				_operation = operation;
			}

			public override bool Matches(object current)
			{
				actual = null;
				// we assume this constraint is only used when T is a reference type but a class generic constraint cannot be used due to propagation of constraints
				T @null = default(T);
				return _inner.Matches(_operation(@null, _expected));
			}
		}
	}

	/// Checks the result of a comparison operator on another type when the first operand is not null and provides construction methods for better legibility.
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

		public static ComparisonConstraint<T, U> NullAlwaysLessThan(U expected)
		{
			Type t = typeof(U);
			return (t.IsValueType) ? 
				new AlwaysMatching() :
				(ComparisonConstraint<T, U>)new NullComparisonConstraint(expected, Operator<T, U>.LessThan, " must be < (less than) ");
		}

		public static ComparisonConstraint<T, U> LessThanOrEqual(U expected)
		{
			return new ComparisonConstraint<T, U>(expected, Operator<T, U>.LessThanOrEqual, " must be <= (less than or equal to) ");
		}

		/// <summary>
		/// Always matches, used when the type is a value type and no comparison to NULL need to be performed
		/// </summary>
		class AlwaysMatching : ComparisonConstraint<T, U>
		{
			internal AlwaysMatching() : base(default(U), null, string.Empty) { }
			public override bool Matches(object current) { return true; }
		}

		/// <summary>
		/// Checks a comparison operator when the first operand is NULL.
		/// </summary>
		class NullComparisonConstraint : ComparisonConstraint<T, U>
		{
			private new readonly Func<T, U, bool> _operation;

			internal NullComparisonConstraint(U expected, Func<T, U, bool> operation, string messageConnector)
				: base(expected, operation, messageConnector)
			{
				_operation = operation;
			}

			public override bool Matches(object current)
			{
				actual = null;
				// we assume this constraint is only used when T is a reference type but a class generic constraint cannot be used due to propagation of constraints
				T @null = default(T);
				return _inner.Matches(_operation(@null, _expected));
			}
		}
	}
}