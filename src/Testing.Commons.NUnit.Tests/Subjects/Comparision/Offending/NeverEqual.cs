using System;
using NSubstitute;

namespace Testing.Commons.NUnit.Tests.Subjects.Comparision.Offending
{
	internal class NamedSubject
	{
		private readonly string _name;

		public NamedSubject(string name)
		{
			_name = name;
		}

		public override string ToString()
		{
			return _name;
		}
	}

	internal class NeverEqual : NamedSubject, IComparable<NeverEqual>
	{
		public NeverEqual(string name) : base(name) { }

		public int CompareTo(NeverEqual other)
		{
			return int.MinValue;
		}
	}

	internal class NeverLess : NamedSubject, IComparable<NeverLess>
	{
		public NeverLess(string name) : base(name) { }

		public int CompareTo(NeverLess other)
		{
			if (ReferenceEquals(this, other)) return 0;
			return int.MaxValue;
		}
	}

	internal class NeverGreater : NamedSubject, IComparable<NeverGreater>
	{
		public NeverGreater(string name) : base(name) { }

		public int CompareTo(NeverGreater other)
		{
			if (ReferenceEquals(this, other)) return 0;
			return int.MinValue;
		}
	}

	internal class ComparableSubject<T> : NamedSubject, IComparable<T>
	{
		private readonly IComparable<T> _inner;

		public ComparableSubject(string name) : base(name)
		{
			_inner = Substitute.For<IComparable<T>>();
		}

		public ComparableSubject<T> Setup(T other, int result)
		{
			_inner.CompareTo(other).Returns(result);
			return this;
		}

		public int CompareTo(T other)
		{
			return _inner.CompareTo(other);
		}
	}
}
