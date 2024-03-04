using System.ComponentModel;
using NUnit.Framework;

namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Custom constraints.
/// </summary>
public partial class Doez : Does
{
	/// <summary>
	/// Custom constraint composer
	/// </summary>
	public static partial class NotRaise
	{
		/// <summary>
		/// Builds an instance of <see cref="NoPropertyChangingConstraint{TSubject}"/> that allows checking whether a type does not raise a
		/// <see cref="INotifyPropertyChanging.PropertyChanging"/> when a property is set.
		/// </summary>
		/// <param name="subject"> Instance of the event not-raising type.</param>
		/// <typeparam name="TSubject">Type that does not raise the <see cref="INotifyPropertyChanging.PropertyChanging"/> event.</typeparam>
		/// <returns>Instance built.</returns>
		public static NoPropertyChangingConstraint<TSubject> PropertyChanging<TSubject>(TSubject subject) where TSubject : INotifyPropertyChanging
		{
			return new NoPropertyChangingConstraint<TSubject>(subject);
		}
	}
}
