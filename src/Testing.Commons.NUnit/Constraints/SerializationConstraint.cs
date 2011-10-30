using System;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Used to check the serialization/deserialization of an object.
	/// </summary>
	/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
	public class SerializationConstraint<T> : Constraint
	{
		private readonly IRoundTripSerializer<T> _serializer;
		private readonly Constraint _constraintOverDeserialized;
		private Exception _ex;

		/// <summary>
		/// Builds an instance with the provided serializer and constraint.
		/// </summary>
		/// <param name="serializer">Serializer used to serialize/deserialize the tested value.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
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

		/// <summary>
		/// Test whether the constraint is satisfied by a given value.
		/// </summary>
		/// <param name="current">The value to be tested.</param>
		/// <returns>True for success, false for failure.</returns>
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

		/// <summary>
		/// Write the constraint description to a MessageWriter.
		/// </summary>
		/// <param name="writer">The writer on which the description is displayed.</param>
		public override void WriteDescriptionTo(MessageWriter writer)
		{
			writer.WritePredicate("Deserialized object");
			_constraintOverDeserialized.WriteDescriptionTo(writer);
		}

		/// <summary>
		/// Write the actual value for a failing constraint test to a
		/// MessageWriter. The default implementation simply writes
		/// the raw value of actual, leaving it to the writer to
		/// perform any formatting.
		/// </summary>
		/// <param name="writer">The writer on which the actual value is displayed</param>
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