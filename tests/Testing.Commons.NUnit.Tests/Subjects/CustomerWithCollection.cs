namespace Testing.Commons.NUnit.Tests.Subjects;

class CustomerWithCollection
{
	public string Name { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public IEnumerable<Address> Addresses { get; set; } = Array.Empty<Address>();
}
