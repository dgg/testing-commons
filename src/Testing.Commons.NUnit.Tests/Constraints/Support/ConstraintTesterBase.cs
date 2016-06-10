using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;

namespace Testing.Commons.NUnit.Tests.Constraints.Support
{
	public abstract class ConstraintTesterBase
	{
		protected string getMessage<T>(Constraint subject, T actual)
		{
			string message = getMessage(subject.ApplyTo(actual));
			return message;
		}

		protected string getMessage<T>(Constraint subject, ActualValueDelegate<T> actual)
		{
			string message = getMessage(subject.ApplyTo(actual));
			return message;
		}

		private string getMessage(ConstraintResult result)
		{
			var writer = new TextMessageWriter();
			result.WriteMessageTo(writer);

			return writer.ToString();
		}

		protected bool matches<T>(Constraint subject, T actual)
		{
			ConstraintResult result = subject.ApplyTo(actual);
			return result.IsSuccess;
		}

		protected bool matches<T>(Constraint subject, ActualValueDelegate<T> actual)
		{
			ConstraintResult result = subject.ApplyTo(actual);
			return result.IsSuccess;
		}
	}
}