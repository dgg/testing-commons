using System.Configuration;

namespace Testing.Commons.Configuration
{
	/// <summary>
	/// Allows easy extensibility for testing messages of <see cref="ConfigurationErrorsException"/>.
	/// </summary>
	public static class ExceptionMessagePart
	{
		/// <summary>
		/// Allows meaningful extensions for messages of <see cref="ConfigurationErrorsException"/>.
		/// </summary>
		public class ForEntryPoint
		{
			internal ForEntryPoint() { }
		}

		private static readonly ForEntryPoint _forEntryPoint = new ForEntryPoint();
		/// <summary>
		/// Allows meaningful extensions for messages of <see cref="ConfigurationErrorsException"/>.
		/// </summary>
		/// <example><code>Assert.That(new[]{1, 2, 3}, Must.Contain.ASingleEvenNumber())</code>
		/// where <c>.ASingleEvenNumber()</c> returns a custom assertion.</example>
		public static ForEntryPoint For { get { return _forEntryPoint; } }
	}
}
