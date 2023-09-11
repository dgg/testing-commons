using System.Diagnostics.CodeAnalysis;

namespace Testing.Commons.Serialization
{
	/// <summary>
	/// Provides extensions to facilite the usage of <see cref="JsonString"/>.
	/// </summary>
	public static class JsonStringExtensions
	{
		/// <summary>
		/// Expands a compact JSON string into its expanded JSON representation.
		/// </summary>
		/// <remarks>A compact JSON string notation uses single quotes for names and string values instead
		/// of double quotes, removing the need to escape such double quotes.
		/// <para>An extended JSON string uses the canonical double quote style for names an string values.</para>
		/// </remarks>
		/// <param name="json">JSON string in compact form.</param>
		/// <returns>The expanded JSON.</returns>
		public static string Jsonify([NotNull] this string json)
		{
			return JsonString.jsonify(json);
		}
	}
}
