namespace Testing.Commons.Tests.Builders.Support;
public class Product
{
	public decimal Price { get; private set; }
	public string Name { get; private set; }
	public string Manufacturer { get; private set; }

	public Product(decimal price, string manufacturer, string name)
	{
		Price = price;
		Manufacturer = manufacturer;
		Name = name;
	}
}
