using System;
using System.Collections.Specialized;
using System.Web;

namespace Testing.Commons.Web
{
	/// <summary>
	/// Allows customizing members of <see cref="HttpContext.Request"/>.
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

		/// <summary>
		/// Sets a value indicating that the HTTP connection uses secure sockets (that is, HTTPS) in <see cref="HttpRequest.IsSecureConnection"/>.
		/// </summary>
		/// <returns>This instance of the builder.</returns>
		public HttpRequestBuilder MakeSecure()
		{
			_model.IsSecure = true;
			return this;
		}

		/// <summary>
		/// The custom built instance.
		/// </summary>
		public HttpContext Context { get { return _builder.Context; } }

		/// <summary>
		/// Sets information about the URL of the client's previous request that linked to the current URL in <see cref="HttpRequest.UrlReferrer"/>.
		/// </summary>
		/// <param name="referrer">A <see cref="Uri"/> object.</param>
		/// <returns>This instance of the builder.</returns>
		public HttpRequestBuilder WithReferrer(Uri referrer)
		{
			_model.Referrer = referrer;
			return this;
		}

		/// <summary>
		/// Adds a query variable to <see cref="HttpRequest.QueryString"/>.
		/// </summary>
		/// <param name="key">The <c>String</c> key of the entry to add. The key can be null.</param>
		/// <param name="value">The <c>String</c> value of the entry to add. The value can be null.</param>
		/// <returns>This instance of the builder.</returns>
		public HttpRequestBuilder AddToQueryString(string key, string value)
		{
			_model.QueryString.Add(key, value);
			return this;
		}

		/// <summary>
		/// Adds multiple query variables to <see cref="HttpRequest.QueryString"/>.
		/// </summary>
		/// <param name="queryString">Collection of HTTP query variables.</param>
		/// <returns>This instance of the builder.</returns>
		public HttpRequestBuilder AddToQueryString(NameValueCollection queryString)
		{
			_model.QueryString.Add(queryString);
			return this;
		}

		/// <summary>
		/// Adds a form variable to <see cref="HttpRequest.Form"/>.
		/// </summary>
		/// <remarks>Adding form variables sets the <see cref="HttpRequest.HttpMethod"/> to <c>POST</c>.</remarks>
		/// <param name="key">The <c>String</c> key of the entry to add. The key can be null.</param>
		/// <param name="value">The <c>String</c> value of the entry to add. The value can be null.</param>
		/// <returns>This instance of the builder.</returns>
		public HttpRequestBuilder AddToForm(string key, string value)
		{
			_model.Form.Add(key, value);
			return this;
		}

		/// <summary>
		/// Adds multiple form variables to <see cref="HttpRequest.Form"/>.
		/// </summary>
		/// <remarks>Adding form variables sets the <see cref="HttpRequest.HttpMethod"/> to <c>POST</c>.</remarks>
		/// <param name="form">Collection of form variables.</param>
		/// <returns>This instance of the builder.</returns>
		public HttpRequestBuilder AddToForm(NameValueCollection form)
		{
			_model.Form.Add(form);
			return this;
		}

		/// <summary>
		/// Set parts of the url of <see cref="HttpRequest.Url"/>.
		/// </summary>
		/// <remarks>Query variables are ignored; in order to initialize them, use <see cref="AddToQueryString(NameValueCollection)"/>.
		/// <para>Query variables set in <see cref="AddToQueryString(NameValueCollection)"/> are added to the resulting <see cref="HttpRequest.QueryString"/>.</para>
		/// <para>The <see cref="Uri.Scheme"/> can change due to <see cref="MakeSecure"/>.</para>
		/// </remarks>
		/// <param name="url">Information about the URL of the current request.</param>
		/// <returns>This instance of the builder.</returns>
		public HttpRequestBuilder WithUrl(Uri url)
		{
			_model.Url = url;
			return this;
		}

		/// <summary>
		/// Adds an entry to <see cref="HttpRequest.Headers"/>.
		/// </summary>
		/// <param name="key">The <c>String</c> key of the entry to add. The key can be null.</param>
		/// <param name="value">The <c>String</c> value of the entry to add. The value can be null.</param>
		/// <returns>This instance of the builder.</returns>
		public HttpRequestBuilder AddToHeaders(string key, string value)
		{
			_model.Headers.Add(key, value);
			return this;
		}

		/// <summary>
		/// Adds multiple entries to <see cref="HttpRequest.Headers"/>.
		/// </summary>
		/// <param name="headers">A collection of HTTP headers.</param>
		/// <returns>This instance of the builder.</returns>
		public HttpRequestBuilder AddToHeaders(NameValueCollection headers)
		{
			_model.Headers.Add(headers);
			return this;
		}

		/// <summary>
		/// Adds a cookie to <see cref="HttpRequest.Cookies"/>.
		/// </summary>
		/// <param name="name">The name of the new cookie.</param>
		/// <param name="value">The value of the new cookie.</param>
		/// <returns>This instance of the builder.</returns>
		public HttpRequestBuilder AddToCookies(string name, string value)
		{
			return AddToCookies(new HttpCookie(name, value));
		}

		/// <summary>
		/// Adds a cookie to <see cref="HttpRequest.Cookies"/>.
		/// </summary>
		/// <param name="cookie">A HTTP cookie.</param>
		/// <returns>This instance of the builder.</returns>
		public HttpRequestBuilder AddToCookies(HttpCookie cookie)
		{
			_model.Cookies.Add(cookie);
			return this;
		}
	}
}