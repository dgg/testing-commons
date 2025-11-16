namespace Testing.Commons.Time;

/// <summary>
/// Contains the components that represent several time-related types.
/// </summary>
/// <param name="Year">The year component</param>
/// <param name="Month">The month component</param>
/// <param name="Day">The day component</param>
/// <param name="Hour">The hour component</param>
/// <param name="Minute">The minute component</param>
/// <param name="Second">The second component</param>
/// <param name="Millisecond">The millisecond component</param>
/// <param name="Offset"> The time's offset from Coordinated Universal Time (UTC)</param>
public readonly record struct TimeComponents(
	ushort Year = 1,
	byte Month = 1,
	byte Day = 1,
	byte Hour = 0,
	byte Minute = 0,
	byte Second = 0,
	ushort Millisecond = 0,
	TimeSpan Offset = default)
{
	/// <summary>
	/// Converts the <see cref="TimeComponents"/> to a <see cref="DateOnly"/>.
	/// </summary>
	/// <param name="self">The time components to convert.</param>
	/// <returns>A <see cref="DateOnly"/> instance.</returns>
	public static implicit operator DateOnly(TimeComponents self) => new(self.Year, self.Month, self.Day);

	/// <summary>
	/// Converts the <see cref="TimeComponents"/> to a <see cref="TimeOnly"/>.
	/// </summary>
	/// <param name="self">The time components to convert.</param>
	/// <returns>A <see cref="TimeOnly"/> instance.</returns>
	public static implicit operator TimeOnly(TimeComponents self) =>
		new(self.Hour, self.Minute, self.Second, self.Millisecond);

	/// <summary>
	/// Converts the <see cref="TimeComponents"/> to a <see cref="DateTime"/>.
	/// </summary>
	/// <param name="self">The time components to convert.</param>
	/// <returns>A <see cref="DateTime"/> instance.</returns>
	/// <remarks>The <see cref="DateTime.Kind"/> property is initialized to <see cref="DateTimeKind.Utc"/> when
	/// <see cref="Offset"/> is 0, otherwise <see cref="DateTimeKind.Local"/>.</remarks>
	public static implicit operator DateTime(TimeComponents self) => new(
		self.Year, self.Month, self.Day,
		self.Hour, self.Minute, self.Second, self.Millisecond,
		self.Offset.Equals(TimeSpan.Zero) ? DateTimeKind.Utc : DateTimeKind.Local);

	/// <summary>
	/// Converts the <see cref="TimeComponents"/> to a <see cref="DateTimeOffset"/>.
	/// </summary>
	/// <param name="self">The time components to convert.</param>
	/// <returns>A <see cref="DateTimeOffset"/> instance.</returns>
	public static implicit operator DateTimeOffset(TimeComponents self) => new(
		self.Year, self.Month, self.Day,
		self.Hour, self.Minute, self.Second, self.Millisecond,
		self.Offset);

	/// <summary>
	/// Sets the specified components into a new instance of <see cref="TimeComponents"/>.
	/// </summary>
	/// <remarks>Allows expressive creation of date with time instances:</remarks>
	/// <example><code>11.March(1977).At(15, 35)</code></example>
	/// <param name="hour">The hours (0 through 23).</param>
	/// <param name="minute">The minutes (0 through 59).</param>
	/// <param name="second">The seconds (0 through 59).</param>
	/// <param name="millisecond">The milliseconds (0 through 999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public TimeComponents At(byte hour = 0, byte minute = 0, byte second = 0, ushort millisecond = 0) => this with
	{
		Hour = hour,
		Minute = minute,
		Second = second,
		Millisecond = millisecond
	};

	/// <summary>
	/// Sets the specified components (hour, minute, second, millisecond) into a new instance of <see cref="TimeComponents"/>.
	/// </summary>
	/// <remarks>Allows expressive creation of date with time instances:</remarks>
	/// <example><code>11.March(1977).At(new TimeOnly(15, 35))</code></example>
	/// <param name="timeOfDay">The interval that contains the hours, minutes, seconds and milliseconds.</param>
	/// <returns>A new instance with the information provided.</returns>
	public TimeComponents At(TimeOnly timeOfDay) => At(
		Convert.ToByte(timeOfDay.Hour),
		Convert.ToByte(timeOfDay.Minute),
		Convert.ToByte(timeOfDay.Second),
		Convert.ToUInt16(timeOfDay.Millisecond)
	);

	/// <summary>
	/// Sets the specified components (hour, minute, second, millisecond) into a new instance of <see cref="TimeComponents"/>.
	/// </summary>
	/// <remarks>Uses <see cref="TimeOnly"/> behind the scenes to take advantage of its validation.
	/// <para>Allows expressive creation of date with time instances:
	/// </para>
	/// </remarks>
	/// <example><code>11.March(1977).At(TimeSpan.FromHours(15).Add(TimeSpan.FromMinutes(35))</code></example>
	/// <param name="timeOfDay">The interval that contains the hours, minutes, seconds and milliseconds.</param>
	/// <returns>A new instance with the information provided.</returns>
	public TimeComponents At(TimeSpan timeOfDay) =>
		At(new TimeOnly(timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds, timeOfDay.Milliseconds));

	/// <summary>
	/// Sets the specified components (hour, minute, second, millisecond) into a new instance of <see cref="TimeComponents"/>.
	/// </summary>
	/// <remarks>Uses <see cref="TimeOnly"/> behind the scenes to take advantage of its validation.</remarks>
	/// <param name="timeOfDay">The interval that contains the hours, minutes, seconds and milliseconds.</param>
	/// <returns>A new instance with the information provided.</returns>
	public TimeComponents At(TimeComponents timeOfDay) =>
		At(new TimeOnly(timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond));

	/// <summary>
	/// Represents named times of the day.
	/// </summary>
	public sealed class TimeOfDay
	{
		private TimeOfDay()
		{
		}

		/// <summary>
		/// Midday, twolve o'clock.
		/// </summary>
		public TimeOnly Noon { get; } = new(12, 0);

		/// <summary>
		/// Middle of the night, twelve o'clock at night.
		/// </summary>
		public TimeOnly MidNight { get; } = TimeOnly.MinValue;

		/// <summary>
		/// The last time of a given day to the millisecond
		/// </summary>
		public TimeOnly EndOfDay { get; } = TimeOnly.MaxValue;

		/// <summary>
		/// Middle of the night, twelve o'clock at night.
		/// </summary>
		public TimeOnly BeginningOfDay { get; } = TimeOnly.MinValue;

		internal static TimeOfDay Instance => Nested.instance;

		private class Nested
		{
			// Explicit static constructor to tell C# compiler
			// not to mark type as beforefieldinit
			static Nested()
			{
			}

			internal static readonly TimeOfDay instance = new();
		}
	}

	/// <summary>
	/// Sets the specified components (hour, minute, second, millisecond) in the named time of the day into a new instance of <see cref="TimeComponents"/>.
	/// </summary>
	/// <remarks>Allows expressive creation of date instances:</remarks>
	/// <example><code>11.March(1977).At(t => t.Noon)</code></example>
	/// <param name="timeOfDay">Represents a named time of the day.</param>
	/// <returns>A new instance with the information provided.</returns>
	public TimeComponents At(Func<TimeOfDay, TimeOnly> timeOfDay) => At(timeOfDay(TimeOfDay.Instance));

	/// <summary>
	/// Sets the <see cref="Offset"/> component used when converting to <see cref="DateTimeOffset"/> and
	/// to specify <see cref="DateTimeKind"/> when converting to <see cref="DateTime"/>.
	/// </summary>
	/// <remarks>Allows expressive creation of date instances:</remarks>
	/// <example><code>11.March(1977).In(TimeSpan.FromHours(1))</code></example>
	/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
	/// <returns>A new instance with the information provided.</returns>
	public TimeComponents In(TimeSpan offset) => this with { Offset = offset };

	/// <summary>
	/// Sets the <see cref="Offset"/> component to <see cref="TimeSpan.Zero"/>
	/// </summary>
	/// <remarks>Allows expressive creation of date instances:</remarks>
	/// <example><code>11.March(1977).InUtc()</code></example>
	/// <seealso cref="In"/>
	/// <returns>A new instance with Coordinated Universal Time (UTC) offset.</returns>
	public TimeComponents InUtc() => this with { Offset = TimeSpan.Zero };
}
