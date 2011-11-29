using System;

namespace Testing.Commons.NUnit.Tests.Subjects.Equality.Offending
{
	internal class NotEqualToItself : IEquatable<NotEqualToItself>
	{
		public NotEqualToItself(int value)
		{
			Value = value;
		}

		public int Value { get; private set; }

		public bool Equals(NotEqualToItself other)
		{
			if (ReferenceEquals(null, other)) return false;

			// NOTE: one should always be equals to oneself
			if (ReferenceEquals(this, other)) return false;
			return other.Value == Value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (NotEqualToItself)) return false;
			return Equals((NotEqualToItself) obj);
		}

		public override int GetHashCode()
		{
			return Value;
		}
	}
}
