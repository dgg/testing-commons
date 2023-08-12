using System;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints.Support;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Used to check the deserialization of an object.
	/// </summary>
	public class DeserializationConstraint<T> : Constraint
	{
		private readonly IDeserializer _deserializer;
		private readonly IConstraint _constraintOverDeserialized;
		
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
		/// Applies the constraint to an actual value, returning a ConstraintResult.
		/// </summary>
		/// <param name="actual">The value to be tested</param>
		/// <returns>A ConstraintResult</returns>
		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			Exception ex = null;
			ConstraintResult result = null;
			T deserialized = default(T);
			try
			{
				deserialized = getDeserializedObject(actual?.ToString());
				result = _constraintOverDeserialized.ApplyTo(deserialized);
			}
			catch (Exception caught)
			{
				ex = caught;
			}
			return new DeserializationResult(ex, deserialized, result, this, actual, (result?.IsSuccess).GetValueOrDefault());
		}


		/// <summary>
		/// The Description of what this constraint tests, for
		/// use in messages and in the ConstraintResult.
		/// </summary>
		public override string Description => "Deserialized object " + _constraintOverDeserialized.Description;

		class DeserializationResult : ConstraintResult
		{
			private readonly Exception _ex;
			private readonly T _deserialized;
			private readonly ConstraintResult _constraintOverDeserialized;

			public DeserializationResult(Exception ex, T deserialized, ConstraintResult constraintOverDeserialized, IConstraint constraint, object actualValue, bool isSuccess) : base(constraint, actualValue, isSuccess)
			{
				_ex = ex;
				_deserialized = deserialized;
				_constraintOverDeserialized = constraintOverDeserialized;
			}

			public override void WriteActualValueTo(MessageWriter writer)
			{
				if (_ex == null)
				{
					_constraintOverDeserialized.WriteActualValueTo(writer);
					writer.WriteActualConnector();
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

	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="DeserializationConstraint{T}"/> that allows checking the deserialization of an object.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="deserializer">Deserializer used to deserialize the tested value.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Deserializable<T>(this Must.BeEntryPoint entry, IDeserializer deserializer, Constraint constraintOverDeserialized)
		{
			return new DeserializationConstraint<T>(deserializer, constraintOverDeserialized);
		}

		/// <summary>
		/// Builds an instance of <see cref="DeserializationConstraint{T}"/> that allows checking the XML deserialization of an object.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <returns>Instance built.</returns>
		public static Constraint XmlDeserializable<T>(this Must.BeEntryPoint entry, Constraint constraintOverDeserialized)
		{
			return new DeserializationConstraint<T>(new XmlDeserializer(), constraintOverDeserialized);
		}

		/// <summary>
		/// Builds an instance of <see cref="DeserializationConstraint{T}"/> that allows checking the data contract deserialization of an object.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <returns>Instance built.</returns>
		public static Constraint DataContractDeserializable<T>(this Must.BeEntryPoint entry, Constraint constraintOverDeserialized)
		{
			return new DeserializationConstraint<T>(new DataContractDeserializer(), constraintOverDeserialized);
		}
	}
}