using NUnit.Framework;

namespace Testing.Commons.Service_Stack.v3
{
	public abstract class SingleHostPerTest : HostTesterBase
	{
		[SetUp]
		public void SetUp()
		{
			StartHost();
		}

		[TearDown]
		public void TearDown()
		{
			ShutdownHost();
		}
	}
}