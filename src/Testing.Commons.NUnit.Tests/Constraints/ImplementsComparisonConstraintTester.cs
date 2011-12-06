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
			ComparisonAgainstReference.Setup(c => c.Gtoet(true).Ltoet(true).Gt(true).Lt(true));
			var target = new ComparisonAgainstReference("target");
			string eq = "eq", lt = "lt", gt = "gt";

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstReference, string>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_CorrectImplementationValue_True()
		{
			var target = new ComparisonAgainstValue("target");
			int eq = 20, lt = 10, gt = 30;
			ComparisonAgainstValue.Setup(c => c.Gtoet(true).Ltoet(true).Gt(true).Lt(true));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstValue, int>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_CorrectImplementationInverseReference_True()
		{
			ComparisonAgainstReference.SetupInverse(c => c.Gtoet(true).Ltoet(true).Gt(true).Lt(true));
			var target = "target";
			ComparisonAgainstReference eq = new ComparisonAgainstReference("eq"), lt = new ComparisonAgainstReference("lt"), gt = new ComparisonAgainstReference("gt");
			var subject = new ImplementsComparisonConstraint<string, ComparisonAgainstReference>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_CorrectImplementationSelf_True()
		{
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");
			ComparisonAgainstSelf.Setup(c => c.Gtoet(true).Ltoet(true).Gt(true).Lt(true));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_NotGreaterThanLess_False()
		{
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");
			ComparisonAgainstSelf.Setup(c => c.Gtoet(true).Ltoet(true));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotLessThanGreater_False()
		{
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");
			ComparisonAgainstSelf.Setup(c => c.Gtoet(true).Ltoet(true).Gt(true));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotGreaterThanOtherLess_False()
		{
			ComparisonAgainstReference.Setup(c => c.Gtoet(true).Ltoet(true));
			
			var target = new ComparisonAgainstReference("target");
			string eq = "eq", lt = "lt", gt = "gt";

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstReference, string>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotLessThanOtherGreater_False()
		{
			var target = new ComparisonAgainstValue("target");
			int eq = 20, lt = 10, gt = 30;
			ComparisonAgainstValue.Setup(c => c.Gtoet(true).Ltoet(true).Gt(true));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstValue, int>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_TargetNotImplementingComparisonOperator_Exception()
		{
			var target = new ComparisonAgainstValue("target");
			string eq = "eq", lt = "lt", gt = "gt";

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstValue, string>(lt, gt, eq);

			Assert.That(()=>subject.Matches(target), Throws.InvalidOperationException.With.Message.StringContaining("String")
				.And.With.Message.StringContaining("ComparisonAgainstValue"));
		}

		[Test]
		public void Match_NotGreaterThanNullSelf_False()
		{
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");
			ComparisonAgainstSelf.Setup(c =>
				c.Gtoet(target, target, true)
				.Ltoet(target, target, true)
				.Ltoet(target, gt, true)
				.Lt(target, gt, true)
				.Gt(target, lt, true)
				.Gtoet(target, lt, true)
				.Gt(target, null, false));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Match_LessThanNullSelf_False()
		{
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");
			ComparisonAgainstSelf.Setup(c =>
				c.Gtoet(target, target, true)
				.Ltoet(target, target, true)
				.Ltoet(target, gt, true)
				.Lt(target, gt, true)
				.Gt(target, lt, true)
				.Gtoet(target, lt, true)
				.Gt(target, null, true)
				.Lt(null, lt, false));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Match_NotGreaterThanNullOther_False()
		{
			var target = new ComparisonAgainstReference("target");
			string lt = "lt", gt = "gt", eq = "eq";
			ComparisonAgainstReference.Setup(c =>
				c.Gtoet(target, eq, true)
				.Ltoet(target, eq, true)
				.Ltoet(target, gt, true)
				.Lt(target, gt, true)
				.Gt(target, lt, true)
				.Gtoet(target, lt, true)
				.Gt(target, null, false));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstReference, string>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Match_LessThanNullOther_False()
		{
			var target = new ComparisonAgainstReference("target");
			string lt = "lt", gt = "gt", eq = "eq";
			ComparisonAgainstReference.Setup(c =>
				c.Gtoet(target, eq, true)
				.Ltoet(target, eq, true)
				.Ltoet(target, gt, true)
				.Lt(target, gt, true)
				.Gt(target, lt, true)
				.Gtoet(target, lt, true)
				.Gt(target, null, true)
				.Lt(null, lt, false));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstReference, string>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.False);
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_NotGreaterThanLess_WritesContractAndFailureAndFailureDetails()
		{
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");
			ComparisonAgainstSelf.Setup(c => c.Gtoet(true).Ltoet(true));

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
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");
			ComparisonAgainstSelf.Setup(c => c.Gtoet(true).Ltoet(true).Gt(true));

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
			ComparisonAgainstReference.Setup(c => c.Gtoet(true).Ltoet(true));

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
			var target = new ComparisonAgainstValue("target");
			int eq = 20, lt = 10, gt = 30;
			ComparisonAgainstValue.Setup(c => c.Gtoet(true).Ltoet(true).Gt(true));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstValue, int>(lt, gt, eq);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("comparison operators to <Int32>.")
				.And.StringContaining("<target> must be < (less than) 30")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "True")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + "False")
				);
		}
		
		[Test]
		public void WriteMessageTo_NotGreaterThanNullSelf_WritesContractAndFailureAndFailureDetails()
		{
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");
			ComparisonAgainstSelf.Setup(c =>
				c.Gtoet(target, target, true)
				.Ltoet(target, target, true)
				.Ltoet(target, gt, true)
				.Lt(target, gt, true)
				.Gt(target, lt, true)
				.Gtoet(target, lt, true)
				.Gt(target, null, false));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("comparison operators to <ComparisonAgainstSelf>.")
				.And.StringContaining("<target> must be > (greater than) null")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "True")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + "False")
				);
		}

		[Test]
		public void WriteMessageTo_LessThanNullSelf_WritesContractAndFailureAndFailureDetails()
		{
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");
			ComparisonAgainstSelf.Setup(c =>
				c.Gtoet(target, target, true)
				.Ltoet(target, target, true)
				.Ltoet(target, gt, true)
				.Lt(target, gt, true)
				.Gt(target, lt, true)
				.Gtoet(target, lt, true)
				.Gt(target, null, true)
				.Lt(null, lt, false));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("comparison operators to <ComparisonAgainstSelf>.")
				.And.StringContaining("null must be < (less than) <lt>")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "True")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + "False")
				);
		}

		[Test]
		public void WriteMessageTo_NotGreaterThanNullOther_WritesContractAndFailureAndFailureDetails()
		{
			var target = new ComparisonAgainstReference("target");
			string lt = "lt", gt = "gt", eq = "eq";
			ComparisonAgainstReference.Setup(c =>
				c.Gtoet(target, eq, true)
				.Ltoet(target, eq, true)
				.Ltoet(target, gt, true)
				.Lt(target, gt, true)
				.Gt(target, lt, true)
				.Gtoet(target, lt, true)
				.Gt(target, null, false));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstReference, string>(lt, gt, eq);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("comparison operators to <String>.")
				.And.StringContaining("<target> must be > (greater than) null")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "True")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + "False")
				);
		}

		[Test]
		public void WriteMessageTo_LessThanNullOther_WritesContractAndFailureAndFailureDetails()
		{
			var target = new ComparisonAgainstReference("target");
			string lt = "lt", gt = "gt", eq = "eq";
			ComparisonAgainstReference.Setup(c =>
				c.Gtoet(target, eq, true)
				.Ltoet(target, eq, true)
				.Ltoet(target, gt, true)
				.Lt(target, gt, true)
				.Gt(target, lt, true)
				.Gtoet(target, lt, true)
				.Gt(target, null, true)
				.Lt(null, lt, false));

			var subject = new ImplementsComparisonConstraint<ComparisonAgainstReference, string>(lt, gt, eq);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("comparison operators to <String>.")
				.And.StringContaining("null must be < (less than) \"lt\"")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "True")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + "False")
				);
		}

		#endregion

		[Test]
		public void CanBeNewedUp_ComparableToSelf()
		{
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");
			ComparisonAgainstSelf.Setup(c => c.Gtoet(true).Ltoet(true).Gt(true).Lt(true));

			Assert.That(target, new ImplementsComparisonConstraint<ComparisonAgainstSelf>(lt, gt));
		}

		[Test]
		public void CanBeNewedUp_ComparableToOther()
		{
			var target = new ComparisonAgainstValue("target");
			int eq = 20, lt = 10, gt = 30;
			ComparisonAgainstValue.Setup(c => c.Gtoet(true).Ltoet(true).Gt(true).Lt(true));

			Assert.That(target, new ImplementsComparisonConstraint<ComparisonAgainstValue, int>(lt, gt, eq));
		}

		[Test]
		public void CanBeCreatedWithExtension_ComparableToSelf()
		{
			ComparisonAgainstSelf target = new ComparisonAgainstSelf("target"),
				lt = new ComparisonAgainstSelf("lt"),
				gt = new ComparisonAgainstSelf("gt");
			ComparisonAgainstSelf.Setup(c => c.Gtoet(true).Ltoet(true).Gt(true).Lt(true));

			Assert.That(target, Must.Satisfy.ComparisonSpecificationAgainst(lt, gt));
		}

		[Test]
		public void CanBeCreatedWithExtension_ComparableToOther()
		{
			ComparisonAgainstReference.Setup(c => c.Gtoet(true).Ltoet(true).Gt(true).Lt(true));
			var target = new ComparisonAgainstReference("target");
			string eq = "eq", lt = "lt", gt = "gt";

			Assert.That(target, Must.Satisfy.ComparisonSpecificationAgainst<ComparisonAgainstReference, string>(lt, gt, eq));
		}
	}
}
