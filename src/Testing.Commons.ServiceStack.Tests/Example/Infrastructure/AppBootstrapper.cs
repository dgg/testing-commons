using System;
using System.Collections.Generic;
using ServiceStack.Configuration;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure.Shared;

namespace Testing.Commons.Service_Stack.Tests.Example.Infrastructure
{
	// used by the application's AppHost as well as tests' TestHost
	// centralizes the configuration so that both AppHosts have the same properties.
	// Sometimes, the boostrapper has to be disposable
	public class AppBootstrapper : IAppBootstrapper
	{

		// The method that will be called from the AppHosts in order to get themsselvesconfigured
		public void Bootstrap<T>(T host) where T : IAppHost
		{
			bootstrap()
				.bootstrap(host.Plugins)
				.bootstrap(host.RequestFilters, host.ResponseFilters)
				.bootstrap(host.GetContainer())
				// more methods
				.endBootstrapping(host.Config);
		}

		// global configurations
		private AppBootstrapper bootstrap()
		{
			// examples would be:
			// * configuring serialization
			// * configuring singletons: LogManager,...

			return this;
		}

		// configure plugins
		private AppBootstrapper bootstrap(IList<IPlugin> plugins)
		{
			return this;
		}

		// configure filters
		private AppBootstrapper bootstrap(
						List<Action<IHttpRequest, IHttpResponse, object>> requestFilters,
						List<Action<IHttpRequest, IHttpResponse, object>> responseFilters)
		{

			return this;
		}

		// configure IOC
		private AppBootstrapper bootstrap(Funq.Container container, IContainerAdapter adapter = null)
		{
			return this;
		}

		#region more bootstrap overloads that configure different application parameters

		// private AppBootstrapper bootstrap(IContentTypeFilter filter)
		// ...

		#endregion

		// finish configuration. Last method in the chain
		private void endBootstrapping(EndpointHostConfig config)
		{
			// do whatever is needed in the configuration
		}

		public void Dispose()
		{
		}
	}
}