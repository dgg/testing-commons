using ExpectedObjects;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints.Support;

namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Allows matching partial expected objects.
/// </summary>
/// <remarks>It is a useful feature when it is easier to create a object with the same shape and a subset of the values as the actual object.</remarks>
public class MatchingConstraint : Constraint
{
	private readonly ExpectedObject _expected;
	private readonly ExposingWriter _writer;
	private WritableEqualityResult _exposed = default!;

	/// <summary>
	/// Creates the instance of the constraint.
	/// </summary>
	/// <param name="expected">The object to match the actual value against.</param>
	public MatchingConstraint(object expected)
	{
		_writer = new ExposingWriter(new ShouldWriter());
		_expected = expected.ToExpectedObject().Configure(ctx =>
		{
			ctx.IgnoreTypes();
			ctx.SetWriter(_writer);
		});
	}

	/// <summary>
	/// Applies the constraint to an actual value, returning a ConstraintResult.
	/// </summary>
	/// <param name="actual">The value to be tested</param>
	/// <returns>A ConstraintResult</returns>
	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{
		bool matched = _expected.Equals(actual);
		if (!matched)
		{
			_writer.GetFormattedResults();
			_exposed = _writer.Exposed;
		}
		return new MatchingResult(_exposed, this, actual!, matched);
	}

	/// <summary>
	/// The Description of what this constraint tests, for
	/// use in messages and in the ConstraintResult.
	/// </summary>
	public override string Description => _exposed.WriteExpected();

	class MatchingResult : ConstraintResult
	{
		private readonly WritableEqualityResult _exposed;

		public MatchingResult(WritableEqualityResult exposed, IConstraint constraint, object actualValue, bool isSuccess) : base(constraint, actualValue, isSuccess)
		{
			_exposed = exposed;
		}

		public override void WriteMessageTo(MessageWriter writer)
		{
			_exposed.WriteOffendingMember(writer);
			base.WriteMessageTo(writer);
		}

		public override void WriteActualValueTo(MessageWriter writer)
		{
			_exposed.WriteActual(writer);
		}
	}
}
