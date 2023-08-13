using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Custom date constraint extensions.
/// </summary>
public static partial class ConstraintsExtensions
{
	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs after the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <returns>Compound constraint built.</returns>
	public static GreaterThanConstraint After([NotNull] this ConstraintExpression expression, DateTime date)
	{
		var constraint = new GreaterThanConstraint(date);
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs before the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <returns>Compound constraint built.</returns>
	public static Constraint Before([NotNull] this ConstraintExpression expression, DateTime date)
	{
		var constraint = new LessThanConstraint(date);
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs either on, or after the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <returns>Compound constraint built.</returns>
	public static Constraint OnOrAfter([NotNull] this ConstraintExpression expression, DateTime date)
	{
		var constraint = new GreaterThanOrEqualConstraint(date);
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs either on, or before the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <returns>Compound constraint built.</returns>
	public static Constraint OnOrBefore([NotNull] this ConstraintExpression expression, DateTime date)
	{
		var constraint = new LessThanOrEqualConstraint(date);
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs within the specified time span (20 ms by default) from
	/// the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <param name="within">Tolerance determining equality.</param>
	/// <returns>Compound constraint built.</returns>
	public static Constraint CloseTo([NotNull] this ConstraintExpression expression, DateTime date, TimeSpan within)
	{
		var constraint = new EqualConstraint(date).Within(within);
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs within the specified time span (20 ms by default) from
	/// the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <param name="ms">Tolerance (in milliseconds) determining equality.</param>
	/// <returns>Compound constraint built.</returns>
	public static Constraint CloseTo([NotNull] this ConstraintExpression expression, DateTime date, uint ms = 20)
	{
		return CloseTo(expression, date, TimeSpan.FromMilliseconds(ms));
	}
}

/// <summary>
/// Custom date constraints.
/// </summary>
public partial class Iz : Is
{
	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs after the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <returns>Constraint built.</returns>
	public static GreaterThanConstraint After(DateTime date)
	{
		return GreaterThan(date);
	}
	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs before the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint Before(DateTime date)
	{
		return LessThan(date);
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs either on, or after the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint OnOrAfter(DateTime date)
	{
		return GreaterThanOrEqualTo(date);
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs either on, or before the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint OnOrBefore(DateTime date)
	{
		return LessThanOrEqualTo(date);
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs within the specified time span (20 ms by default) from
	/// the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <param name="within">Tolerance determining equality.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint CloseTo(DateTime date, TimeSpan within)
	{
		return EqualTo(date).Within(within);
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value occurs within the specified number of milliseconds (20 by default) from
	/// the specified <paramref name="date"/>.
	/// </summary>
	/// <param name="date">The expected date to compare the actual value with.</param>
	/// <param name="ms">Tolerance (in milliseconds) determining equality.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint CloseTo(DateTime date, uint ms = 20)
	{
		return CloseTo(date, TimeSpan.FromMilliseconds(ms));
	}
}
