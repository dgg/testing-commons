using System.Collections.Generic;

namespace Testing.Commons.Serialization
{
	/// <summary>
	/// Allows easier creation of JSON strings by removing the need to scape quotes.
	/// </summary>
	/// <remarks>A compact JSON string notation uses single quotes for names and string values instead
	/// of double quotes, removing the need to escape such double quotes.
	/// <para>And expanded JSON string uses the canonical double quote style for names an string values.</para>
	/// </remarks>
	/// <example>The string <code>"{\"property\"=\"value\"}"</code> can be written
	/// as <code>"{'property'='value'}"</code></example>
	public class JsonString
	{
		private readonly string _json;
		/// <summary>
		/// Creates a instance of an easier to write JSON string.
		/// </summary>
		/// <param name="json">Compact JSON string (single quotes instead of scaped double quotes)</param>
		public JsonString(string json)
		{
			_json = jsonify(json);
		}

		internal static string jsonify(string s)
		{
			return s == null ? null : s.Replace("'", "\"");
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
		public static implicit operator string(JsonString instance)
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
		public static IEqualityComparer<string> Comparer = new JsonStringComparer();

		private class JsonStringComparer : IEqualityComparer<string>
		{
			public bool Equals(string x, string y)
			{
				return x.Jsonify().Equals(y);
			}

			public int GetHashCode(string obj)
			{
				return obj.Jsonify().GetHashCode();
			}
		}
	}
}