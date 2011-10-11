using System;
using System.Linq.Expressions;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Extracts a named property and uses its value as the actual value for a chained constraint.
	/// </summary>
	public class LambdaPropertyConstraint<T> : PropertyConstraint
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LambdaPropertyConstraint{T}"/> class.
		/// </summary>
		/// <param name="property">Expression that represents the name of the property.</param>
		/// <param name="constraint">The constraint to apply to the property.</param>
		public LambdaPropertyConstraint(Expression<Func<T, object>> property, Constraint constraint)
			: base(Name.Of(property), constraint) { }
	}
}
