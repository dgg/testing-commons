using System;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints
{
	public class DeserializationConstraint<T> : Constraint
	{
		private readonly IDeserializer _deserializer;
		private readonly Constraint _constraintOverDeserialized;
		private Exception _ex;
		private T _deserialized;

		public DeserializationConstraint(IDeserializer deserializer, Constraint constraintOverDeserialized)
		{
			_deserializer = deserializer;
			_constraintOverDeserialized = ((IResolveConstraint)constraintOverDeserialized).Resolve();
		}

		private T getDeserializedObject(string toDeserialize)
		{
			return _deserializer.Deserialize<T>(toDeserialize);
		}

		public override bool Matches(object current)
		{
			actual = current;
			bool matched = false;
			try
			{
				_deserialized = getDeserializedObject((string)actual);
				matched = _constraintOverDeserialized.Matches(_deserialized);
			}
			catch (Exception ex)
			{
				_ex = ex;
			}
			return matched;
		}

		public override void WriteDescriptionTo(MessageWriter writer)
		{
			writer.WritePredicate("Deserialized object");
			_constraintOverDeserialized.WriteDescriptionTo(writer);
		}

		public override void WriteActualValueTo(MessageWriter writer)
		{
			if (_ex == null)
			{
				_constraintOverDeserialized.WriteActualValueTo(writer);
				CustomTextMessageWriter.WriteActualConnector(writer);
				writer.WriteValue(_deserialized);
			}
			else
			{
				writer.WritePredicate("Could not deserialize object because");
				writer.WriteValue(_ex.Message);
			}
		}
	}
}
