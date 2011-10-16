using System;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Used to test that an object is of the same type provided or derived from it and extend the information given for the actual failing value.
	/// </summary>
	/// <remarks></remarks>
	internal class TypeRevealingConstraint : InstanceOfTypeConstraint
	{
		public TypeRevealingConstraint(Type type) : base(type) { }

		public override void WriteActualValueTo(MessageWriter writer)
		{
			if (actual != null)
			{
				writer.WritePredicate("instance of");
				writer.WriteActualValue((actual == null) ? null : actual.GetType());
				CustomTextMessageWriter.WriteActualConnector(writer);
				writer.WriteValue(actual);
			}
			else
			{
				writer.WriteActualValue(null);
			}
		}
	}
}