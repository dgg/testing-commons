using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Subjects.Comparision.Offending;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class ComparisonImplementationConstraint
	{
		[Test]
		public void NotEqualToSelf_Fails()
		{
			NeverEqual subject = new NeverEqual("subject"), lt = new NeverEqual("lt"), gt = new NeverEqual("gt");
			Assert.That(subject, Must.Satisfy.ComparableSpecificationAgainst(lt, gt));
		}

		[Test]
		public void NotGreaterThanAnything_Fails()
		{
			NeverGreater subject = new NeverGreater("subject"),
				lt = new NeverGreater("lt"),
				gt = new NeverGreater("gt");

			Assert.That(subject, Must.Satisfy.ComparableSpecificationAgainst(lt, gt));
		}

		[Test]
		public void NotLessThanAnything_Fails()
		{
			NeverLess subject = new NeverLess("subject"),
				lt = new NeverLess("lt"),
				gt = new NeverLess("gt");

			Assert.That(subject, Must.Satisfy.ComparableSpecificationAgainst(lt, gt));
		}

		[Test]
		public void NotEqualToSelfString_Fails()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var notEqual= new ComparableSubject<string>("subject")
				.Setup(eq, int.MaxValue);
			
			Assert.That(notEqual, Must.Satisfy.ComparableSpecificationAgainst(lt, gt, eq));
		}

		[Test]
		public void NotGreaterThanAnyStr_Fails()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var notMore = new ComparableSubject<string>("subject")
				.Setup(eq, 0)
				.Setup(lt, int.MinValue);

			Assert.That(notMore, Must.Satisfy.ComparableSpecificationAgainst(lt, gt, eq));
		}


		[Test]
		public void NotLessThanAnyString_Fails()
		{
			string eq = "eq", lt  = "lt", gt = "gt";
			var notLess = new ComparableSubject<string>("subject")
				.Setup(eq, 0)
				.Setup(lt, int.MinValue)
				.Setup(gt, -1);

			Assert.That(notLess, Must.Satisfy.ComparableSpecificationAgainst(lt, gt, eq));
		}
	}
}
