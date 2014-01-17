using ServiceStack.ServiceHost;

namespace Testing.Commons.Service_Stack.Tests.Example.Services.Messages
{
	[Route("/echo")]
	public class Echo : IReturn<EchoResponse>
	{
		public string Text { get; set; }
	}
}