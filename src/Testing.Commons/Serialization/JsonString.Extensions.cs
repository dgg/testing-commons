namespace Testing.Commons.Serialization
{
	/// <summary>
	/// Provides extensions to facilite the usage of <see cref="JsonString"/>.
	/// </summary>
	public static class JsonStringExtensions
	{
		/// <summary>
		/// Creates a non-compact JSON string from its compact JSON representation.
		/// </summary>
		/// <remarks>A compact JSON string notation uses single quotes for names and string values instead
		/// of double quotes, removing the need to escape such double quotes.
		/// <para>A non-compact JSON string uses the canonical double quote style for names an string values.</para>
		/// </remarks>
		/// <param name="json">JSON string in compact form.</param>
		/// <returns>JSON string is non-compact form.</returns>
		public static string Jsonify(this string json)
		{
			return JsonString.jsonify(json);
		}
	}
}
