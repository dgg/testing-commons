using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons.Time;

namespace Testing.Commons.Tests
{
	[TestFixture]
	public class RangeTester
	{
		[Test]
		public void Ctor_SetsProperties()
		{
			var subject = new Range<char>('a', 'z');
			Assert.That(subject.LowerBound, Is.EqualTo('a'));
			Assert.That(subject.UpperBound, Is.EqualTo('z'));
		}

		[Test, Culture("da-DK")]
		public void Ctor_PoorlyConstructed_Exception()
		{
			Assert.That(() => new Range<int>(5, 1), throwsBoundException(1, "1"));

			Assert.That(() => new Range<int>(-1, -5), throwsBoundException(-5, "-5"));

			Assert.That(() => new Range<TimeSpan>(3.Seconds(), 2.Seconds()), throwsBoundException(2.Seconds(), "00:00:02"));

			Assert.That(() => new Range<DateTime>(11.March(1977), 31.October(1952)), throwsBoundException(31.October(1952), "31-10-1952 00:00:00"));
		}

		[Test]
		public void Ctor_NicelyConstructed_NoException()
		{
			Assert.That(() => new Range<int>(1, 5), Throws.Nothing);

			Assert.That(() => new Range<int>(-5, -1), Throws.Nothing);

			Assert.That(() => new Range<TimeSpan>(2.Seconds(), 3.Seconds()), Throws.Nothing);

			Assert.That(() => new Range<DateTime>(31.October(1952), 11.March(1977)), Throws.Nothing);
		}

		private static Constraint throwsBoundException<T>(T upperBound, string upperBoundRepresentation)
		{
			return Throws.InstanceOf<ArgumentOutOfRangeException>().With
				.Property("ActualValue").EqualTo(upperBound)
				.And.Message.Contain(upperBoundRepresentation);
		}

		[Test]
		public void Contains_Integers_ContainedAndNotContained()
		{
			Assert.That(new Range<int>(1, 5).Contains(3), Is.True);
			Assert.That(new Range<int>(-5, -1).Contains(3), Is.False);
			Assert.That(new Range<int>(-5, -1).Contains(-4), Is.True);
			Assert.That(new Range<int>(-1, 1).Contains(0), Is.True);
			Assert.That(new Range<int>(-5, -1).Contains(3), Is.False);
		}

		[Test]
		public void Contains_AsOpenRange()
		{
			Assert.That(new Range<int>(1, 5).Contains(1), Is.True);
			Assert.That(new Range<int>(1, 5).Contains(5), Is.True);
		}

		[Test]
		public void Contains_Dates_ContainedAndNotContained()
		{
			Range<DateTime> ww2Period = new Range<DateTime>(3.September(1939), 2.September(1945));
			Assert.That(ww2Period.Contains(1.January(1940)), Is.True);
			Assert.That(ww2Period.Contains(1.January(1980)), Is.False);
			Assert.That(ww2Period.Contains(2.September(1939).At(12, 59, 59, 999)), Is.False);
		}

		[Test]
		public void ToString_ContainsRepresentationOfBounds()
		{
			Assert.That(new Range<int>(-5, -1).ToString(), Is.EqualTo("[-5..-1]"));

			Assert.That(new Range<TimeSpan>(2.Seconds(), 3.Seconds()).ToString(), Is.EqualTo("[00:00:02..00:00:03]"));
		}
	}
}
