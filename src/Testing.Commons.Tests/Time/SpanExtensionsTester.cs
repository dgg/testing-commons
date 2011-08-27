using Testing.Commons.Tests.Time.Support;
using Testing.Commons.Time;
using NUnit.Framework;

namespace Testing.Commons.Tests.Time
{
	[TestFixture]
	public class SpanExtensionsTester
	{
		[Test]
		public void Span_WeeksCreation_AsExpected()
		{
			var ts = 2.Weeks();

			Assert.That(ts, Must.Be.SpanWith(days: 14));
		}

		[Test]
		public void Span_DaysCreation_AsExpected()
		{
			var ts = 3.Days();

			Assert.That(ts, Must.Be.SpanWith(days : 3));
		}

		[Test]
		public void Span_HoursCreation_AsExpected()
		{
			var ts = 3.Hours();

			Assert.That(ts, Must.Be.SpanWith(hours: 3));
		}

		[Test]
		public void Span_MinutesCreation_AsExpected()
		{
			var ts = 3.Minutes();

			Assert.That(ts, Must.Be.SpanWith(minutes: 3));
		}

		[Test]
		public void Span_SecondsCreation_AsExpected()
		{
			var ts = 3.Seconds();

			Assert.That(ts, Must.Be.SpanWith(seconds: 3));
		}

		[Test]
		public void Span_FluentCreation_AsExpected()
		{
			var ts = 5.Days().Hours(4).Minutes(3).Seconds(2).Milliseconds(1);

			Assert.That(ts, Must.Be.SpanWith(5, 4, 3, 2, 1));
		}

		[Test]
		public void Span_FluentCreationWithWeeks_UseAdd()
		{
			var ts = 2.Weeks() + 5.Days().Hours(4).Minutes(3).Seconds(2).Milliseconds(1);

			Assert.That(ts, Must.Be.SpanWith(19, 4, 3, 2, 1));
		}
	}
}
