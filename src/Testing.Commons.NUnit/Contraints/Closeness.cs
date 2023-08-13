namespace Testing.Commons.NUnit.Constraints;

/// <summary>
/// Tolerance values for time constraints
/// </summary>
public static class Closeness
{
	/// <summary>
	/// 20 ms for closeness constraints
	/// </summary>
	public static readonly TimeSpan Default = TimeSpan.FromMilliseconds(20);
}
