using System.Configuration;

namespace Testing.Commons.Tests.Configuration.Support
{
	internal class DependenciesCacheElement : ConfigurationElement
	{
		internal const string ElementName = "cache";

		internal static readonly string NAME = "name";
		private static readonly ConfigurationProperty _name;
		public string Name
		{
			get { return (string)base[_name]; }
		}

		private static readonly ConfigurationProperty _dependantCaches;
		public DependantCachesCollection DependantCaches
		{
			get { return (DependantCachesCollection)base[_dependantCaches]; }
		}

		static DependenciesCacheElement()
		{
			_name = new ConfigurationProperty(NAME, typeof(string), null, null, new StringValidator(1), ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
			_dependantCaches = new ConfigurationProperty(DependantCachesCollection.CollectionName, typeof(DependantCachesCollection), null, null, new CollectionCountValidator(1), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsDefaultCollection);
			_properties = new ConfigurationPropertyCollection { _name, _dependantCaches };
		}

		private static readonly ConfigurationPropertyCollection _properties;
		protected override ConfigurationPropertyCollection Properties
		{
			get { return _properties; }
		}
	}
}