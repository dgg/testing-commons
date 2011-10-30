using System;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Used to check the deserialization of an object.
	/// </summary>
	public class DeserializationConstraint<T> : Constraint
	{
		private readonly IDeserializer _deserializer;
		private readonly Constraint _constraintOverDeserialized;
		private Exception _ex;
		private T _deserialized;

		/// <summary>
		/// Builds an instance with the provided deserializer and constraint.
		/// </summary>
		/// <param name="deserializer">Deserializer used to deserialize the tested value.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		public DeserializationConstraint(IDeserializer deserializer, Constraint constraintOverDeserialized)
		{
			_deserializer = deserializer;
			_constraintOverDeserialized = ((IResolveConstraint)constraintOverDeserialized).Resolve();
		}

		private T getDeserializedObject(string toDeserialize)
		{
			return _deserializer.Deserialize<T>(toDeserialize);
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
				_deserialized = getDeserializedObject((string)actual);
				matched = _constraintOverDeserialized.Matches(_deserialized);
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
