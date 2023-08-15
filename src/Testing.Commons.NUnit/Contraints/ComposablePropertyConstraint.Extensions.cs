using System.Linq.Expressions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

/*public static partial class ConstraintsExtensions
{
	/// <summary>
	/// Builds an instance of <see cref="ComposablePropertyConstraint"/> to check <paramref name="constraint"/> over the value of the property expressed by <paramref name="name"/>
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="name">The name of the property.</param>
	/// <param name="constraint">The constraint to apply to the property.</param>
	/// <returns>Instance built.</returns>
	public static ComposablePropertyConstraint Prop([NotNull] this ConstraintExpression expression, string name, Constraint constraint)
	{
		var compound = Haz.Prop(name, constraint);
		expression.Append(compound);
		return compound;
	}

	/// <summary>
	/// Builds an instance of <see cref="ComposablePropertyConstraint"/> to check <paramref name="constraint"/> over the value of the property expressed by <paramref name="property"/>
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="property">Expression that represents the name of the property.</param>
	/// <param name="constraint">The constraint to apply to the property.</param>
	/// <returns>Instance built.</returns>
	public static ComposablePropertyConstraint Prop<T>([NotNull] this ConstraintExpression expression, Expression<Func<T, object>> property, Constraint constraint)
	{
		var compound = Haz.Prop(property, constraint);
		expression.Append(compound);
		return compound;
	}
}*/

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
