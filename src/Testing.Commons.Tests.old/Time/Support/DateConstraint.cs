using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.Tests.Time.Support
{
	public class DateConstraint : Constraint
	{
		private readonly Constraint _composed;

		public DateConstraint(int year, int month, int day) : this(year, month, day, 0, 0, 0, 0) { }

		public DateConstraint(int year, int month, int day, int hour, int minute, int second, int milliseconds)
		{
			_composed = Has.Property("Year").EqualTo(year) &
				Has.Property("Month").EqualTo(month) &
				Has.Property("Day").EqualTo(day) &
				Has.Property("Hour").EqualTo(hour) &
				Has.Property("Minute").EqualTo(minute) &
				Has.Property("Second").EqualTo(second) &
				Has.Property("Millisecond").EqualTo(milliseconds) &
				Has.Property("TimeOfDay").EqualTo(new TimeSpan(0, hour, minute, second, milliseconds)) &
				Has.Property("Kind").EqualTo(DateTimeKind.Unspecified);
		}

		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			return _composed.ApplyTo(actual);
		}

		public override string Description => _composed.Description;
	}
}
