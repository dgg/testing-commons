using System;

namespace Testing.Commons.NUnit.Tests.Subjects.Comparisons
{
	internal class ComparisonAgainstSelf : NamedSubject
	{
		public ComparisonAgainstSelf(string name) : base(name) { }

		public static bool operator >(ComparisonAgainstSelf left, ComparisonAgainstSelf right)
		{
			return _comparison.GT(left, right);
		}

		public static bool operator <(ComparisonAgainstSelf left, ComparisonAgainstSelf right)
		{
			return _comparison.LT(left, right);
		}

		public static bool operator >=(ComparisonAgainstSelf left, ComparisonAgainstSelf right)
		{
			return _comparison.GTOET(left, right);
		}

		public static bool operator <=(ComparisonAgainstSelf left, ComparisonAgainstSelf right)
		{
			return _comparison.LTOET(left, right);
		}

		private static ComparisonSubject<ComparisonAgainstSelf, ComparisonAgainstSelf> _comparison;
		public static void Setup(Action<ComparisonSubject<ComparisonAgainstSelf, ComparisonAgainstSelf>> comparison)
		{
			_comparison = new ComparisonSubject<ComparisonAgainstSelf, ComparisonAgainstSelf>();
			comparison(_comparison);
		}
	}
}