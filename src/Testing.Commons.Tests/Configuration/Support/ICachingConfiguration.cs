using System;
using System.Collections.Generic;

namespace Testing.Commons.Tests.Configuration.Support
{
	public interface ICachingConfiguration
	{
		TimeSpan TimeToExpire(string cacheName);
		bool HasDependencies(string cacheName);
		IEnumerable<string> GetDependantCaches(string cacheName);
	}
}