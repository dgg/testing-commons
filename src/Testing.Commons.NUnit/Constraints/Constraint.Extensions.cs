using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public static class ConstraintExtensions
	{
		public static IResolveConstraint And(this Constraint entry, Constraint nextInChain)
		{
			return entry & nextInChain;
		}
	}
}
