using System.Threading.Tasks;

namespace Testing.Commons.Tests.Async.Support
{
	public interface IProviderStuff
	{
		Task<string> LongRunningStuff();
	}
}