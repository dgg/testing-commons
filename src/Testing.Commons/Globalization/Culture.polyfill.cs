using System;
using System.Collections.Generic;
using System.Globalization;
#if NET
using System.Threading;
#endif

namespace Testing.Commons.Globalization
{
	internal static class Culture
	{
		public static CultureInfo Get(string name)
		{
			return
#if NET
				CultureInfo.GetCultureInfo(name);
#else
				new CultureInfo(name);
#endif
		}

		public static void SetOnThread(CultureInfo threadCulture, CultureInfo threadUICulture)
		{
#if NET
			Thread.CurrentThread.CurrentCulture = threadCulture;
			Thread.CurrentThread.CurrentUICulture = threadUICulture;
#else
			CultureInfo.DefaultThreadCurrentCulture = threadCulture;
			CultureInfo.DefaultThreadCurrentUICulture = threadUICulture;
#endif
		}

		public static void SetOnThread(CultureInfo threadCulture)
		{
#if NET
			Thread.CurrentThread.CurrentCulture = threadCulture;
#else
			CultureInfo.DefaultThreadCurrentCulture = threadCulture;
#endif
		}

		public static void SetUIOnThread(CultureInfo threadUICulture)
		{
#if NET
			Thread.CurrentThread.CurrentUICulture = threadUICulture;
#else
			CultureInfo.DefaultThreadCurrentUICulture = threadUICulture;
#endif
		}


		public static CultureInfo GetFromThread()
		{
#if NET
			return Thread.CurrentThread.CurrentCulture;
#else
			return CultureInfo.CurrentCulture;
#endif
		}

		public static CultureInfo GetUIFromThread()
		{
#if NET
			return Thread.CurrentThread.CurrentUICulture;
#else
			return CultureInfo.CurrentUICulture;
#endif
		}
	}
}
