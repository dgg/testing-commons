using System;
using System.Collections.Specialized;
using System.Web;

namespace Testing.Commons.Web
{
	internal class HttpRequestModel
	{
		public HttpRequestModel()
		{
			QueryString = new NameValueCollection();
			Form = new NameValueCollection();
			Url = new Uri("http://127.0.0.1/webapp/default.aspx");
			Headers = new NameValueCollection();
			Cookies = new HttpCookieCollection();
		}

		public Uri Referrer;
		public bool IsSecure { get; set; }
		public NameValueCollection QueryString { get; private set; }
		public NameValueCollection Form { get; private set; }
		public Uri Url { get; set; }
		public NameValueCollection Headers { get; private set; }
		public HttpCookieCollection Cookies { get; set; }
	}
}