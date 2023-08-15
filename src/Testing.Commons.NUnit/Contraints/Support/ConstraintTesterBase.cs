using System.Diagnostics.CodeAnalysis;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;

namespace Testing.Commons.NUnit.Constraints.Support;

/// <summary>
/// Eases the testing activities of custom constraints.
/// </summary>
public abstract class ConstraintTesterBase
{
	/// <summary>
	/// Gets the error message of a failing contraint.
	/// </summary>
	/// <param name="subject">Constraint to be tested.</param>
	/// <param name="actual">The value to be tested.</param>
	/// <typeparam name="T">Type of the tested value.</typeparam>
	/// <returns>The message of a failing contraint or <see cref="string.Empty"/> if is not failing.</returns>
	protected static string getMessage<T>([NotNull] Constraint subject, T actual)
	{
		string message = getMessage(subject.ApplyTo(actual));
		return message;
	}

	/// <summary>
	/// Gets the error message of a failing contraint.
	/// </summary>
	/// <param name="subject">Constraint to be tested.</param>
	/// <param name="actual">The action to be tested.</param>
	/// <typeparam name="T">Type of the tested value.</typeparam>
	/// <returns>The message of a failing contraint or <see cref="string.Empty"/> if is not failing.</returns>
	protected static string getMessage<T>([NotNull] Constraint subject, ActualValueDelegate<T> actual)
	{
		string message = getMessage(subject.ApplyTo(actual));
		return message;
	}

	private static string getMessage(ConstraintResult result)
	{
		string message = string.Empty;

		if (!result.IsSuccess)
		{
			using var writer = new TextMessageWriter();
			result.WriteMessageTo(writer);

			message = writer.ToString();
		}

		return message;

	}

	/// <summary>
	/// Checks the success of a constraint.
	/// </summary>
	/// <param name="subject">Constraint to be tested.</param>
	/// <param name="actual">The value to be tested.</param>
	/// <typeparam name="T">Type of the tested value.</typeparam>
	/// <returns><code>true</code> if the constraint matched, <code>false</code> otherwise.</returns>
	protected static bool matches<T>([NotNull] Constraint subject, T actual)
	{
		ConstraintResult result = subject.ApplyTo(actual);
		return result.IsSuccess;
	}

	/// <summary>
	/// Checks the success of a constraint.
	/// </summary>
	/// <param name="subject">Constraint to be tested.</param>
	/// <param name="actual">The action to be tested.</param>
	/// <typeparam name="T">Type of the tested value.</typeparam>
	/// <returns><code>true</code> if the constraint matched, <code>false</code> otherwise.</returns>
	protected static bool matches<T>([NotNull] Constraint subject, [NotNull] ActualValueDelegate<T> actual)
	{
		ConstraintResult result = subject.ApplyTo(actual);
		return result.IsSuccess;
	}
}
