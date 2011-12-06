using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Checks the result of CompareTo() on the same type and provides construction methods for better legibility.
	/// </summary>
	internal class ComparableConstraint<T> : ContractConstraint<T>
	{
		internal ComparableConstraint(T expected, Constraint inner, string messageConnector)
			: base(expected, inner, messageConnector) { }

		public override bool Matches(object current)
		{
			actual = current;
			IComparable<T> comparable = (IComparable<T>)actual;
			return _inner.Matches(comparable.CompareTo(_expected));
		}

		public static ComparableConstraint<T> EqualTo(T expected)
		{
			return new ComparableConstraint<T>(expected, Is.EqualTo(0), " must be equal to ");
		}

		public static ComparableConstraint<T> GreaterThan(T expected)
		{
			return new ComparableConstraint<T>(expected, Is.GreaterThan(0), " must be greater than ");
		}

		public static ComparableConstraint<T> GreaterThanOrEqual(T expected)
		{
			return new ComparableConstraint<T>(expected, Is.GreaterThanOrEqualTo(0), " must be greater than or equal to ");
		}

		public static ComparableConstraint<T> LessThan(T expected)
		{
			return new ComparableConstraint<T>(expected, Is.LessThan(0), " must be less than ");
		}

		public static ComparableConstraint<T> LessThanOrEqual(T expected)
		{
			return new ComparableConstraint<T>(expected, Is.LessThanOrEqualTo(0), " must be less than or equal to ");
		}

		public static ComparableConstraint<T> LessThanNull()
		{
			Type t = typeof(T);
			return (t.IsValueType) ? new AlwaysMatching() : LessThan(default(T));
		}

		/// <summary>
		/// Always matches, used when the type is a value type and no comparison to NULL need to be performed
		/// </summary>
		class AlwaysMatching : ComparableConstraint<T>
		{
			internal AlwaysMatching() : base(default(T), null, string.Empty) { }
			public override bool Matches(object current) { return true; }
		}
	}
}