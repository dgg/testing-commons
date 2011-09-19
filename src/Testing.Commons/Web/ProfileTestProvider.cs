using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Profile;

namespace Testing.Commons.Web
{
	/// <summary>
	/// Allows testing classes that depend directly or indirectly from the ASP.NET profile system.
	/// </summary>
	public class ProfileTestProvider : ProfileProvider, IEnumerable<KeyValuePair<string, object>>
	{
		private static readonly ProfileInfoCollection _emptyInfo = new ProfileInfoCollection();

		/// <summary>
		/// Returns the collection of settings property values for the specified application instance and settings property group.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Configuration.SettingsPropertyValueCollection"/> containing the values for the specified settings property group.
		/// </returns>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext"/> describing the current application use.</param><param name="collection">A <see cref="T:System.Configuration.SettingsPropertyCollection"/> containing the settings property group whose values are to be retrieved.</param><filterpriority>2</filterpriority>
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
		{
			return mergeCollections(collection, Properties);
		}

		/// <summary>
		/// Sets the values of the specified group of property settings.
		/// </summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext"/> describing the current application usage.</param><param name="collection">A <see cref="T:System.Configuration.SettingsPropertyValueCollection"/> representing the group of property settings to set.</param><filterpriority>2</filterpriority>
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
		{
			Properties = collection;
		}

		/// <summary>
		/// Gets or sets the name of the currently running application.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that contains the application's shortened name, which does not contain a full path or extension, for example, SimpleAppSettings.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ApplicationName { get; set; }

		/// <summary>
		/// When overridden in a derived class, deletes profile properties and information for the supplied list of profiles.
		/// </summary>
		/// <returns>
		/// The number of profiles deleted from the data source.
		/// </returns>
		/// <param name="profiles">A <see cref="T:System.Web.Profile.ProfileInfoCollection"/>  of information about profiles that are to be deleted.</param>
		public override int DeleteProfiles(ProfileInfoCollection profiles)
		{
			return 0;
		}

		/// <summary>
		/// When overridden in a derived class, deletes profile properties and information for profiles that match the supplied list of user names.
		/// </summary>
		/// <returns>
		/// The number of profiles deleted from the data source.
		/// </returns>
		/// <param name="usernames">A string array of user names for profiles to be deleted.</param>
		public override int DeleteProfiles(string[] usernames)
		{
			return 0;
		}

		/// <summary>
		/// When overridden in a derived class, deletes all user-profile data for profiles in which the last activity date occurred before the specified date.
		/// </summary>
		/// <returns>
		/// The number of profiles deleted from the data source.
		/// </returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are deleted.</param><param name="userInactiveSinceDate">A <see cref="T:System.DateTime"/> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"/>  value of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			return 0;
		}

