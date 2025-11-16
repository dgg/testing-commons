namespace Testing.Commons.Time;

/// <summary>
/// Allows expressive creation of <see cref="TimeSpan"/> instances.
/// </summary>
/// <example><code>3.Hours()</code>
/// <code>5.Days().Hours(4).Minutes(3)</code></example>
public static class SpanExtensions
{
	/// <summary>
	/// Adds the specified <see cref="TimeSpan"/> to the one represented by the specified number of weeks, where a week is assumed to have 7 days.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of weeks.</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Weeks(this int value, TimeSpan? ts = null) => Days(7 * value).Add(ts ?? TimeSpan.Zero);

	/// <summary>
	/// Adds the specified <see cref="TimeSpan"/> to the one represented by the specified number of weeks (usually one), where a week is assumed to have 7 days.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of weeks (usually one).</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Week(this int value, TimeSpan? ts = null) => Weeks(value, ts);

	/// <summary>
	/// Adds the specified <see cref="TimeSpan"/> to the one represented by the specified number of days.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of days.</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Days(this int value, TimeSpan? ts = null) => TimeSpan.FromDays(value).Add(ts ?? TimeSpan.Zero);

	/// <summary>
	/// Adds the specified <see cref="TimeSpan"/> to the one represented by the specified number of days (usually one).
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of days (usually one).</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Day(this int value, TimeSpan? ts = null) => Days(value, ts);

	/// <summary>
	/// Adds the specified <see cref="TimeSpan"/> to the one represented by the specified number of hours.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of hours.</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Hours(this int value, TimeSpan? ts = null) => TimeSpan.FromHours(value).Add(ts ?? TimeSpan.Zero);

	/// <summary>
	/// Adds the specified <see cref="TimeSpan"/> to the one represented by the specified number of hours (usually one).
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of hours (usually one).</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Hour(this int value, TimeSpan? ts = null) =>Hours(value, ts);

	/// <summary>
	/// Adds the specified <see cref="TimeSpan"/> to the one represented by the specified number of minutes.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of minutes.</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Minutes(this int value, TimeSpan? ts = null) => TimeSpan.FromMinutes(value).Add(ts ?? TimeSpan.Zero);

	/// <summary>
	/// Adds the specified <see cref="TimeSpan"/> to the one represented by the specified number of minutes (usually one).
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of minutes (usually one).</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Minute(this int value, TimeSpan? ts = null) => Minutes(value, ts);

	/// <summary>
	/// Adds the specified <see cref="TimeSpan"/> to the one represented by the specified number of seconds.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of seconds.</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Seconds(this int value, TimeSpan? ts = null) => TimeSpan.FromSeconds(value).Add(ts ?? TimeSpan.Zero);

	/// <summary>
	/// Adds the specified <see cref="TimeSpan"/> to the one represented by the specified number of seconds (usually one).
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of seconds (usually one).</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Second(this int value, TimeSpan? ts = null) => Seconds(value, ts);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> that represents a specified number of milliseconds.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of milliseconds.</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Milliseconds(this int value, TimeSpan? ts = null) => TimeSpan.FromMilliseconds(value).Add(ts ?? TimeSpan.Zero);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> that represents a specified number of milliseconds (usually one).
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="value">A number of milliseconds (usually one).</param>
	/// <param name="ts">The time interval to add.</param>
	/// <returns>An object that represents value plus the value of <paramref name="ts"/>.</returns>
	public static TimeSpan Millisecond(this int value, TimeSpan? ts) => TimeSpan.FromMilliseconds(value).Add(ts ?? TimeSpan.Zero);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of days.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="ts">The interval that contains the information.</param>
	/// <param name="days">A number of days.</param>
	/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of days.</returns>
	public static TimeSpan Days(this TimeSpan ts, int days) => new(days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of days (usually one).
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="ts">The interval that contains the information.</param>
	/// <param name="days">A number of days (usually one).</param>
	/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of days.</returns>
	public static TimeSpan Day(this TimeSpan ts, int days) => ts.Days(days);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of hours.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="ts">The interval that contains the information.</param>
	/// <param name="hours">A number of hours.</param>
	/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of hours.</returns>
	public static TimeSpan Hours(this TimeSpan ts, int hours) => new(ts.Days, hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of hours (usually one).
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="ts">The interval that contains the information.</param>
	/// <param name="hours">A number of hours (usually one).</param>
	/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of hours.</returns>
	public static TimeSpan Hour(this TimeSpan ts, int hours) => ts.Hours(hours);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of minutes.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="ts">The interval that contains the information.</param>
	/// <param name="minutes">A number of minutes.</param>
	/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of minutes.</returns>
	public static TimeSpan Minutes(this TimeSpan ts, int minutes) => new(ts.Days, ts.Hours, minutes, ts.Seconds, ts.Milliseconds);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of minutes (usually one).
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="ts">The interval that contains the information.</param>
	/// <param name="minutes">A number of minutes (usually one).</param>
	/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of minutes.</returns>
	public static TimeSpan Minute(this TimeSpan ts, int minutes) => ts.Minutes(minutes);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of seconds.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="ts">The interval that contains the information.</param>
	/// <param name="seconds">A number of seconds.</param>
	/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of seconds.</returns>
	public static TimeSpan Seconds(this TimeSpan ts, int seconds) => new(ts.Days, ts.Hours, ts.Minutes, seconds, ts.Milliseconds);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of seconds (usually one).
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="ts">The interval that contains the information.</param>
	/// <param name="seconds">A number of seconds (usually one).</param>
	/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of seconds.</returns>
	public static TimeSpan Second(this TimeSpan ts, int seconds) => ts.Seconds(seconds);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of milliseconds.
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="ts">The interval that contains the information.</param>
	/// <param name="milliseconds">A number of milliseconds.</param>
	/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of milliseconds.</returns>
	public static TimeSpan Milliseconds(this TimeSpan ts, int milliseconds) => new(ts.Days, ts.Hours, ts.Minutes, ts.Seconds, milliseconds);

	/// <summary>
	/// Returns a <see cref="TimeSpan"/> with the the information from the passed interval and the specified number of milliseconds (usually one).
	/// </summary>
	/// <remarks>Enables method chaining in order to expressively create instances.</remarks>
	/// <param name="ts">The interval that contains the information.</param>
	/// <param name="milliseconds">A number of milliseconds (usually one).</param>
	/// <returns>A new instance with the information of <paramref name="ts"/> and the specified number of milliseconds.</returns>
	public static TimeSpan Millisecond(this TimeSpan ts, int milliseconds) => ts.Milliseconds(milliseconds);

	/// <summary>
	/// Subtracts a specified time interval from the specified date and time, and yields a new date and time.
	/// </summary>
	/// <param name="ts">The time interval to subtract .</param>
	/// <param name="dt">The date and time object to subtract from.</param>
	/// <returns>A new instance whose value is the difference of the values of <paramref name="dt"/> and <paramref name="ts"/>.</returns>
	public static DateTimeOffset Before(this TimeSpan ts, DateTimeOffset dt) => dt - ts;

	/// <summary>
	/// Adds a specified time interval to the specified date and time, and yields a new a date and time.
	/// </summary>
	/// <param name="ts">The time interval to add.</param>
	/// <param name="dt">The object to add the time interval to.</param>
	/// <returns>A new instance whose value is the sum of the values of <paramref name="dt"/> and <paramref name="ts"/>.</returns>
	public static DateTimeOffset After(this TimeSpan ts, DateTimeOffset dt) => dt + ts;
}
