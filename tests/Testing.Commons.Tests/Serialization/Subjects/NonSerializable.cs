namespace Testing.Commons.Tests.Serialization.Subjects;
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
