using NUnit.Framework;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.NUnit.Tests.Subjects.Comparisons;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class ImplementsComparisonConstraintTester : ConstraintTesterBase
	{
		#region Match

		[Test]
		public void Matches_CorrectImplementationReference_True()
		{
			ComparisonAgainstReference.GTOET = ComparisonAgainstReference.LTOET = ComparisonAgainstReference.GT = ComparisonAgainstReference.LT = true;
			var target = new ComparisonAgainstReference("target");
			string eq = "eq", lt = "lt", gt = "gt";

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstReference, string>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_CorrectImplementationValue_True()
		{
			ComparisonAgainstValue.GTOET = ComparisonAgainstValue.LTOET = ComparisonAgainstValue.GT = ComparisonAgainstValue.LT = true;
			var target = new ComparisonAgainstValue("target");
			int eq = 20, lt = 10, gt = 30;

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstValue, int>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_CorrectImplementationInverseReference_True()
		{
			ComparisonAgainstReference.GTOET = ComparisonAgainstReference.LTOET = ComparisonAgainstReference.GT = ComparisonAgainstReference.LT = true;
			var target = "target";
			ComparisonAgainstReference eq = new ComparisonAgainstReference("eq"), lt = new ComparisonAgainstReference("lt"), gt = new ComparisonAgainstReference("gt");
			var subject = new ImplementsComparisonConstraint<string, ComparisonAgainstReference>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_CorrectImplementationSelf_True()
		{
			ComparisonAgainstSelf.GTOET = ComparisonAgainstSelf.LTOET = ComparisonAgainstSelf.GT = ComparisonAgainstSelf.LT = true;
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_NotGreaterThanLess_False()
		{
			ComparisonAgainstSelf.GTOET = ComparisonAgainstSelf.LTOET = true;
			ComparisonAgainstSelf.GT = false;

			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotLessThanGreater_False()
		{
			ComparisonAgainstSelf.GTOET = ComparisonAgainstSelf.LTOET = ComparisonAgainstSelf.GT = true;
			ComparisonAgainstSelf.LT = false;

			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotGreaterThanOtherLess_False()
		{
			ComparisonAgainstReference.GTOET = ComparisonAgainstReference.LTOET = true;
			ComparisonAgainstReference.GT = false;

			var target = new ComparisonAgainstReference("target");
			string eq = "eq", lt = "lt", gt = "gt";

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstReference, string>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotLessThanOtherGreater_False()
		{
			ComparisonAgainstValue.GTOET = ComparisonAgainstValue.LTOET = ComparisonAgainstValue.GT = true;
			ComparisonAgainstValue.LT = false;

			var target = new ComparisonAgainstValue("target");
			int eq = 20, lt = 10, gt = 30;

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstValue, int>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_TargetNotImplementingComparisonOperator_Exception()
		{
			ComparisonAgainstValue.GTOET = ComparisonAgainstValue.LTOET = ComparisonAgainstValue.GT = true;
			ComparisonAgainstValue.LT = false;

			var target = new ComparisonAgainstValue("target");
			string eq = "eq", lt = "lt", gt = "gt";

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstValue, string>(lt, gt, eq);

			Assert.That(()=>subject.Matches(target), Throws.InvalidOperationException.With.Message.StringContaining("String")
				.And.With.Message.StringContaining("ComparisonAgainstValue"));
		}

		[Test]
		public void Match_NotLessThanNullSelf_False()
		{
			Assert.Fail("missing");			
		}

		[Test]
		public void Match_NotGreaterThanNullSelf_False()
		{
			Assert.Fail("missing");
		}

		[Test]
		public void Match_NotLessThanNullOther_False()
		{
			Assert.Fail("missing");
		}

		[Test]
		public void Match_NotGreaterThanNullOther_False()
		{
			Assert.Fail("missing");
		}

		[Test]
		public void Match_NotEqualToNullSelf_False()
		{
			Assert.Fail("missing");
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_NotGreaterThanLess_WritesContractAndFailureAndFailureDetails()
		{
			ComparisonAgainstSelf.GTOET = ComparisonAgainstSelf.LTOET = true;
			ComparisonAgainstSelf.GT = false;

			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("comparison operators to <ComparisonAgainstSelf>.")
				.And.StringContaining("<target> must be > (greater than) <lt>")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "True")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + "False")
				);
		}

		[Test]
		public void WriteMessageTo_NotLessThanGreater_WritesContractAndFailureAndFailureDetails()
		{
			ComparisonAgainstSelf.GTOET = ComparisonAgainstSelf.LTOET = ComparisonAgainstSelf.GT = true;
			ComparisonAgainstSelf.LT = false;

			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("comparison operators to <ComparisonAgainstSelf>.")
				.And.StringContaining("<target> must be < (less than) <gt>")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "True")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + "False")
				);
		}

		[Test]
		public void WriteMessageTo_NotGreaterThanOtherLess_WritesContractAndFailureAndFailureDetails()
		{
			ComparisonAgainstReference.GTOET = ComparisonAgainstReference.LTOET = true;
			ComparisonAgainstReference.GT = false;

			var target = new ComparisonAgainstReference("target");
			string eq = "eq", lt = "lt", gt = "gt";

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstReference, string>(lt, gt, eq);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("comparison operators to <String>.")
				.And.StringContaining("<target> must be > (greater than) \"lt\"")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "True")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + "False")
				);
		}

		[Test]
		public void WriteMessageTo_NotLessThanOtherGreater_WritesContractAndFailureAndFailureDetails()
		{
			ComparisonAgainstValue.GTOET = ComparisonAgainstValue.LTOET = ComparisonAgainstValue.GT = true;
			ComparisonAgainstValue.LT = false;

			var target = new ComparisonAgainstValue("target");
			int eq = 20, lt = 10, gt = 30;

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstValue, int>(lt, gt, eq);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("comparison operators to <Int32>.")
				.And.StringContaining("<target> must be < (less than) 30")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "True")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + "False")
				);
		}

		#endregion

		[Test]
		public void CanBeNewedUp_ComparableToSelf()
		{
			ComparisonAgainstSelf.GTOET = ComparisonAgainstSelf.LTOET = ComparisonAgainstSelf.GT = ComparisonAgainstSelf.LT = true;
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");

			Assert.That(target, new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt));
		}

		[Test]
		public void CanBeNewedUp_ComparableToOther()
		{
			ComparisonAgainstValue.GTOET = ComparisonAgainstValue.LTOET = ComparisonAgainstValue.GT = ComparisonAgainstValue.LT = true;
			var target = new ComparisonAgainstValue("target");
			int eq = 20, lt = 10, gt = 30;

			Assert.That(target, new ImplementsComparisonConstraint<ComparisonAgainstValue, int>(lt, gt, eq));
		}

		[Test]
		public void CanBeCreatedWithExtension_ComparableToSelf()
		{
			ComparisonAgainstSelf.GTOET = ComparisonAgainstSelf.LTOET = ComparisonAgainstSelf.GT = ComparisonAgainstSelf.LT = true;
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");

			Assert.That(target, Must.Satisfy.ComparisonSpecificationAgainst(lt, gt));
		}

		[Test]
		public void CanBeCreatedWithExtension_ComparableToOther()
		{
			ComparisonAgainstReference.GTOET = ComparisonAgainstReference.LTOET = ComparisonAgainstReference.GT = ComparisonAgainstReference.LT = true;
			var target = new ComparisonAgainstReference("target");
			string eq = "eq", lt = "lt", gt = "gt";


			Assert.That(target, Must.Satisfy.ComparisonSpecificationAgainst<ComparisonAgainstReference, string>(lt, gt, eq));
		}
	}
}
