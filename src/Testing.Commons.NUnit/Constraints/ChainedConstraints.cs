using System;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Allows delegating matching while maintaining the control over what the failure message will be.
	/// </summary>
	internal class ChainedConstraints
	{
		private readonly Func<Constraint>[] _rules;

		public ChainedConstraints(params Func<Constraint>[] rules)
		{
			_rules = rules;
		}

		public Constraint Offender { get; private set; }
		public bool Evaluate(object subject)
		{
			bool satisfied = true;
			Offender = null;
			for (int i = 0; i < _rules.Length; i++)
			{
				Offender = _rules[i]();
				satisfied = Offender.Matches(subject);
				if (!satisfied) break;
			}
			if (satisfied) Offender = null;
			return satisfied;
		}
	}
}