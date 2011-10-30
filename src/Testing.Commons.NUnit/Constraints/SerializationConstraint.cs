using System;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints
{
	public class SerializationConstraint<T> : Constraint
	{
		private readonly IRoundTripSerializer<T> _serializer;
		private readonly Constraint _constraintOverDeserialized;
		private Exception _ex;

		public SerializationConstraint(IRoundTripSerializer<T> serializer, Constraint constraintOverDeserialized)
		{
			_serializer = serializer;
			_constraintOverDeserialized = ((IResolveConstraint)constraintOverDeserialized).Resolve();
		}

		private T getDeserializedObject(T toSerialize)
		{
			using (_serializer)
			{
				_serializer.Serialize(toSerialize);
				return _serializer.Deserialize();
			}
		}

		public override bool Matches(object current)
		{
			actual = current;
			bool matched = false;
			try
			{
				T deserialized = getDeserializedObject((T)actual);
				matched = _constraintOverDeserialized.Matches(deserialized);
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
				writer.WriteValue(actual);
			}
			else
			{
				writer.WritePredicate("Could not serialize/deserialize object because");
				writer.WriteValue(_ex.Message);
			}
		}
	}
}