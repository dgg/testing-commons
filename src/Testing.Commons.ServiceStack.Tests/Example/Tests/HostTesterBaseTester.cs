using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using RestSharp;
using ServiceStack.WebHost.Endpoints;
using Testing.Commons.ServiceStack.v3;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure.Shared;

namespace Testing.Commons.Service_Stack.Tests.Example.Tests
{
	// tests for shared functionality, personalized by SingleHostPerFixture
	public class HostTesterBaseTester : SingleHostPerFixture
	{
		protected override string ServiceName { get { return HostInfo.ServiceName; } }

		protected override IEnumerable<Assembly> AssembliesWithServices { get { return HostInfo.AssembliesWithServices; } }

		protected override void Boootstrap(IAppHost arg)
		{
			// normally inside a shared IAppBootstrapper of some kind
			arg.Config.PostExecuteServiceFilter = (dto, request, response) =>
			{
				if (response.StatusCode == 200) response.StatusDescription = "SUPER GREEN";
			};
		}

		[Test]
		public void AllowsSharingConfiguration_BetweenImplementation_AndTests()
		{
			string text = "something";
			var client = new RestClient(BaseUrl.ToString());
			var request = new RestRequest("/echo", Method.GET);
			request.AddParameter("Text", text);

			var response = client.Execute(request);

			Assert.That(response.StatusDescription, Is.EqualTo("SUPER GREEN"));
		}		
	}
}