namespace Testing.Commons.NUnit.Tests.Subjects.Comparisons
{
	internal class ComparisonAgainstReference : NamedSubject
	{
		public ComparisonAgainstReference(string name) : base(name) { }

		public static bool GT, LT, GTOET, LTOET;

		public static bool operator >(ComparisonAgainstReference left, string right)
		{
			return GT;
		}

		public static bool operator <(ComparisonAgainstReference left, string right)
		{
			return LT;
		}

		public static bool operator >=(ComparisonAgainstReference left, string right)
		{
			return GTOET;
		}

		public static bool operator <=(ComparisonAgainstReference left, string right)
		{
			return LTOET;
		}

		public static bool operator >(string right, ComparisonAgainstReference left)
		{
			return GT;
		}

		public static bool operator <(string right, ComparisonAgainstReference left)
		{
			return LT;
		}

		public static bool operator >=(string right, ComparisonAgainstReference left)
		{
			return GTOET;
		}

		public static bool operator <=(string right, ComparisonAgainstReference left)
		{
			return LTOET;
		}
	}
}