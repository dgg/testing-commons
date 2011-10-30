namespace Testing.Commons.NUnit.Tests.Constraints.Subjects
{
	// missing attribute
	public class NonSerializable
	{
		// missing default constructor

		public NonSerializable(string s)
		{
			S = s;
		}

		public string S { get; private set; }
	}
}