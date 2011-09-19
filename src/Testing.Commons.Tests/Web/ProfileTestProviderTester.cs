using System;
using System.Collections.Generic;
using System.Web.Profile;
using NUnit.Framework;
using Testing.Commons.Tests.Web.Subjects;
using Testing.Commons.Time;
using Testing.Commons.Web;

namespace Testing.Commons.Tests.Web
{
	[TestFixture]
	public class ProfileTestProviderTester
	{
		[SetUp]
		public void Setup()
		{
			ProfileTestProvider.SetAsDefault();
		}

		[Test]
		public void StubValues_KeyValueParams_PreparesValuesToBeRetrieved()
		{
			string aString = "aString";
			DateTime aDateTime = 5.September(2011);
			TimeSpan anInterval = 4.Days();
			byte enumB = 1;

			ProfileTestProvider provider = (ProfileTestProvider)ProfileManager.Provider;
			provider.StubValues(
				new KeyValuePair<string, object>(ProfileSubject.A_STRING, aString),
				new KeyValuePair<string, object>(ProfileSubject.AN_ENUM, enumB),
				new KeyValuePair<string, object>(ProfileSubject.A_NULLABLE_DATE, aDateTime),
				new KeyValuePair<string, object>(ProfileSubject.AN_INTERVAL, anInterval)
				);
			
			var subject = new ProfileSubject("anyName");
			Assert.That(subject.AString, Is.EqualTo(aString));
			Assert.That(subject.AnEnum, Is.EqualTo(ACustomEnum.B));
			Assert.That(subject.ANullableDate, Is.EqualTo(aDateTime));
			Assert.That(subject.AnInterval, Is.EqualTo(anInterval));
		}

		[Test]
		public void StubValues_Dictionary_PreparesValuesToBeRetrieved()
		{
			string aString = "aString";
			DateTime aDateTime = 5.September(2011);
			TimeSpan anInterval = 4.Days();
			byte enumB = 1;

			ProfileTestProvider provider = (ProfileTestProvider)ProfileManager.Provider;
			provider.StubValues(
				new Dictionary<string, object>
				{
					{ProfileSubject.A_STRING, aString},
					{ProfileSubject.AN_ENUM, enumB},
					{ProfileSubject.A_NULLABLE_DATE, aDateTime},
					{ProfileSubject.AN_INTERVAL, anInterval}
				});

			var subject = new ProfileSubject("anyName");
			Assert.That(subject.AString, Is.EqualTo(aString));
			Assert.That(subject.AnEnum, Is.EqualTo(ACustomEnum.B));
			Assert.That(subject.ANullableDate, Is.EqualTo(aDateTime));
			Assert.That(subject.AnInterval, Is.EqualTo(anInterval));
		}

		[Test]
		public void StubValues_IDictionary_PreparesValuesToBeRetrieved()
		{
			string aString = "aString";
			DateTime aDateTime = 5.September(2011);
			TimeSpan anInterval = 4.Days();
			byte enumB = 1;

			ProfileTestProvider provider = (ProfileTestProvider)ProfileManager.Provider;
			IDictionary<string, object> iDictionary = new Dictionary<string, object>
			{
				{ProfileSubject.A_STRING, aString},
				{ProfileSubject.AN_ENUM, enumB},
				{ProfileSubject.A_NULLABLE_DATE, aDateTime},
				{ProfileSubject.AN_INTERVAL, anInterval}
			};
			provider.StubValues(iDictionary);

			var subject = new ProfileSubject("anyName");
			Assert.That(subject.AString, Is.EqualTo(aString));
			Assert.That(subject.AnEnum, Is.EqualTo(ACustomEnum.B));
			Assert.That(subject.ANullableDate, Is.EqualTo(aDateTime));
			Assert.That(subject.AnInterval, Is.EqualTo(anInterval));
		}

		[Test]
		public void StubValues_AnonymousObject_PreparesValuesToBeRetrieved()
		{
			string aString = "aString";
			DateTime aDateTime = 5.September(2011);
			TimeSpan anInterval = 4.Days();
			byte enumB = 1;

			ProfileTestProvider provider = (ProfileTestProvider)ProfileManager.Provider;

			provider.StubValues(new
			{
				aString,
				AnEnum = enumB,
				ANullableDate = aDateTime,
				anInterval
			});

			var subject = new ProfileSubject("anyName");
			Assert.That(subject.AString, Is.EqualTo(aString));
			Assert.That(subject.AnEnum, Is.EqualTo(ACustomEnum.B));
			Assert.That(subject.ANullableDate, Is.EqualTo(aDateTime));
			Assert.That(subject.AnInterval, Is.EqualTo(anInterval));
		}

		[Test]
		public void StubValues_UndefinedProperty_DoesNotThrow()
		{
			ProfileTestProvider provider = (ProfileTestProvider)ProfileManager.Provider;

			Assert.That(() => provider.StubValues(new { notDefinedInConfiguration = 3m }),
				Throws.Nothing);
		}

		[Test]
		public void StubValues_HandlesNullValues()
		{
			ProfileTestProvider provider = (ProfileTestProvider)ProfileManager.Provider;

			provider.StubValues(
				new Dictionary<string, object>
				{
					{ProfileSubject.A_STRING, null},
					{ProfileSubject.A_NULLABLE_DATE, null}
				});

			var subject = new ProfileSubject("anyName");

			// the default value of a string property is string.Empty
			// Can be overriden by using the defaultValue attribute in the profile definition
			Assert.That(subject.AString, Is.Empty);
			Assert.That(subject.ANullableDate, Is.Null);
		}

		[Test]
		public void AssertPropertyValue_AllowsCheckingTheValueSet()
		{
			string aString = "aString";
			DateTime aDateTime = 5.September(2011);
			TimeSpan anInterval = 4.Days();
			byte enumB = 1;

			new ProfileSubject("anyName")
			{
				AString = aString,
				ANullableDate = aDateTime,
				AnEnum = ACustomEnum.B,
				AnInterval = anInterval
			}
				// important to save the values to the profile system
			.Save();

			ProfileTestProvider provider = (ProfileTestProvider)ProfileManager.Provider;
			provider
				.AssertPropertyValue(ProfileSubject.A_STRING, value => Assert.That(value, Is.EqualTo(aString)))
				.AssertPropertyValue(ProfileSubject.AN_ENUM, value => Assert.That(value, Is.EqualTo(enumB)))
				.AssertPropertyValue(ProfileSubject.A_NULLABLE_DATE, value => Assert.That(value, Is.EqualTo(aDateTime)))
				.AssertPropertyValue(ProfileSubject.AN_INTERVAL, value => Assert.That(value, Is.EqualTo(anInterval)));
		}

		[Test]
		public void AssertPropertyValue_HandlesNullProperties()
		{
			new ProfileSubject("anyName")
			{
				AString = null,
				ANullableDate = null,
			}
			.Save();

			ProfileTestProvider provider = (ProfileTestProvider)ProfileManager.Provider;
			provider
				.AssertPropertyValue(ProfileSubject.A_STRING, value => Assert.That(value, Is.Null))
				.AssertPropertyValue(ProfileSubject.A_NULLABLE_DATE, value => Assert.That(value, Is.Null));
		}

		[Test]
		public void AssertPropertyValue_UndefinedValue_Exception()
		{
			new ProfileSubject("anyName")
			{
				AString = "aString",
			}
			.Save();

			ProfileTestProvider provider = (ProfileTestProvider)ProfileManager.Provider;
			Assert.That(() => provider.AssertPropertyValue("notDefinedInConfiguration", _ => { }),
				Throws.Nothing);
		}
	}
}
