using NUnit.Framework;

namespace Testing.Commons.Service_Stack.v3
{
	public abstract class SingleHostPerFixture : HostTesterBase
	{
		[TestFixtureSetUp]
		public void SetUp()
		{
			StartHost();
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			ShutdownHost();
		}
	}
}