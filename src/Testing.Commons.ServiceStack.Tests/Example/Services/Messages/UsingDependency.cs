using ServiceStack.ServiceHost;

namespace Testing.Commons.Service_Stack.Tests.Example.Services.Messages
{
	public class UsingDependency : IReturn<UsingDependencyResponse>
	{
		public int I { get; set; }
	}
}