using System;
using System.Collections;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace Testing.Commons.Web
{
	/// <summary>
	/// 
	/// </summary>
	public class HttpContextBuilder
	{

		public HttpContextBuilder()
		{
			_session = new SessionStateItemCollection();
			_items = new Hashtable();
			_application = new SessionStateItemCollection();
			_output = new StringBuilder();
			_model = new HttpRequestModel();
		}

		private readonly SessionStateItemCollection _session;
		public HttpContextBuilder AddToSession(string key, object value)
		{
			_session[key] = value;
			return this;
		}

		private readonly IDictionary _items;
		public HttpContextBuilder AddToItems(object key, object value)
		{
			_items.Add(key, value);
			return this;
		}

		public HttpContextBuilder AddToItems(IDictionary items)
		{
			foreach (object key in items.Keys)
			{
				_items.Add(key, items[key]);
			}
			return this;
		}

		private readonly SessionStateItemCollection _application;
		public HttpContextBuilder AddToApplication(string key, object value)
		{
			_application[key] = value;
			return this;
		}

		StringBuilder _output;
		public HttpContextBuilder OuputWrittenTo(StringBuilder sb)
		{
			_output = sb;
			return this;
		}

		private readonly HttpRequestModel _model;
		public HttpRequestBuilder Request { get { return new HttpRequestBuilder(this, _model); } }

		public HttpContext Context
		{
			get
			{
				const string keyAppPathKey = ".appPath";
				const string keyAppVPathKey = ".appVPath";
				Thread.GetDomain().SetData(keyAppPathKey, WorkerRequest.Path(_model.Url));
				Thread.GetDomain().SetData(keyAppVPathKey, WorkerRequest.VPath(_model.Url));

				var request = new WorkerRequest(_model.Url, 
					new QueryBuilder(_model.QueryString).Query,
					new StringWriter(_output),
					_model.IsSecure,
					_model.Referrer,
					_model.Form,
					_model.Headers);
				HttpContext context = new HttpContext(request);

				var session = initSession();
				initItems(context, session);
				iniApplication(context);
				initCookies(context, _model.Cookies);
				return context;
			}
		}

		private void initCookies(HttpContext context, HttpCookieCollection cookies)
		{
			foreach (string name in cookies.Keys)
			{
				context.Request.Cookies.Add(cookies[name]);
			}
		}

		private void initItems(HttpContext context, HttpSessionState session)
		{
			const string keyAspSessionKey = "AspSession";
			context.Items[keyAspSessionKey] = session;
			foreach (DictionaryEntry item in _items)
			{
				context.Items.Add(item.Key, item.Value);
			}
		}

		private HttpSessionState initSession()
		{
			HttpSessionStateContainer container = new HttpSessionStateContainer(
				"id",
				_session,
				new HttpStaticObjectsCollection(),
				5, true,
				HttpCookieMode.AutoDetect,
				SessionStateMode.InProc,
				false);

			HttpSessionState state = Activator.CreateInstance(
				typeof (HttpSessionState),
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
				null,
				new object[] {container},
				CultureInfo.CurrentCulture) as HttpSessionState;
			return state;
		}


		private void iniApplication(HttpContext context)
		{
			Type appFactoryType = Type.GetType("System.Web.HttpApplicationFactory, System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			object appFactory = getStaticFieldValue<object>("_theApplicationFactory", appFactoryType);
			setPrivateInstanceFieldValue("_state", appFactory, context.Application);
			foreach (string key in _application.Keys)
			{
				context.Application.Add(key, _application[key]);
			}
		}

		public static T getStaticFieldValue<T>(string fieldName, Type type)
		{
			FieldInfo field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
			if (field != null)
			{
				return (T)field.GetValue(type);
			}
			return default(T);
		}

		public static void setPrivateInstanceFieldValue(string memberName, object source, object value)
		{
			Type type = source.GetType();
			FieldInfo field = type.GetField(memberName,
											BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
			if (field == null)
			{
				throw new MissingFieldException(type.Name, memberName);
			}

			field.SetValue(source, value);
		}
	}
}