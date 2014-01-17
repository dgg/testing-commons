using System;

namespace Testing.Commons.Service_Stack.Tests.Example.Services
{
	public class SampleDependency : IObserver<int>
	{
		public void OnNext(int value)
		{
			throw new Exception("real depdency called");
		}

		public void OnError(Exception error) { }

		public void OnCompleted() { }
	}
}