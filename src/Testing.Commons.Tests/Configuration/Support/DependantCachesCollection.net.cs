using System.Configuration;

namespace Testing.Commons.Tests.Configuration.Support
{
	internal class DependantCachesCollection : ConfigurationElementCollection
	{
		internal static readonly string CollectionName = "dependantCaches";

		private static readonly ConfigurationProperty _caches;
		private static readonly ConfigurationPropertyCollection _properties;

		static DependantCachesCollection()
		{
			_caches = new ConfigurationProperty(CollectionName, typeof(DependantCachesCollection), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsDefaultCollection);
			_properties = new ConfigurationPropertyCollection { _caches };
		}

		protected override ConfigurationPropertyCollection Properties { get { return _properties; } }

		protected override string ElementName
		{
			get
			{
				return DependantCacheElement.ElementName;
			}
		}

		public override ConfigurationElementCollectionType CollectionType { get { return ConfigurationElementCollectionType.BasicMap; } }

		public DependantCacheElement this[int index]
		{
			get
			{
				return (DependantCacheElement)BaseGet(index);
			}
		}

		public new DependantCacheElement this[string key]
		{
			get
			{
				return (DependantCacheElement)BaseGet(key);
			}
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new DependantCacheElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((DependantCacheElement)element).Name;
		}
	}
}