
using Testing.Commons.Time;

using Is = Testing.Commons.Tests.Time.Support.Is;

namespace Testing.Commons.Tests.Time;

[TestFixture]
public class SpanExtensionsTester
{
	[Test]
	public void Span_WeeksCreation_AsExpected()
	{
		TimeSpan ts = 2.Weeks();

		Assert.That(ts, Is.SpanWith(days: 14));
	}

	[Test]
	public void Span_DaysCreation_AsExpected()
	{
		TimeSpan ts = 3.Days();

		Assert.That(ts, Is.SpanWith(days: 3));
	}

	[Test]
	public void Span_HoursCreation_AsExpected()
	{
		TimeSpan ts = 3.Hours();

		Assert.That(ts, Is.SpanWith(hours: 3));
	}

	[Test]
	public void Span_MinutesCreation_AsExpected()
	{
		TimeSpan ts = 3.Minutes();

		Assert.That(ts, Is.SpanWith(minutes: 3));
	}

	[Test]
	public void Span_SecondsCreation_AsExpected()
	{
		TimeSpan ts = 3.Seconds();

		Assert.That(ts, Is.SpanWith(seconds: 3));
	}

	[Test]
	public void Span_FluentCreation_AsExpected()
	{
		TimeSpan ts = 5.Days().Hours(4).Minutes(3).Seconds(2).Milliseconds(1);

		Assert.That(ts, Is.SpanWith(5, 4, 3, 2, 1));
	}

	[Test]
	public void Span_FluentCreationWithWeeks_UseAdd()
	{
		TimeSpan ts = 2.Weeks() + 5.Days().Hours(4).Minutes(3).Seconds(2).Milliseconds(1);

		Assert.That(ts, Is.SpanWith(19, 4, 3, 2, 1));
	}

	[Test]
	public void Span_AnotherFluentFlavor_AsExpected()
	{
		TimeSpan ts = 5.Days(4.Hours(3.Minutes(2.Seconds(1.Milliseconds()))));

		Assert.That(ts, Is.SpanWith(5, 4, 3, 2, 1));
	}

	[Test]
	public void After_FastForwardsInstant()
	{
		DateTimeOffset now = 15.November(2011).At(hour: 22, minute: 55).In(1.Hours());
		Assert.That(10.Minutes().After(now),
			Is.OffsetWith(2011, 11, 15, 23, 5, offset: TimeSpan.FromHours(1)));
	}

	[Test]
	public void Before_RewindsInstant()
	{
		DateTimeOffset now = 15.November(2011).At(hour: 22, minute: 55).In(1.Hours());
		Assert.That(10.Minutes().Before(now),
			Is.OffsetWith(2011, 11, 15, 22, 45, offset: TimeSpan.FromHours(1)));
	}
}
