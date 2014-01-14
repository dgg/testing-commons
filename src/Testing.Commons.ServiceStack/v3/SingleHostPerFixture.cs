using NUnit.Framework;

namespace Testing.Commons.ServiceStack.v3
{
	public abstract class SingleHostPerFixture : TesterBase
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