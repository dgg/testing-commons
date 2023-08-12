using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value occurs after the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <returns>Instance built.</returns>
		public static Constraint After(this Must.BeEntryPoint entry, DateTime date)
		{
			return Is.GreaterThan(date);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not occur after the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <returns>Instance built.</returns>
		public static Constraint After(this Must.NotBeEntryPoint entry, DateTime date)
		{
			return Is.Not.GreaterThan(date);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value occurs before the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Before(this Must.BeEntryPoint entry, DateTime date)
		{
			return Is.LessThan(date);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not occur before the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Before(this Must.NotBeEntryPoint entry, DateTime date)
		{
			return Is.Not.LessThan(date);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value occurs either on, or after the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <returns>Instance built.</returns>
		public static Constraint OnOrAfter(this Must.BeEntryPoint entry, DateTime date)
		{
			return Is.GreaterThanOrEqualTo(date);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not occur either on, or after the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <returns>Instance built.</returns>
		public static Constraint OnOrAfter(this Must.NotBeEntryPoint entry, DateTime date)
		{
			return Is.Not.GreaterThanOrEqualTo(date);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value occurs either on, or before the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <returns>Instance built.</returns>
		public static Constraint OnOrBefore(this Must.BeEntryPoint entry, DateTime date)
		{
			return Is.LessThanOrEqualTo(date);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not occur either on, or before the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <returns>Instance built.</returns>
		public static Constraint OnOrBefore(this Must.NotBeEntryPoint entry, DateTime date)
		{
			return Is.Not.LessThanOrEqualTo(date);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value occurs within the specified number of milliseconds (20 by default) from
		/// the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <param name="ms"></param>
		/// <returns>Instance built.</returns>
		public static Constraint CloseTo(this Must.BeEntryPoint entry, DateTime date, uint ms = 20)
		{
			return CloseTo(entry, date, TimeSpan.FromMilliseconds(ms));
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value occurs within the specified time span (20 ms by default) from
		/// the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <param name="within"></param>
		/// <returns>Instance built.</returns>
		public static Constraint CloseTo(this Must.BeEntryPoint entry, DateTime date, TimeSpan within)
		{
			return Is.EqualTo(date).Within(within);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not occur within the specified number of milliseconds (20ms by default) from
		/// the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <param name="ms"></param>
		/// <returns>Instance built.</returns>
		public static Constraint CloseTo(this Must.NotBeEntryPoint entry, DateTime date, uint ms = 20)
		{
			return CloseTo(entry, date, TimeSpan.FromMilliseconds(ms));
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not occur within the specified time span (20 ms by default) from
		/// the specified <paramref name="date"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="date">The expected date to compare the actual value with.</param>
		/// <param name="within"></param>
		/// <returns>Instance built.</returns>
		public static Constraint CloseTo(this Must.NotBeEntryPoint entry, DateTime date, TimeSpan within)
		{
			return Is.Not.EqualTo(date).Within(within);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value has the provided <paramref name="year"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="year">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Year(this Must.HaveEntryPoint entry, int year)
		{
			return Has.Property("Year").EqualTo(year);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not have the provided <paramref name="year"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="year">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Year(this Must.NotHaveEntryPoint entry, int year)
		{
			return Has.Property("Year").Not.EqualTo(year);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value has the provided <paramref name="month"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="month">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Month(this Must.HaveEntryPoint entry, uint month)
		{
			return Has.Property("Month").EqualTo(month);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not have the provided <paramref name="month"/>. 
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="month">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Month(this Must.NotHaveEntryPoint entry, uint month)
		{
			return Has.Property("Month").Not.EqualTo(month);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value has the provided <paramref name="day"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="day">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Day(this Must.HaveEntryPoint entry, uint day)
		{
			return Has.Property("Day").EqualTo(day);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not have the provided <paramref name="day"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="day">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Day(this Must.NotHaveEntryPoint entry, uint day)
		{
			return Has.Property("Day").Not.EqualTo(day);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value has the provided <paramref name="hour"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="hour">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Hour(this Must.HaveEntryPoint entry, uint hour)
		{
			return Has.Property("Hour").EqualTo(hour);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not have the provided <paramref name="hour"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="hour">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Hour(this Must.NotHaveEntryPoint entry, uint hour)
		{
			return Has.Property("Hour").Not.EqualTo(hour);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value has the provided <paramref name="minute"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="minute">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Minute(this Must.HaveEntryPoint entry, uint minute)
		{
			return Has.Property("Minute").EqualTo(minute);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not have the provided <paramref name="minute"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="minute">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Minute(this Must.NotHaveEntryPoint entry, uint minute)
		{
			return Has.Property("Minute").Not.EqualTo(minute);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value has the provided <paramref name="second"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="second">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Second(this Must.HaveEntryPoint entry, uint second)
		{
			return Has.Property("Second").EqualTo(second);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not have the provided <paramref name="second"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="second">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Second(this Must.NotHaveEntryPoint entry, uint second)
		{
			return Has.Property("Second").Not.EqualTo(second);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value has the provided <paramref name="millisecond"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="millisecond">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Millisecond(this Must.HaveEntryPoint entry, uint millisecond)
		{
			return Has.Property("Millisecond").EqualTo(millisecond);
		}

		/// <summary>
		/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
		/// value does not have the provided <paramref name="millisecond"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="millisecond">Expected property of the current date.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Millisecond(this Must.NotHaveEntryPoint entry, uint millisecond)
		{
			return Has.Property("Millisecond").Not.EqualTo(millisecond);
		}
	}
}