using System.Diagnostics.CodeAnalysis;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints.Support;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Used to check the serialization/deserialization of an object.
/// </summary>
/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
public class SerializationConstraint<T> : Constraint
{
	private readonly IRoundtripSerializer<T> _serializer;
	private readonly IConstraint _constraintOverDeserialized;

	/// <summary>
	/// Builds an instance with the provided serializer and constraint.
	/// </summary>
	/// <param name="serializer">Serializer used to serialize/deserialize the tested value.</param>
	/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
	public SerializationConstraint([NotNull] IRoundtripSerializer<T> serializer, [NotNull] Constraint constraintOverDeserialized)
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
	/// Applies the constraint to an actual value, returning a ConstraintResult.
	/// </summary>
	/// <param name="actual">The value to be tested</param>
	/// <returns>A ConstraintResult</returns>
	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{
		ConstraintResult? result = null;
		Exception? ex = null;
#pragma warning disable CA1031
		try
		{
			T deserialized = getDeserializedObject((T)((object)actual!));
			result = _constraintOverDeserialized.ApplyTo(deserialized);
		}
		catch (Exception caught)
		{
			ex = caught;
		}
#pragma warning restore CA1031
		return new SerializationResult(ex, result, this, actual!, (result?.IsSuccess).GetValueOrDefault());
	}

	/// <summary>
	/// The Description of what this constraint tests, for
	/// use in messages and in the ConstraintResult.
	/// </summary>
	public override string Description => "Deserialized object " + _constraintOverDeserialized.Description;

	class SerializationResult : ConstraintResult
	{
		private readonly Exception? _ex;
		private readonly ConstraintResult? _constraintOverDeserialized;

		public SerializationResult(Exception? ex, ConstraintResult? constraintOverDeserialized, IConstraint constraint, object actualValue, bool isSuccess) : base(constraint, actualValue, isSuccess)
		{
			_ex = ex;
			_constraintOverDeserialized = constraintOverDeserialized;
		}

		public override void WriteActualValueTo(MessageWriter writer)
		{
			if (_ex == null)
			{
				_constraintOverDeserialized!.WriteActualValueTo(writer);
				writer.WriteActualConnector();
				writer.WriteValue(ActualValue);
			}
			else
			{
				writer.WritePredicate("Could not serialize/deserialize object because");
				writer.WriteValue(_ex.Message);
			}
		}
	}
}
