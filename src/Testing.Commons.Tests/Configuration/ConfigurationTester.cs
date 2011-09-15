using System;
using System.Reflection;
using NUnit.Framework;
using Testing.Commons.Configuration;
using Testing.Commons.Tests.Configuration.Support;
using Testing.Commons.Time;

namespace Testing.Commons.Tests.Configuration
{
	[TestFixture, Category("Integration")]
	public class ConfigurationTester
	{
		[Test]
		// both .dll and .config files needs to be copies to the ouput
		[ConfigurationAssembly("..\\..\\Configuration\\ConfigFiles\\CachingCorrectConfig.dll")]
		public void GetConfigurationAssemblyPath_TestDecoratedWithAssemblyFile_AllowsAccessToConfigurationValuesFromCustomConfiguration()
		{
			string assemblyPath = ExternalConfiguration.GetConfigurationAssemblyPath(MethodBase.GetCurrentMethod());

			ICachingConfiguration subject = null;
			Assert.That(() => subject = new CachingConfiguration(assemblyPath), Throws.Nothing);

			Assert.That(subject.TimeToExpire("expiration1"), Is.EqualTo(1.Seconds()));
			Assert.That(subject.HasDependencies("cache2"), Is.True);
			Assert.That(subject.GetDependantCaches("cache1"), Is.EqualTo(new[]{"cache1_1", "cache1_2"}));
		}

		[Test]
		// both .dll and .config files needs to be copies to the ouput
		[ConfigurationAssembly("..\\..\\Configuration\\ConfigFiles\\notExisting.dll")]
		public void GetConfigurationAssemblyPath_AssemblyFileDoesNotExist_Exception()
		{
			// in order to get the test method, we have to execute outside the delegate
			MethodBase testMethod = MethodBase.GetCurrentMethod();

			Assert.That(() => ExternalConfiguration.GetConfigurationAssemblyPath(testMethod), Throws.InstanceOf<ArgumentException>()
				.With.Message.StringContaining("\\notExisting.dll").And
				.Property("ParamName").EqualTo("path"));
		}

		[Test]
		// both .dll and .config files needs to be copies to the ouput
		[ConfigurationAssembly("..\\..\\Configuration\\ConfigFiles\\notAnAssembly.txt")]
		public void GetConfigurationAssemblyPath_AssemblyFileNotAnAssembly_Exception()
		{
			MethodBase testMethod = MethodBase.GetCurrentMethod();

			Assert.That(() => ExternalConfiguration.GetConfigurationAssemblyPath(testMethod), Throws.InstanceOf<ArgumentException>()
				.With.Message.StringContaining("\\notAnAssembly.txt").And
				.Property("ParamName").EqualTo("path"));
		}

		[Test, Category("Integration")]
		public void GetConfigurationAssemblyPath_TestNotDecoratedWithAssemblyFile_Exception()
		{
			MethodBase testMethod = MethodBase.GetCurrentMethod();

			Assert.That(() => ExternalConfiguration.GetConfigurationAssemblyPath(testMethod), Throws.InstanceOf<MissingMemberException>()
				.With.Message.StringContaining(typeof(ConfigurationAssemblyAttribute).Name));
		}
	}
}
