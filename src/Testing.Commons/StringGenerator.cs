using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Testing.Commons;

/// <summary>
/// Allows generating strings with different patterns and different lengths.
/// </summary>
public static class StringGenerator
{
	private const string NumericPattern = "0123456789";

	/// <summary>
	/// Generates a string with the pattern {0123456789} for the given length.
	/// </summary>
	/// <param name="length">The number of characters of the string generated.</param>
	/// <returns>A string of <paramref name="length"/> characters with figures.</returns>
	/// <example><code>
	/// .Numeric(2) --> "01"
	/// .Numeric(10) --> "0123456789"
	/// </code></example>
	public static string Numeric(uint length)
	{
		return RepeatPattern(NumericPattern, length);
	}

	/// <summary>
	/// Generates a string with the provided pattern for the given length.
	/// </summary>
	/// <param name="pattern">The  sequence of characters to loop.</param>
	/// <param name="length">The number of characters of the string generated.</param>
	/// <returns>A string of <paramref name="length"/> characters with characters from the pattern.</returns>
	/// <example><code>
	/// .RepeatPattern("abc", 2) --> "ab"
	/// .RepeatPattern("abc", 5) --> "abcab"
	/// </code></example>
	public static string RepeatPattern([NotNull] string pattern, uint length)
	{
		ArgumentNullException.ThrowIfNull(pattern, nameof(pattern));
		var sb = new StringBuilder((int)length);
		for (uint i = 0; i < length; i++)
		{
			int patternIndex = (int)(i % pattern.Length);
			sb.Append(pattern[patternIndex]);
		}
		return sb.ToString();
	}
}
