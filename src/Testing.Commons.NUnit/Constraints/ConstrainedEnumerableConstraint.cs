using System.Collections;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Used to check every item of an enumerable against a constraint.
/// </summary>
public class ConstrainedEnumerableConstraint : Constraint
{
	private readonly Constraint[] _constraints;

	/// <summary>
	/// Builds an instance with the provided constraints.
	/// </summary>
	/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
	public ConstrainedEnumerableConstraint(params Constraint[] constraints) : this(constraints.AsEnumerable()) { }

	/// <summary>
	/// Builds an instance with the provided constraints.
	/// </summary>
	/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
	public ConstrainedEnumerableConstraint([NotNull] IEnumerable<Constraint> constraints)
	{
		_constraints = constraints.ToArray();
	}

	private Constraint? _beingMatched;

	/// <summary>
	/// Applies the constraint to an actual value, returning a ConstraintResult.
	/// </summary>
	/// <param name="actual">The value to be tested</param>
	/// <returns>A ConstraintResult</returns>
	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{
		ArgumentNullException.ThrowIfNull(actual, nameof(actual));
		// it is ok to iterate the collection as most of the times it will be a small controlled collection
		var collection = ((IEnumerable)actual).Cast<object>().ToArray();

		ConstraintResult result = sameNumberOfElementsAndConstraints(collection, _constraints.Length);
		if (result.IsSuccess)
		{
			for (int i = 0; i < collection.Length && result.IsSuccess; i++)
			{
				_beingMatched = new IndexedConstraint(collection, i, _constraints[i]);
				result = _beingMatched.ApplyTo(collection.GetValue(i));
			}
		}

		return result;
	}

	/// <summary>
	/// The Description of what this constraint tests, for
	/// use in messages and in the ConstraintResult.
	/// </summary>
	public override string Description => _beingMatched?.Description ?? string.Empty;

	private ConstraintResult sameNumberOfElementsAndConstraints(Array current, int numberOfConstraints)
	{
		_beingMatched = new MatchingLength(current, current.Length);
		var result = _beingMatched.ApplyTo(numberOfConstraints);
		return result;
	}

	/// <summary>
	/// Checks that the count of the enumerable and the provided constraints is the same.
	/// </summary>
	class MatchingLength : EqualConstraint
	{
		private readonly Array _items;

		public MatchingLength(Array items, object expected)
			: base(expected)
		{
			_items = items;
		}

		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			return new MatchingLengthResult(_items, this, base.ApplyTo(actual));
		}

		class MatchingLengthResult : ConstraintResult
		{
			private readonly Array _items;

			public MatchingLengthResult(Array items, IConstraint constraint, ConstraintResult result) : base(constraint, result.ActualValue, result.IsSuccess)
			{
				_items = items;
			}

			public override void WriteMessageTo(MessageWriter writer)
			{
				writer.WriteMessageLine(0, "{0} has a different number of items than constraints are provided.", getCollectionValue(writer));
				base.WriteMessageTo(writer);
			}

			private string getCollectionValue(MessageWriter writer)
			{
				writer.WriteActualValue(_items);
				var sb = writer.GetStringBuilder();
				string items = sb.ToString();
				sb.Remove(0, sb.Length);
				return items;
			}
		}
	}

	/// <summary>
	/// Encapsulates a contraint applied to an enumerable element to enhance the feedback in case of failure by providing the index of the element that provoked the failure.
	/// </summary>
	class IndexedConstraint : Constraint
	{
		private readonly Array _items;
		private readonly int _index;
		private readonly IConstraint _constraint;
		public IndexedConstraint(Array items, int index, IResolveConstraint constraint)
		{
			_items = items;
			_index = index;
			_constraint = constraint.Resolve();
		}

		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			return new IndexedResult(_items, _index, _constraint, _constraint.ApplyTo(actual));
		}

		public override string Description { get; }

		class IndexedResult : ConstraintResult
		{
			private readonly Array _items;
			private readonly int _index;

			public IndexedResult(Array items, int index, IConstraint constraint, ConstraintResult result) : base(constraint, result.ActualValue, result.IsSuccess)
			{
				_items = items;
				_index = index;
			}

			public override void WriteMessageTo(MessageWriter writer)
			{
				string collectionValue = getCollectionValue(writer);
				writer.WriteMessageLine(0, "Element # {0} from {1} failed to satisfy constraint.", _index, collectionValue);
				base.WriteMessageTo(writer);
			}

			private string getCollectionValue(MessageWriter writer)
			{
				writer.WriteActualValue(_items);
				var sb = writer.GetStringBuilder();
				string items = sb.ToString();
				sb.Remove(0, sb.Length);
				return items;
			}
		}
	}
}
