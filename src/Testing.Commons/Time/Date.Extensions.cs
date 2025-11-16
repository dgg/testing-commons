namespace Testing.Commons.Time;

/// <summary>
/// Allows expressive creation of <see cref="TimeComponents"/> instances that represent different time-related types.
/// </summary>
/// <example><code>11.March(1977)</code></example>
public static class DateExtensions
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in January.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00). .</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents January(this int day, ushort year) =>
		new(Year: year, Month: 1, Day: Convert.ToByte(day));

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in February.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00).</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents February(this int day, ushort year) =>
		new(Year: year, Month: 2, Day: Convert.ToByte(day));

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in March.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00).</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents March(this int day, ushort year) =>
		new(Year: year, Month: 3, Day: Convert.ToByte(day));

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in April.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00).</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents April(this int day, ushort year) =>
		new(Year: year, Month: 4, Day: Convert.ToByte(day));


	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in May.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00).</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents May(this int day, ushort year) => new(Year: year, Month: 5, Day: Convert.ToByte(day));

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in June.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00).</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents June(this int day, ushort year) => new(Year: year, Month: 6, Day: Convert.ToByte(day));

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in July.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00).</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents July(this int day, ushort year) => new(Year: year, Month: 7, Day: Convert.ToByte(day));

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in August.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00).</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents August(this int day, ushort year) =>
		new(Year: year, Month: 8, Day: Convert.ToByte(day));

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in September.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00).</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents September(this int day, ushort year) =>
		new(Year: year, Month: 9, Day: Convert.ToByte(day));

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in October.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00).</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents October(this int day, ushort year) =>
		new(Year: year, Month: 10, Day: Convert.ToByte(day));

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in November.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00).</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents November(this int day, ushort year) =>
		new(Year: year, Month: 11, Day: Convert.ToByte(day));

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeComponents"/> structure to the specified year and day in December.
	/// </summary>
	/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
	/// The time of day for the resulting <see cref="TimeComponents"/> is midnight (00:00:00).</remarks>
	/// <param name="day">The day (1 through the number of days in month).</param>
	/// <param name="year">The year (1 through 9999).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static TimeComponents December(this int day, ushort year) =>
		new(Year: year, Month: 12, Day: Convert.ToByte(day));
}
