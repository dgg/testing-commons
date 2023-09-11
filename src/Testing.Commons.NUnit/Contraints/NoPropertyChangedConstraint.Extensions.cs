using System.ComponentModel;
using NUnit.Framework;

namespace Testing.Commons.NUnit.Constraints;

public partial class Doez : Does
{
	public static partial class NotRaise
	{

		/// <summary>
		/// Builds an instance of <see cref="PropertyChangedConstraint{TSubject}"/> that allows checking whether a type does not raise a
		/// <see cref="INotifyPropertyChanged.PropertyChanged"/> when a property is set.
		/// </summary>
		/// <typeparam name="TSubject">Type that does not raise the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</typeparam>
		/// <param name="subject"> Instance of the event not-raising type.</param>
		/// <returns>Instance built.</returns>
		public static NoPropertyChangedConstraint<TSubject> PropertyChanged<TSubject>(TSubject subject) where TSubject : INotifyPropertyChanged
		{
			return new NoPropertyChangedConstraint<TSubject>(subject);
		}
	}
}
