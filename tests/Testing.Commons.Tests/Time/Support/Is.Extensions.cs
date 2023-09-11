using NUnit.Framework.Constraints;

namespace Testing.Commons.Tests.Time.Support
{
	internal static class ConstraintsExtensions
	{
		public static DateConstraint DateWith(this ConstraintExpression expression, int year, int month, int day)
		{
			var constraint = new DateConstraint(year, month, day, 0, 0, 0, 0);
			expression.Append(constraint);
			return constraint;
		}

		public static DateConstraint TimeWith(this ConstraintExpression expression, int year = 1, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0)
		{
			var constraint = new DateConstraint(year, month, day, hour, minute, second, millisecond);
			expression.Append(constraint);
			return constraint;
		}

		public static OffsetConstraint OffsetWith(this ConstraintExpression expression, int year = 1, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0, TimeSpan offset = new TimeSpan())
		{
			var constraint = new OffsetConstraint(year, month, day, hour, minute, second, millisecond, offset);
			expression.Append(constraint);
			return constraint;
		}

		public static SpanConstraint SpanWith(this ConstraintExpression expression, int days = 0, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0)
		{
			var constraint = new SpanConstraint(days, hours, minutes, seconds, milliseconds);
			expression.Append(constraint);
			return constraint;
		}
	}

	internal class Is : NUnit.Framework.Is
	{
		public static DateConstraint DateWith(int year, int month, int day)
		{
			var constraint = new DateConstraint(year, month, day, 0, 0, 0, 0);
			return constraint;
		}

		public static DateConstraint TimeWith(int year = 1, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0)
		{
			var constraint = new DateConstraint(year, month, day, hour, minute, second, millisecond);
			return constraint;
		}

		public static OffsetConstraint OffsetWith(int year = 1, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0, TimeSpan offset = new TimeSpan())
		{
			var constraint = new OffsetConstraint(year, month, day, hour, minute, second, millisecond, offset);
			return constraint;
		}

		public static SpanConstraint SpanWith(int days = 0, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0)
		{
			var constraint = new SpanConstraint(days, hours, minutes, seconds, milliseconds);
			return constraint;
		}
	}
}
