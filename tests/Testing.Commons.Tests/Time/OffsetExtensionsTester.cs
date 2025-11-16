using Testing.Commons.Time;

using Iz = Testing.Commons.Tests.Time.Support.Iz;

namespace Testing.Commons.Tests.Time;

[TestFixture]
public class OffsetExtensionsTester
{
	[Test]
	public void Offset_Creation_CorrectData()
	{
		DateTimeOffset actual = 11.March(1977).At(12, 30, 45).In(2.Hours());
		var expected = new DateTimeOffset(1977, 3, 11, 12, 30, 45, 0, TimeSpan.FromHours(2));
		Assert.That(actual, Is.EqualTo(expected));

		Assert.That(11.March(1977).At(12, 30).In(2.Hours()), Iz
			.TimeWith(1977, 3, 11, 12, 30, offset: TimeSpan.FromHours(2)));

		Assert.That(11.March(1977).At(12).In(3.Hours()), Iz
			.TimeWith(1977, 3, 11, 12, offset: TimeSpan.FromHours(3)));

		Assert.That(11.March(1977).At().In(5.Hours()), Iz
			.TimeWith(1977, 3, 11, offset: TimeSpan.FromHours(5)));
	}

	[Test]
	public void Offset_InvalidOffset_Exception()
	{
		TimeSpan tooBig = 25.Hours();
		Assert.That(() =>
		{
			DateTimeOffset _ = 11.March(1977).At().In(tooBig);
		} , Throws.InstanceOf<ArgumentOutOfRangeException>());
	}

	[Test]
	public void Offset_CreationOfUTC_CorrectData()
	{
		Assert.That(11.March(1977).At(12, 30, 45).InUtc(),
			Iz.TimeWith(1977, 3, 11, 12, 30, 45), "UTC by default");

		Assert.That(11.March(1977).At(12, 30, 45).In(TimeSpan.Zero),
			Iz.TimeWith(1977, 3, 11, 12, 30, 45), "Explicit offset");

		Assert.That(11.March(1977).At(12, 30, 45).In(new TimeSpan()),
			Iz.TimeWith(1977, 3, 11, 12, 30, 45), "Also explicit");
	}
}
