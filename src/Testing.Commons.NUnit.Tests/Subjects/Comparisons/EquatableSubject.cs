using System;
using NSubstitute;

namespace Testing.Commons.NUnit.Tests.Subjects.Comparisons
{
	public class EquatableSubject : NamedSubject, IEquatable<EquatableSubject>
	{
		private readonly IEquatable<EquatableSubject> _inner;
		public EquatableSubject(string name) : base(name)
		{
			_inner = Substitute.For<IEquatable<EquatableSubject>>();
		}

		public bool Equals(EquatableSubject other)
		{
			return _inner.Equals(other);
		}

		public EquatableSubject Setup(EquatableSubject other, bool result)
		{
			_inner.Equals(other).Returns(result);
			return this;
		}
	}

	internal class EquatableSubject<T> : NamedSubject, IEquatable<T>
	{
		private readonly IEquatable<T> _inner;

		public EquatableSubject(string name)
			: base(name)
		{
			_inner = Substitute.For<IEquatable<T>>();
		}

		public EquatableSubject<T> Setup(T other, bool result)
		{
			_inner.Equals(other).Returns(result);
			return this;
		}

		public bool Equals(T other)
		{
			return _inner.Equals(other);
		}
	}
}