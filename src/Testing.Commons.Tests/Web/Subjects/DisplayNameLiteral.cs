using System;
using System.Web.UI.WebControls;

namespace Testing.Commons.Tests.Web.Subjects
{
	internal class DisplayNameLiteral : Literal
	{
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			string name = (string)Context.Session["name"];
			string surname = (string)Context.Session["surname"];

			Text = string.Join(", ", surname, name);
		}
	}
}