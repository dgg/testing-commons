using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure;

namespace Testing.Commons.Service_Stack.Tests.Example.Tests
{
	[TestFixture]
	public class SanityChecker
	{
		[Test]
		public void SanityCheck_AgainstDocumentationConsoleHostSample()
		{
			using (var host = new ApplicationsAppHost())
			{
				
				host.Init();
				string hostUrl = "http://localhost:1337/";
				host.Start(hostUrl);

				string text = "something";
				var client = new RestClient(hostUrl);
				var request = new RestRequest("/echo", Method.GET);
				request.AddParameter("Text", text);

				var response = client.Execute<Dictionary<string, string>>(request);
				
				Assert.That(response.Data["Echoed"], Is.EqualTo(text));

				host.Stop();
			}
		}
	}
}