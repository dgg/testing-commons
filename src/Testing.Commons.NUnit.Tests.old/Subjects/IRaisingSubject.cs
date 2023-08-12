using System.ComponentModel;

namespace Testing.Commons.NUnit.Tests.Subjects
{
	public interface IRaisingSubject : INotifyPropertyChanged, INotifyPropertyChanging
	{
		int I { get; set; }
		string S { get; set; }
	}
}
