using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;

namespace Testing.Commons.Web
{
	/// <summary>
	/// Allows the creation of a limited <see cref="HttpContext"/>.
	/// </summary>
	public sealed class HttpContextFactory
	{
		// NOTE: This code is based on the following article: http://righteousindignation.gotdns.org/blog/archive/2004/04/13/149.aspx
		
		private readonly HttpContext _context;

		/// <summary>
		/// Builds a limited <see cref="HttpContext"/>.
		/// </summary>
		/// <param name="isSecure">Indicates whether the HTTP connection uses secure sockets (that is, HTTPS).</param>
		public HttpContextFactory(bool isSecure) : this(isSecure, new NameValueCollection()) { }

		/// <summary>
		/// Builds a limited <see cref="HttpContext"/> allowing passing arguments to the querystring pf the request.
		/// </summary>
		/// <param name="isSecure">Indicates whether the context will be secure (as in using https) or not.</param>
		/// <param name="queryString">Collection of HTTP query string variables.</param>
		public HttpContextFactory(bool isSecure, NameValueCollection queryString) : this(isSecure, new QueryBuilder(queryString).Query, new StringWriter()) { }

		/// <summary>
		/// Builds a limited <see cref="HttpContext"/> allowing passing arguments to the querystring pf the request and access to the output of the response.
		/// </summary>
		/// <param name="isSecure">Indicates whether the HTTP connection uses secure sockets (that is, HTTPS).</param>
		/// <param name="queryString">Collection of HTTP query string variables.</param>
		/// <param name="writer">Enables accessing the output of text to the outgoing HTTP response stream.</param>
		public HttpContextFactory(bool isSecure, NameValueCollection queryString, TextWriter writer) : this(isSecure, new QueryBuilder(queryString).Query, writer) { }

		private HttpContextFactory(bool isSecure, string query, TextWriter writer)
		{
			const string keyAppPathKey = ".appPath", appPathValue = "d:\\inetpub\\wwwroot\\webapp\\";
			const string keyAppVPathKey = ".appVPath", appVPathValue = "/webapp";
			
			Thread.GetDomain().SetData(keyAppPathKey, appPathValue);
			Thread.GetDomain().SetData(keyAppVPathKey, appVPathValue);

			const string workerRequestPage = "default.aspx";
			SimpleWorkerRequest request = new WorkerRequest(workerRequestPage, query, writer, isSecure);
			_context = new HttpContext(request);

			HttpSessionStateContainer container = new HttpSessionStateContainer(
				Guid.NewGuid().ToString("N"),
				new SessionStateItemCollection(),
				new HttpStaticObjectsCollection(),
				5, true,
				HttpCookieMode.AutoDetect,
				SessionStateMode.InProc,
				false);

			const string keyAspSessionKey = "AspSession";
			HttpSessionState state = Activator.CreateInstance(
				typeof(HttpSessionState),
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
				null,
				new object[] { container },
				CultureInfo.CurrentCulture) as HttpSessionState;
			_context.Items[keyAspSessionKey] = state;
		}

		/// <summary>
		/// Gives access to the instance just built.
		/// </summary>
		public HttpContext Context { get { return _context; } }

		private class WorkerRequest : SimpleWorkerRequest
		{
			private readonly bool _isSecure;

			public WorkerRequest(string page, string query, TextWriter output, bool isSecure)
				: base(page, query, output)
			{
				_isSecure = isSecure;
			}

			public override bool IsSecure()
			{
				return _isSecure;
			}
		}
	}
}