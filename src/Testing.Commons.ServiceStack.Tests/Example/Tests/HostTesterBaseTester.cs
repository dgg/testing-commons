using System;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using RestSharp;
using ServiceStack.ServiceClient.Web;
using ServiceStack.WebHost.Endpoints;
using Testing.Commons.ServiceStack.v3;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure.Shared;
using Testing.Commons.Service_Stack.Tests.Example.Services.Messages;

namespace Testing.Commons.Service_Stack.Tests.Example.Tests
{
	// tests for shared functionality, personalized by SingleHostPerFixture
	public class HostTesterBaseTester : SingleHostPerFixture
	{
		protected override string ServiceName { get { return HostInfo.ServiceName; } }

		protected override IEnumerable<Assembly> AssembliesWithServices { get { return HostInfo.AssembliesWithServices; } }

		protected override void Boootstrap(IAppHost arg)
		{
			new AppHostBootstrapper().Bootstrap(arg);

			// normally inside a shared IHostBootstrapper of some kind
			arg.Config.PostExecuteServiceFilter = (dto, request, response) =>
			{
				if (response.StatusCode == 200) response.StatusDescription = "SUPER GREEN";
			};
		}

		[Test]
		public void Bootstrap_AllowsSharingConfiguration_BetweenImplementationAndTests()
		{
			string text = "something";
			var client = new RestClient(BaseUrl.ToString());
			var request = new RestRequest("/echo", Method.GET);
			request.AddParameter("Text", text);

			var response = client.Execute(request);

			Assert.That(response.StatusDescription, Is.EqualTo("SUPER GREEN"));
		}

		protected override ushort TestPort { get { return 49160; } }

		[Test]
		public void TestPort_AllowsChangingPort_InCaseOfConflictInTheMachineRunningTests()
		{
			var client = new RestClient(BaseUrl.ToString());
			var request = new RestRequest("/echo", Method.GET);

			var response = client.Execute(request);
			var uri = client.BuildUri(response.Request);

			Assert.That(uri.Port, Is.EqualTo(49160));
		}

		[Test]
		public void Replacing_ADependency_BehaviorOfReplacementInvoked()
		{
			var substitute = Substitute.For<IObserver<int>>();
			Replacing(substitute);

			using (var client = new JsonServiceClient(BaseUrl.ToString()))
			{
				client.Get(new UsingDependency {I = 42});
			}

			substitute.Received(1).OnNext(42);
		}

		[Test]
		public void UriFor_ServiceUrls_AreEasier()
		{
			string text = "something";
			using (var client = new JsonServiceClient(BaseUrl.ToString()))
			{
				var serviceUri = Urifor("/echo?Text=" + text);

				var response = client.Get<EchoResponse>(serviceUri.ToString());
				Assert.That(response.Echoed, Is.EqualTo(text));
			}
		}
	}
}