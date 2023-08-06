using System;

namespace Testing.Commons.Tests.Time.Support
{
	internal static class MustExtensions
	{
		public static DateConstraint DateWith(this Must.BeEntryPoint entry, int year, int month, int day)
		{
			return new DateConstraint(year, month, day, 0, 0, 0, 0);
		}

		public static DateConstraint TimeWith(this Must.BeEntryPoint entry, int year = 1, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0)
		{
			return new DateConstraint(year, month, day, hour, minute, second, millisecond);
		}

		public static OffsetConstraint OffsetWith(this Must.BeEntryPoint entry, int year = 1, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0, TimeSpan offset = new TimeSpan())
		{
			return new OffsetConstraint(year, month, day, hour, minute, second, millisecond, offset);
		}

		public static SpanConstraint SpanWith(this Must.BeEntryPoint entry, int days = 0, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0)
		{
			return new SpanConstraint(days, hours, minutes, seconds, milliseconds);
		}
	}
}
