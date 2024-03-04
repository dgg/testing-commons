using System.Collections;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints.Support;

namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Allows asserting on the number of elements of any instance of <see cref="IEnumerable"/>.
/// </summary>
/// <remarks>When evaluating linq queries NUnit does not provide a way of aserting on the element count.</remarks>
public class EnumerableTallyConstraint : Constraint
{
	private readonly Constraint _countConstraint;

	/// <summary>
	/// Creates the instance of the constraint.
	/// </summary>
	/// <param name="countConstraint">The constraint to be applied to the element count.</param>
	public EnumerableTallyConstraint([NotNull] Constraint countConstraint)
	{
		_countConstraint = countConstraint;
	}

	private Constraint? _beingMatched;

	/// <summary>
	/// Applies the constraint to an actual value, returning a ConstraintResult.
	/// </summary>
	/// <param name="actual">The value to be tested</param>
	/// <returns>A ConstraintResult</returns>
	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{
		_beingMatched = new TypeRevealingConstraint(typeof(IEnumerable));
		ConstraintResult result = _beingMatched.ApplyTo(actual);
		if (result.IsSuccess)
		{
			var collection = (IEnumerable)actual!;
			ushort count = calculateCount(collection!);
			_beingMatched = new CountConstraint(_countConstraint, collection);
			result = _beingMatched.ApplyTo(count);
		}
		return result;
	}

	/// <inheritdoc />
	public override string Description { get; }

	private static ushort calculateCount(IEnumerable current)
	{
		ushort num = 0;
		IEnumerator enumerator = current.GetEnumerator();
		while (enumerator.MoveNext())
		{
			num++;
		}
		return num;
	}


	/// <summary>
	/// Used to test that an object is of the same type provided or derived from it and extend the information given for the actual failing value.
	/// </summary>
	/// <remarks></remarks>
	internal class TypeRevealingConstraint : InstanceOfTypeConstraint
	{
		public TypeRevealingConstraint(Type type) : base(type) { }

		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			return new TypeRevealingResult(this, actual!, base.ApplyTo(actual));
		}

		class TypeRevealingResult : ConstraintResult
		{
			private readonly object _actual;

			public TypeRevealingResult(IConstraint constraint, object actual, ConstraintResult result)
				: base(constraint, result.ActualValue, result.IsSuccess)
			{
				_actual = actual;
			}

			public override void WriteActualValueTo(MessageWriter writer)
			{
				if (ActualValue != null)
				{
					writer.WritePredicate("instance of");
					// ActualValue cannot be used since it contains the type itself
					writer.WriteActualValue(_actual.GetType());
					writer.WriteActualConnector();
					writer.WriteValue(_actual);
				}
				else
				{
					writer.WriteActualValue(null);
				}
			}
		}
	}

	/// <summary>
	/// Wraps a constraint when used on the number of elements of an enumerable.
	/// </summary>
	internal class CountConstraint : Constraint
	{
		private readonly Constraint _decoree;
		private readonly IEnumerable _enumerable;

		public CountConstraint(Constraint decoree, IEnumerable enumerable)
		{
			_decoree = decoree;
			_enumerable = enumerable;
		}

		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			// actual is the count of the enumerable
			return new CountResult(this, _enumerable, _decoree.ApplyTo(actual));
		}

		public override string Description => "number of elements " + _decoree.Description;

		class CountResult : ConstraintResult
		{
			private readonly IEnumerable _collection;
			private readonly ConstraintResult _result;

			public CountResult(IConstraint constraint, IEnumerable collection, ConstraintResult result) : base(constraint, result.ActualValue, result.IsSuccess)
			{
				_collection = collection;
				_result = result;
			}

			public override void WriteActualValueTo(MessageWriter writer)
			{
				_result.WriteActualValueTo(writer);
				writer.WriteActualConnector();
				writer.WriteActualValue(_collection.Cast<object>().ToArray());
			}
		}
	}
}
