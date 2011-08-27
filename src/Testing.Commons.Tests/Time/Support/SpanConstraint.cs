using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.Tests.Time.Support
{
	internal class SpanConstraint : Constraint
	{
		private readonly Constraint _composed;

		public SpanConstraint(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			_composed = Has.Property("Days").EqualTo(days) &
				Has.Property("Hours").EqualTo(hours) &
				Has.Property("Minutes").EqualTo(minutes) &
				Has.Property("Seconds").EqualTo(seconds) &
				Has.Property("Milliseconds").EqualTo(milliseconds);
		}

		public override bool Matches(object current)
		{
			actual = current;

			return _composed.Matches(current);
		}

		public override void WriteDescriptionTo(MessageWriter writer)
		{
			_composed.WriteDescriptionTo(writer);
		}
	}
}
