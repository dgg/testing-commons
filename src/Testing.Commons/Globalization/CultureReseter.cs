using System;
using System.Globalization;
using System.Resources;

namespace Testing.Commons.Globalization
{
	/// <summary>
	/// Allows setting and restoring both the culture and the UI culture in the current thread.
	/// </summary>
	public class CultureReseter : IDisposable
	{
		private readonly CultureInfo _threadCulture, _threadUICulture;

		/// <summary>
		/// Creates an instance of the reseter.
		/// </summary>
		/// <remarks>"Freezes" the current information in order to be restored upon disposal.</remarks>
		public CultureReseter()
		{
			backUpThreadCulture(out _threadCulture, out _threadUICulture);
		}

		private CultureReseter set(CultureInfo threadCulture, CultureInfo threadUICulture)
		{
			Culture.SetOnThread(threadCulture, threadUICulture);
			return this;
		}

		/// <summary>
		/// Sets culture and UI culture in the current thread to the provided cultures.
		/// </summary>
		/// <param name="threadCulture">Culture for the current thread.</param>
		/// <param name="threadUICulture">Culture used by the <see cref="ResourceManager"/></param>
		/// <returns>An instance of <see cref="CultureReseter"/> with the cultures set and able to restore the previous cultures.</returns>
		public static CultureReseter Set(CultureInfo threadCulture, CultureInfo threadUICulture)
		{
			return new CultureReseter().set(threadCulture, threadUICulture);
		}

		/// <summary>
		/// Sets culture and UI culture in the current thread to the provided cultures.
		/// </summary>
		/// <remarks>Instances of <see cref="CultureInfo"/> will be retrieved from a cache for the full .NET Framework
		/// and instantiated otherwise.</remarks>
		/// <param name="cultureName">Culture name for the current thread.</param>
		/// <param name="uiCultureName">Culture name used by the <see cref="ResourceManager"/></param>
		/// <returns>An instance of <see cref="CultureReseter"/> with the cultures set and able to restore the previous cultures.</returns>
		public static CultureReseter Set(string cultureName, string uiCultureName)
		{
			return Set(Culture.Get(cultureName), Culture.Get(uiCultureName));
		}

		/// <summary>
		/// Sets culture and UI culture in the current thread to the provided culture.
		/// </summary>
		/// <param name="bothCultures">Culture for the current thread and used by the <see cref="ResourceManager"/></param>
		/// <returns>An instance of <see cref="CultureReseter"/> with the cultures set and able to restore the previous cultures.</returns>
		public static CultureReseter Set(CultureInfo bothCultures)
		{
			return new CultureReseter().set(bothCultures, bothCultures);
		}

		/// <summary>
		/// Sets culture and UI culture in the current thread to the provided culture.
		/// </summary>
		/// <remarks>Instances of <see cref="CultureInfo"/> will be retrieved from a cache for the full .NET Framework
		/// and instantiated otherwise.</remarks>
		/// <param name="bothCultureName">Culture name for the current thread and used by the <see cref="ResourceManager"/></param>
		/// <returns>An instance of <see cref="CultureReseter"/> with the cultures set and able to restore the previous cultures.</returns>
		public static CultureReseter Set(string bothCultureName)
		{
			return Set(Culture.Get(bothCultureName));
		}

		/// <summary>
		/// Restores culture and UI culture in the current thread to the previous values.
		/// </summary>
		public void Dispose()
		{
			Culture.SetOnThread(_threadCulture, _threadUICulture);
		}

		private static void backUpThreadCulture(out CultureInfo threadCulture, out CultureInfo threadUICulture)
		{
			threadCulture = CultureInfo.CurrentCulture;
			threadUICulture = CultureInfo.CurrentUICulture;
		}
	}
}
