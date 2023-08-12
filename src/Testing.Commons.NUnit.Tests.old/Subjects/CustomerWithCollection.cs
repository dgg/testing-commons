using System.Collections.Generic;

namespace Testing.Commons.NUnit.Tests.Subjects
{
	class CustomerWithCollection
	{
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public IEnumerable<Address> Addresses { get; set; }
	}
}