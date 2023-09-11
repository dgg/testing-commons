namespace Testing.Commons.Time;

/// <summary>
/// Allows expressive creation of <see cref="DateTimeOffset"/> instances.
/// </summary>
/// <example><code>11.March(1977).In(TimeSpan.FromHours(1))</code></example>
public static class OffsetExtensions
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DateTimeOffset"/> structure using the specified <see cref="DateTime"/> values and offset.
	/// </summary>
	/// <param name="dt">A date and time.</param>
	/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
	/// <returns>A new instance with the information provided.</returns>
	public static DateTimeOffset In(this DateTime dt, TimeSpan offset)
	{
		return new DateTimeOffset(dt, offset);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="DateTimeOffset"/> structure using the specified <see cref="DateTime"/> values and an offset of <see cref="TimeSpan.Zero"/>.
	/// </summary>
	/// <param name="dt">A date and time.</param>
	/// <returns>A new instance in the Coordinated Universal Time (UTC) with the information provided.</returns>
	public static DateTimeOffset InUtc(this DateTime dt)
	{
		return dt.In(TimeSpan.Zero);
	}
}
