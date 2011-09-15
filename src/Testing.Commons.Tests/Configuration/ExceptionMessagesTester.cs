using System.Configuration;
using System.Reflection;
using NUnit.Framework;
using Testing.Commons.Configuration;
using Testing.Commons.Configuration.Messages;
using Testing.Commons.Tests.Configuration.Support;

namespace Testing.Commons.Tests.Configuration
{
	[TestFixture, Category("Integration")]
	public class ExceptionMessagesTester
	{
		[Test, ConfigurationAssembly("..\\..\\Configuration\\ConfigFiles\\CachingIncorrectConfig_UndefinedElement.dll")]
		public void UndefinedElement_ExceptionWithMessageContaining()
		{
			string assemblyPath = ExternalConfiguration.GetConfigurationAssemblyPath(MethodBase.GetCurrentMethod());

			Assert.That(() => new CachingConfiguration(assemblyPath), Throws.InstanceOf<ConfigurationErrorsException>()
				.With.Message.StringContaining(ExceptionMessagePart.For.UndefinedElement(ExpirationElement.ElementName)));
		}

		[Test, ConfigurationAssembly("..\\..\\Configuration\\ConfigFiles\\CachingIncorrectConfig_MissingRequiredElement.dll")]
		public void MissingRequiredElement_ExceptionWithMessageContaining()
		{
			string assemblyPath = ExternalConfiguration.GetConfigurationAssemblyPath(MethodBase.GetCurrentMethod());

			Assert.That(() => new CachingConfiguration(assemblyPath), Throws.InstanceOf<ConfigurationErrorsException>()
				.With.Message.StringContaining(ExceptionMessagePart.For.MissingRequiredMember(ExpirationsCollection.CollectionName)));
		}

		[Test, ConfigurationAssembly("..\\..\\Configuration\\ConfigFiles\\CachingIncorrectConfig_MissingRequiredAttribute.dll")]
		public void MissingRequiredAttribute_ExceptionWithMessageContaining()
		{
			string assemblyPath = ExternalConfiguration.GetConfigurationAssemblyPath(MethodBase.GetCurrentMethod());

			Assert.That(() => new CachingConfiguration(assemblyPath), Throws.InstanceOf<ConfigurationErrorsException>()
				.With.Message.StringContaining(ExceptionMessagePart.For.MissingRequiredMember(ExpirationElement.NAME)));
		}

		[Test, ConfigurationAssembly("..\\..\\Configuration\\ConfigFiles\\CachingIncorrectConfig_InvalidAttributeValue.dll")]
		public void InvalidAttributeValue_ExceptionWithMessageContaining()
		{
			string assemblyPath = ExternalConfiguration.GetConfigurationAssemblyPath(MethodBase.GetCurrentMethod());

			Assert.That(() => new CachingConfiguration(assemblyPath), Throws.InstanceOf<ConfigurationErrorsException>()
				.With.Message.StringContaining(ExceptionMessagePart.For.InvalidAttributeValue(ExpirationElement.NAME)));
		}

		[Test, ConfigurationAssembly("..\\..\\Configuration\\ConfigFiles\\CachingIncorrectConfig_WrongAttributeValue.dll")]
		public void WrongAttributeValue_ExceptionWithMessageContaining()
		{
			string assemblyPath = ExternalConfiguration.GetConfigurationAssemblyPath(MethodBase.GetCurrentMethod());

			//NOTE: for the exception to be thrown the element needs to be accessed
			Assert.That(() => new CachingConfiguration(assemblyPath).TimeToExpire("name"), Throws.InstanceOf<ConfigurationErrorsException>()
				.With.Message.StringContaining(ExceptionMessagePart.For.WrongMemberValue(ExpirationElement.VALUE)));
		}

		[Test, ConfigurationAssembly("..\\..\\Configuration\\ConfigFiles\\CachingIncorrectConfig_MissingChildrenElement.dll")]
		public void MissingChildrenElement_ExceptionWithMessageContaining()
		{
			string assemblyPath = ExternalConfiguration.GetConfigurationAssemblyPath(MethodBase.GetCurrentMethod());

			Assert.That(() => new CachingConfiguration(assemblyPath), Throws.InstanceOf<ConfigurationErrorsException>()
				.With.Message.StringContaining(ExceptionMessagePart.For.MissingRequiredChildElement<DependantCachesCollection>()));
		}
	}
}
