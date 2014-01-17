using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Testing.Commons.Service_Stack.Tests.Example.Services.Messages;

namespace Testing.Commons.Service_Stack.Tests.Example.Services
{
	public class SampleService : Service, IGet<Echo>
	{
		public object Get(Echo request)
		{
			return new EchoResponse { Echoed = request.Text };
		}
	}
}