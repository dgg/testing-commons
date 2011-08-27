using System;

namespace Testing.Commons.Time
{
	/// <summary>
	/// Allows expressive creation of <see cref="TimeSpan"/> instances.
	/// </summary>
	/// <example><code>3.Hours()</code>
	/// <code>5.Days().Hours(4).Minutes(3)</code></example>
	public static class SpanExtensions
	{
		/// <summary>
		/// Returns a <see cref="TimeSpan"/> that represents a specified number of weeks, where a week is assumed to have 7 days.
		/// </summary>
		/// <param name="value">A number of weeks.</param>
		/// <returns>An object that represents value.</returns>
		public static TimeSpan Weeks(this int value)
		{
			return TimeSpan.FromDays(7 * value);
		}

		/// <summary>
		/// Returns a <see cref="TimeSpan"/> that represents a specified number of days.
		/// </summary>
		/// <param name="value">A number of days.</param>
		/// <returns>An object that represents value.</returns>
		public static TimeSpan Days(this int value)
		{
			return TimeSpan.FromDays(value);
		}

		/// <summary>
		/// Returns a <see cref="TimeSpan"/> that represents a specified number of hours.
		/// </summary>
		/// <param name="value">A number of hours.</param>
		/// <returns>An object that represents value.</returns>
		public static TimeSpan Hours(this int value)
		{
			return TimeSpan.FromHours(value);
		}

		/// <summary>
		/// Returns a <see cref="TimeSpan"/> that represents a specified number of minutes.
		/// </summary>
		/// <param name="value">A number of minutes.</param>
		/// <returns>An object that represents value.</returns>
		public static TimeSpan Minutes(this int value)
		{
			return TimeSpan.FromMinutes(value);
		}

		/// <summary>
		/// Returns a <see cref="TimeSpan"/> that represents a specified number of hours.
		/// </summary>
		/// <param name="value">A number of seconds.</param>
		/// <returns>An object that represents value.</returns>
		public static TimeSpan Seconds(this int value)
		{
			return TimeSpan.FromSeconds(value);
		}

		/// <summary>
		/// Returns a <see cref="TimeSpan"/> that represents a specified number of milliseconds.
		/// </summary>
		/// <param name="value">A number of value.</param>
		/// <returns>An object that represents value.</returns>
		public static TimeSpan Milliseconds(this int value)
		{
			return TimeSpan.FromMilliseconds(value);
		}

		/// <summary>
		/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of days.
		/// </summary>
		/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
		/// <param name="ts">The interval that contains the information.</param>
		/// <param name="days">A number of days.</param>
		/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of days.</returns>
		public static TimeSpan Days(this TimeSpan ts, int days)
		{
			return new TimeSpan(days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
		}

		/// <summary>
		/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of hours.
		/// </summary>
		/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
		/// <param name="ts">The interval that contains the information.</param>
		/// <param name="hours">A number of hours.</param>
		/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of hours.</returns>
		public static TimeSpan Hours(this TimeSpan ts, int hours)
		{
			return new TimeSpan(ts.Days, hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
		}

		/// <summary>
		/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of minutes.
		/// </summary>
		/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
		/// <param name="ts">The interval that contains the information.</param>
		/// <param name="minutes">A number of minutes.</param>
		/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of minutes.</returns>
		public static TimeSpan Minutes(this TimeSpan ts, int minutes)
		{
			return new TimeSpan(ts.Days, ts.Hours, minutes, ts.Seconds, ts.Milliseconds);
		}

		/// <summary>
		/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of seconds.
		/// </summary>
		/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
		/// <param name="ts">The interval that contains the information.</param>
		/// <param name="seconds">A number of seconds.</param>
		/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of seconds.</returns>
		public static TimeSpan Seconds(this TimeSpan ts, int seconds)
		{
			return new TimeSpan(ts.Days, ts.Hours, ts.Minutes, seconds, ts.Milliseconds);
		}

		/// <summary>
		/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of milliseconds.
		/// </summary>
		/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
		/// <param name="ts">The interval that contains the information.</param>
		/// <param name="milliseconds">A number of milliseconds.</param>
		/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of milliseconds.</returns>
		public static TimeSpan Milliseconds(this TimeSpan ts, int milliseconds)
		{
			return new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds, milliseconds);
		}
	}
}
