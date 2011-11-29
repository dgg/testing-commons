using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	internal class ComparableConstraint<T> : Constraint
	{
		private readonly T _expected;
		private readonly Constraint _inner;
		private readonly string _messageConnector;

		internal ComparableConstraint(T expected, Constraint inner, string messageConnector)
		{
			_expected = expected;
			_inner = inner;
			_messageConnector = messageConnector;
		}

		public override bool Matches(object current)
		{
			actual = current;
			IComparable<T> comparable = (IComparable<T>)actual;
			return _inner.Matches(comparable.CompareTo(_expected));
		}

		public override void WriteDescriptionTo(MessageWriter writer)
		{
			_inner.WriteDescriptionTo(writer);
		}

		public override void WriteMessageTo(MessageWriter writer)
		{
			writer.WriteValue(actual);
			writer.Write(_messageConnector);
			writer.WriteValue(_expected);
			writer.WriteLine();
			base.WriteMessageTo(writer);
		}

		public override void WriteActualValueTo(MessageWriter writer)
		{
			_inner.WriteActualValueTo(writer);
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
	}
}