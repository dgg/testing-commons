using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	internal class EquatableConstraint<T> : ContractConstraint<T>
	{
		public EquatableConstraint(T expected, Constraint inner, string messageConnector)
			: base(expected, inner, messageConnector) { }

		public override bool Matches(object current)
		{
			actual = current;
			var comparable = (IEquatable<T>)actual;
			return _inner.Matches(comparable.Equals(_expected));
		}

		public static EquatableConstraint<T> EqualTo(T expected)
		{
			return new EquatableConstraint<T>(expected, Is.True, " must be equal to ");
		}

		public static EquatableConstraint<T> NotEqualTo(T expected)
		{
			return new EquatableConstraint<T>(expected, Is.False, " must not be equal to ");
		}

		public static EquatableConstraint<T> NotEqualToNull()
		{
			Type t = typeof(T);
			return (t.IsValueType) ? new AlwaysMatching() : NotEqualTo(default(T));
		}

		/// <summary>
		/// Always matches, used when the type is a value type and no comparison to NULL need to be performed
		/// </summary>
		class AlwaysMatching : EquatableConstraint<T>
		{
			internal AlwaysMatching() : base(default(T), null, string.Empty) { }
			public override bool Matches(object current) { return true; }
		}
	}
}