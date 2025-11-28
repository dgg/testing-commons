namespace Testing.Commons.Builders;

/// <summary>
/// Provides static methods for creating enumerable sequences, mostly for test scenarios.
/// </summary>
public static class Chain
{
	/// <summary>
	/// Returns an empty <see cref="IEnumerable{T}"/> of the specified type.
	/// </summary>
	/// <typeparam name="T">The type of elements in the empty sequence.</typeparam>
	/// <returns>An empty <see cref="IEnumerable{T}"/>.</returns>
	public static IEnumerable<T> Empty<T>() => Enumerable.Empty<T>();

	/// <summary>
	/// Returns a <see langword="null"/> <see cref="IEnumerable{T}"/> of the specified type.
	/// </summary>
	/// <typeparam name="T">The type of elements in the sequence.</typeparam>
	/// <returns><see langword="null"/>.</returns>
	public static IEnumerable<T>? Null<T>() => null;

	/// <summary>
	/// Creates an infinite <see cref="IEnumerable{T}"/> sequence that repeatedly yields the specified element.
	/// </summary>
	/// <typeparam name="T">The type of the element to yield.</typeparam>
	/// <param name="element">The element to repeat infinitely.</param>
	/// <returns>An infinite <see cref="IEnumerable{T}"/> sequence containing the specified element.</returns>
	public static IEnumerable<T> Of<T>(T element)
	{
		while (true) yield return element;
		// ReSharper disable once IteratorNeverReturns
	}

	/// <summary>
	/// Creates an <see cref="IEnumerable{T}"/> sequence from the specified elements.
	/// </summary>
	/// <typeparam name="T">The type of elements in the sequence.</typeparam>
	/// <param name="args">The elements to include in the sequence.</param>
	/// <returns>An <see cref="IEnumerable{T}"/> sequence containing the specified elements.</returns>
	public static IEnumerable<T> From<T>(params T[] args) => args;
}
