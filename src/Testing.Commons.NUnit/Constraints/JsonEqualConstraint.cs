using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Extends <see cref="EqualConstraint"/> allowing asserting using compact JSON strings.
	/// </summary>
	/// <remarks>A compact JSON string notation uses single quotes for names and string values instead
	/// of double quotes, removing the need to escape such double quotes.
	/// <para>An expanded JSON string uses the canonical double quote style for names an string values.</para>
	/// </remarks>
	/// <example><code>Assert.That("{\"prop\"=\"value\"}", new JsonConstraint("{'prop'='value'}"))</code></example>
	public class JsonEqualConstraint : EqualConstraint
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JsonEqualConstraint"/> class. 
		/// </summary>
		/// <param name="expected">The expected value in JSON compact notation.</param>
		public JsonEqualConstraint(string expected) : base(expected.Jsonify()) { }
	}

	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="JsonEqualConstraint"/> that allows assertions using
		/// compact JSON strings.
		/// </summary>
		/// <remarks>A compact JSON string notation uses single quotes for names and string values instead
		/// of double quotes, removing the need to escape such double quotes.
		/// <para>A non-compact JSON string uses the canonical double quote style for names an string values.</para>
		/// </remarks>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="expected">The expected value in JSON compact notation.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Json(this Must.BeEntryPoint entry, string expected)
		{
			return new JsonEqualConstraint(expected);
		}
	}
}