using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Provides a set of static methods to create custom constraints.
	/// </summary>
	public static class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="Testing.Commons.NUnit.Constraints.ConstrainedEnumerable"/> with the provided constraints.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Constrained(this Must.BeEntryPoint entry, params Constraint[] constraints)
		{
			return new ConstrainedEnumerable(constraints);
		}

		/// <summary>
		/// Builds an instance of <see cref="Testing.Commons.NUnit.Constraints.ConstrainedEnumerable"/> with the provided constraints.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Constrained(this Must.BeEntryPoint entry, IEnumerable<Constraint> constraints)
		{
			return new ConstrainedEnumerable(constraints);
		}

		/// <summary>
		/// Builds an instance of <see cref="MatchingConstraint"/> to match the provided expected object.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="expected">The object to match the actual value against.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Expected(this Must.MatchEntryPoint entry, object expected)
		{
			return new MatchingConstraint(expected);
		}

		/// <summary>
		/// Builds an instance of <see cref="LambdaPropertyConstraint{T}"/> to check <paramref name="constraint"/> over the value of the property expressed by <paramref name="property"/>
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="property">A member expression representing a property.</param>
		/// <param name="constraint">The constraint to apply to the property.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Property<T>(this Must.HaveEntryPoint entry, Expression<Func<T, object>> property,  Constraint constraint)
		{
			return new LambdaPropertyConstraint<T>(property, constraint);
		}

		/// <summary>
		/// Builds an instance of <see cref="LambdaPropertyConstraint{T}"/> to check the negated <paramref name="constraint"/> over the value of the property expressed by <paramref name="property"/>
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="property">A member expression representing a property.</param>
		/// <param name="constraint">The constraint which negation to apply to the property.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Property<T>(this Must.NotHaveEntryPoint entry, Expression<Func<T, object>> property, Constraint constraint)
		{
			return new NotConstraint(new LambdaPropertyConstraint<T>(property, constraint));
		}

		public static Constraint PropertyChanged<T>(this Must.RaiseEntryPoint entry, T subject, Expression<Func<T, object>> property) where T : INotifyPropertyChanged
		{
			return new RaisesPropertyChangedConstraint<T>(subject, property);
		}
	}
}
