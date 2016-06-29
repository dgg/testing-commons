using NUnit.Framework;

namespace Testing.Commons.Service_Stack.v3
{
	public abstract class SingleHostPerFixture : HostTesterBase
	{
		[OneTimeSetUp]
		public void SetUp()
		{
			StartHost();
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			ShutdownHost();
		}
	}
}