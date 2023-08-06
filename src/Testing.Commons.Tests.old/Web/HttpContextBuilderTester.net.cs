using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using NUnit.Framework;
using Testing.Commons.Web;

namespace Testing.Commons.Tests.Web
{
	[TestFixture]
	public class HttpContextBuilderTester
	{
		#region Session

		[Test]
		public void Session_StartsEmpty()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			Assert.That(subject.Session, Is.Empty);
		}

		[Test]
		public void Session_ElementsCanBeSet_InAdvance()
		{
			HttpContext subject = new HttpContextBuilder()
				.AddToSession("a", 1)
				.AddToSession("b", 2)
				.Context;

			Assert.That(subject.Session["a"], Is.EqualTo(1));
			Assert.That(subject.Session["b"], Is.EqualTo(2));
		}

		[Test]
		public void Session_ElementsCanBeAdded_AfterCreation()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			subject.Session["a"] = 1;
			Assert.That(subject.Session["a"], Is.EqualTo(1));
		}

		[Test]
		public void Session_ElementsCanBeRemoved_WhenSetInAdvance()
		{
			HttpContext subject = new HttpContextBuilder()
				.AddToSession("a", 1)
				.Context;
			subject.Session.Remove("a");
			Assert.That(subject.Session, Has.No.Member("a"));
		}

		[Test]
		public void Session_ElementsCanBeRemoved_WhenSetAfterCreation()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			subject.Session["b"] = 2;
			subject.Session.Remove("b");
			Assert.That(subject.Session, Has.No.Member("b"));
		}

		#endregion

		#region Items

		[Test]
		public void Items_DoesNotStartEmpty()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			Assert.That(subject.Items, Has.Count.EqualTo(1), "There is always the session state");
		}

		[Test]
		public void Items_ElementsCanBeSet_InAdvance()
		{
			var massInit = new Hashtable { { "3", 3 }, { 4, "4" } };

			HttpContext subject = new HttpContextBuilder()
				.AddToItems("1", 1)
				.AddToItems(2, "2")
				.AddToItems(massInit)
				.Context;

			Assert.That(subject.Items["1"], Is.EqualTo(1));
			Assert.That(subject.Items[2], Is.EqualTo("2"));
			Assert.That(subject.Items["3"], Is.EqualTo(3));
			Assert.That(subject.Items[4], Is.EqualTo("4"));
		}

		[Test]
		public void Items_ElementsCanBeAdded_AfterCreation()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			subject.Items["a"] = 1;
			Assert.That(subject.Items["a"], Is.EqualTo(1));
		}

		[Test]
		public void Items_ElementsCanBeRemoved_WhenSetInAdvance()
		{
			HttpContext subject = new HttpContextBuilder()
				.AddToItems("a", 1)
				.Context;
			subject.Items.Remove("a");
			Assert.That(subject.Items["a"], Is.Null);
		}

		[Test]
		public void Items_ElementsCanBeRemoved_WhenSetAfterCreation()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			subject.Items.Add("b", 2);
			subject.Items.Remove("b");
			Assert.That(subject.Items, Has.No.Contains("b"));
		}

		#endregion

		#region Application

		[Test]
		public void Application_DoesNotStartEmpty()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			Assert.That(subject.Application, Is.Empty);
		}

		[Test]
		public void Application_ElementsCanBeSet_InAdvance()
		{
			HttpContext subject = new HttpContextBuilder()
				.AddToApplication("1", 1)
				.AddToApplication("2", "2")
				.Context;

			Assert.That(subject.Application["1"], Is.EqualTo(1));
			Assert.That(subject.Application["2"], Is.EqualTo("2"));
		}

		[Test]
		public void Application_ElementsCanBeAdded_AfterCreation()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			subject.Application["a"] = 1;
			Assert.That(subject.Application["a"], Is.EqualTo(1));
		}

		[Test]
		public void Application_ElementsCanBeRemoved_WhenSetInAdvance()
		{
			HttpContext subject = new HttpContextBuilder()
				.AddToApplication("a", 1)
				.Context;
			subject.Application.Remove("a");
			Assert.That(subject.Application["a"], Is.Null);
		}

		[Test]
		public void Application_ElementsCanBeRemoved_WhenSetAfterCreation()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			subject.Application.Add("b", 2);
			subject.Application.Remove("b");
			Assert.That(subject.Application, Has.No.Contains("b"));
		}

		#endregion

		#region Response.Output

		[Test]
		public void Output_CanBeAccessed_AfterResponseEnd()
		{
			string written = "written";
			StringBuilder sb = new StringBuilder();

			HttpContext context = new HttpContextBuilder()
				.OuputWrittenTo(sb)
				.Context;

			context.Response.Write(written);
			context.Response.Flush();

			Assert.That(sb.ToString(), Is.EqualTo(written));
		}

		#endregion

		#region Request.IsSecure

		[Test]
		public void Requests_NotSecure_ByDefault()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(subject.Request.IsSecureConnection, Is.False);
			Assert.That(subject.Request.Url.Scheme, Is.EqualTo(Uri.UriSchemeHttp));
		}

		[Test]
		public void Requests_CanBeMade_Secure()
		{
			HttpContext subject = new HttpContextBuilder().Request.MakeSecure().Context;
			Assert.That(subject.Request.IsSecureConnection, Is.True);
			Assert.That(subject.Request.Url.Scheme, Is.EqualTo(Uri.UriSchemeHttps));
		}

		[Test]
		public void Requests_CanBeMade_Secure_ByPassingSecureUrl()
		{
			HttpContext subject = new HttpContextBuilder().Request.WithUrl(new Uri("https://www.secure.org")).Context;
			Assert.That(subject.Request.IsSecureConnection, Is.True);
			Assert.That(subject.Request.Url.Scheme, Is.EqualTo(Uri.UriSchemeHttps));
		}

		#endregion

		#region Request.Url.UrlReferrer

		[Test]
		public void UrlReferrer_IsNullByDefault()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			Assert.That(subject.Request.UrlReferrer, Is.Null);
		}

		[Test]
		public void UrlReferrer_CanBeSet_InAdvance()
		{
			Uri referrer = new Uri("http://www.google.com");
			HttpContext subject = new HttpContextBuilder().Request.WithReferrer(referrer).Context;

			Assert.That(subject.Request.UrlReferrer, Is.EqualTo(referrer));
		}

		#endregion

		#region Request.QueryString

		[Test]
		public void QueryString_StartsEmpty()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(subject.Request.QueryString, Is.Empty);
		}

		[Test]
		public void QueryString_CannotAddItems()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(() => subject.Request.QueryString.Add("something", "else"), Throws.InstanceOf<NotSupportedException>());
		}

		[Test]
		public void QueryString_CannotRemoveItems()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(() => subject.Request.QueryString.Remove("something"), Throws.InstanceOf<NotSupportedException>());
		}

		[Test]
		public void QueryString_CanBeInitialized()
		{
			NameValueCollection massInitialization = new NameValueCollection()
			{
				{"key3", "value3"},
				{"key4", "value4"}
			};
			HttpContext subject = new HttpContextBuilder().Request
				.AddToQueryString("key1", "value1")
				.AddToQueryString("key2", "value2")
				.AddToQueryString(massInitialization)
				.Context;

			Assert.That(subject.Request.QueryString["key1"], Is.EqualTo("value1"));
			Assert.That(subject.Request.QueryString["key2"], Is.EqualTo("value2"));
			Assert.That(subject.Request.QueryString["key3"], Is.EqualTo("value3"));
			Assert.That(subject.Request.QueryString["key4"], Is.EqualTo("value4"));
		}

		[Test]
		public void QueryString_UrlEncodableKey_AccesibleByDecodedKey()
		{
			HttpContext subject = new HttpContextBuilder().Request.AddToQueryString("a&b", "value").Context;

			Assert.That(subject.Request.QueryString["a&b"], Is.EqualTo("value"));
		}

		[Test]
		public void QueryString_UrlEncodableValue_ValueIsDecoded()
		{
			HttpContext subject = new HttpContextBuilder().Request.AddToQueryString("key", "b b").Context;

			Assert.That(subject.Request.QueryString["key"], Is.EqualTo("b b"));
		}

		[Test]
		public void QueryString_ChangesUrl()
		{
			HttpContext subject = new HttpContextBuilder().Request
				.AddToQueryString("key1", "value1")
				.Context;

			Assert.That(subject.Request.Url.Query, Is.EqualTo("?key1=value1"));
			
		}

		#endregion

		#region Request.Form

		[Test]
		public void Form_StartsEmpty()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(subject.Request.Form, Is.Empty);
		}

		[Test]
		public void form_CannotAddItems()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(() => subject.Request.Form.Add("something", "else"), Throws.InstanceOf<NotSupportedException>());
		}

		[Test]
		public void Form_CannotRemoveItems()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(() => subject.Request.Form.Remove("something"), Throws.InstanceOf<NotSupportedException>());
		}

		[Test]
		public void Form_CanBeInitialized()
		{
			NameValueCollection massInitialization = new NameValueCollection()
			{
				{"key3", "value3"},
				{"key4", "value4"}
			};
			HttpContext subject = new HttpContextBuilder().Request
				.AddToForm("key1", "value1")
				.AddToForm("key2", "value2")
				.AddToForm(massInitialization)
				.Context;

			Assert.That(subject.Request.Form["key1"], Is.EqualTo("value1"));
			Assert.That(subject.Request.Form["key2"], Is.EqualTo("value2"));
			Assert.That(subject.Request.Form["key3"], Is.EqualTo("value3"));
			Assert.That(subject.Request.Form["key4"], Is.EqualTo("value4"));
		}

		[Test]
		public void Form_UrlEncodableKey_AccesibleByDecodedKey()
		{
			HttpContext subject = new HttpContextBuilder().Request.AddToForm("a&b", "value").Context;

			Assert.That(subject.Request.Form["a&b"], Is.EqualTo("value"));
		}

		[Test]
		public void Form_UrlEncodableValue_ValueIsDecoded()
		{
			HttpContext subject = new HttpContextBuilder().Request.AddToForm("key", "b b").Context;

			Assert.That(subject.Request.Form["key"], Is.EqualTo("b b"));
		}

		#endregion

		#region Request.HttpMethod

		[Test]
		public void HttpMethod_ByDefault_GET()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(subject.Request.HttpMethod, Is.EqualTo("GET"));
		}

		[Test]
		public void HttpMethod_WhenFormItemsHaveBeenAdded_POST()
		{
			HttpContext subject = new HttpContextBuilder().Request.AddToForm("a", "b").Context;
			Assert.That(subject.Request.HttpMethod, Is.EqualTo("POST"));
		}

		#endregion

		#region Request.Url

		[Test]
		public void Url_ByDefault_PointingToLocalhost()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			Assert.That(subject.Request.Url, Is.EqualTo(new Uri("http://127.0.0.01/webapp/default.aspx")));
		}

		[Test]
		public void Url_CanBeSet_ButQueryIsIgnored()
		{
			Uri url = new Uri("https://www.myweb.com:8080/ajax/entity/id?p=3");
			HttpContext subject = new HttpContextBuilder().Request.WithUrl(url).Context;


			Assert.That(subject.Request.Url.AbsolutePath, Is.EqualTo("/ajax/entity/id"));
			Assert.That(subject.Request.Url.Host, Is.EqualTo("www.myweb.com"));
			Assert.That(subject.Request.Url.Scheme, Is.EqualTo("https"));
			Assert.That(subject.Request.Url.Query, Is.Empty);
		}

		#endregion

		#region Request.Headers

		[Test]
		public void Headers_StartEmpty()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(subject.Request.Headers, Is.Empty);
		}

		[Test]
		public void Headers_CannotAddItems()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(() => subject.Request.Headers.Add("something", "else"), Throws.InstanceOf<NotSupportedException>());
		}

		[Test]
		public void Headers_CannotRemoveItems()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(() => subject.Request.Headers.Remove("something"), Throws.InstanceOf<NotSupportedException>());
		}

		[Test]
		public void Headers_CanBeInitialized()
		{
			NameValueCollection massInitialization = new NameValueCollection()
			{
				{"key3", "value3"},
				{"key4", "value4"}
			};
			HttpContext subject = new HttpContextBuilder().Request
				.AddToHeaders("key1", "value1")
				.AddToHeaders("key2", "value2")
				.AddToHeaders(massInitialization)
				.Context;

			Assert.That(subject.Request.Headers["key1"], Is.EqualTo("value1"));
			Assert.That(subject.Request.Headers["key2"], Is.EqualTo("value2"));
			Assert.That(subject.Request.Headers["key3"], Is.EqualTo("value3"));
			Assert.That(subject.Request.Headers["key4"], Is.EqualTo("value4"));
		}

		#endregion

		#region Request.Cookies

		[Test]
		public void Cookies_StartEmpty()
		{
			HttpContext subject = new HttpContextBuilder().Context;
			Assert.That(subject.Request.Cookies, Is.Empty);
		}

		[Test]
		public void Cookies_ElementsCanBeAdded_AfterCreation()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			subject.Request.Cookies.Add(new HttpCookie("a", "1"));
			Assert.That(subject.Request.Cookies["a"].Value, Is.EqualTo("1"));
		}

		[Test]
		public void Cookies_ElementsCanBeSet_InAdvance()
		{
			HttpContext subject = new HttpContextBuilder().Request
				.AddToCookies("1", "1")
				.AddToCookies(new HttpCookie("2", "2"))
				.Context;

			Assert.That(subject.Request.Cookies["1"].Value, Is.EqualTo("1"));
			Assert.That(subject.Request.Cookies["2"].Value, Is.EqualTo("2"));
		}

		[Test]
		public void Cookies_ElementsCanBeRemoved_WhenSetInAdvance()
		{
			HttpContext subject = new HttpContextBuilder().Request
				.AddToCookies("a", "1")
				.Context;
			subject.Request.Cookies.Remove("a");
			Assert.That(subject.Request.Cookies["a"], Is.Null);
		}

		[Test]
		public void Cookies_ElementsCanBeRemoved_WhenSetAfterCreation()
		{
			HttpContext subject = new HttpContextBuilder().Context;

			subject.Request.Cookies.Add(new HttpCookie("b", "2"));
			subject.Request.Cookies.Remove("b");
			Assert.That(subject.Request.Cookies, Has.No.Contains("b"));
		}

		#endregion

		[Test]
		public void Complex_Building()
		{
			StringBuilder sb = new StringBuilder();
			Uri google = new Uri("http://www.google.com"),
				url = new Uri("http://dgondotnet.blogspot.com/2011/09/configure-reality.html");

			var complexContext = new HttpContextBuilder()
				.AddToSession("session", 1)
				.AddToItems(2, "item")
				.AddToApplication("application", 3)
				.OuputWrittenTo(sb)
				.Request
					.MakeSecure()
					.WithReferrer(google)
					.AddToQueryString("query", "4")
					.AddToForm("form", "5")
					.AddToHeaders("header", "6")
					.AddToCookies("cookie", "7")
					.WithUrl(url)
				.Context;

			Assert.That(complexContext.Session["session"], Is.EqualTo(1));
			Assert.That(complexContext.Items[2], Is.EqualTo("item"));
			Assert.That(complexContext.Application["application"], Is.EqualTo(3));
			Assert.That(complexContext.Request.IsSecureConnection, Is.True);
			Assert.That(complexContext.Request.Url.Scheme, Is.EqualTo("https"));
			Assert.That(complexContext.Request.UrlReferrer, Is.EqualTo(google));
			Assert.That(complexContext.Request.QueryString["query"], Is.EqualTo("4"));
			Assert.That(complexContext.Request.Form["form"], Is.EqualTo("5"));
			Assert.That(complexContext.Request.Headers["header"], Is.EqualTo("6"));
			Assert.That(complexContext.Request.Cookies["cookie"].Value, Is.EqualTo("7"));
			Assert.That(complexContext.Request.Cookies["cookie"].Value, Is.EqualTo("7"));
			Assert.That(complexContext.Request.Url, Is.EqualTo(new UriBuilder(url){Scheme = "https", Query = "query=4"}.Uri));
			


			
			
		}

	}


}
