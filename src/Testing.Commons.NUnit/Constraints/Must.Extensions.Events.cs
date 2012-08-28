using System;
using System.ComponentModel;
using System.Linq.Expressions;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public static partial class MustExtensions
	{
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
		/// Builds an instance of <see cref="PropertyChangedConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanged.PropertyChanged"/> when a property is set.
		/// </summary>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</typeparam>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="eventArgsConstraint">Constraint to be applied to the event arg property.</param>
		/// <returns>Instance built.</returns>
		public static PropertyChangedConstraint<TSubject> PropertyChanged<TSubject>(this Must.RaiseEntryPoint entry, TSubject subject, Constraint eventArgsConstraint) where TSubject : INotifyPropertyChanged
		{
			return new PropertyChangedConstraint<TSubject>(subject, eventArgsConstraint);
		}

		/// <summary>
		/// Builds an instance of <see cref="PropertyChangedConstraint{TSubject}"/> that allows checking whether a type does not raise a
		/// <see cref="INotifyPropertyChanged.PropertyChanged"/> when a property is set.
		/// </summary>
		/// <typeparam name="TSubject">Type that does not raise the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</typeparam>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="subject"> Instance of the type not raising the event.</param>
		/// <returns>Instance built.</returns>
		public static NoPropertyChangedConstraint<TSubject> PropertyChanged<TSubject>(this Must.NotRaiseEntryPoint entry, TSubject subject) where TSubject : INotifyPropertyChanged
		{
			return new NoPropertyChangedConstraint<TSubject>(subject);
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
		/// Builds an instance of <see cref="PropertyChangingConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanging.PropertyChanging"/> when a property is set.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="eventArgsConstraint">Constraint to be applied to the event arg property.</param>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanging.PropertyChanging"/> event.</typeparam>
		/// <returns>Instance built.</returns>
		public static PropertyChangingConstraint<TSubject> PropertyChanging<TSubject>(this Must.RaiseEntryPoint entry, TSubject subject, Constraint eventArgsConstraint) where TSubject : INotifyPropertyChanging
		{
			return new PropertyChangingConstraint<TSubject>(subject, eventArgsConstraint);
		}

		/// <summary>
		/// Builds an instance of <see cref="PropertyChangingConstraint{TSubject}"/> that allows checking whether a type does not raise a
		/// <see cref="INotifyPropertyChanging.PropertyChanging"/> when a property is set.
		/// </summary>
		/// <typeparam name="TSubject">Type that does not raise the <see cref="INotifyPropertyChanging.PropertyChanging"/> event.</typeparam>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="subject"> Instance of the type not raising the event.</param>
		/// <returns>Instance built.</returns>
		public static NoPropertyChangingConstraint<TSubject> PropertyChanging<TSubject>(this Must.NotRaiseEntryPoint entry, TSubject subject) where TSubject : INotifyPropertyChanging
		{
			return new NoPropertyChangingConstraint<TSubject>(subject);
		}
	}
}