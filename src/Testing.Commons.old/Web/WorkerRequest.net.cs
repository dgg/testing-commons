using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;

using HttpMethod = System.Net.WebRequestMethods.Http;

namespace Testing.Commons.Web
{
	internal class WorkerRequest : SimpleWorkerRequest
	{
		private readonly bool _isSecure;
		private readonly string _verb;
		private readonly Uri _referrer;
		private readonly NameValueCollection _form;
		private readonly NameValueCollection _headers;
		private readonly Uri _request;

		public WorkerRequest(Uri request, string query, TextWriter output, bool isSecure, Uri referrer, NameValueCollection form, NameValueCollection headers)
			: base(pageFrom(request), query, output)
		{
			_request = request;
			_isSecure = isSecure;
			_referrer = referrer;
			_form = form;
			_headers = headers;
			_verb = form != null && form.Count > 0 ? HttpMethod.Post : HttpMethod.Get;
		}

		private static string pageFrom(Uri url)
		{
			string[] segments = url.Segments;
			return segments[segments.Length - 1];
		}

		public override bool IsSecure()
		{
			return _isSecure || _request.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);
		}

		public override string GetHttpVerbName()
		{
			return _verb;
		}

		public override byte[] GetPreloadedEntityBody()
		{
			QueryBuilder query = new QueryBuilder(_form);
			return Encoding.UTF8.GetBytes(query.Query);
		}

		public override bool IsEntireEntityBodyIsPreloaded()
		{
			return true;
		}

		public override string[][] GetUnknownRequestHeaders()
		{
			if (_headers == null || _headers.Count == 0)
			{
				return null;
			}
			string[][] headersArray = new string[_headers.Count][];
			for (int i = 0; i < _headers.Count; i++)
			{
				headersArray[i] = new string[2];
				headersArray[i][0] = _headers.Keys[i];
				headersArray[i][1] = _headers[i];
			}
			return headersArray;
		}

		public override string GetKnownRequestHeader(int index)
		{
			if (index == 0x24)
			{
				return _referrer == null ? string.Empty : _referrer.ToString();
			}

			if (index == 12 && _verb == HttpMethod.Post)
			{
				return "application/x-www-form-urlencoded";
			}

			return base.GetKnownRequestHeader(index);
		}

		public override string GetLocalAddress()
		{
			return _request.Host;
		}

		public override int GetLocalPort()
		{
			return _request.Port;
		}

		public override string GetProtocol()
		{
			return IsSecure() ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;
		}

		public static string VPath(Uri request)
		{
			string[] segments = request.Segments;
			return string.Join(string.Empty, segments.Take(segments.Length - 1)).TrimEnd('/');
		}

		public static string Path(Uri request)
		{
			return System.IO.Path.Combine(
				  "d:\\inetpub\\wwwroot",
				  VPath(request).Replace('/', '\\').TrimStart('\\'));
		}
	}
}