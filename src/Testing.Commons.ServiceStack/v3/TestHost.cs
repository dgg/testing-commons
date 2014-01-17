using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Funq;
using ServiceStack.WebHost.Endpoints;

namespace Testing.Commons.ServiceStack.v3
{
	public class TestHost : AppHostHttpListenerBase
	{
		private readonly Action<IAppHost> _bootstrap;
		private readonly Action<bool> _onDispose;

		public TestHost(string serviceName, IEnumerable<Assembly> assembliesWithServices,
			Action<IAppHost> bootstrap, Action<bool> onDispose)
			: base(serviceName, assembliesWithServices.ToArray())
		{
			if (bootstrap == null) throw new ArgumentNullException("bootstrap");

			_bootstrap = bootstrap;
			_onDispose = onDispose;
		}

		public override void Configure(Container container)
		{
			_bootstrap(this);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (_onDispose != null) _onDispose(disposing);
		}
	}
}