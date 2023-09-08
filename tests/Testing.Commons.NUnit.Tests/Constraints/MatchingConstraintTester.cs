using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using Testing.Commons.NUnit.Constraints;
using Testing.Commons.NUnit.Constraints.Support;
using Testing.Commons.NUnit.Tests.Subjects;

namespace Testing.Commons.NUnit.Tests.Constraints;
[TestFixture]
public class MatchingConstraintTester : ConstraintTesterBase
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
		public bool Equals(CustomerWithCollection? x, CustomerWithCollection? y)
		{
			// for brevity, let's consider arguments as not null
			bool areEqual = x!.Name == y!.Name;
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

	#region Matches

	[Test]
	public void ApplyTo_ExpectedWithSameShapeAndSameValues_True()
	{
		var subject = new MatchingConstraint(new { A = "a" });
		Assert.That(matches(subject, new { A = "a" }), Is.True);
	}

	[TestCase("a"), TestCase("B")]
	public void ApplyTo_ExpectedWithSameShapeAndDifferentValues_FalseRegardlessOfCasing(string differentValue)
	{
		var subject = new MatchingConstraint(new { A = differentValue });

		Assert.That(matches(subject, new { A = "b" }), Is.False);
	}

	[Test]
	public void ApplyTo_ExpectedIsSubsetOfActual_True()
	{
		var subject = new MatchingConstraint(new { A = "a" });
		Assert.That(matches(subject, new { A = "a", B = 1 }), Is.True);
	}

	[Test]
	public void ApplyTo_ExpectedIsSupersetOfActual_False()
	{
		var subject = new MatchingConstraint(new { A = "a", B = 1 });
		Assert.That(matches(subject, new { A = "a" }), Is.False);
	}

	[Test]
	public void ApplyTo_PrettyDeepWithSameValues_True()
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
		Assert.That(matches(subject, actual), Is.True);
	}

	[Test]
	public void ApplyTo_DifferentDeepValue_False()
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
		Assert.That(matches(subject, actual), Is.False);
	}

	[Test]
	public void ApplyTo_WithCollectionMemberWithSameShapeAndValues_True()
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
		Assert.That(matches(subject, complex), Is.True);
	}

	[Test]
	public void ApplyTo_WithCollectionMemberWithDifferentShape_False()
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
		Assert.That(matches(subject, complex), Is.False);
	}

	[Test]
	public void ApplyTo_WithCollectionMemberWithSameShapeAndDifferentValues_False()
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
		Assert.That(matches(subject, complex), Is.False);
	}

	[Test]
	public void ApplyTo_WithCollectionMemberAgainstAnonymousWithoutCollection_True()
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

		Assert.That(matches(subject, withAddresses), Is.True);
	}

	[Test]
	public void ApplyTo_WithCollectionMemberAgainstSameTypeWithoutCollection_False()
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

		Assert.That(matches(subject, withAddresses), Is.False);
	}

	#endregion

	#region WriteMessageTo

	[Test]
	public void WriteMessageTo_ExpectedWithSameShapeAndDifferentValues_ContainsMemberDiscrepancy()
	{
		var subject = new MatchingConstraint(new { A = "differentValue" });

		string message = getMessage(subject, new { A = "b" });
		Assert.That(message,
			offendingMember("A") &
			expectedValue("differentValue") &
			actualValue("b"));
	}

	[Test]
	public void WriteMessageTo_ExpectedIsSupersetOfActual_ContainsMemberDiscrepancy()
	{
		var subject = new MatchingConstraint(new { A = "a", B = 1 });

		Assert.That(getMessage(subject, new { A = "a" }),
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
		Assert.That(getMessage(subject, actual),
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
		Assert.That(getMessage(subject, complex),
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
		Assert.That(getMessage(subject, complex),
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

		Assert.That(getMessage(subject, withAddresses),
			memberContainer("Addresses") &
			expectedValue(null) &
			actualValue(Does.Contain("<")));

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
		Assert.That(getMessage(subject, withAddresses),
			memberContainer("Addresses") &
			indexOfOffendingCollectionItem(1) &
			missingExpectedMember() &
			actualValue(Does.Contain("Address")));
	}

	private Constraint offendingMember(string memberName)
	{
		return Does.Contain("." + memberName);
	}

	private Constraint expectedValue(string? value)
	{
		return Does.Contain(TextMessageWriter.Pfx_Expected + valueOf(value));
	}

	private string valueOf(string? str)
	{
		return str == null ? "nothing" : $"\"{str}\"";
	}

	private Constraint actualValue(string value)
	{
		return Does.Contain(TextMessageWriter.Pfx_Actual + valueOf(value));
	}

	private Constraint expectedValue(object value)
	{
		return Does.Contain(TextMessageWriter.Pfx_Expected + valueOf(value));
	}

	private string valueOf(object i)
	{
		return i == null ? "null" : $"{i}";
	}

	private Constraint missingActualMember()
	{
		return Does.Contain(TextMessageWriter.Pfx_Actual + "member was missing");
	}

	private Constraint actualValue(object value)
	{
		return Does.Contain(TextMessageWriter.Pfx_Actual + valueOf(value));
	}

	private Constraint memberContainer(string containerName)
	{
		return Does.Contain(containerName);
	}

	private Constraint indexOfOffendingCollectionItem(int i)
	{
		return Does.Contain($"[{i}]");
	}

	private Constraint actualValue(Constraint constraint)
	{
		return Does.Contain(TextMessageWriter.Pfx_Actual) & constraint;
	}

	private Constraint missingExpectedMember()
	{
		return Does.Contain(TextMessageWriter.Pfx_Expected + "nothing");
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
		Assert.That(new { A = "a", B = "b" }, Haz.Match(new { A = "a" }));
	}
}
