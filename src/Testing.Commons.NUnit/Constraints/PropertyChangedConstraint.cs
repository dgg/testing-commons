using System;
using System.ComponentModel;
using System.Linq.Expressions;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Allows checking whether a type raises a <see cref="INotifyPropertyChanged.PropertyChanged"/> when a property is set.
	/// </summary>
	/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</typeparam>
	public class PropertyChangedConstraint<TSubject> : RaisingConstraint<TSubject, PropertyChangedEventArgs> where TSubject : INotifyPropertyChanged
	{
		/// <summary>
		/// Instantiate the constraint
		/// </summary>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="property">Expression that represents the name of a property.</param>
		public PropertyChangedConstraint(TSubject subject, Expression<Func<TSubject, object>> property)
			: base(subject, property, c => Must.Have.Property<PropertyChangedEventArgs>(e => e.PropertyName, c)) { }

		/// <summary>
		/// Instantiate the constraint
		/// </summary>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="eventArgsConstraint">Constraint to be applied to the event arg property.</param>
		public PropertyChangedConstraint(TSubject subject, Constraint eventArgsConstraint)
			: base(subject, eventArgsConstraint, c => Must.Have.Property<PropertyChangedEventArgs>(e => e.PropertyName, c)) { }

		/// <summary>
		/// Applies the constraint to an ActualValueDelegate that returns
		/// the value to be tested. The default implementation simply evaluates
		/// the delegate but derived classes may override it to provide for
		/// delayed processing.
		/// </summary>
		/// <param name="del">An ActualValueDelegate</param>
		/// <returns>A ConstraintResult</returns>
		public override ConstraintResult ApplyTo<TActual>(ActualValueDelegate<TActual> del)
		{
			Subject.PropertyChanged += (sender, e) => onEventRaised(e);
			del();
			return result();
		}

		/// <summary>
		/// Name of the event.
		/// </summary>
		protected override string EventName => nameof(INotifyPropertyChanged.PropertyChanged);
	}

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
	}
}