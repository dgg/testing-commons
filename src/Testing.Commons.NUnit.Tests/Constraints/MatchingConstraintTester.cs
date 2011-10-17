using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Constraints.Support;
using Testing.Commons.NUnit.Tests.Subjects;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class MatchingConstraintTester : ConstraintTesterBase
	{
		#region Matches

		[Test]
		public void Matches_ExpectedWithSameShapeAndSameValues_True()
		{
			var subject = new MatchingConstraint(new { A = "a" });
			Assert.That(subject.Matches(new { A = "a" }), Is.True);
		}

		[TestCase("a"), TestCase("B")]
		public void Matches_ExpectedWithSameShapeAndDifferentValues_FalseRegardlessOfCasing(string differentValue)
		{
			var subject = new MatchingConstraint(new { A = differentValue });

			Assert.That(subject.Matches(new { A = "b" }), Is.False);
		}

		[Test]
		public void Matches_ExpectedIsSubsetOfActual_True()
		{
			var subject = new MatchingConstraint(new { A = "a" });
			Assert.That(subject.Matches(new { A = "a", B = 1 }), Is.True);
		}

		[Test]
		public void Matches_ExpectedIsSupersetOfActual_False()
		{
			var subject = new MatchingConstraint(new { A = "a", B = 1 });
			Assert.That(subject.Matches(new { A = "a" }), Is.False);
		}

		[Test]
		public void Matches_PrettyDeepWithSameValues_True()
		{
			var actual = new
			{
				A = "a",
				B = new
				{
					C = 1,
					D = new { E = TimeSpan.Zero }
				}
			};

			var expected = new
			{
				A = "a",
				B = new
				{
					C = 1,
					D = new { E = TimeSpan.Zero }
				}
			};

			var subject = new MatchingConstraint(expected);
			Assert.That(subject.Matches(actual), Is.True);
		}

		[Test]
		public void Matches_DifferentDeepValue_False()
		{
			var actual = new
			{
				A = "a",
				B = new
				{
					C = 1,
					D = new { E = TimeSpan.Zero }
				}
			};

			var expected = new
			{
				A = "a",
				B = new
				{
					C = 1,
					D = new { E = TimeSpan.MaxValue }
				}
			};

			var subject = new MatchingConstraint(expected);
			Assert.That(subject.Matches(actual), Is.False);
		}

		[Test]
		public void Matches_WithCollectionMemberWithSameShapeAndValues_True()
		{
			var complex = new CustomerWithCollection
			{
				Name = "name",
				PhoneNumber = "number",
				Addresses = new[]
				{
					new Address { AddressLineOne = "1_1", AddressLineTwo = "1_2", City = "city_1", State = "state_1", Zipcode = "zip_1"},
					new Address { AddressLineOne = "2_1", AddressLineTwo = "2_2", City = "city_2", State = "state_2", Zipcode = "zip_2"}
				}
			};

			var expected = new
			{
				Name = "name",
				Addresses = new object[]
				{
					new { State = "state_1"},
					new { Zipcode = "zip_2"}
				}
			};

			var subject = new MatchingConstraint(expected);
			Assert.That(subject.Matches(complex), Is.True);
		}

		[Test]
		public void Matches_WithCollectionMemberWithDifferentShape_False()
		{
			var complex = new CustomerWithCollection
			{
				Name = "name",
				PhoneNumber = "number",
				Addresses = new[]
				{
					new Address { AddressLineOne = "1_1", AddressLineTwo = "1_2", City = "city_1", State = "state_1", Zipcode = "zip_1"},
					new Address { AddressLineOne = "2_1", AddressLineTwo = "2_2", City = "city_2", State = "state_2", Zipcode = "zip_2"}
				}
			};

			var expected = new
			{
				Name = "name",
				Addresses = new object[]
				{
					new { State = "state_1"},
					new { NotThere = 0}
				}
			};

			var subject = new MatchingConstraint(expected);
			Assert.That(subject.Matches(complex), Is.False);
		}

		[Test]
		public void Matches_WithCollectionMemberWithSameShapeAndDifferntValues_False()
		{
			var complex = new CustomerWithCollection
			{
				Name = "name",
				PhoneNumber = "number",
				Addresses = new[]
				{
					new Address { AddressLineOne = "1_1", AddressLineTwo = "1_2", City = "city_1", State = "state_1", Zipcode = "zip_1"},
					new Address { AddressLineOne = "2_1", AddressLineTwo = "2_2", City = "city_2", State = "state_2", Zipcode = "zip_2"}
				}
			};

			var expected = new
			{
				Name = "name",
				Addresses = new object[]
				{
					new { State = "state_1"},
					new { Zipcode = "notSameValue"}
				}
			};

			Assert.That(complex, Must.Match.Expected(expected));
			

			var subject = new MatchingConstraint(expected);
			Assert.That(subject.Matches(complex), Is.False);
		}

		[Test]
		public void Matches_WithCollectionMemberAgainstAnonymousWithoutCollection_True()
		{
			var withAddresses = new CustomerWithCollection
			{
				Name = "name",
				PhoneNumber = "number",
				Addresses = new[]
				{
					new Address { AddressLineOne = "1_1", AddressLineTwo = "1_2", City = "city_1", State = "state_1", Zipcode = "zip_1"},
					new Address { AddressLineOne = "2_1", AddressLineTwo = "2_2", City = "city_2", State = "state_2", Zipcode = "zip_2"}
				}
			};

			var withoutAddresses = new
			{
				Name = "name",
				PhoneNumber = "number",
			};

			var subject = new MatchingConstraint(withoutAddresses);

			Assert.That(subject.Matches(withAddresses), Is.True);

		}

		[Test]
		public void Matches_WithCollectionMemberAgainstSameTypeWithoutCollection_False()
		{
			var withAddresses = new CustomerWithCollection
			{
				Name = "name",
				PhoneNumber = "number",
				Addresses = new[]
				{
					new Address { AddressLineOne = "1_1", AddressLineTwo = "1_2", City = "city_1", State = "state_1", Zipcode = "zip_1"},
					new Address { AddressLineOne = "2_1", AddressLineTwo = "2_2", City = "city_2", State = "state_2", Zipcode = "zip_2"}
				}
			};

			var withoutAddresses = new CustomerWithCollection
			{
				Name = "name",
				PhoneNumber = "number",
			};

			var subject = new MatchingConstraint(withoutAddresses);

			Assert.That(subject.Matches(withAddresses), Is.False);

		}
		#endregion

		#region WriteMessageTo

		[Test]
		public void WriteMessageTo_ExpectedWithSameShapeAndDifferentValues_ContainsMemberDiscrepancy()
		{
			var subject = new MatchingConstraint(new { A = "differentValue" });

			string message = GetMessage(subject, new { A = "b" });
			Assert.That(message,
				offendingMember("A") &
				expectedValue("differentValue") &
				actualValue("b")
				);
		}

		[Test]
		public void WriteMessageTo_ExpectedIsSupersetOfActual_ContainsMemberDiscrepancy()
		{
			var subject = new MatchingConstraint(new { A = "a", B = 1 });

			Assert.That(GetMessage(subject, new { A = "a" }),
				offendingMember("B") &
				expectedValue(1) &
				missingActualMember()
				);
		}

		[Test]
		public void WriteMessageTo_DifferentDeepValue_ContainsMemberDiscrepancy()
		{
			var actual = new
			{
				A = "a",
				B = new
				{
					C = 1,
					D = new { E = TimeSpan.Zero }
				}
			};

			var expected = new
			{
				A = "a",
				B = new
				{
					C = 1,
					D = new { E = TimeSpan.MaxValue }
				}
			};

			var subject = new MatchingConstraint(expected);
			Assert.That(GetMessage(subject, actual),
				offendingMember("E") &
				actualValue(TimeSpan.Zero) &
				expectedValue(TimeSpan.MaxValue));
		}

		[Test]
		public void WriteMessageTo_WithCollectionMemberWithDifferentShape_ContainsIndexedDiscrepancy()
		{
			var complex = new CustomerWithCollection
			{
				Name = "name",
				PhoneNumber = "number",
				Addresses = new[]
				{
					new Address { AddressLineOne = "1_1", AddressLineTwo = "1_2", City = "city_1", State = "state_1", Zipcode = "zip_1"},
					new Address { AddressLineOne = "2_1", AddressLineTwo = "2_2", City = "city_2", State = "state_2", Zipcode = "zip_2"}
				}
			};

			var expected = new
			{
				Name = "name",
				Addresses = new object[]
				{
					new { State = "state_1"},
					new { NotThere = 0}
				}
			};

			var subject = new MatchingConstraint(expected);
			Assert.That(GetMessage(subject, complex),
				memberContainer("CustomerWithCollection.Addresses") &
				indexOfOffendingCollectionItem(1) &
				offendingMember("NotThere") &
				expectedValue(0) &
				missingActualMember()
				);
		}

		[Test]
		public void WriteMessageTo_WithCollectionMemberWithSameShapeAndDifferentValues_ContainsIndexedDiscrepancy()
		{
			var complex = new CustomerWithCollection
			{
				Name = "name",
				PhoneNumber = "number",
				Addresses = new[]
				{
					new Address { AddressLineOne = "1_1", AddressLineTwo = "1_2", City = "city_1", State = "state_1", Zipcode = "zip_1"},
					new Address { AddressLineOne = "2_1", AddressLineTwo = "2_2", City = "city_2", State = "state_2", Zipcode = "zip_2"}
				}
			};

			var expected = new
			{
				Name = "name",
				Addresses = new object[]
				{
					new { State = "state_1"},
					new { Zipcode = "notSameValue"}
				}
			};

			var subject = new MatchingConstraint(expected);
			Assert.That(GetMessage(subject, complex),
				memberContainer("CustomerWithCollection.Addresses") &
				indexOfOffendingCollectionItem(1) &
				offendingMember("Zipcode") &
				expectedValue("notSameValue") &
				actualValue("zip_2")
				);
		}

		[Test]
		public void WriteMessageTo_WithCollectionMemberAgainstSameTypeWithoutCollection_ContainsMemberDiscrepancy()
		{
			var withAddresses = new CustomerWithCollection
			{
				Name = "name",
				PhoneNumber = "number",
				Addresses = new[]
				{
					new Address { AddressLineOne = "1_1", AddressLineTwo = "1_2", City = "city_1", State = "state_1", Zipcode = "zip_1"},
					new Address { AddressLineOne = "2_1", AddressLineTwo = "2_2", City = "city_2", State = "state_2", Zipcode = "zip_2"}
				}
			};

			var withoutAddresses = new CustomerWithCollection
			{
				Name = "name",
				PhoneNumber = "number",
			};

			var subject = new MatchingConstraint(withoutAddresses);
			Assert.That(GetMessage(subject, withAddresses),
				memberContainer("Addresses") &
				expectedValue(null) &
				actualValue(Is.StringContaining("<")));
		}

		[Test]
		public void WriteMessageTo_WithCollectionMemberWithLessMembers_ContainsIndexedDiscrepancy()
		{
			var withAddresses = new CustomerWithCollection
			{
				Name = "name",
				PhoneNumber = "number",
				Addresses = new[]
				{
					new Address { AddressLineOne = "1_1", AddressLineTwo = "1_2", City = "city_1", State = "state_1", Zipcode = "zip_1"},
					new Address { AddressLineOne = "2_1", AddressLineTwo = "2_2", City = "city_2", State = "state_2", Zipcode = "zip_2"}
				}
			};

			var lessAddresses = new
			{
				Name = "name",
				PhoneNumber = "number",
				Addresses = new object[] { new { AddressLineOne = "1_1" } }
			};

			var subject = new MatchingConstraint(lessAddresses);
			Assert.That(GetMessage(subject, withAddresses),
				memberContainer("Addresses") &
				indexOfOffendingCollectionItem(1) &
				missingExpectedMember() &
				actualValue(Is.StringContaining("Address")));
		}

		private Constraint actualValue(string value)
		{
			return Is.StringContaining(TextMessageWriter.Pfx_Actual + valueOf(value));
		}

		private Constraint actualValue(object value)
		{
			return Is.StringContaining(TextMessageWriter.Pfx_Actual + valueOf(value));
		}

		private Constraint actualValue(Constraint constraint)
		{
			return Is.StringContaining(TextMessageWriter.Pfx_Actual) & constraint;
		}

		private Constraint expectedValue(string value)
		{
			return Is.StringContaining(TextMessageWriter.Pfx_Expected + valueOf(value));
		}

		private Constraint expectedValue(object value)
		{
			return Is.StringContaining(TextMessageWriter.Pfx_Expected + valueOf(value));
		}

		private Constraint indexOfOffendingCollectionItem(int i)
		{
			return Is.StringContaining(indexOf(i));
		}

		private Constraint memberContainer(string containerName)
		{
			return Is.StringContaining(containerName);
		}

		private Constraint offendingMember(string memberName)
		{
			return Is.StringContaining("." + memberName);
		}

		private Constraint missingActualMember()
		{
			return Is.StringContaining(TextMessageWriter.Pfx_Actual + "member was missing");
		}

		private Constraint missingExpectedMember()
		{
			return Is.StringContaining(TextMessageWriter.Pfx_Expected + "nothing");
		}

		private string valueOf(string str)
		{
			return str == null ? "null" : string.Format("\"{0}\"", str);
		}

		private string valueOf(object i)
		{
			return i == null ? "null" : string.Format("{0}", i);
		}

		private string indexOf(int i)
		{
			return string.Format("[{0}]", i);
		}

		#endregion

		[Test]
		public void CanBeNewedUp()
		{
			Assert.That(new { A = "a", B = "b" }, new MatchingConstraint(new { A = "a" }));
		}

		[Test]
		public void CanBeCreatedWithExtension()
		{
			Assert.That(new { A = "a", B = "b" }, Must.Match.Expected(new { A = "a" }));
		}
	}
}
