using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Testing.Commons.Globalization
{
	/// <summary>
	/// Allows setting and restoring both <see cref="Thread.CurrentCulture"/> and <see cref="Thread.CurrentUICulture"/>.
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
			setThreadCulture(threadCulture, threadUICulture);
			return this;
		}

		/// <summary>
		/// Sets <see cref="Thread.CurrentCulture"/> and <see cref="Thread.CurrentUICulture"/> to the provided cultures.
		/// </summary>
		/// <param name="threadCulture">Culture for the current thread.</param>
		/// <param name="threadUICulture">Culture used by the <see cref="ResourceManager"/></param>
		/// <returns>An instance of <see cref="CultureReseter"/> with the cultures set and able to restore the previous cultures.</returns>
		public static CultureReseter Set(CultureInfo threadCulture, CultureInfo threadUICulture)
		{
			return new CultureReseter().set(threadCulture, threadUICulture);
		}

		/// <summary>
		/// Sets <see cref="Thread.CurrentCulture"/> and <see cref="Thread.CurrentUICulture"/> to the provided cultures.
		/// </summary>
		/// <remarks>Instances of <see cref="CultureInfo"/> will be created using <see cref="CultureInfo.GetCultureInfo(string)"/>.</remarks>
		/// <param name="cultureName">Culture name for the current thread.</param>
		/// <param name="uiCultureName">Culture name used by the <see cref="ResourceManager"/></param>
		/// <returns>An instance of <see cref="CultureReseter"/> with the cultures set and able to restore the previous cultures.</returns>
		public static CultureReseter Set(string cultureName, string uiCultureName)
		{
			return Set(CultureInfo.GetCultureInfo(cultureName), CultureInfo.GetCultureInfo(uiCultureName));
		}

		/// <summary>
		/// Sets <see cref="Thread.CurrentCulture"/> and <see cref="Thread.CurrentUICulture"/> to the provided culture.
		/// </summary>
		/// <param name="bothCultures">Culture for the current thread and used by the <see cref="ResourceManager"/></param>
		/// <returns>An instance of <see cref="CultureReseter"/> with the cultures set and able to restore the previous cultures.</returns>
		public static CultureReseter Set(CultureInfo bothCultures)
		{
			return new CultureReseter().set(bothCultures, bothCultures);
		}

		/// <summary>
		/// Sets <see cref="Thread.CurrentCulture"/> and <see cref="Thread.CurrentUICulture"/> to the provided culture.
		/// </summary>
		/// <remarks>Instances of <see cref="CultureInfo"/> will be created using <see cref="CultureInfo.GetCultureInfo(string)"/>.</remarks>
		/// <param name="bothCultureName">Culture name for the current thread and used by the <see cref="ResourceManager"/></param>
		/// <returns>An instance of <see cref="CultureReseter"/> with the cultures set and able to restore the previous cultures.</returns>
		public static CultureReseter Set(string bothCultureName)
		{
			return Set(new CultureInfo(bothCultureName));
		}

		/// <summary>
		/// Restores <see cref="Thread.CurrentCulture"/> and <see cref="Thread.CurrentUICulture"/> to the previous values.
		/// </summary>
		public void Dispose()
		{
			setThreadCulture(_threadCulture, _threadUICulture);
		}

		private static void backUpThreadCulture(out CultureInfo threadCulture, out CultureInfo threadUICulture)
		{
			threadCulture = CultureInfo.CurrentCulture;
			threadUICulture = CultureInfo.CurrentUICulture;
		}

		private static void setThreadCulture(CultureInfo threadCulture, CultureInfo threadUICulture)
		{
			Thread.CurrentThread.CurrentCulture = threadCulture;
			Thread.CurrentThread.CurrentUICulture = threadUICulture;
		}
	}
}
