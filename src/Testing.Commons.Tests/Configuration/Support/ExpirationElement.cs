using System;
using System.Configuration;

namespace Testing.Commons.Tests.Configuration.Support
{
	internal class ExpirationElement : ConfigurationElement
	{
		internal const string ElementName = "expiration";

		internal static readonly string NAME = "name";
		private static readonly ConfigurationProperty _name;
		public string Name
		{
			get { return (string)base[_name]; }
		}

		internal const string VALUE = "value";
		private static readonly ConfigurationProperty _expirationInSeconds;
		public TimeSpan Value
		{
			get { return (TimeSpan)base[_expirationInSeconds]; }
		}

		static ExpirationElement()
		{
			_name = new ConfigurationProperty(NAME, typeof(string), null, null, new StringValidator(1), ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
			_expirationInSeconds = new ConfigurationProperty(VALUE, typeof(TimeSpan), TimeSpan.MaxValue, null, new PositiveTimeSpanValidator(), ConfigurationPropertyOptions.IsRequired);
			_properties = new ConfigurationPropertyCollection { _name, _expirationInSeconds };
		}

		private static readonly ConfigurationPropertyCollection _properties;
		protected override ConfigurationPropertyCollection Properties
		{
			get { return _properties; }
		}
	}
}