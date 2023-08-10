using System.Diagnostics.CodeAnalysis;

namespace Testing.Commons.Time;

/// <summary>
/// Allows expressive creation of <see cref="DateTime"/> instances that represent a date and a time.
/// </summary>
/// <example><code>11.March(1977).At(15, 35)</code></example>
public static class TimeExtensions
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DateTime"/> structure to year, month, day specified by the date and the specified hour, minute, second, and millisecond.
	/// </summary>
	/// <param name="dt">The date that contains the years, months and days.</param>
	/// <param name="hour">The hours (0 through 23).</param>
	/// <param name="minute">The minutes (0 through 59).</param>
	/// <param name="second">The seconds (0 through 59).</param>
	/// <param name="miliSecond">The milliseconds (0 through 999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static DateTime At(this DateTime dt, int hour = 0, int minute = 0, int second = 0, int miliSecond = 0)
	{
		return new DateTime(dt.Year, dt.Month, dt.Day, hour, minute, second, miliSecond);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="DateTime"/> structure to year, month, day specified by the date
	/// and the hour, minute, second, and millisecond specified in the interval.
	/// </summary>
	/// <remarks>The days of the interval are ignored.</remarks>
	/// <param name="dt">The date that contains the years, months and days.</param>
	/// <param name="span">The interval that contains the hours, minutes, seconds and milliseconds.</param>
	/// <returns>A new instance with the information provided.</returns>
	public static DateTime At(this DateTime dt, TimeSpan span)
	{
		return At(dt, span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
	}

	/// <summary>
	/// Represents named times of the day.
	/// </summary>
	public class TimeOfDay
	{
		private TimeOfDay() { }

		/// <summary>
		/// Midday, twolve o'clock.
		/// </summary>
		public TimeSpan Noon { get; } = new(0, 12, 0, 0, 0);
		/// <summary>
		/// Middle of the night, twelve o'clock at night.
		/// </summary>
		public TimeSpan MidNight { get; } = TimeSpan.Zero;
		/// <summary>
		/// The last time of a given day to the millisecond
		/// </summary>
		public TimeSpan EndOfDay { get; } = new(0, 23, 59, 59, 999);
		/// <summary>
		/// Middle of the night, twelve o'clock at night.
		/// </summary>
		public TimeSpan BeginningOfDay { get; } = TimeSpan.Zero;

		internal static TimeOfDay Instance = new();
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="DateTime"/> structure to year, month, day specified by the date
	/// and the hour, minute, second, and millisecond specified in the named interval.
	/// </summary>
	/// <param name="dt">The date that contains the years, months and days.</param>
	/// <param name="timeOfDay">Represents a named time of the day.</param>
	/// <returns>A new instance with the information provided.</returns>
	public static DateTime At(this DateTime dt, [NotNull] Func<TimeOfDay, TimeSpan> timeOfDay)
	{
		ArgumentNullException.ThrowIfNull(timeOfDay, nameof(timeOfDay));

		return At(dt, timeOfDay(TimeOfDay.Instance));
	}
}
