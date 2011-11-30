using System;
using NSubstitute;

namespace Testing.Commons.NUnit.Tests.Subjects.Comparisons
{
	public class ComparableSubject : NamedSubject, IComparable<ComparableSubject>
	{
		private readonly IComparable<ComparableSubject> _inner;
		public ComparableSubject(string name) : base(name)
		{
			_inner = Substitute.For<IComparable<ComparableSubject>>();
		}

		public ComparableSubject Setup(ComparableSubject other, int result)
		{
			_inner.CompareTo(other).Returns(result);
			return this;
		}

		public int CompareTo(ComparableSubject other)
		{
			return _inner.CompareTo(other);
		}
	}

	internal class ComparableSubject<T> : NamedSubject, IComparable<T>
	{
		private readonly IComparable<T> _inner;

		public ComparableSubject(string name)
			: base(name)
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