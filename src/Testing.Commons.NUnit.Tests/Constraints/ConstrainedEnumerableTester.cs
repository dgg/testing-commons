using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class ConstrainedEnumerableTester : Support.ConstraintTesterBase
	{
		#region ApplyTo

		#endregion

		#region WriteMessageTo

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			Assert.That(new[] { 1, 2 }, new ConstrainedEnumerable(Is.EqualTo(1), Is.LessThan(3)));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			Assert.That(new[] { 1, 2 }, Must.Be.Constrained(Is.EqualTo(1), Is.LessThan(3)));
		}
	}

	public class ConstrainedEnumerable : Constraint
	{
		private readonly Constraint[] _constraints;

		/// <summary>
		/// Builds an instance with the provided constraints.
		/// </summary>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		public ConstrainedEnumerable(params Constraint[] constraints) : this(constraints.AsEnumerable()) { }

		/// <summary>
		/// Builds an instance with the provided constraints.
		/// </summary>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		public ConstrainedEnumerable(IEnumerable<Constraint> constraints)
		{
			_constraints = constraints.ToArray();
		}

		public override ConstraintResult ApplyTo<TActual>(TActual actual)
		{
			return new ConstraintResult(this, actual, true);
		}
	}

	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="ConstrainedEnumerable"/> with the provided constraints.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		/// <returns>Instance built.</returns>
		public static ConstrainedEnumerable Constrained(this Must.BeEntryPoint entry, params Constraint[] constraints)
		{
			return new ConstrainedEnumerable(constraints);
		}
	}
}