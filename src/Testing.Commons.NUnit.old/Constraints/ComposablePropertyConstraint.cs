using System;
using System.Linq.Expressions;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Extracts a named property and uses its value as the actual value for the provided constraint. 
	/// </summary>
	public class ComposablePropertyConstraint : PropertyConstraint
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ComposablePropertyConstraint"/> class.
		/// </summary>
		/// <param name="name">The name of the property.</param>
		/// <param name="constraint">The constraint to apply to the property.</param>
		public ComposablePropertyConstraint(string name, IConstraint constraint) : base(name, constraint) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ComposablePropertyConstraint"/> class.
		/// </summary>
		/// <typeparam name="T">Type of the member container.</typeparam>
		/// <param name="property">Expression that represents the name of the property.</param>
		/// <param name="constraint"></param>
		/// <returns></returns>
		public static ComposablePropertyConstraint New<T>(Expression<Func<T, object>> property, Constraint constraint)
		{
			return new ComposablePropertyConstraint(Name.Of(property), constraint);
		}
	}

	/// <summary>
	/// Provides a set of static methods to create custom constraints.
	/// </summary>
	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="ComposablePropertyConstraint"/> to check <paramref name="constraint"/> over the value of the property expressed by <paramref name="name"/>
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="name">The name of the property.</param>
		/// <param name="constraint">The constraint to apply to the property.</param>
		/// <returns>Instance built.</returns>
		public static ComposablePropertyConstraint Property(this Must.HaveEntryPoint entry, string name, Constraint constraint)
		{
			return new ComposablePropertyConstraint(name, constraint);
		}

		/// <summary>
		/// Builds an instance of <see cref="ComposablePropertyConstraint"/> to check <paramref name="constraint"/> over the value of the property expressed by <paramref name="property"/>
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="property">Expression that represents the name of the property.</param>
		/// <param name="constraint">The constraint to apply to the property.</param>
		/// <returns>Instance built.</returns>
		public static ComposablePropertyConstraint Property<T>(this Must.HaveEntryPoint entry, Expression<Func<T, object>> property, Constraint constraint)
		{
			return ComposablePropertyConstraint.New(property, constraint);
		}
	}
}