using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using NUnit.Framework;
using Testing.Commons.Web;

namespace Testing.Commons.Tests.Web
{
	[TestFixture]
	public class HttpContextFactoryTester
	{
		private readonly string _key = "key", _value = "value";

		[Test]
		public void Constructor_CreateSecureContext()
		{
			HttpContext context = new HttpContextFactory(true).Context;
			Assert.That(context.Request.IsSecureConnection, Is.True);
		}

		[Test]
		public void Constructor_CreateUnsecureContext()
		{
			HttpContext context = new HttpContextFactory(false).Context;
			Assert.That(context.Request.IsSecureConnection, Is.False);
		}

		#region Session

		[Test]
		public void Session_StartsEmpty()
		{
			HttpContext context = new HttpContextFactory(false).Context;
			Assert.That(context.Session, Is.Empty);
		}

		[Test]
		public void Session_CanAddItems()
		{
			HttpContext context = new HttpContextFactory(false).Context;

			Assert.That(context.Session[_key], Is.Null);

			context.Session[_key] = _value;
			Assert.That(context.Session, Is.Not.Empty);
			Assert.That(context.Session[_key], Is.EqualTo(_value));
		}

		[Test]
		public void Session_CanRemoveItems()
		{
			HttpContext context = new HttpContextFactory(false).Context;

			context.Session[_key] = _value;

			context.Session.Remove(_key);
			Assert.That(context.Session[_key], Is.Null);
			Assert.That(context.Session, Is.Empty);
		}

		#endregion
		
		#region Items

		[Test]
		public void Items_StartsEmpty()
		{
			HttpContext context = new HttpContextFactory(false).Context;
			Assert.That(context.Items.Count, Is.EqualTo(1), "There is always the session state");
		}

		[Test]
		public void Items_CanAddItems()
		{
			HttpContext context = new HttpContextFactory(false).Context;

			context.Items.Add(_key, _value);
			Assert.That(context.Items.Count, Is.EqualTo(2));
			Assert.That(context.Items[_key], Is.EqualTo(_value));
		}

		[Test]
		public void Items_CanRemoveItems()
		{
			HttpContext context = new HttpContextFactory(false).Context;

			context.Items.Add(_key, _value);
			context.Items.Remove(_key);
			Assert.That(context.Items.Count, Is.EqualTo(1), "There is always the session state");
			Assert.That(context.Items[_key], Is.Null);
		}

		#endregion
		
		#region QueryString

		[Test]
		public void QueryString_StartsEmpty()
		{
			HttpContext context = new HttpContextFactory(true).Context;
			Assert.That(context.Request.QueryString, Is.Empty);
		}

		[Test]
		public void QueryString_CannotAddItems()
		{
			HttpContext context = new HttpContextFactory(true).Context;
			Assert.That(() => context.Request.QueryString.Add("something", "else"), Throws.InstanceOf<NotSupportedException>());
		}

		[Test]
		public void QueryString_CannotRemoveItems()
		{
			HttpContext context = new HttpContextFactory(true).Context;
			Assert.That(() => context.Request.QueryString.Remove("something"), Throws.InstanceOf<NotSupportedException>());
		}

		[Test]
		public void QueryString_CanBeInitialized()
		{
			HttpContext context = new HttpContextFactory(true, new NameValueCollection {{_key, _value}}).Context;

			Assert.That(context.Request.QueryString, Is.Not.Empty);
			Assert.That(context.Request.QueryString[_key], Is.EqualTo(_value));
		}

		[Test]
		public void QueryString_UrlEncodableKey_AccesibleByDecodedKey()
		{
			HttpContext context = new HttpContextFactory(true, new NameValueCollection { { "a&b", _value } }).Context;

			Assert.That(context.Request.QueryString["a&b"], Is.EqualTo(_value));
		}

		[Test]
		public void QueryString_UrlEncodableValue_ValueIsDecoded()
		{
			HttpContext context = new HttpContextFactory(true, new NameValueCollection { { _key, "b b" } }).Context;

			Assert.That(context.Request.QueryString[_key], Is.EqualTo("b b"));
		}

		#endregion

		[Test]
		public void Output_CanBeAccessed_AfterResponseEnd()
		{
			string written = "written";
			StringBuilder sb = new StringBuilder();
			HttpContext context = new HttpContextFactory(false, new NameValueCollection(), new StringWriter(sb)).Context;

			context.Response.Write(written);
			context.Response.End();

			Assert.That(sb.ToString(), Is.EqualTo(written));
		}
	}
}
