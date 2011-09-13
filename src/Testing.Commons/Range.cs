using System;

namespace Testing.Commons
{
	internal class Range<T> where T : IComparable<T>
	{
		private readonly T _lowerBound;
		private readonly T _upperBound;

		public Range() { }

		public Range(T lowerBound, T upperBound)
		{
			assertBounds(lowerBound, upperBound);

			_lowerBound = lowerBound;
			_upperBound = upperBound;
		}

		private static bool checkBounds(T lowerBound, T upperBound)
		{
			return lowerBound.CompareTo(upperBound) <= 0;
		}

		public static void assertBounds(T lowerBound, T upperBound)
		{
			if (!checkBounds(lowerBound, upperBound)) throw new ArgumentOutOfRangeException("upperBound", upperBound, Resources.Exceptions.UnorderedRangeBounds);
		}

		public T LowerBound { get { return _lowerBound; } }

		public T UpperBound { get { return _upperBound; } }

		public virtual bool Contains(T item)
		{
			return item.CompareTo(_lowerBound) >= 0 && item.CompareTo(_upperBound) <= 0;
		}

		public override string ToString()
		{
			return string.Format("[{0}..{1}]", LowerBound, UpperBound);
		}
	}
}