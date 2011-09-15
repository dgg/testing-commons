using System.Configuration;

namespace Testing.Commons.Tests.Configuration.Support
{
	internal class ExpirationsCollection : ConfigurationElementCollection
	{
		internal static readonly string CollectionName = "expirations";

		private static readonly ConfigurationProperty _expirations;
		private static readonly ConfigurationPropertyCollection _properties;

		static ExpirationsCollection()
		{
			_expirations = new ConfigurationProperty(CollectionName, typeof(ConfigurationElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
			_properties = new ConfigurationPropertyCollection { _expirations };
		}

		protected override ConfigurationPropertyCollection Properties { get { return _properties; } }

		protected override string ElementName
		{
			get
			{
				return ExpirationElement.ElementName;
			}
		}

		public override ConfigurationElementCollectionType CollectionType { get { return ConfigurationElementCollectionType.BasicMap; } }

		public ExpirationElement this[int index]
		{
			get
			{
				return (ExpirationElement)BaseGet(index);
			}
		}

		public new ExpirationElement this[string key]
		{
			get
			{
				return (ExpirationElement)BaseGet(key);
			}
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ExpirationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ExpirationElement)element).Name;
		}
	}
}