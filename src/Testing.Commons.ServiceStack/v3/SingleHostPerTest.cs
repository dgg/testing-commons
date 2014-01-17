using NUnit.Framework;

namespace Testing.Commons.ServiceStack.v3
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