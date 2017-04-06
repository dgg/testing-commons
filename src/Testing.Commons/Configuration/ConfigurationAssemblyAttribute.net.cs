using System;
using System.IO;

namespace Testing.Commons.Configuration
{
	/// <summary>
	/// Allows specifying the path to the fake configuration assembly
	/// </summary>
	/// <remarks>The fake configuration assembly is an empty .dll file that serves as a loading point for a external configuration file to test.</remarks>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ConfigurationAssemblyAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationAssemblyAttribute"/> class. 
		/// </summary>
		/// <param name="relativePathToBinCopiedConfigFile">Relative path to the fake configuration assembly.</param>
		public ConfigurationAssemblyAttribute(string relativePathToBinCopiedConfigFile)
		{
			RelativePath = relativePathToBinCopiedConfigFile;
			var pathToCurrentAssembly = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
			string executingAssemlyDirectory = Path.GetDirectoryName(pathToCurrentAssembly);
			FullPath = Path.Combine(executingAssemlyDirectory, RelativePath);
		}

		/// <summary>
		/// Relative path to the fake configuration assembly.
		/// </summary>
		public string RelativePath { get; }

		/// <summary>
		/// Full path to the fake configuration assembly.
		/// </summary>
		public string FullPath { get; }

		/// <summary>
		/// true when the fake configuration assembly exists in the filesystem, false otherwise.
		/// </summary>
		public bool Exists()
		{
			return File.Exists(FullPath);
		}

		/// <summary>
		/// true if the path points to an assembly (.dll) file, false otherwise
		/// </summary>
		public bool PointsToAnAssembly() => Exists() && Path.GetExtension(RelativePath) == ".dll";
	}
}
