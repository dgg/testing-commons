using Subject = Testing.Commons.StringGenerator;

namespace Testing.Commons.Tests;

[TestFixture]
public class StringGeneratorTester
{
	[Test]
	public void Numeric_Zero_Empty()
	{
		Assert.That(Subject.Numeric(0), Is.Empty);
	}

	[Test]
	public void Numeric_ShorterThanPattern_IncompletePattern()
	{
		Assert.That(Subject.Numeric(2), Is.EqualTo("01"));
	}

	[Test]
	public void Numeric_AsLongAsPattern_Pattern()
	{
		Assert.That(Subject.Numeric(10), Is.EqualTo("0123456789"));
	}

	[Test]
	public void Numeric_LongerThanPattern_RepeatedPattern()
	{
		Assert.That(Subject.Numeric(23), Is.EqualTo("01234567890123456789012"));
	}

	[Test]
	public void RepeatPattern_Zero_Empty()
	{
		Assert.That(Subject.RepeatPattern("abc", 0), Is.Empty);
	}

	[Test]
	public void RepeatPattern_ShorterThanPattern_IncompletePattern()
	{
		Assert.That(Subject.RepeatPattern("abc", 2), Is.EqualTo("ab"));
	}

	[Test]
	public void RepeatPattern_AsLongAsPattern_Pattern()
	{
		Assert.That(Subject.RepeatPattern("abc", 3), Is.EqualTo("abc"));
	}

	[Test]
	public void RepeatPattern_LongerThanPattern_RepeatedPattern()
	{
		Assert.That(Subject.RepeatPattern("abc", 5), Is.EqualTo(
			"abcab"));
	}

	#region documentation

	[Test]
	public void Documentation_Wiki_RepeatPattern()
	{
		Assert.That(Subject.RepeatPattern("abc", 5), Is.EqualTo("abcab"));
		Assert.That(Subject.RepeatPattern("abc", 2), Is.EqualTo("ab"));
	}

	[Test]
	public void Documentation_Wiki_Numeric()
	{
		Assert.That(Subject.Numeric(3), Is.EqualTo("012"));
		Assert.That(Subject.Numeric(23), Is.EqualTo("01234567890123456789012"));
	}

	#endregion
}
