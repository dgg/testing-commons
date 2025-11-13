using NUnit.Framework.Constraints;
using Testing.Commons.Time.Newer;

namespace Testing.Commons.Tests.Time.Support
{
	internal class DateOnlyConstraint : Constraint
	{
		private readonly Constraint _inner;
		public DateOnlyConstraint(int year, int month, int day)
		{
			_inner = Has.Property(nameof(DateOnly.Year)).EqualTo(year).And
				.Property(nameof(DateOnly.Month)).EqualTo(month).And
				.Property(nameof(DateOnly.Day)).EqualTo(day);
		}

		public override string Description => _inner.Description;

		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			DateOnly date = (TimeComponents)(object)actual!;

			return _inner.ApplyTo(date);
		}
	}
}
