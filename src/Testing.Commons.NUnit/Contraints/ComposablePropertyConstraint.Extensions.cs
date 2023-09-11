using System.Linq.Expressions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

public partial class Haz : Has
{

	/// <summary>
	/// Builds an instance of <see cref="ComposablePropertyConstraint"/> to check <paramref name="constraint"/> over the value of the property expressed by <paramref name="name"/>
	/// </summary>
	/// <param name="name">The name of the property.</param>
	/// <param name="constraint">The constraint to apply to the property.</param>
	/// <returns>Instance built.</returns>
	public static ComposablePropertyConstraint Prop(string name, Constraint constraint)
	{
		return new ComposablePropertyConstraint(name, constraint);
	}

	/// <summary>
	/// Builds an instance of <see cref="ComposablePropertyConstraint"/> to check <paramref name="constraint"/> over the value of the property expressed by <paramref name="property"/>
	/// </summary>
	/// <param name="property">Expression that represents the name of the property.</param>
	/// <param name="constraint">The constraint to apply to the property.</param>
	/// <returns>Instance built.</returns>
	public static ComposablePropertyConstraint Prop<T>(Expression<Func<T, object>> property, Constraint constraint)
	{
		return ComposablePropertyConstraint.New(property, constraint);
	}
}
