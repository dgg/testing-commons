using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using NUnit.Framework;
using Testing.Commons.Tests.Web.Subjects;
using Testing.Commons.Web;

namespace Testing.Commons.Tests.Web
{
	[TestFixture]
	public class HttpContextBuilderSamplesTester
	{
		[Test]
		public void HttpContextBuilder_CanBeUsedToTest_HttpHandlers()
		{
			StringBuilder sb = new StringBuilder();

			var subject = new MessageHandler();

			HttpContext context = new HttpContextBuilder()
				.OuputWrittenTo(sb)
				.Request
					.AddToForm("language", "es-ES")
					.AddToForm("resourceName", "Message_1")	
				.Context;

			subject.ProcessRequest(context);
			context.Response.Flush();
			
			Assert.That(sb.ToString(), Is.EqualTo("Mensaje Uno"));
		}

		[Test]
		public void HttpContextBuilder_CanBeUsedToTest_ControlsThatUseTheContext()
		{
			StringBuilder sb = new StringBuilder();
			HtmlTextWriter writer = new HtmlTextWriter(new StringWriter(sb));
			var subject = new DisplayNameLiteral();

			using (HttpContextReseter.Set(new HttpContextBuilder()
				.AddToSession("name", "Daniel")
				.AddToSession("surname", "Gonzalez")))
			{
				ControlLifecycle.Fake(subject, c => c.PreRender += null, EventArgs.Empty);

				subject.RenderControl(writer);

				Assert.That(sb.ToString(), Is.EqualTo("Gonzalez, Daniel"));
			}
		}
	}
}
