using System.ComponentModel;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Allows checking whether a type does not raise a <see cref="INotifyPropertyChanged.PropertyChanged"/> when a property is set.
	/// </summary>
	/// <typeparam name="TSubject">Type that does not raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</typeparam>
	public class NoPropertyChangedConstraint<TSubject> : NoRaisingConstraint<TSubject, PropertyChangedEventArgs> where TSubject : INotifyPropertyChanged
	{
		/// <summary>
		/// Instantiate the constraint
		/// </summary>
		/// <param name="subject"> Instance of the type not raising the event.</param>
		public NoPropertyChangedConstraint(TSubject subject)
			: base(subject) { }

		/// <summary>
		/// Test whether the constraint is satisfied by an
		/// ActualValueDelegate that returns the value to be tested.
		/// The default implementation simply evaluates the delegate
		/// but derived classes may override it to provide for delayed 
		/// processing.
		/// </summary>
		/// <param name="del">An ActualValueDelegate</param>
		/// <returns>True for success, false for failure</returns>
		public override bool Matches<T>(ActualValueDelegate<T> del)
		{
			Subject.PropertyChanged += (sender, e) => OnEventRaised(e);
			del();
			// does not matter what is sent to the base as long as 'del' is executed
			return base.Matches(del);
		}

		/// <summary>
		/// Name of the event.
		/// </summary>
		protected override string EventName { get { return "PropertyChanged"; } }
	}
}