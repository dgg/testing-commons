using System;
using Testing.Commons.Tests.Time.Support;
using Testing.Commons.Time;
using NUnit.Framework;

namespace Testing.Commons.Tests.Time
{
	[TestFixture]
	public class TimeExtensionsTester
	{
		[Test]
		public void Time_Creation_hms_CorrectData()
		{
			DateTime dt = 11.March(1977).At(12, 30, 45);
			Assert.That(dt, Must.Be.TimeWith(1977, 3, 11, 12, 30, 45));
		}

		[Test]
		public void Time_Creation_hm_CorrectData()
		{
			DateTime dt = 11.March(1977).At(12, 30);
			Assert.That(dt, Must.Be.TimeWith(1977, 3, 11, 12, 30));
		}

		[Test]
		public void Time_Creation_h_CorrectData()
		{
			DateTime dt = 11.March(1977).At(12);
			Assert.That(dt, Must.Be.TimeWith(1977, 3, 11, 12));
		}
		
		[Test]
		public void Time_Creation_NoMembers_IsADate()
		{
			DateTime dt = 11.March(1977).At();
			Assert.That(dt, Must.Be.DateWith(1977, 3, 11));
		}

		[Test]
		public void Time_Creation_WithSpan_CorrectData()
		{
			DateTime dt = 11.March(1977).At(new TimeSpan(0, 12, 30, 45));
			Assert.That(dt, Must.Be.TimeWith(1977, 3, 11, 12, 30, 45));
		}

		[Test]
		public void Time_Creation_WithSpan_DaysDiscarded()
		{
			DateTime dt = 11.March(1977).At(new TimeSpan(1, 12, 30, 45));
			Assert.That(dt, Must.Be.TimeWith(1977, 3, 11, 12, 30, 45));
		}

		[Test]
		public void Time_Creation_WithTimeOfDay_CorrectData()
		{
			DateTime dt = 28.August(2006).At(t => t.Noon);
			Assert.That(dt, Must.Be.TimeWith(2006, 8, 28, 12));

			dt = 30.September(2008).At(t => t.MidNight);
			Assert.That(dt, Must.Be.TimeWith(2008, 9, 30));
		}
	}
}
