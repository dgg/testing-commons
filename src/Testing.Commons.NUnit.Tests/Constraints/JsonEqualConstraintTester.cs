using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Tests.Constraints
{
    [TestFixture]
    public class JsonEqualConstraintTester : ConstraintTesterBase
	{
        #region Matches

        [Test]
        public void Matches_SameProperJson_True()
        {
	        string properJson = "{\"prop\"=\"value\"}";
            var subject = new JsonEqualConstraint("{'prop'='value'}");

            Assert.That(subject.Matches(properJson), Is.True);
        }

		[Test]
		public void Matches_SameJsonified_False()
		{
			string jsonified = "{'prop'='value'}";
			var subject = new JsonEqualConstraint("{'prop'='value'}");

			Assert.That(subject.Matches(jsonified), Is.False);
		}

		[Test]
		public void Matches_NotSame_False()
		{
			string notSame = "{\"abc\"=123}";
			var subject = new JsonEqualConstraint("{'prop'='value'}");

			Assert.That(subject.Matches(notSame), Is.False);
		}

		 #endregion

		 #region WriteMessageTo

		 [Test]
		 public void WriteMessageTo_DifferentJson_DelegateToEquals()
		 {
			 string expected = "{'prop'='value'}",
				actual = "{\"abcd\"=\"12345\"}";
			 var subject = new JsonEqualConstraint(expected);
			 var equals = new EqualConstraint(expected.Jsonify());
			 Assert.That(GetMessage(subject, actual),
				 Is.EqualTo(GetMessage(equals, actual)));
		 }

		#endregion

		[Test]
        public void CanBeNewedUp()
        {
			var actual = "{\"prop\"=\"value\"}";
            Assert.That(actual, new JsonEqualConstraint("{'prop'='value'}"));
        }

        [Test]
        public void CanBeCreatedWithExtension()
        {
			var actual = "{\"prop\"=\"value\"}";
			Assert.That(actual, Must.Be.Json("{'prop'='value'}"));
		}
    }
}