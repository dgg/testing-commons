using System.Collections.Generic;

namespace Dgg.Testing
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Iterate<T>(this IEnumerable<T> enumerable)
		{
			foreach (var v in enumerable) { yield return v; }
		}
	}
}
