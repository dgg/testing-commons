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
		var constraint = Iz.After(date);
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
		var constraint = Iz.Before(date);
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
		var constraint = Iz.OnOrAfter(date);
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
		var constraint = Iz.OnOrBefore(date);
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
		var constraint = Iz.CloseTo(date, within);
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
		var constraint = Iz.CloseTo(date, TimeSpan.FromMilliseconds(ms));
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="year"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="year">Expected property of the current date.</param>
	/// <returns>Compound constraint built.</returns>
	public static Constraint Year([NotNull] this ConstraintExpression expression, uint year)
	{
		var constraint = Haz.Year(year);
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="month"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="month">Expected property of the current date.</param>
	/// <returns>Compound constraint built.</returns>

	public static Constraint Month([NotNull] this ConstraintExpression expression, byte month)
	{
		var constraint = Haz.Month(month);
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="day"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="day">Expected property of the current date.</param>
	/// <returns>Compound constraint built.</returns>

	public static Constraint Day([NotNull] this ConstraintExpression expression, byte day)
	{
		var constraint = Haz.Day(day);
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="hour"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="hour">Expected property of the current date.</param>
	/// <returns>Compound constraint built.</returns>

	public static Constraint Hour([NotNull] this ConstraintExpression expression, byte hour)
	{
		var constraint = Haz.Hour(hour);
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="minute"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="minute">Expected property of the current date.</param>
	/// <returns>Compound constraint built.</returns>

	public static Constraint Minute([NotNull] this ConstraintExpression expression, byte minute)
	{
		var constraint = Haz.Minute(minute);
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="second"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="second">Expected property of the current date.</param>
	/// <returns>Compound constraint built.</returns>

	public static Constraint Second([NotNull] this ConstraintExpression expression, byte second)
	{
		var constraint = Haz.Second(second);
		expression.Append(constraint);
		return constraint;
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="millisecond"/>.
	/// </summary>
	/// <param name="expression">Compound constraint being constructed.</param>
	/// <param name="millisecond">Expected property of the current date.</param>
	/// <returns>Compound constraint built.</returns>

	public static Constraint Millisecond([NotNull] this ConstraintExpression expression, ushort millisecond)
	{
		var constraint = Haz.Millisecond(millisecond);
		expression.Append(constraint);
		return constraint;
	}
}

/// <summary>
/// Custom constraints.
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

/// <summary>
/// Custom constraints.
/// </summary>
public partial class Haz : Has
{
	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="year"/>.
	/// </summary>
	/// <param name="year">Expected property of the current date.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint Year(uint year)
	{
		return Property("Year").EqualTo(year);
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="month"/>.
	/// </summary>
	/// <param name="month">Expected property of the current date.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint Month(byte month)
	{
		return Property("Month").EqualTo(month);
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="day"/>.
	/// </summary>
	/// <param name="day">Expected property of the current date.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint Day(byte day)
	{
		return Property("Day").EqualTo(day);
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="hour"/>.
	/// </summary>
	/// <param name="hour">Expected property of the current date.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint Hour(byte hour)
	{
		return Property("Hour").EqualTo(hour);
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="minute"/>.
	/// </summary>
	/// <param name="minute">Expected property of the current date.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint Minute(byte minute)
	{
		return Property("Minute").EqualTo(minute);
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="second"/>.
	/// </summary>
	/// <param name="second">Expected property of the current date.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint Second(byte second)
	{
		return Property("Second").EqualTo(second);
	}

	/// <summary>
	/// Builds an instance of <see cref="Constraint"/> that tests whether the actual
	/// value has the provided <paramref name="millisecond"/>.
	/// </summary>
	/// <param name="millisecond">Expected property of the current date.</param>
	/// <returns>Contraint built.</returns>
	public static Constraint Millisecond(ushort millisecond)
	{
		return Property("Millisecond").EqualTo(millisecond);
	}
}
