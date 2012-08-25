using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Tests.Constraints.Support
{
	internal class UppercaseConstraint : Constraint
	{
		public override bool Matches(object current)
		{
			actual = current;
			var c = (char)current;
			return char.IsUpper(c);
		}

		public override void WriteDescriptionTo(MessageWriter writer)
		{
			writer.Write("An uppercase character");
		}
	}
}
