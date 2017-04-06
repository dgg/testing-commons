namespace Testing.Commons.Resources
{
	public static class Messages
	{
		public static readonly string InvertedRange_Template = "The end date has to occur later than the start date '{0}'.";
		public static readonly string MissingExternalConfigurationAssemblyAttribute_Template ="The test method needs to be decorated with a '{0}' attribute.";
		public static readonly string MissingExternalConfigurationAssemblyFile_Template ="The file '{0}' does not exist.";
		public static readonly string NotAnExternalConfigurationAssembly_Template ="The file '{0}' is not an assembly.";
		public static readonly string ParseableEventOperation ="Only subscriptions and unsubscriptions of events are supported.";
		public static readonly string UnorderedRangeBounds ="The start value of the range must not be greater than its end value.";
	}
}