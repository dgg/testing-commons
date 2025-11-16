using Testing.Commons.Time;

namespace Testing.Commons.Tests.Time;

[TestFixture]
public class SpanExtensionsTester
{
	[Test]
	public void Span_WeeksCreation_AsExpected() => Assert.That(2.Weeks(), Is.EqualTo(TimeSpan.FromDays(14)));

	[Test]
	public void Span_DaysCreation_AsExpected() => Assert.That(3.Days(), Is.EqualTo(TimeSpan.FromDays(3)));

	[Test]
	public void Span_HoursCreation_AsExpected() => Assert.That(3.Hours(), Is.EqualTo(TimeSpan.FromHours(3)));

	[Test]
	public void Span_MinutesCreation_AsExpected() => Assert.That(3.Minutes(), Is.EqualTo(TimeSpan.FromMinutes(3)));

	[Test]
	public void Span_SecondsCreation_AsExpected() => Assert.That(3.Seconds(), Is.EqualTo(TimeSpan.FromSeconds(3)));

	[Test]
	public void Span_FluentCreation_AsExpected() =>
		Assert.That(5.Days().Hours(4).Minutes(3).Seconds(2).Milliseconds(1),
			Is.EqualTo(new TimeSpan( 5, 4, 3, 2, 1)));

	[Test]
	public void Span_FluentCreationWithWeeks_UseAdd()
	{
		TimeSpan ts = 2.Weeks() + 5.Days().Hours(4).Minutes(3).Seconds(2).Milliseconds(1);

		Assert.That(ts, Is.EqualTo(new TimeSpan(19, 4, 3, 2, 1)));
	}

	[Test]
	public void Span_AnotherFluentFlavor_AsExpected() =>
		Assert.That(5.Days(4.Hours(3.Minutes(2.Seconds(1.Milliseconds())))),
			Is.EqualTo(new TimeSpan( 5, 4, 3, 2, 1)));

	[Test]
	public void After_FastForwardsInstant()
	{
		DateTimeOffset now = 15.November(2011).At(hour: 22, minute: 55).In(1.Hours());
		var expected = new DateTimeOffset(2011, 11, 15, 23, 5, 0, TimeSpan.FromHours(1));
		Assert.That(10.Minutes().After(now), Is.EqualTo(expected));
	}

	[Test]
	public void Before_RewindsInstant()
	{
		DateTimeOffset now = 15.November(2011).At(hour: 22, minute: 55).In(1.Hours());
		var expected = new DateTimeOffset(2011, 11, 15, 22, 45, 0, TimeSpan.FromHours(1));
		Assert.That(10.Minutes().Before(now), Is.EqualTo(expected));
	}
}
