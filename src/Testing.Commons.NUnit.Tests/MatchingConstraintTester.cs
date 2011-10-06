﻿using System;
using NUnit.Framework;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Subjects;

namespace Testing.Commons.NUnit.Tests
{
	[TestFixture]
	public class MatchingConstraintTester
	{
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

			var subject = new MatchingConstraint(expected);
			Assert.That(subject.Matches(complex), Is.False);
		}
	}
}
