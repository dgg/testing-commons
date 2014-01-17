using System.Reflection;

namespace Testing.Commons.Service_Stack.Tests.Example.Infrastructure.Shared
{
	public static class HostInfo
	{
		public static readonly string ServiceName = "Testing.Commons.Sample.Barebones";
		public static readonly Assembly[] AssembliesWithServices = new[] {typeof (HostInfo).Assembly};
	}
}