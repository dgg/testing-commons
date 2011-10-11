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
		public static LambdaPropertyConstraint<T> Property<T>(this Must.HaveEntryPoint entry, Expression<Func<T, object>> property, Constraint constraint)
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

		/// <summary>
		/// Builds an instance of <see cref="PropertyChangedConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanged.PropertyChanged"/> when a property is set.
		/// </summary>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</typeparam>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="property">Expression that represents the name of a property.</param>
		/// <returns>Instance built.</returns>
		public static PropertyChangedConstraint<TSubject> PropertyChanged<TSubject>(this Must.RaiseEntryPoint entry, TSubject subject, Expression<Func<TSubject, object>> property) where TSubject : INotifyPropertyChanged
		{
			return new PropertyChangedConstraint<TSubject>(subject, property);
		}

		/// <summary>
		/// Builds a negated instance of <see cref="PropertyChangedConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanged.PropertyChanged"/> when a property is set.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="property">Expression that represents the name of a property.</param>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</typeparam>
		/// <returns>Instance built.</returns>
		public static Constraint PropertyChanged<TSubject>(this Must.NotRaiseEntryPoint entry, TSubject subject, Expression<Func<TSubject, object>> property) where TSubject : INotifyPropertyChanged
		{
			return new NotConstraint(new PropertyChangedConstraint<TSubject>(subject, property));
		}

		/// <summary>
		/// Builds an instance of <see cref="PropertyChangingConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanging.PropertyChanging"/> when a property is set.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="property">Expression that represents the name of a property.</param>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanging.PropertyChanging"/> event.</typeparam>
		/// <returns>Instance built.</returns>
		public static PropertyChangingConstraint<TSubject> PropertyChanging<TSubject>(this Must.RaiseEntryPoint entry, TSubject subject, Expression<Func<TSubject, object>> property) where TSubject : INotifyPropertyChanging
		{
			return new PropertyChangingConstraint<TSubject>(subject, property);
		}

		/// <summary>
		/// Builds a negated instance of <see cref="PropertyChangingConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanging.PropertyChanging"/> when a property is set.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="property">Expression that represents the name of a property.</param>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanging.PropertyChanging"/> event.</typeparam>
		/// <returns>Instance built.</returns>
		public static Constraint PropertyChanging<TSubject>(this Must.NotRaiseEntryPoint entry, TSubject subject, Expression<Func<TSubject, object>> property) where TSubject : INotifyPropertyChanging
		{
			return new NotConstraint(new PropertyChangingConstraint<TSubject>(subject, property));
		}
	}
}
