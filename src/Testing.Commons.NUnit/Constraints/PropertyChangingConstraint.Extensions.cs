using System.ComponentModel;
using System.Linq.Expressions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Custom constraints.
/// </summary>
public partial class Doez : Does
{
	/// <summary>
	/// Custom constraint composer
	/// </summary>
	public static partial class Raise
	{
		/// <summary>
		/// Builds an instance of <see cref="PropertyChangingConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanging.PropertyChanging"/> when a property is set.
		/// </summary>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="property">Expression that represents the name of a property.</param>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanging.PropertyChanging"/> event.</typeparam>
		/// <returns>Instance built.</returns>
		public static PropertyChangingConstraint<TSubject> PropertyChanging<TSubject>(TSubject subject, Expression<Func<TSubject, object>> property) where TSubject : INotifyPropertyChanging
		{
			return new PropertyChangingConstraint<TSubject>(subject, property);
		}

		/// <summary>
		/// Builds an instance of <see cref="PropertyChangingConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanging.PropertyChanging"/> when a property is set.
		/// </summary>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="eventArgsConstraint">Constraint to be applied to the event arg property.</param>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanging.PropertyChanging"/> event.</typeparam>
		/// <returns>Instance built.</returns>
		public static PropertyChangingConstraint<TSubject> PropertyChanging<TSubject>(TSubject subject, Constraint eventArgsConstraint) where TSubject : INotifyPropertyChanging
		{
			return new PropertyChangingConstraint<TSubject>(subject, eventArgsConstraint);
		}
	}
}
