namespace Testing.Commons.NUnit.Tests.Subjects.Comparisons
{
	internal class ComparisonAgainstValue : NamedSubject
	{
		public ComparisonAgainstValue(string name) : base(name) { }

		public static bool GT, LT, GTOET, LTOET;

		public static bool operator >(ComparisonAgainstValue left, int right)
		{
			return GT;
		}

		public static bool operator <(ComparisonAgainstValue left, int right)
		{
			return LT;
		}

		public static bool operator >=(ComparisonAgainstValue left, int right)
		{
			return GTOET;
		}

		public static bool operator <=(ComparisonAgainstValue left, int right)
		{
			return LTOET;
		}
	}
}
