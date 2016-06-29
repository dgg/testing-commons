using System;
using NUnit.Framework;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.Time;

namespace Testing.Commons.NUnit.Tests.Constraints
{
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

		#region Exploratory

		[Test]
		public void Date_Comparisons()
		{
			Assert.That(Tomorrow, Must.Be.After(Today));

			Assert.That(Yesterday, Must.Be.Before(Today));

			Assert.That(Today, Must.Be.OnOrAfter(Today));
			Assert.That(Tomorrow, Must.Be.OnOrAfter(Today));

			Assert.That(Today, Must.Be.OnOrBefore(Today));
			Assert.That(Yesterday, Must.Be.OnOrBefore(Today));
		}

		[Test]
		public void Time_Comparisons()
		{
			DateTime nearbyPastTime = Today.Add(-Closeness.Default);
			DateTime nearbyFutureTime = Today.Add(Closeness.Default);

			Assert.That(Today, Must.Be.CloseTo(nearbyPastTime));
			Assert.That(Today, Must.Be.CloseTo(nearbyFutureTime));

			nearbyPastTime = Today.Add(-35.Milliseconds());
			nearbyFutureTime = Today.Add(35.Milliseconds());

			Assert.That(Today, Must.Be.CloseTo(nearbyPastTime, ms: 35));
			Assert.That(Today, Must.Be.CloseTo(nearbyPastTime, within: 35.Milliseconds()));
			Assert.That(Today, Must.Be.CloseTo(nearbyFutureTime, ms: 35));
			Assert.That(Today, Must.Be.CloseTo(nearbyFutureTime, within: 35.Milliseconds()));
		}

		[Test]
		public void Negative_Date_Comparisons()
		{
			Assert.That(Yesterday, Must.Not.Be.After(Today));
			Assert.That(Tomorrow, Must.Not.Be.Before(Today));
			Assert.That(Yesterday, Must.Not.Be.OnOrAfter(Today));
			Assert.That(Tomorrow, Must.Not.Be.OnOrBefore(Today));
		}

		[Test]
		public void Negative_Time_Comparisons()
		{
			DateTime nearbyPastTime = Today.Add(-Closeness.Default - 1.Milliseconds());
			DateTime nearbyFutureTime = Today.Add(Closeness.Default + 1.Milliseconds());

			Assert.That(Today, Must.Not.Be.CloseTo(nearbyPastTime));
			Assert.That(Today, Must.Not.Be.CloseTo(nearbyFutureTime));

			nearbyPastTime = Today.Add(-35.Milliseconds());
			nearbyFutureTime = Today.Add(35.Milliseconds());

			Assert.That(Today, Must.Not.Be.CloseTo(nearbyPastTime, ms: 30));
			Assert.That(Today, Must.Not.Be.CloseTo(nearbyPastTime, within: 30.Milliseconds()));
			Assert.That(Today, Must.Not.Be.CloseTo(nearbyFutureTime, ms: 30));
			Assert.That(Today, Must.Not.Be.CloseTo(nearbyFutureTime, within: 30.Milliseconds()));
		}

		[Test]
		public void Date_Components()
		{
			Assert.That(11.March(1977), Must.Have.Year(1977));
			Assert.That(11.March(1977), Must.Have.Month(3));
			Assert.That(11.March(1977), Must.Have.Day(11));
			Assert.That(11.March(1977), Must.Have.Hour(0));
			Assert.That(11.March(1977), Must.Have.Minute(0));
			Assert.That(11.March(1977), Must.Have.Second(0));
			Assert.That(11.March(1977), Must.Have.Millisecond(0));
		}

		[Test]
		public void Negative_Date_Components()
		{
			Assert.That(11.March(1977), Must.Not.Have.Year(1978));
			Assert.That(11.March(1977), Must.Not.Have.Month(1));
			Assert.That(11.March(1977), Must.Not.Have.Day(10));
			Assert.That(11.March(1977), Must.Not.Have.Hour(1));
			Assert.That(11.March(1977), Must.Not.Have.Minute(1));
			Assert.That(11.March(1977), Must.Not.Have.Second(1));
			Assert.That(11.March(1977), Must.Not.Have.Millisecond(1));
		}

		#endregion
	}
}