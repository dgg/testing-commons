using Testing.Commons.Time;
using Iz = Testing.Commons.Tests.Time.Support.Iz;

namespace Testing.Commons.Tests.Time;

[TestFixture]
public class DateExtensionsTester
{
	[Test]
	public void Date_Creation_SameAsDateTime()
	{
		DateTime actual = 11.March(1977);
		var expected = new DateTime(1977, 3, 11);

		Assert.That(actual, Is.EqualTo(expected));
	}

	[Test]
	public void Date_Creation_SameAsDateOnly()
	{
		DateOnly actual = 11.March(1977);
		var expected = new DateOnly(1977, 3, 11);

		Assert.That(actual, Is.EqualTo(expected));
	}

	[Test]
	public void Date_Creation_SameAsDateTimeUtcOffset()
	{
		DateTimeOffset actual = 11.March(1977);
		var expected = new DateTimeOffset(1977, 3,  11,  0, 0, 0, TimeSpan.Zero);

		Assert.That(actual, Is.EqualTo(expected));
	}

	[Test]
	public void Date_Creation_CorrectData()
	{
		Assert.That(2.January(2000),  Iz.TimeWith(2000, 1, 2));
		Assert.That(3.February(2001), Iz.TimeWith(2001, 2, 3));
		Assert.That(4.March(2002), Iz.TimeWith(2002, 3, 4));
		Assert.That(5.April(2003), Iz.TimeWith(2003, 4, 5));
		Assert.That(6.May(2004), Iz.TimeWith(2004, 5, 6));
		Assert.That(7.June(2005), Iz.TimeWith(2005, 6, 7));
		Assert.That(8.July(2006), Iz.TimeWith(2006, 7, 8));
		Assert.That(9.August(2007), Iz.TimeWith(2007, 8, 9));
		Assert.That(10.September(2007), Iz.TimeWith(2007, 9, 10));
		Assert.That(20.October(2008), Iz.TimeWith(2008, 10, 20));
		Assert.That(28.November(2009), Iz.TimeWith(2009, 11, 28));
		Assert.That(29.December(2010), Iz.TimeWith(2010, 12, 29));
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
}
