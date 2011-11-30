namespace Testing.Commons.NUnit.Tests.Subjects.Comparisons
{
	internal class ComparisonAgainstSelf : NamedSubject
	{
		public ComparisonAgainstSelf(string name) : base(name) { }

		public static bool GT, LT, GTOET, LTOET;

		public static bool operator >(ComparisonAgainstSelf left, ComparisonAgainstSelf right)
		{
			return GT;
		}

		public static bool operator <(ComparisonAgainstSelf left, ComparisonAgainstSelf right)
		{
			return LT;
		}

		public static bool operator >=(ComparisonAgainstSelf left, ComparisonAgainstSelf right)
		{
			return GTOET;
		}

		public static bool operator <=(ComparisonAgainstSelf left, ComparisonAgainstSelf right)
		{
			return LTOET;
		}
	}
}