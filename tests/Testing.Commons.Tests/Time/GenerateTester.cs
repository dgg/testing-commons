using Testing.Commons.Time;

namespace Testing.Commons.Tests.Time;

[TestFixture]
public class GenerateTester
{
	[Test]
	public void Single_BoundsUnordered_Exception()
	{
		DateTime start = 11.March(1978), end = 11.March(1977);

		Assert.That(() => Generate.Between(start).And(11.March(1977)).Single(),
			Throws.InstanceOf<ArgumentOutOfRangeException>()
				.With.Property("ParamName").EqualTo("to").And
				.Property("ActualValue").EqualTo(end).And
				.Message.Contain(start.ToString()));
	}

	[Test]
	public void Single_OrderedBounds_ADateBetweenBounds()
	{
		DateTime start = 11.March(1977), end = 11.March(1978);

		Assert.That(Generate.Between(start).And(end).Single(),
			Is.GreaterThanOrEqualTo(start).And.LessThanOrEqualTo(end));
	}

	[Test]
	public void Stream_BoundsUnordered_Exception()
	{
		DateTime start = 11.March(1978), end = 11.March(1977);

		Assert.That(() => Generate.Between(start).And(end).Stream().Iterate(),
			Throws.InstanceOf<ArgumentOutOfRangeException>()
				.With.Property("ParamName").EqualTo("to").And
				.Property("ActualValue").EqualTo(end).And
				.Message.Contain(start.ToString()));
	}

	[Test]
	public void Stream_OrderedBound_InfiniteSucession()
	{
		DateTime start = 11.March(1977), end = 11.March(1978);

		//NOTE: do limit the infinite stream or wait very long time ;-p
		Assert.That(Generate.Between(start).And(end).Stream().Take(10),
			Has.All.InRange(start, end));
	}

	[Test]
	public void Stream_OrderedBound_FiniteSucession()
	{
		DateTime start = 11.March(1977), end = 11.March(1978);

		Assert.That(Generate.Between(start).And(end).Stream(10).ToArray(),
			Has.Length.EqualTo(10).And
			.All.InRange(start, end));
	}
}
