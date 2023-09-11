using Testing.Commons.Builders;

namespace Testing.Commons.Tests.Builders.Support;

internal static class BuilderExtensions
{
	public static ExceptionBuilder Exceptions(this Builder.OfEntryPoint _)
	{
		return new ExceptionBuilder();
	}

	public static ArgumentExceptionBuilder ArgumentExceptions(this Builder.ForEntryPoint _)
	{
		return new ArgumentExceptionBuilder();
	}

	public static IPreProductNameBuilder Products(this Builder.ForEntryPoint _)
	{
		return new ProductBuilder();
	}
}
