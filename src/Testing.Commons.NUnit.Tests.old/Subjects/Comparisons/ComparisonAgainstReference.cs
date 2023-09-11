using System;

namespace Testing.Commons.NUnit.Tests.Subjects.Comparisons
{
	internal class ComparisonAgainstReference : NamedSubject
	{
		public ComparisonAgainstReference(string name) : base(name) { }

		public static bool operator >(ComparisonAgainstReference left, string right)
		{
			return _comparison.GT(left, right);
		}

		public static bool operator <(ComparisonAgainstReference left, string right)
		{
			return _comparison.LT(left, right);
		}

		public static bool operator >=(ComparisonAgainstReference left, string right)
		{
			return _comparison.GTOET(left, right);
		}

		public static bool operator <=(ComparisonAgainstReference left, string right)
		{
			return _comparison.LTOET(left, right);
		}

		public static bool operator >(string l, ComparisonAgainstReference r)
		{
			return _inverse.GT(l, r);
		}

		public static bool operator <(string l, ComparisonAgainstReference r)
		{
			return _inverse.LT(l, r);
		}

		public static bool operator >=(string l, ComparisonAgainstReference r)
		{
			return _inverse.GTOET(l, r);
		}

		public static bool operator <=(string l, ComparisonAgainstReference r)
		{
			return _inverse.LTOET(l, r);
		}

		private static ComparisonSubject<ComparisonAgainstReference, string> _comparison;
		public static void Setup(Action<ComparisonSubject<ComparisonAgainstReference, string>> comparison)
		{
			_comparison = new ComparisonSubject<ComparisonAgainstReference, string>();
			comparison(_comparison);
		}

		private static ComparisonSubject<string, ComparisonAgainstReference> _inverse;
		public static void SetupInverse(Action<ComparisonSubject<string, ComparisonAgainstReference>> comparison)
		{
			_inverse = new ComparisonSubject<string, ComparisonAgainstReference>();
			comparison(_inverse);
		}
	}
}