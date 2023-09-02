using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Provides a strongly-typed base class to implement custom constraints that
/// delegate matching and message formatting to another instance of <see cref="Constraint"/>
/// </summary>
public abstract class DelegatingConstraint : Constraint
{
	/// <summary>
	/// Instance of <see cref="Constraint"/> that will be in charge of matching and message formatting.
	/// </summary>
	protected IConstraint Delegate { get; set; } = default!;

	/// <summary>
	/// Applies the constraint to an actual value, returning a ConstraintResult.
	/// </summary>
	/// <param name="actual">The value to be tested</param>
	/// <returns>A ConstraintResult</returns>
	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{
		return new DelegatingResult(Delegate, matches(actual));
	}

	/// <summary>
	/// Test whether the constraint is satisfied by <paramref name="current"/>.
	/// </summary>
	/// <param name="current">The value to be tested.</param>
	/// <returns>The resulting <see cref="ConstraintResult"/> from the vertification.</returns>
	protected abstract ConstraintResult matches(object current);

	/// <summary>
	/// The Description of what this constraint tests, for
	/// use in messages and in the ConstraintResult.
	/// </summary>
	public override string Description => Delegate.Description;

	class DelegatingResult : ConstraintResult
	{
		private readonly ConstraintResult _result;

		public DelegatingResult(IConstraint constraint, ConstraintResult result) : base(constraint, result.ActualValue, result.IsSuccess)
		{
			_result = result;
		}

		public override void WriteMessageTo(MessageWriter writer)
		{
			_result.WriteMessageTo(writer);
		}

		public override void WriteActualValueTo(MessageWriter writer)
		{
			_result.WriteActualValueTo(writer);
		}
	}
}
