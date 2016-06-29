using System;
using System.Collections.Generic;
using System.Reflection;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace Testing.Commons.Service_Stack.v3
{
	public abstract class HostTesterBase
	{
		private TestHost _host;
		protected TestHost Host => _host;

		protected virtual ushort TestPort => 49160;
		protected abstract string ServiceName { get; }
		protected abstract IEnumerable<Assembly> AssembliesWithServices { get; }
		protected abstract void Boootstrap(IAppHost arg);
		protected virtual void OnHostDispose(bool disposing) { }

		public Uri BaseUrl => new Uri($"http://localhost:{TestPort}/");

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

		protected Uri UriFor(string restRelativeUri)
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

			return UriFor(restRelativeUri).ToString();
		}

		protected string UrlFor(IReturn request, Http method)
		{
			return UriFor(request, method).ToString();
		}

		protected Uri UriFor(IReturn request, Http method)
		{
			string json = EndpointHostConfig.Instance.ServiceEndpointsMetadataConfig.Json.Format;

			string url = request.ToUrl(method.ToString().ToUpperInvariant(), "");
			url = url.Replace("//", "/" + json + "/");
			return UriFor(url);
		}
	}
}