using System.Diagnostics.CodeAnalysis;

namespace Testing.Commons.Serialization;

internal static class Arg
{
	internal static void ThrowIfNullOrEmpty([NotNull] string? argument, string? paramName = null)
	{
#if NET7_0_OR_GREATER
		ArgumentException.ThrowIfNullOrEmpty(argument, paramName);
#else
		ArgumentNullException.ThrowIfNull(argument, paramName);
		if (argument.Length == 0)
		{
			throw new ArgumentException("The value cannot be an empty string.", paramName);
		}
#endif
	}
}
