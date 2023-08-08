namespace Testing.Commons;

/// <summary>
/// Allows extensions of IEnumerable that are useful for testing
/// </summary>
public static class EnumerableExtensions
{
	/// <summary>
	/// Iterates a given enumerable.
	/// </summary>
	/// <remarks>When testing, the lazy nature of enumerables might give false positives. This provides an easy way to evaluate the enumerable.</remarks>
	/// <typeparam name="T">The type of objects to enumerate</typeparam>
	/// <param name="enumerable">Enumerable to be iterated.</param>
	public static void Iterate<T>(this IEnumerable<T> enumerable)
	{
		foreach (var _ in enumerable) { }
	}
}
