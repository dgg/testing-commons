using System;
using NSubstitute;

namespace Testing.Commons.NUnit.Tests.Subjects.Comparisons
{
	public class ComparisonSubject<T, U>
	{
		private static readonly Func<T, U, bool> _noOp = (_, __) => false; 

		public Func<T, U, bool> GT = _noOp,
		                        LT = _noOp,
		                        GTOET = _noOp,
		                        LTOET = _noOp;

		public ComparisonSubject<T, U> Gt(T t, U u, bool result)
		{
			GT = substituteOrSame(GT);
			GT.Invoke(t, u).Returns(result);
			return this;
		}

		public ComparisonSubject<T, U> Gt(bool result)
		{
			GT = substituteOrSame(GT);
			GT.Invoke(Arg.Any<T>(), Arg.Any<U>()).Returns(result);
			return this;
		}

		public ComparisonSubject<T, U> Lt(T t, U u, bool result)
		{
			LT = LT = substituteOrSame(LT);
			LT.Invoke(t, u).Returns(result);
			return this;
		}

		public ComparisonSubject<T, U> Lt(bool result)
		{
			LT = substituteOrSame(LT);
			LT.Invoke(Arg.Any<T>(), Arg.Any<U>()).Returns(result);
			return this;
		}

		public ComparisonSubject<T, U> Gtoet(T t, U u, bool result)
		{
			GTOET = substituteOrSame(GTOET);
			GTOET.Invoke(t, u).Returns(result);
			return this;
		}
		
		public ComparisonSubject<T, U> Gtoet(bool result)
		{
			GTOET = substituteOrSame(GTOET);
			GTOET.Invoke(Arg.Any<T>(), Arg.Any<U>()).Returns(result);
			return this;
		}

		public ComparisonSubject<T, U> Ltoet(T t, U u, bool result)
		{
			LTOET = substituteOrSame(LTOET);
			LTOET.Invoke(t, u).Returns(result);
			return this;
		}

		public ComparisonSubject<T, U> Ltoet(bool result)
		{
			LTOET = substituteOrSame(LTOET);
			LTOET.Invoke(Arg.Any<T>(), Arg.Any<U>()).Returns(result);
			return this;
		}

		private Func<T, U, bool> substituteOrSame(Func<T, U, bool> op)
		{
			return (ReferenceEquals(op, _noOp)) ? Substitute.For<Func<T, U, bool>>() : op;
		}
	}
}