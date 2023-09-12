using Testing.Commons.Time;
using Testing.Commons.NUnit.Constraints;

using Iz = Testing.Commons.NUnit.Constraints.Iz;
using Haz = Testing.Commons.NUnit.Constraints.Haz;

namespace Testing.Commons.NUnit.Tests.Constraints;

[TestFixture]
public class TimeExtensionTester
{
	public DateTime Today { get; } = DateTime.UtcNow;
	public DateTime Tomorrow { get; } = DateTime.UtcNow.AddDays(1);
	public DateTime Yesterday { get; } = DateTime.UtcNow.AddDays(-1);


	[Test]
	public void DateTime_Comparison()
	{
		Assert.That(Tomorrow, Iz.After(Today));
		Assert.That(Yesterday, Iz.Before(Today));
		Assert.That(Tomorrow, Iz.OnOrAfter(Today));
		Assert.That(Yesterday, Iz.OnOrBefore(Today));
	}

	[Test]
	public void DateTime_Closeness()
	{
		var nearbyPastTime = Today.AddMilliseconds(-10);
		var nearbyFutureTime = Today.AddMilliseconds(10);
		Assert.That(Today, Iz.CloseTo(nearbyPastTime));
		Assert.That(Today, Iz.CloseTo(nearbyFutureTime));
		Assert.That(Today, Iz.CloseTo(nearbyPastTime, ms: 35));
		Assert.That(Today, Iz.CloseTo(nearbyFutureTime, within: 35.Milliseconds()));
	}

	[Test]
	public void DateTime_Props()
	{
		Assert.That(11.March(1977), Haz.Year(1977));
		Assert.That(11.March(1977), Haz.Month(3));
		Assert.That(11.March(1977), Haz.Day(11));
		Assert.That(11.March(1977), Haz.Hour(0));
		Assert.That(11.March(1977), Haz.Minute(0));
		Assert.That(11.March(1977), Haz.Second(0));
		Assert.That(11.March(1977), Haz.Millisecond(0));
	}

	[Test]
	public void Negative_Contraints()
	{
		Assert.That(Yesterday, Is.Not.After(Today));
		Assert.That(Today, Is.Not.CloseTo(Tomorrow));
		Assert.That(11.March(1977), Has.No.Year(1978));
	}
}
