using System;
using NUnit.Framework;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.NUnit.Tests.Subjects.Comparisons;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class ImplementsComparableConstraintTester : ConstraintTesterBase
	{
		#region Matches

		[Test]
		public void Matches_CorrectImplementationReference_True()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var target = new ComparableSubject<string>("target")
				.Setup(eq, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue)
				.Setup(null, -1);

			var subject = new ImplementsComparableConstraint<string>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_CorrectImplementationValue_True()
		{
			int eq = 20, lt = 10, gt = 30;
			var target = new ComparableSubject<int>("target")
				.Setup(eq, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue);
			// no need to setup null comparison as the comparer target is a value type

			var subject = new ImplementsComparableConstraint<int>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_CorrectImplementationSelf_True()
		{
			ComparableSubject lt = new ComparableSubject("lt"), gt = new ComparableSubject("gt");
			var target = new ComparableSubject("target");
			target.Setup(target, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue)
				.Setup(null, int.MinValue);
			
			var subject = new ImplementsComparableConstraint<ComparableSubject>(lt, gt);

			Assert.That(subject.Matches(target), Is.True);
		}

		[Test]
		public void Matches_NotEqualToItself_False()
		{
			ComparableSubject lt = new ComparableSubject("lt"), gt = new ComparableSubject("gt");
			var target = new ComparableSubject("target");
			target.Setup(target, -1);

			var subject = new ImplementsComparableConstraint<ComparableSubject>(lt, gt);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotGreaterThanLess_False()
		{
			ComparableSubject lt = new ComparableSubject("lt"), gt = new ComparableSubject("gt");
			var target = new ComparableSubject("target");
			target.Setup(target, 0)
				.Setup(lt, int.MinValue);

			var subject = new ImplementsComparableConstraint<ComparableSubject>(lt, gt);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotLessThanGreater_False()
		{
			ComparableSubject lt = new ComparableSubject("lt"), gt = new ComparableSubject("gt");
			var target = new ComparableSubject("target");
			target.Setup(target, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, 1);

			var subject = new ImplementsComparableConstraint<ComparableSubject>(lt, gt);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotLessThanNull_False()
		{
			ComparableSubject lt = new ComparableSubject("lt"), gt = new ComparableSubject("gt");
			var target = new ComparableSubject("target");
			target.Setup(target, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue)
				.Setup(null, int.MaxValue);

			var subject = new ImplementsComparableConstraint<ComparableSubject>(lt, gt);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotEqualToOther_False()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var target = new ComparableSubject<string>("target")
				.Setup(eq, int.MaxValue);

			var subject = new ImplementsComparableConstraint<string>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotGreaterThanOtherLess_False()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var target = new ComparableSubject<string>("target")
				.Setup(eq, 0)
				.Setup(lt, int.MinValue);

			var subject = new ImplementsComparableConstraint<string>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotLessThanOtherGreater_False()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var target = new ComparableSubject<string>("target")
				.Setup(eq, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, 1);

			var subject = new ImplementsComparableConstraint<string>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_NotLessThanOtherNull_False()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var target = new ComparableSubject<string>("target")
				.Setup(eq, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue)
				.Setup(null, int.MaxValue);

			var subject = new ImplementsComparableConstraint<string>(lt, gt, eq);

			Assert.That(subject.Matches(target), Is.False);
		}

		[Test]
		public void Matches_CompareToOther_EqNotsupplied_Exception()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var target = new ComparableSubject<string>("target")
				.Setup(eq, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue)
				.Setup(null, int.MaxValue);

			var subject = new ImplementsComparableConstraint<string>(lt, gt/*, eq*/);

			Assert.That(() => subject.Matches(target), Throws.InstanceOf<InvalidCastException>()
				.With.Message.StringContaining("ComparableSubject`1[System.String]")
				.And.Message.StringContaining("System.String"));
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_NotEqualToItself_WritesContractAndFailureAndFailureDetails()
		{
			ComparableSubject lt = new ComparableSubject("lt"), gt = new ComparableSubject("gt");
			var target = new ComparableSubject("target");
			target.Setup(target, -1);

			var subject = new ImplementsComparableConstraint<ComparableSubject>(lt, gt);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("IComparable<ComparableSubject> contract.")
				.And.StringContaining("<target> must be equal to <target>")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "0")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + "-1")
				);
		}

		[Test]
		public void WriteMessageTo_NotGreaterThanLess_WritesContractAndFailureAndFailureDetails()
		{
			ComparableSubject lt = new ComparableSubject("lt"), gt = new ComparableSubject("gt");
			var target = new ComparableSubject("target");
			target.Setup(target, 0)
				.Setup(lt, int.MinValue);

			var subject = new ImplementsComparableConstraint<ComparableSubject>(lt, gt);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("IComparable<ComparableSubject> contract.")
				.And.StringContaining("<target> must be greater than <lt>")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "greater than 0")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + int.MinValue)
				);
		}

		[Test]
		public void WriteMessageTo_NotLessThanGreater_WritesContractAndFailureAndFailureDetails()
		{
			ComparableSubject lt = new ComparableSubject("lt"), gt = new ComparableSubject("gt");
			var target = new ComparableSubject("target");
			target.Setup(target, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, 1);

			var subject = new ImplementsComparableConstraint<ComparableSubject>(lt, gt);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("IComparable<ComparableSubject> contract.")
				.And.StringContaining("<target> must be less than <gt>")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "less than 0")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + 1)
				);
		}

		[Test]
		public void WriteMessageTo_NotLessThanNull_WritesContractAndFailureAndFailureDetails()
		{
			ComparableSubject lt = new ComparableSubject("lt"), gt = new ComparableSubject("gt");
			var target = new ComparableSubject("target");
			target.Setup(target, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue)
				.Setup(null, int.MaxValue);

			var subject = new ImplementsComparableConstraint<ComparableSubject>(lt, gt);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("IComparable<ComparableSubject> contract.")
				.And.StringContaining("<target> must be less than null")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "less than 0")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + int.MaxValue)
				);
		}

		[Test]
		public void WriteMessageTo_NotEqualToOther_WritesContractAndFailureAndFailureDetails()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var target = new ComparableSubject<string>("target")
				.Setup(eq, int.MaxValue);

			var subject = new ImplementsComparableConstraint<string>(lt, gt, eq);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("IComparable<String> contract.")
				.And.StringContaining("<target> must be equal to \"eq\"")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "0")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + int.MaxValue)
				);
		}

		[Test]
		public void WriteMessageTo_NotGreaterThanOtherLess_WritesContractAndFailureAndFailureDetails()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var target = new ComparableSubject<string>("target")
				.Setup(eq, 0)
				.Setup(lt, int.MinValue);

			var subject = new ImplementsComparableConstraint<string>(lt, gt, eq);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("IComparable<String> contract.")
				.And.StringContaining("<target> must be greater than \"lt\"")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "greater than 0")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + int.MinValue)
				);
		}

		[Test]
		public void WriteMessageTo_NotLessThanOtherGreater_WritesContractAndFailureAndFailureDetails()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var target = new ComparableSubject<string>("target")
				.Setup(eq, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, 1);

			var subject = new ImplementsComparableConstraint<string>(lt, gt, eq);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("IComparable<String> contract.")
				.And.StringContaining("<target> must be less than \"gt\"")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "less than 0")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + 1)
				);
		}

		[Test]
		public void WriteMessageTo_NotLessThanOtherNull_WritesContractAndFailureAndFailureDetails()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var target = new ComparableSubject<string>("target")
				.Setup(eq, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue)
				.Setup(null, int.MaxValue);

			var subject = new ImplementsComparableConstraint<string>(lt, gt, eq);

			Assert.That(GetMessage(subject, target), Is
				.StringContaining("IComparable<String> contract.")
				.And.StringContaining("<target> must be less than null")
				.And.StringContaining(TextMessageWriter.Pfx_Expected + "less than 0")
				.And.StringContaining(TextMessageWriter.Pfx_Actual + int.MaxValue)
				);
		}

		#endregion

		[Test]
		public void CanBeNewedUp_ComparableToSelf()
		{
			ComparableSubject lt = new ComparableSubject("lt"), gt = new ComparableSubject("gt");
			var target = new ComparableSubject("target");
			target.Setup(target, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue)
				.Setup(null, int.MinValue);

			Assert.That(target, new ImplementsComparableConstraint<ComparableSubject>(lt, gt));
		}

		[Test]
		public void CanBeNewedUp_ComparableToOther()
		{
			int eq = 20, lt = 10, gt = 30;
			var target = new ComparableSubject<int>("target")
				.Setup(eq, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue);

			Assert.That(target, new ImplementsComparableConstraint<int>(lt, gt, eq));
		}

		[Test]
		public void CanBeCreatedWithExtension_ComparableToSelf()
		{
			ComparableSubject lt = new ComparableSubject("lt"), gt = new ComparableSubject("gt");
			var target = new ComparableSubject("target");
			target.Setup(target, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue)
				.Setup(null, int.MinValue);

			Assert.That(target, Must.Satisfy.ComparableSpecificationAgainst(lt, gt));
		}

		[Test]
		public void CanBeCreatedWithExtension_ComparableToOther()
		{
			string eq = "eq", lt = "lt", gt = "gt";
			var target = new ComparableSubject<string>("target")
				.Setup(eq, 0)
				.Setup(lt, int.MaxValue)
				.Setup(gt, int.MinValue)
				.Setup(null, -1);

			Assert.That(target, Must.Satisfy.ComparableSpecificationAgainst(lt, gt, eq));
		}
	}
}
