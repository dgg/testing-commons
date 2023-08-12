namespace Testing.Commons.NUnit.Tests.Subjects;

// interfaces must to be public
public interface ISubjectWithDependencies
{
	void DoSomethingWithOne();
	void DoSomethingWithTwo();
	void DosomethingWithBoth();
}

// implementations can be internal
internal class SubjectWithDependencies : ISubjectWithDependencies
{
	private readonly IDependencyOne _one;
	private readonly IDependencyTwo _two;

	public SubjectWithDependencies(IDependencyOne one, IDependencyTwo two)
	{
		_one = one;
		_two = two;
	}

	public void DoSomethingWithOne()
	{
		_one.DoSomething();
	}

	public void DoSomethingWithTwo()
	{
		_two.DoSomethingElse();
	}

	public void DosomethingWithBoth()
	{
		_one.DoSomething();
		_two.DoSomethingElse();
	}
}

public interface IDependencyOne { void DoSomething(); }

public interface IDependencyTwo { void DoSomethingElse(); }
