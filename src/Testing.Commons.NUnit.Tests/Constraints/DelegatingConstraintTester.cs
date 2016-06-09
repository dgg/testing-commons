using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class DelegatingConstraintTester : ConstraintTesterBase
	{
		class LongerThanFourCharactersWithStartingCapital : DelegatingConstraint
		{
			public LongerThanFourCharactersWithStartingCapital()
			{
				// we have to resolve the constraint as we are using the fluent builder
				Delegate = ((IResolveConstraint)Has.Length.GreaterThan(4)).Resolve();
			}

			protected override ConstraintResult matches(object current)
			{
				string actual = (string) current;
				// check length first
				var result = Delegate.ApplyTo(actual);
				if (result.IsSuccess)
				{
					// if long enough, check uppercase of first character
					Delegate = new UppercaseConstraint();
					result = Delegate.ApplyTo(actual[0]);
				}
				return result;
			}
		}

		#region ApplyTo

		[Test]
		public void ApplyTo_MatchesConditions_True()
		{
			var matching = "Daniel";
			Assert.That(matches(new LongerThanFourCharactersWithStartingCapital(), matching), Is.True);
		}

		[Test]
		public void ApplyTo_DoesNotMatchFirstCondition_False()
		{
			var notLongerThanFour = "Dani";
			Assert.That(matches(new LongerThanFourCharactersWithStartingCapital(), notLongerThanFour), Is.False);
		}

		[Test]
		public void ApplyTo_DoesNotMatchSecondCondition_False()
		{
			var lowercaseInitial = "daniel";
			Assert.That(matches(new LongerThanFourCharactersWithStartingCapital(), lowercaseInitial), Is.False);
		}

		[Test]
		public void ApplyTo_WrongType_Exception()
		{
			decimal notAnException = 3m;
			Assert.That(() => matches(new LongerThanFourCharactersWithStartingCapital(), notAnException),
				Throws.InstanceOf<InvalidCastException>());
		}

		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_FailingFirstCondition_MessageFromFirstDelegateConstraint()
		{
			var subject = new LongerThanFourCharactersWithStartingCapital();
			var notLongerThanFour = "Dani";
			Assert.That(getMessage(subject, notLongerThanFour), 
				Does.StartWith(TextMessageWriter.Pfx_Expected + "property Length greater than 4").And
				.Contains(TextMessageWriter.Pfx_Actual + "4"));
		}

		[Test]
		public void WriteMessageTo_FailingSecondCondition_MessageFromSecondDelegateConstraint()
		{
			var subject = new LongerThanFourCharactersWithStartingCapital();
			var lowercaseInitial = "daniel";
			Assert.That(getMessage(subject, lowercaseInitial),
				Does.StartWith(TextMessageWriter.Pfx_Expected + "An uppercase character").And
				.Contains(TextMessageWriter.Pfx_Actual + "'d'"));
		}

		#endregion
	}
}