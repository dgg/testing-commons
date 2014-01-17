using Funq;
using ServiceStack.WebHost.Endpoints;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure.Shared;

namespace Testing.Commons.Service_Stack.Tests.Example.Infrastructure
{
	// this is the application AppHost
	public class ApplicationsAppHost : // it would usually derive from AppHostBase
		// but since we are hosting it in a "console" for sanity, derives from
		AppHostHttpListenerBase 
	{
		private readonly AppHostBootstrapper _bootstrapper;

		public ApplicationsAppHost() : base(HostInfo.ServiceName, HostInfo.AssembliesWithServices)
		{
			_bootstrapper =  new AppHostBootstrapper();
		}

		public override void Configure(Container container)
		{
			_bootstrapper.Bootstrap(this);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			_bootstrapper.Dispose();
		}

		// if it was a normal hosted in IIS service, we would override plain Dispose()
		/*public override void Dispose()
		{
			base.Dispose();
			_bootstrapper.Dispose();
		}*/
		
	}
}