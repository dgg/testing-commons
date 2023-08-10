namespace Testing.Commons.Resources;

#pragma warning disable CA1802
internal static class Exceptions
{
	public static readonly string InvertedRange_Template = "The end date has to occur later than the start date '{0}'.";
	public static readonly string UnorderedRangeBounds = "The start value of the range must not be greater than its end value.";
}
#pragma warning restore CA1802