		/// <summary>
		/// When overridden in a derived class, returns the number of profiles in which the last activity date occurred on or before the specified date.
		/// </summary>
		/// <returns>
		/// The number of profiles in which the last activity date occurred on or before the specified date.
		/// </returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param><param name="userInactiveSinceDate">A <see cref="T:System.DateTime"/> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"/>  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			return 0;
		}

		/// <summary>
		/// When overridden in a derived class, retrieves user profile data for all profiles in the data source.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Web.Profile.ProfileInfoCollection"/> containing user-profile information for all profiles in the data source.
		/// </returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param><param name="pageIndex">The index of the page of results to return.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
		{
			totalRecords = 0;
			return _emptyInfo;
		}

		/// <summary>
		/// When overridden in a derived class, retrieves user-profile data from the data source for profiles in which the last activity date occurred on or before the specified date.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Web.Profile.ProfileInfoCollection"/> containing user-profile information about the inactive profiles.
		/// </returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param><param name="userInactiveSinceDate">A <see cref="T:System.DateTime"/> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"/>  of a user profile occurs on or before this date and time, the profile is considered inactive.</param><param name="pageIndex">The index of the page of results to return.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			totalRecords = 0;
			return _emptyInfo;
		}

		/// <summary>
		/// When overridden in a derived class, retrieves profile information for profiles in which the user name matches the specified user names.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Web.Profile.ProfileInfoCollection"/> containing user-profile information for profiles where the user name matches the supplied <paramref name="usernameToMatch"/> parameter.
		/// </returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param><param name="usernameToMatch">The user name to search for.</param><param name="pageIndex">The index of the page of results to return.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			totalRecords = 0;
			return _emptyInfo;
		}

		/// <summary>
		/// When overridden in a derived class, retrieves profile information for profiles in which the last activity date occurred on or before the specified date and the user name matches the specified user name.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Web.Profile.ProfileInfoCollection"/> containing user profile information for inactive profiles where the user name matches the supplied <paramref name="usernameToMatch"/> parameter.
		/// </returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param><param name="usernameToMatch">The user name to search for.</param><param name="userInactiveSinceDate">A <see cref="T:System.DateTime"/> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"/> value of a user profile occurs on or before this date and time, the profile is considered inactive.</param><param name="pageIndex">The index of the page of results to return.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			totalRecords = 0;
			return _emptyInfo;
		}

		/// <summary>
		/// Surfaces the list of properties set in the provider.
		/// </summary>
		public SettingsPropertyValueCollection Properties { get; set; }

		///  <summary>
		/// Allows asserting on a given property value.
		/// </summary>
		/// <remarks>The assertion is only performed if the property named after <paramref name="propertyName"/> exists.</remarks>
		/// <param name="propertyName">the name of the property which value is going to be asserted on.</param>
		/// <param name="assertion">Assertion on the value</param>
		/// <returns>The instance of <see cref="ProfileTestProvider"/> in order to be able to chain method calls.</returns>
		public ProfileTestProvider AssertPropertyValue(string propertyName, Action<object> assertion)
		{
			SettingsPropertyValue p = Properties[propertyName];
			if (p != null) assertion(p.PropertyValue);
			return this;
		}

		/// <summary>
		/// Sets the values for the properties defined in the profile system.
		/// </summary>
		/// <remarks>Setting the value for a property not defined in configuration would not throw.</remarks>
		/// <param name="values">A collection of property names and values.</param>
		public void StubValues(params KeyValuePair<string, object>[] values)
		{
			Properties = new SettingsPropertyValueCollection();

			foreach (var pair in values)
			{
				stubValue(pair.Key, pair.Value);
			}
		}

		/// <summary>
		/// Sets the values for the properties defined in the profile system.
		/// </summary>
		/// <remarks>Setting the value for a property not defined in configuration would not throw.</remarks>
		/// <param name="values">A collection of property names and values.</param>
		public void StubValues(Dictionary<string, object> values)
		{
			StubValues((IDictionary<string, object>)values);
		}

		/// <summary>
		/// Sets the values for the properties defined in the profile system.
		/// </summary>
		/// <remarks>Setting the value for a property not defined in configuration would not throw.</remarks>
		/// <param name="values">A collection of property names and values.</param>
		public void StubValues(IDictionary<string, object> values)
		{
			Properties = new SettingsPropertyValueCollection();

			foreach (var pair in values)
			{
				stubValue(pair.Key, pair.Value);
			}
		}

		/// <summary>
		/// Sets the values for the properties defined in the profile system as defined by the anonymous object.
		/// </summary>
		/// <remarks>Setting the value for a property not defined in configuration would not throw.</remarks>
		/// <param name="values">A collection of property names and values in the shape of an anonymous object.</param>
		public void StubValues<T>(T values)
		{
			StubValues(values.AsDictionary());
		}

		private static SettingsPropertyValueCollection mergeCollections(SettingsPropertyCollection noValues, SettingsPropertyValueCollection withValues)
		{
			SettingsPropertyValueCollection merged = new SettingsPropertyValueCollection();
			foreach (SettingsProperty property in noValues)
			{
				var setting = new SettingsPropertyValue(property);
				if (withValues != null && withValues[property.Name] != null && withValues[property.Name].PropertyValue != null)
				{
					setting.PropertyValue = withValues[property.Name].PropertyValue;
				}
				merged.Add(setting);
			}

			return merged;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return Properties
				.Cast<SettingsPropertyValue>()
				.Select(v => new KeyValuePair<string, object>(v.Name, v.PropertyValue))
				.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private void stubValue(string key, object value)
		{
			Properties = Properties ?? new SettingsPropertyValueCollection();

			Type type = value != null ? value.GetType() : typeof(object);
			var settings = new SettingsProperty(key, type, null, false, type.Default(),
				SettingsSerializeAs.ProviderSpecific, null, false, false);

			var settingsValue = new SettingsPropertyValue(settings) { PropertyValue = value };
			Properties.Add(settingsValue);
		}

		private static readonly FieldInfo s_Provider = typeof(ProfileManager).GetField("s_Provider", BindingFlags.Static | BindingFlags.NonPublic);
		/// <summary>
		/// Sets the default provider (<see cref="ProfileManager.Provider"/>) to a test provider.
		/// </summary>
		/// <remarks>Invoke it once per test fixture to ease configuration of the profile system while testing.
		/// <para>The test provider must have been added to the collection of available providers and should be named <c>test</c>.</para>
		/// </remarks>
		public static void SetAsDefault()
		{
			SetAsDefault("test");
		}

		/// <summary>
		/// Sets the default provider (<see cref="ProfileManager.Provider"/>) to a test provider.
		/// </summary>
		/// <remarks>Invoke it once per test fixture to ease configuration of the profile system while testing.
		/// <para>The test provider must have been added to the collection of available providers and should be named <paramref name="testProviderName"/>.</para>
		/// </remarks>
		/// <param name="testProviderName">Name of the test provider.</param>
		public static void SetAsDefault(string testProviderName)
		{
			s_Provider.SetValue(null, ProfileManager.Providers[testProviderName]);
		}
	}
}
