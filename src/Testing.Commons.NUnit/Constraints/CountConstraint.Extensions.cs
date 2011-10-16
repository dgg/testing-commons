using System.Collections;
using System.Linq;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Wraps a constraint when used on the number of elements of an enumerable.
	/// </summary>
	internal class CountConstraint : Constraint
	{
		private readonly Constraint _decoree;
		public CountConstraint(Constraint decoree, IEnumerable enumerable)
		{
			_decoree = decoree;
			actual = enumerable;
		}

		public override bool Matches(object count)
		{
			return _decoree.Matches(count);
		}

		public override void WriteDescriptionTo(MessageWriter writer)
		{
			writer.WritePredicate("number of elements");
			_decoree.WriteDescriptionTo(writer);
		}

		public override void WriteActualValueTo(MessageWriter writer)
		{
			_decoree.WriteActualValueTo(writer);
			CustomTextMessageWriter.WriteActualConnector(writer);
			writer.WriteActualValue(((IEnumerable)actual).Cast<object>().ToArray());
		}
	}
}