using System.Collections.Generic;

namespace Testing.Commons
{
	/// <summary>
	/// Creates instances of <see cref="KeyValuePair{TKey,TValue}"/> taking advantage of type inference.
	/// </summary>
	public static class KeyValue
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KeyValuePair{TKey,TValue}"/> structure with the specified key and value.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="key">The object defined in each key/value pair.</param>
		/// <param name="value">The definition associated with <paramref name="key"/>.</param>
		/// <returns>A new instance of <see cref="KeyValuePair{TKey,TValue}"/> with the specified values.</returns>
		public static KeyValuePair<TKey, TValue> New<TKey, TValue>(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyValuePair{TKey,TValue}"/> structure with the specified key and value.
		/// </summary>
		/// <param name="key">The object defined in each key/value pair.</param>
		/// <param name="value">The definition associated with <paramref name="key"/>.</param>
		/// <returns>A new instance of <see cref="KeyValuePair{TKey,TValue}"/> with the specified values.</returns>
		public static KeyValuePair<string, object> Pair(string key, object value)
		{
			return New(key, value);
		}
	}
}
