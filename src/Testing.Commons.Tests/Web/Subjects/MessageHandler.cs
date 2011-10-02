using System.Globalization;
using System.Web;

namespace Testing.Commons.Tests.Web.Subjects
{
	/// <summary>
	/// Displays a message in the language specified in the post
	/// </summary>
	internal class MessageHandler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			string resourceName = context.Request.Form["resourceName"];
			CultureInfo language = CultureInfo.GetCultureInfo(context.Request.Form["language"]);
			string message = Messages.ResourceManager.GetString(resourceName, language);
			context.Response.Write(message);
		}

		public bool IsReusable { get { return false; } }
	}


}