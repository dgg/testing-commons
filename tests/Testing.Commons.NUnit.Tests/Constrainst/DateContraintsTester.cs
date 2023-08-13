using System;
using NUnit.Framework;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.Time;

using Is = Testing.Commons.NUnit.Constraints.Iz;

namespace Testing.Commons.NUnit.Tests.Constraints;

[TestFixture]
public class DateConstraintsTester
{
	private DateTime Today { get; set; }
	private DateTime Yesterday { get; set; }
	private DateTime Tomorrow { get; set; }

	[OneTimeSetUp]
	public void Setup()
	{
		Today = DateTime.Today;
		Tomorrow = Today.AddDays(1);
		Yesterday = Today.AddDays(-1);
	}

	[Test]
	public void Date_Comparisons()
	{
		Assert.That(Tomorrow, Is.After(Today));

		Assert.That(Yesterday, Is.Before(Today));

		Assert.That(Today, Is.OnOrAfter(Today));
		Assert.That(Tomorrow, Is.OnOrAfter(Today));

		Assert.That(Today, Is.OnOrBefore(Today));
		Assert.That(Yesterday, Is.OnOrBefore(Today));
	}

	[Test]
	public void Negative_Date_Comparisons()
	{
		Assert.That(Yesterday, Is.Not.After(Today));
		Assert.That(Tomorrow, Is.Not.Before(Today));
		Assert.That(Yesterday, Is.Not.OnOrAfter(Today));
		Assert.That(Tomorrow, Is.Not.OnOrBefore(Today));
	}

	[Test]
	public void Time_Comparisons()
	{
		DateTime nearbyPastTime = Today.Add(-Closeness.Default);
		DateTime nearbyFutureTime = Today.Add(Closeness.Default);

		Assert.That(Today, Is.CloseTo(nearbyPastTime));
		Assert.That(Today, Is.CloseTo(nearbyFutureTime));

		nearbyPastTime = Today.Add(-35.Milliseconds());
		nearbyFutureTime = Today.Add(35.Milliseconds());

		Assert.That(Today, Is.CloseTo(nearbyPastTime, ms: 35));
		Assert.That(Today, Is.CloseTo(nearbyPastTime, within: 35.Milliseconds()));
		Assert.That(Today, Is.CloseTo(nearbyFutureTime, ms: 35));
		Assert.That(Today, Is.CloseTo(nearbyFutureTime, within: 35.Milliseconds()));
	}

	[Test]
	public void Negative_Time_Comparisons()
	{
		DateTime nearbyPastTime = Today.Add(-Closeness.Default - 1.Milliseconds());
		DateTime nearbyFutureTime = Today.Add(Closeness.Default + 1.Milliseconds());

		Assert.That(Today, Is.Not.CloseTo(nearbyPastTime));
		Assert.That(Today, Is.Not.CloseTo(nearbyFutureTime));

		nearbyPastTime = Today.Add(-35.Milliseconds());
		nearbyFutureTime = Today.Add(35.Milliseconds());

		Assert.That(Today, Is.Not.CloseTo(nearbyPastTime, ms: 30));
		Assert.That(Today, Is.Not.CloseTo(nearbyPastTime, within: 30.Milliseconds()));
		Assert.That(Today, Is.Not.CloseTo(nearbyFutureTime, ms: 30));
		Assert.That(Today, Is.Not.CloseTo(nearbyFutureTime, within: 30.Milliseconds()));
	}
}
