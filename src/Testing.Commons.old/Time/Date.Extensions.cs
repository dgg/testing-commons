using System;

namespace Testing.Commons.Time
{
	/// <summary>
	/// Allows expressive creation of <see cref="DateTime"/> instances that represent a date.
	/// </summary>
	/// <example><code>11.March(1977)</code></example>
	public static class DateExtensions
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in January.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime January(this int day, int year)
		{
			return new DateTime(year, 1, day);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in February.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime February(this int day, int year)
		{
			return new DateTime(year, 2, day);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in March.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime March(this int day, int year)
		{
			return new DateTime(year, 3, day);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in April.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime April(this int day, int year)
		{
			return new DateTime(year, 4, day);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in May.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime May(this int day, int year)
		{
			return new DateTime(year, 5, day);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in June.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime June(this int day, int year)
		{
			return new DateTime(year, 6, day);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in July.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime July(this int day, int year)
		{
			return new DateTime(year, 7, day);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in August.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime August(this int day, int year)
		{
			return new DateTime(year, 8, day);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in September.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime September(this int day, int year)
		{
			return new DateTime(year, 9, day);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in October.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime October(this int day, int year)
		{
			return new DateTime(year, 10, day);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in November.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime November(this int day, int year)
		{
			return new DateTime(year, 11, day);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTime"/> structure to the specified year and day in December.
		/// </summary>
		/// <remarks>Year, month, and day are interpreted as a year, month, and day in the Gregorian calendar.
		/// The time of day for the resulting <see cref="DateTime"/> is midnight (00:00:00). The <see cref="DateTime.Kind"/> property is initialized to
		/// <see cref="DateTimeKind.Unspecified"/>.</remarks>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="year">The year (1 through 9999).</param>
		/// <returns>A new instance with the information provided.</returns>
		public static DateTime December(this int day, int year)
		{
			return new DateTime(year, 12, day);
		}
	}
}
