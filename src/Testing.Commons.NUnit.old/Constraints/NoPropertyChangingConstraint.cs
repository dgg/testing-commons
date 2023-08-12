using System.ComponentModel;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Allows checking whether a type does not raise a <see cref="INotifyPropertyChanging.PropertyChanging"/> when a property is set.
	/// </summary>
	/// <typeparam name="TSubject">Type that does not raises the <see cref="INotifyPropertyChanging.PropertyChanging"/> event.</typeparam>
	public class NoPropertyChangingConstraint<TSubject> : NoRaisingConstraint<TSubject, PropertyChangingEventArgs> where TSubject : INotifyPropertyChanging
	{
		/// <summary>
		/// Instantiate the constraint
		/// </summary>
		/// <param name="subject"> Instance of the type not raising the event.</param>
		public NoPropertyChangingConstraint(TSubject subject)
			: base(subject) { }

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
			Subject.PropertyChanging += (sender, e) => OnEventRaised(e);
			del();
			// does not matter what is sent to the base as long as 'del' is executed
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