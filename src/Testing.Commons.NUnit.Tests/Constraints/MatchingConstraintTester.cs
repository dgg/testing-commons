using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Tests.Subjects;

namespace Testing.Commons.NUnit.Tests.Constraints
{
	[TestFixture]
	public class MatchingConstraintTester : Support.ConstraintTesterBase
	{
		#region problem exploration

		[Test]
		public void MultipleAssertions_AmongstOtherThings_DoNotCommunicateIntentVeryWell()
		{
			CustomerWithCollection actual = someComplicatedOperation();

			//only interested on name and the zip code of the addresses
			Assert.That(actual.Name, Is.EqualTo("someName"));
			var addresses = actual.Addresses.ToArray();
			Assert.That(addresses[0].Zipcode, Is.EqualTo("zip_1"));
			Assert.That(addresses[1].Zipcode, Is.EqualTo("zip_2"));
		}

		[Test]
		public void BetterAssertions_MakeThingsSlightlyBetter()
		{
			CustomerWithCollection actual = someComplicatedOperation();

			//only interested on name and the zip code of the addresses
			Assert.That(actual.Name, Is.EqualTo("someName"));
			Assert.That(actual.Addresses, Has.Some.Matches<Address>(a => a.Zipcode == "zip_1"));
			Assert.That(actual.Addresses, Has.Some.Matches(addressWithZipCode("zip_2")));

			// fails but and give nice feedback about was expected but nothing about the actual object, but the type name
			Assert.That(() => Assert.That(actual.Addresses, Has.Some.Matches(addressWithZipCode("wrong"))),
				Throws.InstanceOf<AssertionException>());
		}

		private IConstraint addressWithZipCode(string zipCode)
		{
			return ((IResolveConstraint)Has.Property("Zipcode").EqualTo(zipCode)).Resolve();
		}

		[Test]
		public void UsingCustomEqualityComparer_Works_ButFeedbackWhenFailingIsPoor()
		{
			CustomerWithCollection actual = someComplicatedOperation();
			CustomerWithCollection expected = new CustomerWithCollection
			{
				Name = "someName",
				Addresses = new[]
				{
					new Address{Zipcode = "zip_1"},
					new Address{Zipcode = "wrong"}
				}
			};

			// when fails only the class name is reported, nothing else
			string typeName = $"<{typeof(CustomerWithCollection).FullName}>";
			Assert.That(() => Assert.That(actual, Is.EqualTo(expected).Using(new CustomComparer())),
				Throws.InstanceOf<AssertionException>().With.Message.Contains(
					TextMessageWriter.Pfx_Expected + typeName + Environment.NewLine +
					TextMessageWriter.Pfx_Actual + typeName
				));
		}

		class CustomComparer : IEqualityComparer<CustomerWithCollection>
		{
			public bool Equals(CustomerWithCollection x, CustomerWithCollection y)
			{
				// for brevity, let's consider arguments as not null
				bool areEqual = x.Name == y.Name;
				if (areEqual)
				{
					// to make things easier, let's use indexes
					Address[] inX = x.Addresses.ToArray(), inY = y.Addresses.ToArray();
					areEqual = inX.Length == inY.Length;
					{
						for (int i = 0; i < inX.Length && areEqual; i++)
						{
							areEqual = inX[i].Zipcode == inY[i].Zipcode;
						}
					}
				}
				return areEqual;
			}

			public int GetHashCode(CustomerWithCollection obj)
			{
				//makes equality method to be always executed
				return 0;
			}
		}

		private CustomerWithCollection someComplicatedOperation()
		{
			return new CustomerWithCollection
			{
				Name = "someName",
				PhoneNumber = "someNumber",
				Addresses = new[]
				{
					new Address
					{
						AddressLineOne = "1_1", AddressLineTwo = "1_2", City = "city_1", State = "state_1", Zipcode = "zip_1"
					},
					new Address
					{
						AddressLineOne = "2_1", AddressLineTwo = "2_2", City = "city_2", State = "state_2", Zipcode = "zip_2"
					}
				}
			};
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