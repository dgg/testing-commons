using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Tests.Constraints.Support
{
	public abstract class ConstraintTesterBase
	{
		protected string GetMessage<T>(T subject, object actual) where T : Constraint
		{
			var writer  = new TextMessageWriter();

			subject.Matches(actual);
			subject.WriteMessageTo(writer);

			return writer.ToString();
		}

		protected string GetMessage<T>(T subject, ActualValueDelegate actual) where T : Constraint
		{
			var writer = new TextMessageWriter();

			subject.Matches(actual);
			subject.WriteMessageTo(writer);

			return writer.ToString();
		}
	}
}
