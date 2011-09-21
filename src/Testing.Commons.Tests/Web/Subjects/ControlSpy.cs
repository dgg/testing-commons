using System.Reflection;
using System.Web.UI;

namespace Testing.Commons.Tests.Web.Subjects
{
	internal class ControlSpy : Page
	{
		internal class StepSignature
		{
			public string StepName { get; private set; }
			public object[] StepArguments { get; private set; }

			public StepSignature(string stepName, object[] stepArguments)
			{
				StepName = stepName;
				StepArguments = stepArguments;
			}

			public static StepSignature FromMethod(MethodBase method, object[] arguments)
			{

				return new StepSignature(method.Name, arguments);
			}
		}
		internal StepSignature LastStep { get; private set; }
		protected override void OnLoad(System.EventArgs e)
		{
			LastStep = StepSignature.FromMethod(MethodBase.GetCurrentMethod(), new[] { e });
			base.OnLoad(e);
		}

		protected override bool OnBubbleEvent(object source, System.EventArgs args)
		{
			LastStep = StepSignature.FromMethod(MethodBase.GetCurrentMethod(), new[] { source, args });
			return base.OnBubbleEvent(source, args);
		}
	}
}