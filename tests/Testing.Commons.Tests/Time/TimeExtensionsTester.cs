using Testing.Commons.Time;
using Iz = Testing.Commons.Tests.Time.Support.Iz;

namespace Testing.Commons.Tests.Time;

[TestFixture]
public class TimeExtensionsTester
{
	[Test]
	public void Time_Creation_hms_CorrectData()
	{
		DateTime actual = 11.March(1977).At(12, 30, 45);
		DateTime expected = new DateTime(
			new DateOnly(1977, 3, 11),
			new TimeOnly(12, 30, 45),
			// UTC by default
			DateTimeKind.Utc);
		Assert.That(actual, Is.EqualTo(expected));

		Assert.That(11.March(1977).At(12, 30, 45), Iz
			.TimeWith(1977, 3, 11, 12, 30, 45));
	}

	[Test]
	public void Time_Creation_hm_CorrectData() =>
		Assert.That(11.March(1977).At(12, 30), Iz
			.TimeWith(1977, 3, 11, 12, 30));

	[Test]
	public void Time_Creation_h_CorrectData() =>
		Assert.That(11.March(1977).At(12), Iz
			.TimeWith(1977, 3, 11, 12));

	[Test]
	public void Time_Creation_NoMembers_IsADate() =>
		Assert.That(11.March(1977).At(), Iz
			.TimeWith(1977, 3, 11));

	[Test]
	public void Time_Creation_WithSpan_CorrectData() =>
		Assert.That(11.March(1977).At(new TimeSpan(0, 12, 30, 45)), Iz
			.TimeWith(1977, 3, 11, 12, 30, 45));

	[Test]
	public void Time_Creation_WithSpan_DaysDiscarded() =>
		Assert.That(11.March(1977).At(new TimeSpan(1, 12, 30, 45)), Iz
			.TimeWith(1977, 3, 11, 12, 30, 45));

	[Test]
	public void Time_Creation_Noon_CorrectComponents() =>
		Assert.That(28.August(2006).At(t => t.Noon), Iz
			.TimeWith(2006, 8, 28, 12));

	[Test]
	public void Time_Creation_WithTimeOfDay_CorrectComponents() =>
		Assert.That(30.September(2008).At(t => t.MidNight), Iz
			.TimeWith(2008, 9, 30));

	[Test]
	public void Time_Creation_WithSpanExtensions_DaysDiscarded()
	{
		TimeComponents subject = 11.March(1977).At(new TimeSpan(1, 12, 30, 45));
		DateTime dt = subject;
		DateTime dt2 = 11.March(1977).At(1.Day().Hours(12).Minutes(30).Seconds(45));
		DateTime dt3 = 11.March(1977).At(12.Hours(30.Minutes(45.Seconds())));

		Assert.That(dt, Is.EqualTo(dt2));
		Assert.That(dt2, Is.EqualTo(dt3));
		Assert.That(dt, Is.EqualTo(dt3));
		Assert.That(subject, Iz.TimeWith(1977, 3, 11, 12, 30, 45));
	}
}
