using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Provides a set of static methods to create custom constraints.
	/// </summary>
	public static class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="Testing.Commons.NUnit.Constraints.ConstrainedEnumerable"/> with the provided constraints.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Constrained(this Must.BeEntryPoint entry, params Constraint[] constraints)
		{
			return new ConstrainedEnumerable(constraints);
		}

		/// <summary>
		/// Builds an instance of <see cref="Testing.Commons.NUnit.Constraints.ConstrainedEnumerable"/> with the provided constraints.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Constrained(this Must.BeEntryPoint entry, IEnumerable<Constraint> constraints)
		{
			return new ConstrainedEnumerable(constraints);
		}
	}
}
