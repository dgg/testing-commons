using Testing.Commons.Time;

using Is = Testing.Commons.Tests.Time.Support.Is;

namespace Testing.Commons.Tests.Time;

[TestFixture]
public class OffsetExtensionsTester
{
	[Test]
	public void Offset_Creation_CorrectData()
	{
		DateTimeOffset dto = 11.March(1977).At(12, 30, 45).In(2.Hours());
		Assert.That(dto, Is.OffsetWith(1977, 3, 11, 12, 30, 45,
			offset: TimeSpan.FromHours(2)));

		dto = 11.March(1977).At(12, 30).In(2.Hours());
		Assert.That(dto, Is.OffsetWith(1977, 3, 11, 12, 30,
			offset: TimeSpan.FromHours(2)));

		dto = 11.March(1977).At(12).In(3.Hours());
		Assert.That(dto, Is.OffsetWith(1977, 3, 11, 12,
			offset: TimeSpan.FromHours(3)));

		dto = 11.March(1977).At().In(5.Hours());
		Assert.That(dto, Is.OffsetWith(1977, 3, 11,
			offset: TimeSpan.FromHours(5)));
	}

	[Test]
	public void Offset_InvalidOffset_Exception()
	{
		TimeSpan tooBig = 25.Hours();
		Assert.That(() => 11.March(1977).At().In(tooBig), Throws.InstanceOf<ArgumentOutOfRangeException>());
	}

	[Test]
	public void Offset_CreationOfUTC_CorrectData()
	{
		DateTimeOffset dto = 11.March(1977).At(12, 30, 45).InUtc();
		Assert.That(dto, Is.OffsetWith(1977, 3, 11, 12, 30, 45));

		dto = 11.March(1977).At(12, 30, 45).In(TimeSpan.Zero);
		Assert.That(dto, Is.OffsetWith(1977, 3, 11, 12, 30, 45));

		dto = 11.March(1977).At(12, 30, 45).In(new TimeSpan());
		Assert.That(dto, Is.OffsetWith(1977, 3, 11, 12, 30, 45));
	}
}
