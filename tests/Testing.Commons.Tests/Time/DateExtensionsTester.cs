using Testing.Commons.Time;
using Is = Testing.Commons.Tests.Time.Support.Is;

namespace Testing.Commons.Tests.Time;

[TestFixture]
public class DateExtensionsTester
{
	[Test]
	public void Date_Creation_SameAsDateTime()
	{
		var birthday = 11.March(1977);
		var dtBirthday = new DateTime(1977, 3, 11);

		Assert.That(birthday, Is.EqualTo(dtBirthday));
	}

	[Test]
	public void Date_Creation_CorrectData()
	{
		Assert.That(2.January(2000), Is.DateWith(2000, 1, 2));
		Assert.That(3.February(2001), Is.DateWith(2001, 2, 3));
		Assert.That(4.March(2002), Is.DateWith(2002, 3, 4));
		Assert.That(5.April(2003), Is.DateWith(2003, 4, 5));
		Assert.That(6.May(2004), Is.DateWith(2004, 5, 6));
		Assert.That(7.June(2005), Is.DateWith(2005, 6, 7));
		Assert.That(8.July(2006), Is.DateWith(2006, 7, 8));
		Assert.That(9.August(2007), Is.DateWith(2007, 8, 9));
		Assert.That(10.September(2007), Is.DateWith(2007, 9, 10));
		Assert.That(20.October(2008), Is.DateWith(2008, 10, 20));
		Assert.That(28.November(2009), Is.DateWith(2009, 11, 28));
		Assert.That(29.December(2010), Is.DateWith(2010, 12, 29));
	}

	[Test]
	public void Date_CreateIncorrectDay_Exception()
	{
		int nonLeapYear = 2001;
		Assert.Throws<ArgumentOutOfRangeException>(() => 29.February(nonLeapYear));
	}
}
