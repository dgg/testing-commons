namespace Testing.Commons.Builders
{
	/// <summary>
	/// Simple interface for objects that create complex objects for testing purposes.
	/// </summary>
	/// <typeparam name="T">Type to be built</typeparam>
	public interface IBuilder<out T>
	{
		/// <summary>
		/// Creates a instance of a complex object as the final step of the building process.
		/// </summary>
		/// <returns>Instance built.</returns>
		T Build();
	}
}