using NUnit.Framework;
using NUnit.Framework.Constraints;
using Rhino.Mocks;
using Testing.Commons.NUnit.Constraints;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class ConstraintEnumerableTester
	{
		[Test]
		public void Match_SubjectHasLessItemsThanConstraintsProvided_Failure()
		{
			var subject = new ConstrainedEnumerable(Is.EqualTo(1), Is.EqualTo(2));

			Assert.That(subject.Matches(new[] { 1 }), Is.False);
		}

		[Test]
		public void WriteMessageTo_SubjectHasLessItemsThanConstraintsProvided_ExpectedValueIsLengthOfSubject()
		{
			var writer = new TextMessageWriter();

			var subject = new ConstrainedEnumerable(Is.EqualTo(1), Is.EqualTo(2));
			subject.Matches(new[] { 1 });
			subject.WriteMessageTo(writer);

			Assert.That(writer.ToString(), Is.StringContaining(TextMessageWriter.Pfx_Expected + 1));
		}

		[Test]
		public void WriteMessageTo_SubjectHasLessItemsThanConstraintsProvided_ActualValueIsNumberOfConstraints()
		{
			var writer = new TextMessageWriter();

			var subject = new ConstrainedEnumerable(Is.EqualTo(1), Is.EqualTo(2));
			subject.Matches(new[] { 1 });
			subject.WriteMessageTo(writer);

			Assert.That(writer.ToString(), Is.StringContaining(TextMessageWriter.Pfx_Actual + 2));
		}

		[Test]
		public void Match_SubjectHasMoreItemsThanConstraintsProvided_Failure()
		{
			var subject = new ConstrainedEnumerable(Is.EqualTo(1));

			Assert.That(subject.Matches(new[] { 1, 2 }), Is.False);
		}

		[Test]
		public void WriteMessageTo_SubjectHasMoreItemsThanConstraintsProvided_ExpectedValueIsLengthOfSubject()
		{
			var writer = new TextMessageWriter();

			var subject = new ConstrainedEnumerable(Is.EqualTo(1));
			subject.Matches(new[] { 1, 2 });
			subject.WriteMessageTo(writer);

			Assert.That(writer.ToString(), Is.StringContaining(TextMessageWriter.Pfx_Expected + 2));
		}

		[Test]
		public void WriteMessageTo_SubjectHasMoreItemsThanConstraintsProvided_ActualValueIsNumberOfConstraints()
		{
			var writer = new TextMessageWriter();

			var subject = new ConstrainedEnumerable(Is.EqualTo(1));
			subject.Matches(new[] { 1, 2 });
			subject.WriteMessageTo(writer);

			Assert.That(writer.ToString(), Is.StringContaining(TextMessageWriter.Pfx_Actual + 1));
		}

		[Test]
		public void Match_FirstItemDoesNotMatchConstraint_False()
		{
			var subject = new ConstrainedEnumerable(Is.EqualTo(1), Is.EqualTo(2));

			Assert.That(subject.Matches(new[] { -1, 2 }), Is.False);
		}

		[Test]
		public void Match_FirstItemDoesNotMatchConstraint_SubsequentConstraintsNotEvaluated()
		{
			Constraint failing = MockRepository.GeneratePartialMock<Constraint>(),
				notEvaluated = MockRepository.GenerateStrictMock<Constraint>();

			var subject = new ConstrainedEnumerable(failing, notEvaluated);
			failing.Expect(c => c.Matches(-1)).Return(false);

			subject.Matches(new[] { -1, 2 });

			notEvaluated.AssertWasNotCalled(c => c.Matches(2));
		}

		[Test]
		public void WriteMessageTo_FirstItemDoesNotMatchConstraint_DescriptionContainsIndexOfFailingItem()
		{
			var writer = new TextMessageWriter();

			var subject = new ConstrainedEnumerable(Is.EqualTo(1), Is.EqualTo(2));
			subject.Matches(new[] { -1, 2 });
			subject.WriteMessageTo(writer);

			Assert.That(writer.ToString(), Is.StringContaining("# 0"));
		}

		[Test]
		public void WriteMessageTo_FirstItemDoesNotMatchConstraint_ExpectedValueIsExpectedValueOfFailingConstraint()
		{
			var writer = new TextMessageWriter();

			var subject = new ConstrainedEnumerable(Is.GreaterThan(0), Is.EqualTo(2));
			subject.Matches(new[] { -1, 2 });
			subject.WriteMessageTo(writer);

			Assert.That(writer.ToString(), Is.StringContaining(TextMessageWriter.Pfx_Expected + "greater than 0"));
		}

		[Test]
		public void WriteMessageTo_FirstItemDoesNotMatchConstraint_ActualValueIsValueOfOffendingItem()
		{
			var writer = new TextMessageWriter();

			var subject = new ConstrainedEnumerable(Is.GreaterThan(0), Is.EqualTo(2));
			subject.Matches(new[] { -1, 2 });
			subject.WriteMessageTo(writer);

			Assert.That(writer.ToString(), Is.StringContaining(TextMessageWriter.Pfx_Actual + "-1"));
		}

		[Test]
		public void Match_SecondItemDoesNotMatchConstraint_False()
		{
			var subject = new ConstrainedEnumerable(Is.EqualTo(1), Is.EqualTo(2), Is.EqualTo(3));

			Assert.That(subject.Matches(new[] { 1, -2, 3 }), Is.False);
		}

		[Test]
		public void Match_SecondItemDoesNotMatchConstraint_SubsequentConstraintsNotEvaluated()
		{
			Constraint
				passing = MockRepository.GeneratePartialMock<Constraint>(),
				failing = MockRepository.GeneratePartialMock<Constraint>(),
				notEvaluated = MockRepository.GenerateStrictMock<Constraint>();

			var subject = new ConstrainedEnumerable(failing, notEvaluated);
			passing.Expect(c => c.Matches(1)).Return(true);
			failing.Expect(c => c.Matches(-2)).Return(false);

			subject.Matches(new[] { 1, -2, 3 });

			notEvaluated.AssertWasNotCalled(c => c.Matches(3));
		}

		[Test]
		public void WriteMessageTo_SecondItemDoesNotMatchConstraint_DescriptionContainsIndexOfFailingItem()
		{
			var writer = new TextMessageWriter();

			var subject = new ConstrainedEnumerable(Is.EqualTo(1), Is.EqualTo(2));
			subject.Matches(new[] { 1, -2 });
			subject.WriteMessageTo(writer);

			Assert.That(writer.ToString(), Is.StringContaining("# 1"));
		}

		[Test]
		public void WriteMessageTo_SecondItemDoesNotMatchConstraint_ExpectedValueIsExpectedValueOfFailingConstraint()
		{
			var writer = new TextMessageWriter();

			var subject = new ConstrainedEnumerable(Is.EqualTo('1'), Is.GreaterThan('3'));
			subject.Matches(new[] { '1', '2' });
			subject.WriteMessageTo(writer);

			Assert.That(writer.ToString(), Is.StringContaining(TextMessageWriter.Pfx_Expected + "greater than '3'"));
		}

		[Test]
		public void WriteMessageTo_SecondItemDoesNotMatchConstraint_ActualValueIsValueOfOffendingItem()
		{
			var writer = new TextMessageWriter();

			var subject = new ConstrainedEnumerable(Is.EqualTo('1'), Is.GreaterThan('3'));
			subject.Matches(new[] { '1', '2' });
			subject.WriteMessageTo(writer);

			Assert.That(writer.ToString(), Is.StringContaining(TextMessageWriter.Pfx_Actual + "'2'"));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			Assert.That(new[]{1, 2}, Must.Be.Constrained(Is.EqualTo(1), Is.LessThan(3)));
		}
	}
}
