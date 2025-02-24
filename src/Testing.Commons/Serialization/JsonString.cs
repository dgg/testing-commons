using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Testing.Commons.Serialization;

/// <summary>
/// Allows easier creation of JSON strings by removing the need to scape quotes.
/// </summary>
/// <remarks>A compact JSON string notation uses single quotes for names and string values instead
/// of double quotes, removing the need to escape such double quotes.
/// <para>And expanded JSON string uses the canonical double quote style for names a string values.</para>
/// <para>All string comparisons are performed using case-sensitive <see cref="CultureInfo.CurrentCulture"/>.</para>
/// </remarks>
/// <example>The string <code>"{\"property\"=\"value\"}"</code> can be written
/// as <code>"{'property'='value'}"</code></example>
public class JsonString
{
	private readonly string _json;
	/// <summary>
	/// Creates a instance of an easier to write JSON string.
	/// </summary>
	/// <param name="json">Compact JSON string (single quotes instead of escaped double quotes)</param>
	public JsonString([NotNull] string json)
	{
		_json = jsonify(json);
	}

	internal static string jsonify([NotNull] string json)
	{
		Arg.ThrowIfNullOrEmpty(json, nameof(json));
		return json.Replace("'", "\"", false, CultureInfo.CurrentCulture);
	}

	/// <summary>
	/// Returns the non-compact version of the compact JSON string provided.
	/// </summary>
	/// <returns>The expanded JSON.</returns>
	public override string ToString()
	{
		return _json;
	}

	/// <summary>
	/// Returns the expanded version of the compact JSON string provided.
	/// </summary>
	/// <param name="instance">The compact JSON string.</param>
	public static implicit operator string([NotNull] JsonString instance)
	{
		return instance._json;
	}

	/// <summary>
	/// Allows comparing expanded JSON strings to compact JSON ones.
	/// </summary>
	/// <remarks>A compact JSON string notation uses single quotes for names and string values instead
	/// of double quotes, removing the need to escape such double quotes.
	/// <para>An expanded JSON string uses the canonical double quote style for names an string values.</para>
	/// </remarks>
	public static StringComparer Comparer { get; } = new JsonStringComparer();

	private class JsonStringComparer : StringComparer
	{
		public override int Compare(string? x, string? y)
		{
			Arg.ThrowIfNullOrEmpty(y, nameof(y));
			return StringComparer.CurrentCulture.Compare(x, jsonify(y));
		}

		public override bool Equals(string? x, string? y)
		{
			Arg.ThrowIfNullOrEmpty(y, nameof(y));
			return StringComparer.CurrentCulture.Equals(x, jsonify(y));
		}

		public override int GetHashCode(string obj)
		{
			return obj.Jsonify().GetHashCode(StringComparison.CurrentCulture);
		}
	}
}
