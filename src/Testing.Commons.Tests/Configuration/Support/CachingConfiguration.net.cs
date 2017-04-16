using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Testing.Commons.Tests.Configuration.Support
{
	public class CachingConfiguration : ICachingConfiguration
	{
		private readonly CachingConfigurationSection _section;
		public CachingConfiguration()
		{
			_section = (CachingConfigurationSection)ConfigurationManager.GetSection(CachingConfigurationSection.SectionName);
		}

		public CachingConfiguration(string configFile)
		{
			_section = (CachingConfigurationSection)ConfigurationManager.OpenExeConfiguration(configFile).GetSection(CachingConfigurationSection.SectionName);
		}

		public TimeSpan TimeToExpire(string cacheName)
		{
			ExpirationElement expiration = _section.Expirations[cacheName];
			if (expiration == null) throw new ConfigurationErrorsException();
			return expiration.Value;
		}

		public bool HasDependencies(string cacheName)
		{
			DependenciesCollection dependencies = _section.Dependencies;
			return dependencies != null && dependencies[cacheName] != null;
		}

		public IEnumerable<string> GetDependantCaches(string cacheName)
		{
			IEnumerable<string> cacheNames = Enumerable.Empty<string>();
			if (HasDependencies(cacheName))
			{
				cacheNames = _section.Dependencies[cacheName].DependantCaches
					.Cast<DependantCacheElement>()
					.Select(dependant => dependant.Name);
			}
			return cacheNames;
		}
	}
}