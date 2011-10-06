using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Used to check every item of an enumerable against a constraint.
	/// </summary>
	public class ConstrainedEnumerable : Constraint
	{
		/// <summary>
		/// Builds an instance with the provided constraints.
		/// </summary>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		public ConstrainedEnumerable(params Constraint[] constraints)
		{
			_constraints = constraints;
		}

		private readonly Constraint[] _constraints;
		/// <summary>
		/// Builds an instance with the provided constraints.
		/// </summary>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		public ConstrainedEnumerable(IEnumerable<Constraint> constraints)
		{
			_constraints = constraints.ToArray();
		}

		private Constraint _inner;
		private int _index;
		Array _collection;
		
		/// <summary>
		/// Test whether the constraint is satisfied by a given value.
		/// </summary>
		/// <param name="current">The value to be tested.</param>
		/// <returns>True for success, false for failure.</returns>
		public override bool Matches(object current)
		{
			actual = current;

			// it is ok to iterate the collection as most of the times it will be a small controlled collection
			_collection = (current as IEnumerable).Cast<object>().ToArray();

			bool result = sameNumberOfElementsAndConstraints(_collection, _constraints.Length);
			if (result)
			{
				for (_index = 0; _index < _collection.Length && result; _index++)
				{
					_inner = new IndexedConstraint(_collection, _index, _constraints[_index]);
					result = _inner.Matches(_collection.GetValue(_index));
				}
			}
			return result;
		}

		private bool sameNumberOfElementsAndConstraints(Array current, int numberOfConstraints)
		{
			_inner = new MatchingLength(current, current.Length);
			return _inner.Matches(numberOfConstraints);
		}

		/// <summary>
		/// Write the constraint description to a MessageWriter.
		/// </summary>
		/// <param name="writer">The writer on which the description is displayed.</param>
		public override void WriteDescriptionTo(MessageWriter writer)
		{
			_inner.WriteDescriptionTo(writer);
		}

		/// <summary>
		/// Write the failure message to the MessageWriter provided
		/// as an argument. The default implementation simply passes
		/// the constraint and the actual value to the writer, which
		/// then displays the constraint description and the value.
		/// 
		/// Constraints that need to provide additional details,
		/// such as where the error occured can override this.
		/// </summary>
		/// <param name="writer">The MessageWriter on which to display the message</param>
		public override void WriteMessageTo(MessageWriter writer)
		{
			_inner.WriteMessageTo(writer);
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

		/// <summary>
		/// Encapsulates a contraint applied to an enumerable element to enhance the feedback in case of failure by providing the index of the element that provoked the failure.
		/// </summary>
		class IndexedConstraint : Constraint
		{
			private readonly Array _items;
			private readonly int _index;
			private readonly Constraint _constraint;
			public IndexedConstraint(Array items, int index, IResolveConstraint constraint)
			{
				_items = items;
				_index = index;
				_constraint = constraint.Resolve();
			}

			public override bool Matches(object current)
			{
				actual = current;
				return _constraint.Matches(current);
			}

			public override void WriteDescriptionTo(MessageWriter writer)
			{
				_constraint.WriteDescriptionTo(writer);
			}

			public override void WriteActualValueTo(MessageWriter writer)
			{
				_constraint.WriteActualValueTo(writer);
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
