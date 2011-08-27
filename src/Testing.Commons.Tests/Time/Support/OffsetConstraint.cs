﻿using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.Tests.Time.Support
{
	public class OffsetConstraint : Constraint
	{
		private readonly Constraint _composed;

		public OffsetConstraint(int year, int month, int day, int hour, int minute, int second, int milliseconds, TimeSpan offset)
		{
			_composed = Has.Property("Year").EqualTo(year) &
				Has.Property("Month").EqualTo(month) &
				Has.Property("Day").EqualTo(day) &
				Has.Property("Hour").EqualTo(hour) &
				Has.Property("Minute").EqualTo(minute) &
				Has.Property("Second").EqualTo(second) &
				Has.Property("Millisecond").EqualTo(milliseconds) &
				Has.Property("TimeOfDay").EqualTo(new TimeSpan(0, hour, minute, second, milliseconds)) &
				Has.Property("Offset").EqualTo(offset);
		}

		public override bool Matches(object current)
		{
			actual = current;

			return _composed.Matches(current);
		}

		public override void WriteDescriptionTo(MessageWriter writer)
		{
			_composed.WriteDescriptionTo(writer);
		}
	}
}