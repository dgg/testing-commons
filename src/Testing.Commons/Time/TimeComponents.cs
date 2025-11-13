using System.Xml;

namespace Testing.Commons.Time.Newer;

public readonly record struct TimeComponents(
	ushort Year = 1, byte Month = 1, byte Day = 1,
	byte Hour = 0, byte Minute = 0, byte Second = 0, ushort Millisecond = 0,
	TimeSpan Offset = default)
{
	public static implicit operator DateOnly(TimeComponents self) => new(self.Year, self.Month, self.Day);
	private DateOnly asDateOnly() => this;

	public static implicit operator TimeOnly(TimeComponents self) => new(self.Hour, self.Minute, self.Second, self.Millisecond);

	public static implicit operator DateTime(TimeComponents self) => self.asDateOnly().ToDateTime(self, DateTimeKind.Utc);

	public static implicit operator DateTimeOffset(TimeComponents self) => new DateTimeOffset(self, self.Offset);

	public TimeComponents At(byte hour = 0, byte minute = 0, byte second = 0, ushort millisecond = 0) => this with
	{
		Hour = hour,
		Minute = minute,
		Second = second,
		Millisecond = millisecond
	};

	public TimeComponents At(TimeOnly timeOfDay) => At(
		Convert.ToByte(timeOfDay.Hour),
		Convert.ToByte(timeOfDay.Minute),
		Convert.ToByte(timeOfDay.Second),
		Convert.ToUInt16(timeOfDay.Millisecond)
	);

	public TimeComponents At(TimeSpan timeOfDay) => At(new TimeOnly(timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds, timeOfDay.Milliseconds));

	public sealed class TimeOfDay
	{
		private TimeOfDay() { }

		/// <summary>
		/// Midday, twolve o'clock.
		/// </summary>
		public TimeOnly Noon { get; } = new(0, 12);
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
			static Nested() { }

			internal static readonly TimeOfDay instance = new();
		}
	}

	public TimeComponents At(Func<TimeOfDay, TimeOnly> timeOfDay) => At(timeOfDay(TimeOfDay.Instance));

	public TimeComponents In(TimeSpan offset) => this with { Offset = offset };

	public TimeComponents InUtc() => this with { Offset = TimeSpan.Zero };
}

public static class TimeComponentsExtensions
{
	public static TimeComponents January(this int day, ushort year) => new(Year: year, Month: 1, Day: Convert.ToByte(day));

	public static TimeComponents February(this int day, ushort year) => new(Year: year, Month: 2, Day: Convert.ToByte(day));

	public static TimeComponents March(this int day, ushort year) => new(Year: year, Month: 3, Day: Convert.ToByte(day));

	public static TimeComponents April(this int day, ushort year) => new(Year: year, Month: 4, Day: Convert.ToByte(day));

	public static TimeComponents May(this int day, ushort year) => new(Year: year, Month: 5, Day: Convert.ToByte(day));

	public static TimeComponents June(this int day, ushort year) => new(Year: year, Month: 6, Day: Convert.ToByte(day));

	public static TimeComponents July(this int day, ushort year) => new(Year: year, Month: 7, Day: Convert.ToByte(day));

	public static TimeComponents August(this int day, ushort year) => new(Year: year, Month: 8, Day: Convert.ToByte(day));

	public static TimeComponents September(this int day, ushort year) => new(Year: year, Month: 9, Day: Convert.ToByte(day));

	public static TimeComponents October(this int day, ushort year) => new(Year: year, Month: 10, Day: Convert.ToByte(day));

	public static TimeComponents November(this int day, ushort year) => new(Year: year, Month: 11, Day: Convert.ToByte(day));

	public static TimeComponents December(this int day, ushort year) => new(Year: year, Month: 12, Day: Convert.ToByte(day));

}