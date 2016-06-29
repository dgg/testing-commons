using System;
using System.IO;
using System.Reflection;

namespace Testing.Commons.Configuration
{
	/// <summary>
	/// Enables getting the path to a fake configuration assembly configured in the test.
	/// </summary>
	public class ExternalConfiguration
	{
		/// <summary>
		/// Allows acess to the information set in the <see cref="ConfigurationAssemblyAttribute"/> in the test being run,
		/// enabling testing external configuration files.
		/// </summary>
		/// <param name="test">Represents the test being run.</param>
		/// <returns>The full path to the fake configuration assembly.</returns>
		public static string GetConfigurationAssemblyPath(MethodBase test)
		{
			var attribute = (ConfigurationAssemblyAttribute) Attribute.GetCustomAttribute(test, typeof(ConfigurationAssemblyAttribute));

			ensureConfigurationAssembly(attribute);

			return attribute.FullPath;
		}

		private static void ensureConfigurationAssembly(ConfigurationAssemblyAttribute attribute)
		{
			if (attribute == null)
			{
				throw new MissingMemberException(
					string.Format(Resources.Exceptions.MissingExternalConfigurationAssemblyAttribute_Template,
						typeof(ConfigurationAssemblyAttribute).Name));
			}

			if (!attribute.Exists()) throw new ArgumentException(
				string.Format(Resources.Exceptions.MissingExternalConfigurationAssemblyFile_Template, attribute.FullPath),
				"path");
			if (!attribute.PointsToAnAssembly()) throw new ArgumentException(
				string.Format(Resources.Exceptions.NotAnExternalConfigurationAssembly_Template, attribute.FullPath),
				"path");
		}
	}
}
