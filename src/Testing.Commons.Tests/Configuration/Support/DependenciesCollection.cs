using System.Configuration;

namespace Testing.Commons.Tests.Configuration.Support
{
	internal class DependenciesCollection : ConfigurationElementCollection
	{
		internal static readonly string CollectionName = "dependencies";

		private static readonly ConfigurationProperty _caches;
		private static readonly ConfigurationPropertyCollection _properties;

		static DependenciesCollection()
		{
			_caches = new ConfigurationProperty(CollectionName, typeof(DependenciesCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
			_properties = new ConfigurationPropertyCollection { _caches };
		}

		protected override ConfigurationPropertyCollection Properties { get { return _properties; } }

		protected override string ElementName
		{
			get
			{
				return DependenciesCacheElement.ElementName;
			}
		}

		public override ConfigurationElementCollectionType CollectionType { get { return ConfigurationElementCollectionType.BasicMap; } }

		public DependenciesCacheElement this[int index]
		{
			get
			{
				return (DependenciesCacheElement)BaseGet(index);
			}
		}

		public new DependenciesCacheElement this[string key]
		{
			get
			{
				return (DependenciesCacheElement)BaseGet(key);
			}
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new DependenciesCacheElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((DependenciesCacheElement)element).Name;
		}
	}
}