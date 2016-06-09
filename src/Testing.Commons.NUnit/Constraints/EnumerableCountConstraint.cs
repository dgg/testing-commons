using System;
using System.Collections;
using System.Linq;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints.Support;

namespace Testing.Commons.NUnit.Constraints
{
	public class EnumerableCountConstraint : Constraint
	{
		private readonly Constraint _countConstraint;

		public EnumerableCountConstraint(Constraint countConstraint)
		{
			_countConstraint = countConstraint;
		}

		private Constraint _inner;
		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			var result = new ConstraintResult(this, actual, true);
			_inner = new TypeRevealingConstraint(typeof(IEnumerable));
			result = _inner.ApplyTo(actual);
			if (result.IsSuccess)
			{
				var collection = (IEnumerable)actual;
				// ReSharper disable PossibleMultipleEnumeration
				ushort count = calculateCount(collection);
				_inner = new CountConstraint(_countConstraint, collection);
				// ReSharper restore PossibleMultipleEnumeration
				result = _inner.ApplyTo(count);
			}
			return result;
		}

		private ushort calculateCount(IEnumerable current)
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
				return new TypeRevealingResult(this, actual, base.ApplyTo(actual));
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

	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="EnumerableCountConstraint"/> that allows asserting on the number of elements of any instance of <see cref="System.Collections.IEnumerable"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="countConstraint">The constraint to be applied to the element count.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Count(this Must.HaveEntryPoint entry, Constraint countConstraint)
		{
			return new EnumerableCountConstraint(countConstraint);
		}
	}
}