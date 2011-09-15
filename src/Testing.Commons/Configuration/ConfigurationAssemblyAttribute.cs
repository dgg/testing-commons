using System;

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
		/// <param name="path">Relative path to the fake configuration assembly.</param>
		public ConfigurationAssemblyAttribute(string path)
		{
			Path = path;
		}

		/// <summary>
		/// Relative path to the fake configuration assembly.
		/// </summary>
		public string Path { get; private set; }

		/// <summary>
		/// Full path to the fake configuration assembly.
		/// </summary>
		public string FullPath { get{ return System.IO.Path.GetFullPath(Path);} }

		/// <summary>
		/// true when the fake configuration assembly exists in the filesystem, false otherwise.
		/// </summary>
		public bool Exists { get { return System.IO.File.Exists(Path); } }

		/// <summary>
		/// true if the path points to an assembly (.dll) file, false otherwise
		/// </summary>
		public bool PointsToAnAssembly { get { return Exists && System.IO.Path.GetExtension(Path) == ".dll"; } }

		/// <summary>
		/// Returns the file name and extension of the path string specified by <see cref="Path"/>.
		/// </summary>
		public string AssemblyName { get { return System.IO.Path.GetFileName(Path); } }

		/// <summary>
		/// Returns the directory information for the path string specified by <see cref="Path"/>.
		/// </summary>
		public string AssemblyPath { get { return System.IO.Path.GetDirectoryName(Path); } }
	}
}
