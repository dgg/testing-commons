namespace Testing.Commons.NUnit.Tests.Subjects
{
	public class NamedSubject
	{
		private readonly string _name;

		public NamedSubject(string name)
		{
			_name = name;
		}

		public override string ToString()
		{
			return _name;
		}
	}
}