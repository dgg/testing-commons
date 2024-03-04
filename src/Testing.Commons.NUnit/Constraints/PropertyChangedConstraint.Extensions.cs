using System.ComponentModel;
using System.Linq.Expressions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

public partial class Doez : Does
{
	public static partial class Raise
	{

		/// <summary>
		/// Builds an instance of <see cref="PropertyChangedConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanged.PropertyChanged"/> when a property is set.
		/// </summary>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</typeparam>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="property">Expression that represents the name of a property.</param>
		/// <returns>Instance built.</returns>
		public static PropertyChangedConstraint<TSubject> PropertyChanged<TSubject>(TSubject subject, Expression<Func<TSubject, object>> property) where TSubject : INotifyPropertyChanged
		{
			return new PropertyChangedConstraint<TSubject>(subject, property);
		}

		/// <summary>
		/// Builds an instance of <see cref="PropertyChangedConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanged.PropertyChanged"/> when a property is set.
		/// </summary>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</typeparam>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="eventArgsConstraint">Constraint to be applied to the event arg property.</param>
		/// <returns>Instance built.</returns>
		public static PropertyChangedConstraint<TSubject> PropertyChanged<TSubject>(TSubject subject, Constraint eventArgsConstraint) where TSubject : INotifyPropertyChanged
		{
			return new PropertyChangedConstraint<TSubject>(subject, eventArgsConstraint);
		}
	}
}
