using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace Testing.Commons.Service_Stack.Tests.Example.Services.Messages
{
	[Route("/decorated")]
	public class RouteDecorated : IReturn { }

	[Route("/decorated/{param}")]
	public class RouteDecoratedWithParams : IReturnVoid
	{
		public string Param { get; set; }
	}

	public class NotDecorated : IReturnVoid { }

	public class NotDecoratedResponses {
		public string S { get; set; } }

	public class Srv : Service, IGet<RouteDecoratedWithParams>
	{
		public object Get(RouteDecoratedWithParams request)
		{
			return 7;
		}
	}
}