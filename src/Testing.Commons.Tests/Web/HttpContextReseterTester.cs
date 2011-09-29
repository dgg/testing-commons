using System.Web;
using NUnit.Framework;
using Testing.Commons.Web;

namespace Testing.Commons.Tests.Web
{
	[TestFixture]
	public class HttpContextReseterTester
	{
		[Test]
		public void Set_MakesContextAvailableInsideScope()
		{
			Assert.That(HttpContext.Current, Is.Null, "we are outside a web application");

			using (HttpContextReseter.Set(new HttpContextFactory(false)))
			{
				Assert.That(HttpContext.Current, Is.Not.Null, "as if we were inside a limited web application");
			}

			Assert.That(HttpContext.Current, Is.Null, "outside again :-(");
		}
	}
}
