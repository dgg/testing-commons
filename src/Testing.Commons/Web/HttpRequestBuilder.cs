using System;
using System.Collections.Specialized;
using System.Web;

namespace Testing.Commons.Web
{
	/// <summary>
	/// 
	/// </summary>
	public class HttpRequestBuilder
	{
		private readonly HttpContextBuilder _builder;
		private readonly HttpRequestModel _model;

		internal HttpRequestBuilder(HttpContextBuilder builder, HttpRequestModel model)
		{
			_builder = builder;
			_model = model;
		}

		public HttpRequestBuilder MakeSecure()
		{
			_model.IsSecure = true;
			return this;
		}

		public HttpContext Context { get { return _builder.Context; } }

		public HttpRequestBuilder WithReferrer(Uri referrer)
		{
			_model.Referrer = referrer;
			return this;
		}

		public HttpRequestBuilder AddToQueryString(string key, string value)
		{
			_model.QueryString.Add(key, value);
			return this;
		}

		public HttpRequestBuilder AddToQueryString(NameValueCollection queryString)
		{
			_model.QueryString.Add(queryString);
			return this;
		}

		public HttpRequestBuilder AddToForm(string key, string value)
		{
			_model.Form.Add(key, value);
			return this;
		}

		public HttpRequestBuilder AddToForm(NameValueCollection form)
		{
			_model.Form.Add(form);
			return this;
		}

		public HttpRequestBuilder WithUrl(Uri url)
		{
			_model.Url = url;
			return this;
		}

		public HttpRequestBuilder AddToHeaders(string key, string value)
		{
			_model.Headers.Add(key, value);
			return this;
		}

		public HttpRequestBuilder AddToHeaders(NameValueCollection headers)
		{
			_model.Headers.Add(headers);
			return this;
		}

		public HttpRequestBuilder AddToCookies(string name, string value)
		{
			return AddToCookies(new HttpCookie(name, value));
		}

		public HttpRequestBuilder AddToCookies(HttpCookie cookie)
		{
			_model.Cookies.Add(cookie);
			return this;
		}
	}
}