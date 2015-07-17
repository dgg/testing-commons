using System;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using RestSharp;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;
using Testing.Commons.Service_Stack.v3;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure.Shared;
using Testing.Commons.Service_Stack.Tests.Example.Services.Messages;
using Http = ServiceStack.ServiceHost.Http;

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
			using (var client = new JsonServiceClient())
			{
				Uri serviceUri = UriFor("/echo?Text=" + text);

				var response = client.Get<EchoResponse>(serviceUri.ToString());
				Assert.That(response.Echoed, Is.EqualTo(text));
			}
		}

		[Test]
		public void UriFor_RouteDecoratedDto_AbsoluteUri()
		{
			IReturn request = new RouteDecorated();

			Uri u = UriFor(request, Http.Get);

			Assert.That(u.IsAbsoluteUri, Is.True);
			Assert.That(u.LocalPath, Is.EqualTo("/decorated"));
		}

		[Test]
		public void UriFor_RouteDecoratedWithParamsDto_AbsoluteUri()
		{
			IReturn request = new RouteDecoratedWithParams(){ Param = "asd"};

			Uri u = UriFor(request, Http.Get);

			Assert.That(u.IsAbsoluteUri, Is.True);
			Assert.That(u.Host, Is.EqualTo("localhost"));
			Assert.That(u.Port, Is.EqualTo(base.TestPort));

			Assert.That(u.LocalPath, Is.EqualTo("/decorated/asd"));
		}

		[Test]
		public void UriFor_NotDecoreated_AbsoluteUri()
		{
			/*string text = "something";
			var client = new RestClient(BaseUrl.ToString());
			var request = new RestRequest("/json/metadata?op=RouteDecoratedWithParams", Method.GET);

			var response = client.Execute(request);

			Assert.That(response.Content, Is.Empty*/



			var request = new NotDecorated();

			Uri u = UriFor(request, Http.Get);

			Assert.That(u.IsAbsoluteUri, Is.True);
			Assert.That(u.Host, Is.EqualTo("localhost"));
			Assert.That(u.Port, Is.EqualTo(base.TestPort));

			Assert.That(u.LocalPath, Is.EqualTo("/notdecorated"));
		}
	}
}