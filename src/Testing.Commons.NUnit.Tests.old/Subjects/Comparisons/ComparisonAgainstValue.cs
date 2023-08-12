using System;

namespace Testing.Commons.NUnit.Tests.Subjects.Comparisons
{
	internal class ComparisonAgainstValue : NamedSubject
	{
		public ComparisonAgainstValue(string name) : base(name) { }

		public static bool operator >(ComparisonAgainstValue left, int right)
		{
			return _comparison.GT(left, right);
		}

		public static bool operator <(ComparisonAgainstValue left, int right)
		{
			return _comparison.LT(left, right);
		}

		public static bool operator >=(ComparisonAgainstValue left, int right)
		{
			return _comparison.GTOET(left, right);
		}

		public static bool operator <=(ComparisonAgainstValue left, int right)
		{
			return _comparison.LTOET(left, right);
		}

		private static ComparisonSubject<ComparisonAgainstValue, int> _comparison;
		public static void Setup(Action<ComparisonSubject<ComparisonAgainstValue, int>> comparison)
		{
			_comparison = new ComparisonSubject<ComparisonAgainstValue, int>();
			comparison(_comparison);
		}
	}
}
