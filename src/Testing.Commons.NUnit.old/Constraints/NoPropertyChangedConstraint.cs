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
		public NoPropertyChangedConstraint(TSubject subject) : base(subject) { }

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
			Subject.PropertyChanged += (sender, e) => OnEventRaised(e);
			del();
			// does not matter what is sent to the base as long as 'del' is executed
			return base.ApplyTo(del);
		}

		/// <summary>
		/// Name of the event.
		/// </summary>
		protected override string EventName => nameof(INotifyPropertyChanged.PropertyChanged);
	}

	public static partial class MustExtensions
	{
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
	}
}