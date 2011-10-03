using NUnit.Framework;

namespace Testing.Commons.NUnit
{
	/// <summary>
	/// Eases the "arrange" part of AAA tests
	/// </summary>
	/// <remarks>Already annotated with the <see cref="TestFixtureAttribute"/> which means that inheritors do not need to specify it again.</remarks>
	/// <typeparam name="TSubject">Type of the subject (aka. SUT) of the test.</typeparam>
	[TestFixture]
	public abstract class ArrangingTest<TSubject>
	{
		/// <summary>
		/// Invokes the "arrange" part of a AAA test.
		/// </summary>
		/// <remarks>Annotated with the <see cref="SetUpAttribute"/> which means that it will be run before each test.</remarks>
		[SetUp]
		public void Arrange()
		{
			Subject = initSubject();
		}

		/// <summary>
		/// The System Under Test (SUT)
		/// </summary>
		protected TSubject Subject { get; set; }

		/// <summary>
		/// Implement this method to initialize your System Under Test (SUT).
		/// </summary>
		/// <returns>An instance of the SUT.</returns>
		protected abstract TSubject initSubject();
	}
}
