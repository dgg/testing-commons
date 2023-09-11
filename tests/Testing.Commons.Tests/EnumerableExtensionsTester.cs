namespace Testing.Commons.Tests
{
	[TestFixture]
	public class EnumerableExtensionsTester
	{
		private IEnumerable<char> enumerable()
		{
			yield return 'a';
			yield return 'b';
			yield return 'c';
		}

		[Test]
		public void Iterate_ReturnsEnumerableByRunningThoughIt()
		{
			Assert.That(enumerable(), Is.EqualTo(new[] { 'a', 'b', 'c' }));
		}

		[Test]
		public void ThrowingEnumerables_WhenNotIterated_DoNotThrow()
		{
			Assert.That(() => throwingEnumerable(), Throws.Nothing);
		}

		[Test]
		public void ThrowingEnumerables_WhenIterated_Throw()
		{
			Assert.That(() => throwingEnumerable().Iterate(), Throws.Exception.With.Message.EqualTo("only a"));
		}

		private IEnumerable<char> throwingEnumerable()
		{
			yield return 'a';
			throw new Exception("only a");
		}
	}
}
