using NUnit.Framework.Constraints;
using Testing.Commons.Time.Newer;

namespace Testing.Commons.Tests.Time.Support
{
	internal class TimeOnlyConstraint : Constraint
	{
		private readonly Constraint _inner;
		public TimeOnlyConstraint(int hour, int minute, int second, int millisecond)
		{
			_inner = Has.Property(nameof(TimeOnly.Hour)).EqualTo(hour).And
				.Property(nameof(TimeOnly.Minute)).EqualTo(minute).And
				.Property(nameof(TimeOnly.Second)).EqualTo(second).And
				.Property(nameof(TimeOnly.Second)).EqualTo(millisecond);
		}

		public override string Description => _inner.Description;

		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			TimeOnly time = (TimeComponents)(object)actual!;

			return _inner.ApplyTo(time);
		}
	}
}
