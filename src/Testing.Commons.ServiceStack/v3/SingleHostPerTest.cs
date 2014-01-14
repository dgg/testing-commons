using NUnit.Framework;

namespace Testing.Commons.ServiceStack.v3
{
	public abstract class SingleHostPerTest : TesterBase
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