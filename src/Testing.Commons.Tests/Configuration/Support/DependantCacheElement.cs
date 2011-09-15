using System.Configuration;

namespace Testing.Commons.Tests.Configuration.Support
{
	internal class DependantCacheElement : ConfigurationElement
	{
		internal const string ElementName = "dependantCache";

		internal static readonly string NAME = "name";
		private static readonly ConfigurationProperty _name;
		public string Name
		{
			get { return (string)base[_name]; }
		}

		static DependantCacheElement()
		{
			_name = new ConfigurationProperty(NAME, typeof(string), null, null, new StringValidator(1), ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
			_properties = new ConfigurationPropertyCollection { _name };
		}

		private static readonly ConfigurationPropertyCollection _properties;
		protected override ConfigurationPropertyCollection Properties
		{
			get { return _properties; }
		}
	}
}
