using System;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Testing.Commons.Service_Stack.Tests.Example.Services.Messages;

namespace Testing.Commons.Service_Stack.Tests.Example.Services
{
	public class SampleService : Service, IGet<Echo>, IGet<UsingDependency>
	{
		private readonly IObserver<int> _observer;

		public SampleService(IObserver<int> observer)
		{
			_observer = observer;
		}

		public object Get(Echo request)
		{
			return new EchoResponse { Echoed = request.Text };
		}


		public object Get(UsingDependency request)
		{
			_observer.OnNext(request.I);

			return new UsingDependencyResponse();
		}
	}
}