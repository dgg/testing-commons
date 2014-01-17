using System;
using ServiceStack.WebHost.Endpoints;

namespace Testing.Commons.Service_Stack.Tests.Example.Infrastructure.Shared
{
	// this is a simple way to share configuration in a centralized manner. Other, more complex ways,
	// are equally welcome.
	// Used by the application's AppHost as well as tests' TestHost
	// centralizes the configuration so that both AppHosts have the same properties.
	// Sometimes, the boostrapper has to be disposable
	public interface IAppBootstrapper : IDisposable
	{
		void Bootstrap<T>(T host) where T : IAppHost;
	}
}