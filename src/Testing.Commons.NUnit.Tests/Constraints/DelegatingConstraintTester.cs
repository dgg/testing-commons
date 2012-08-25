using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class DelegatingConstraintTester : ConstraintTesterBase
	{
		class LongerThanFourCharactersWithStartingCapital : DelegatingConstraint<string>
		{
			public LongerThanFourCharactersWithStartingCapital()
			{
				// we have to resolve the constraint as we are using the fluent builder
				Delegate = ((IResolveConstraint)Has.Length.GreaterThan(4)).Resolve();
			}

			protected override bool matches(string current)
			{
				// check length first
				bool result = Delegate.Matches(current);
				if (result)
				{
					// if long enough, check uppercase of first character
					Delegate = new UppercaseConstraint();
					result = Delegate.Matches(current[0]);
				}
				return result;
			}
		}

		#region Matches

		[Test]
		public void Matches_MatchesConditions_True()
		{
			var matching = "Daniel";
			Assert.That(new LongerThanFourCharactersWithStartingCapital().Matches(matching), Is.True);
		}

		[Test]
		public void Matches_DoesNotMatchFirstCondition_False()
		{
			var notLongerThanFour = "Dani";
			Assert.That(new LongerThanFourCharactersWithStartingCapital().Matches(notLongerThanFour), Is.False);
		}

		[Test]
		public void Matches_DoesNotMatchSecondCondition_False()
		{
			var lowercaseInitial = "daniel";
			Assert.That(new LongerThanFourCharactersWithStartingCapital().Matches(lowercaseInitial), Is.False);
		}

		[Test]
		public void Matches_WrongType_Exception()
		{
			decimal notAnException = 3m;
			Assert.That(() => new LongerThanFourCharactersWithStartingCapital().Matches(notAnException),
				Throws.InstanceOf<InvalidCastException>());
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_FailingFirstCondition_MessageFromFirstDelegateConstraint()
		{
			var subject = new LongerThanFourCharactersWithStartingCapital();
			var notLongerThanFour = "Dani";
			Assert.That(GetMessage(subject, notLongerThanFour), Is.StringStarting(
				TextMessageWriter.Pfx_Expected + "property Length greater than 4").And
				.StringContaining(TextMessageWriter.Pfx_Actual + "4"));
		}

		[Test]
		public void WriteMessageTo_FailingSecondCondition_MessageFromSecondDelegateConstraint()
		{
			var subject = new LongerThanFourCharactersWithStartingCapital();
			var lowercaseInitial = "daniel";
			Assert.That(GetMessage(subject, lowercaseInitial), Is.StringStarting(
				TextMessageWriter.Pfx_Expected + "An uppercase character").And
				.StringContaining(TextMessageWriter.Pfx_Actual + "'d'"));
		}

		#endregion
	}
}
