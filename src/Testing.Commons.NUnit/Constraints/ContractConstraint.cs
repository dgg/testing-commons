using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	internal abstract class ContractConstraint<T> : Constraint
	{
		protected readonly T _expected;
		protected readonly Constraint _inner;
		private readonly string _messageConnector;
		internal ContractConstraint(T expected, Constraint inner, string messageConnector)
		{
			_expected = expected;
			_inner = inner;
			_messageConnector = messageConnector;
		}

		public override void WriteDescriptionTo(MessageWriter writer)
		{
			_inner.WriteDescriptionTo(writer);
		}

		public override void WriteMessageTo(MessageWriter writer)
		{
			writer.WriteValue(actual);
			writer.Write(_messageConnector);
			writer.WriteValue(_expected);
			writer.WriteLine();
			base.WriteMessageTo(writer);
		}

		public override void WriteActualValueTo(MessageWriter writer)
		{
			_inner.WriteActualValueTo(writer);
		}
	}
}