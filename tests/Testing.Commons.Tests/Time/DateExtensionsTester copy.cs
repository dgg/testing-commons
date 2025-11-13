using Testing.Commons.Time.Newer;
using Is = Testing.Commons.Tests.Time.Support.Is;

namespace Testing.Commons.Tests.Time;

[TestFixture]
public class NewDateExtensionsTester
{
	[Test]
	public void Date_Creation_SameAsDateTime()
	{
		DateTime birthday = 11.March(1977);
		var dtBirthday = new DateTime(1977, 3, 11);

		Assert.That(birthday, Is.EqualTo(dtBirthday));
	}

	
	[Test]
	public void Date_Creation_SameAsDateOnly()
	{
		DateOnly birthday = 11.March(1977);
		var doBirthday = new DateOnly(1977, 3, 11);

		Assert.That(birthday, Is.EqualTo(doBirthday));
	}

	[Test]
	public void Date_Creation_CorrectData()
	{
		Assert.That(2.January(2000), Is.DateOnly(2000, 1, 2));
		Assert.That(3.February(2001), Is.DateOnly(2001, 2, 3));
		Assert.That(4.March(2002), Is.DateOnly(2002, 3, 4));
		Assert.That(5.April(2003), Is.DateOnly(2003, 4, 5));
		Assert.That(6.May(2004), Is.DateOnly(2004, 5, 6));
		Assert.That(7.June(2005), Is.DateOnly(2005, 6, 7));
		Assert.That(8.July(2006), Is.DateOnly(2006, 7, 8));
		Assert.That(9.August(2007), Is.DateOnly(2007, 8, 9));
		Assert.That(10.September(2007), Is.DateOnly(2007, 9, 10));
		Assert.That(20.October(2008), Is.DateOnly(2008, 10, 20));
		Assert.That(28.November(2009), Is.DateOnly(2009, 11, 28));
		Assert.That(29.December(2010), Is.DateOnly(2010, 12, 29));
	}

	[Test]
	public void Date_CreateIncorrectDay_Exception()
	{
		ushort nonLeapYear = 2001;
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			DateOnly dt = 29.February(nonLeapYear);
		});
	}

	[Test]
	[TestCase(-1), TestCase(256)]
	public void Date_ByteOverflow_Exception(int overflower)
	{
		Assert.Throws<OverflowException>(() =>
		{
			overflower.January(2025);
		});
	}

	[Test]
	public void Date_IncorrectDayOfTheMonth_Exception()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() =>
		{
			DateOnly dt = 33.January(2025);
		});
	}

	[Test]
	public void Time_Creation_hms_CorrectData()
	{
		DateTimeOffset dto = 11.March(1977).At(12, 30, 45);
		DateTimeOffset utc = new DateTimeOffset(
			new DateOnly(1977, 3, 11),
			new TimeOnly(12, 30, 45),
			TimeSpan.Zero);
		Assert.That(dto, Is.EqualTo(utc));
	}

	[Test]
	public void Time_Creation_WithSpan_CorrectData()
	{
		DateTime dt = 11.March(1977).At(new TimeSpan(0, 12, 30, 45));
		Assert.That(dt, Is.TimeWith(1977, 3, 11, 12, 30, 45));
	}

	[Test]
	public void Time_Creation_WithSpan_DaysDiscarded()
	{
		DateTime dt = 11.March(1977).At(new TimeSpan(1, 12, 30, 45));
		Assert.That(dt, Is.TimeWith(1977, 3, 11, 12, 30, 45));
	}
}
