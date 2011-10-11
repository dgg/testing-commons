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
		/// Test whether the constraint is satisfied by an
		/// ActualValueDelegate that returns the value to be tested.
		/// The default implementation simply evaluates the delegate
		/// but derived classes may override it to provide for delayed 
		/// processing.
		/// </summary>
		/// <param name="del">An ActualValueDelegate</param>
		/// <returns>True for success, false for failure</returns>
		public override bool Matches(ActualValueDelegate del)
		{
			Subject.PropertyChanged += (sender, e) => OnEventRaised(e);
			del();
			return Matched;
		}

		/// <summary>
		/// Name of the event.
		/// </summary>
		protected override string EventName { get { return "PropertyChanged"; } }
	}
}
