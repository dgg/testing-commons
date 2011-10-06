using ExpectedObjects;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public abstract class ExpectedConstraint : Constraint
	{
		protected readonly ExpectedObject _expected;
		protected readonly IWriter _writer;

		protected ExpectedConstraint(object expected)
		{
			_writer = new ShouldWriter();
			_expected = expected.ToExpectedObject().Configure(ctx =>
			{
				ctx.IgnoreTypes();
				ctx.SetWriter(_writer);
			});
		}

		/// <summary>
		/// Test whether the constraint is satisfied by a given value.
		/// </summary>
		/// <param name="current">The value to be tested</param>
		/// <returns>True for success, false for failure</returns>
		public override bool Matches(object current)
		{
			actual = current;
			return _expected.Equals(actual);
		}
	}
}
