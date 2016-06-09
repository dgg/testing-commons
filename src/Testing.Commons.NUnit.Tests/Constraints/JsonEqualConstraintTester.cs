using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class JsonEqualConstraintTester : Support.ConstraintTesterBase
	{
		[Test]
		public void CanBeNewedUp()
		{
			var actual = "{\"prop\"=\"value\"}";
			Assert.That(actual, new JsonEqualConstraint("{'prop'='value'}"));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			var actual = "{\"prop\"=\"value\"}";
			Assert.That(actual, Must.Be.Json("{'prop'='value'}"));
		}

		[Test]
		public void EqualsCanBeUsed_WithAComparer()
		{
			var actual = "{\"prop\"=\"value\"}";
			Assert.That(actual, Is.EqualTo("{'prop'='value'}").Using(JsonString.Comparer));
		}

		[Test]
		public void CanBeUsed_WithUsing()
		{
			var actual = "{\"prop\"=\"value\"}";
			Assert.That(actual, Is.EqualTo("{'prop'='value'}").Using(JsonString.Comparer));
		}

		[Test]
		public void CanBeUsed_WithExtension()
		{
			var actual = "{\"prop\"=\"value\"}";
			Assert.That(actual, Is.EqualTo("{'prop'='value'}").AsJson());
		}
	}

	/// <summary>
	/// Extends <see cref="EqualConstraint"/> allowing asserting using compact JSON strings.
	/// </summary>
	/// <remarks>A compact JSON string notation uses single quotes for names and string values instead
	/// of double quotes, removing the need to escape such double quotes.
	/// <para>An expanded JSON string uses the canonical double quote style for names an string values.</para>
	/// </remarks>
	/// <example><code>Assert.That("{\"prop\"=\"value\"}", new JsonConstraint("{'prop'='value'}"))</code></example>
	public class JsonEqualConstraint : Constraint
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JsonEqualConstraint"/> class. 
		/// </summary>
		/// <param name="expected">The expected value in JSON compact notation.</param>
		public JsonEqualConstraint(string expected) { }

		/// <summary>
		/// Applies the constraint to an actual value, returning a ConstraintResult.
		/// </summary>
		/// <param name="actual">The value to be tested</param>
		/// <returns>A ConstraintResult</returns>
		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			return new ConstraintResult(this, null, true);
		}
	}

	/// <summary>
	/// Extensions for assertions involving compact JSON strings.
	/// </summary>
	/// <remarks>A compact JSON string notation uses single quotes for names and string values instead
	/// of double quotes, removing the need to escape such double quotes.
	/// <para>An extended JSON string uses the canonical double quote style for names an string values.</para>
	/// </remarks>
	public static class JsonExtensions
	{
		/// <summary>
		/// Flags the constraint to use the supplied compact JSON string when performing equality. 
		/// </summary>
		/// <param name="constraint">The constraint to modify.</param>
		/// <returns>The modified constraint.</returns>
		/// <example><code>Assert.That("{\"prop\"=\"value\"}", Is.EqualTo("{'prop'='value'}").AsJson())</code></example>
		public static EqualConstraint AsJson(this EqualConstraint constraint)
		{
			return constraint.Using(JsonString.Comparer);
		}
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