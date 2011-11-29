using System;

namespace Testing.Commons.NUnit.Tests.Subjects.Equality.Offending
{
	internal class EqualsToNull : IEquatable<EqualsToNull>
	{
		public EqualsToNull(int value)
		{
			Value = value;
		}

		public int Value { get; private set; }

		public bool Equals(EqualsToNull other)
		{
			// NOTE: one should never be equal to NULL
			if (ReferenceEquals(null, other)) return true;

			if (ReferenceEquals(this, other)) return true;
			return other.Value == Value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(EqualsToNull)) return false;
			return Equals((EqualsToNull)obj);
		}

		public override int GetHashCode()
		{
			return Value;
		}
	}
}
