using System;
using System.ComponentModel;
using System.Linq.Expressions;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Allows checking whether a type raises a <see cref="INotifyPropertyChanging.PropertyChanging"/> when a property is set.
	/// </summary>
	/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanging.PropertyChanging"/> event.</typeparam>
	public class PropertyChangingConstraint<TSubject> : RaisingConstraint<TSubject, PropertyChangingEventArgs> where TSubject : INotifyPropertyChanging
	{
		/// <summary>
		/// Instantiate the constraint
		/// </summary>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="property">Expression that represents the name of a property.</param>
		public PropertyChangingConstraint(TSubject subject, Expression<Func<TSubject, object>> property)
			: base(subject, property, c => Must.Have.Property<PropertyChangingEventArgs>(e => e.PropertyName, c)) { }

		/// <summary>
		/// Instantiate the constraint
		/// </summary>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="eventArgsConstraint">Constraint to be applied to the event arg property.</param>
		public PropertyChangingConstraint(TSubject subject, Constraint eventArgsConstraint)
			: base(subject, eventArgsConstraint, c => Must.Have.Property<PropertyChangingEventArgs>(e => e.PropertyName, c)) { }

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
			Subject.PropertyChanging += (sender, e) => onEventRaised(e);
			del();
			return base.ApplyTo(del);
		}

		/// <summary>
		/// Name of the event.
		/// </summary>
		protected override string EventName => nameof(INotifyPropertyChanging.PropertyChanging);
	}

	public static partial class MustExtensions
	{
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
	}
}