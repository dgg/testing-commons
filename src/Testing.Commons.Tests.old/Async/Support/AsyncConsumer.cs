namespace Testing.Commons.Tests.Async.Support
{
	internal class AsyncConsumer
	{
		private readonly IProviderStuff _provider;

		public AsyncConsumer(IProviderStuff provider)
		{
			_provider = provider;
		}

		public string EchoStuff()
		{
			var futureStuff = _provider.LongRunningStuff();

			var stuff = futureStuff.Result;
			return stuff + stuff;
		}
	}
}