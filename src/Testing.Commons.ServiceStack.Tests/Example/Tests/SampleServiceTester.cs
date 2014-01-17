using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using RestSharp;
using ServiceStack.WebHost.Endpoints;
using Testing.Commons.ServiceStack.v3;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure.Shared;

namespace Testing.Commons.Service_Stack.Tests.Example.Tests
{
	[TestFixture]
	public class SampleServiceTester : SingleHostPerFixture
	{
		protected override string ServiceName { get { return HostInfo.ServiceName; } }

		protected override IEnumerable<Assembly> AssembliesWithServices { get { return HostInfo.AssembliesWithServices; } }

		private IAppHostBootstrapper _bootstrapper;
		protected override void Boootstrap(IAppHost arg)
		{
			_bootstrapper = new AppHostBootstrapper();
			_bootstrapper.Bootstrap(arg);
		}

		// since the boostrapper implements IDisposable, we might want to call it
		protected override void OnHostDispose(bool disposing)
		{
			base.OnHostDispose(disposing);
			_bootstrapper.Dispose();
		}

		[Test]
		public void Echo_Something_SomethingReturned()
		{
			string text = "something";
			var client = new RestClient(BaseUrl.ToString());
			var request = new RestRequest("/echo", Method.GET);
			request.AddParameter("Text", text);

			var response = client.Execute<Dictionary<string, string>>(request);

			Assert.That(response.Data["Echoed"], Is.EqualTo(text));
		}
	}
}