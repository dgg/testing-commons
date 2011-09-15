using System.Configuration;

namespace Testing.Commons.Tests.Configuration.Support
{
	internal class CachingConfigurationSection : ConfigurationSection
	{
		public static readonly string SectionName = "cachingConfiguration";

		private static readonly ConfigurationProperty _expirations;
		public ExpirationsCollection Expirations
		{
			get { return (ExpirationsCollection)base[_expirations]; }
		}

		private static readonly ConfigurationProperty _dependencies;
		public DependenciesCollection Dependencies
		{
			get { return (DependenciesCollection)base[_dependencies]; }
		}

		static CachingConfigurationSection()
		{
			_expirations = new ConfigurationProperty(ExpirationsCollection.CollectionName, typeof(ExpirationsCollection), null, ConfigurationPropertyOptions.IsDefaultCollection | ConfigurationPropertyOptions.IsRequired);
			_dependencies = new ConfigurationProperty(DependenciesCollection.CollectionName, typeof(DependenciesCollection), null, ConfigurationPropertyOptions.None);
			_properties = new ConfigurationPropertyCollection { _expirations, _dependencies };
		}

		private static readonly ConfigurationPropertyCollection _properties;
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return _properties;
			}
		}
	}
}