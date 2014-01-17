using System;
using System.Collections.Generic;
using System.Reflection;
using ServiceStack.WebHost.Endpoints;

namespace Testing.Commons.ServiceStack.v3
{
	public abstract class HostTesterBase
	{
		private TestHost _host;
		protected TestHost Host { get { return _host; } }

		protected virtual ushort TestPort { get { return 49160; } }
		protected abstract string ServiceName { get; }
		protected abstract IEnumerable<Assembly> AssembliesWithServices { get; }
		protected abstract void Boootstrap(IAppHost arg);
		protected virtual void OnHostDispose(bool disposing) { }

		public Uri BaseUrl
		{
			get
			{
				return new Uri(string.Format("http://localhost:{0}/", TestPort));
			}
		}

		protected void StartHost()
		{
			_host = new TestHost(ServiceName, AssembliesWithServices, Boootstrap, OnHostDispose);
			_host.Init();

			_host.Start(BaseUrl.ToString());
		}

		protected void ShutdownHost()
		{
			_host.Stop();
			_host.Dispose();
			_host = null;
		}

		public HostTesterBase Replacing<T>(T dependency)
		{
			_host.Register(dependency);
			return this;
		}

		protected Uri Urifor(string restRelativeUri)
		{
			return new Uri(BaseUrl, restRelativeUri);
		}

		protected string UrlFor(string restRelativeUriTemplate, params string[] args)
		{
			string restRelativeUri = restRelativeUriTemplate;
			if (args != null && args.Length > 0)
			{
				restRelativeUri = string.Format(restRelativeUriTemplate, args);
			}

			return Urifor(restRelativeUri).ToString();
		}
	}
}