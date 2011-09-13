using NUnit.Framework;

namespace Testing.Commons.Tests
{
	[TestFixture]
	public class StringGeneratorTester
	{
		[Test]
		public void Numeric_Zero_Empty()
		{
			var subject = new StringGenerator();
			Assert.That(subject.Numeric(0), Is.Empty);
		}

		[Test]
		public void Numeric_ShorterThanPattern_IncompletePattern()
		{
			var subject = new StringGenerator();
			Assert.That(subject.Numeric(2), Is.EqualTo("01"));
		}

		[Test]
		public void Numeric_AsLongAsPattern_Pattern()
		{
			var subject = new StringGenerator();
			Assert.That(subject.Numeric(10), Is.EqualTo("0123456789"));
		}

		[Test]
		public void Numeric_LongerThanPattern_RepeatedPattern()
		{
			var subject = new StringGenerator();
			Assert.That(subject.Numeric(23), Is.EqualTo(
				"01234567890123456789012"));
		}

		[Test]
		public void RepeatPattern_Zero_Empty()
		{
			var subject = new StringGenerator();
			Assert.That(subject.RepeatPattern("abc", 0), Is.Empty);
		}

		[Test]
		public void RepeatPattern_ShorterThanPattern_IncompletePattern()
		{
			var subject = new StringGenerator();
			Assert.That(subject.RepeatPattern("abc", 2), Is.EqualTo("ab"));
		}

		[Test]
		public void RepeatPattern_AsLongAsPattern_Pattern()
		{
			var subject = new StringGenerator();
			Assert.That(subject.RepeatPattern("abc", 3), Is.EqualTo("abc"));
		}

		[Test]
		public void RepeatPattern_LongerThanPattern_RepeatedPattern()
		{
			var subject = new StringGenerator();
			Assert.That(subject.RepeatPattern("abc", 5), Is.EqualTo(
				"abcab"));
		}
	}
}
