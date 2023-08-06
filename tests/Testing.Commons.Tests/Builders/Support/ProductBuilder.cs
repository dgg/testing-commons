using Testing.Commons.Builders;

namespace Testing.Commons.Tests.Builders.Support;

public interface IPreProductNameBuilder
{
	IPostProductNameBuilder Named(string name);
}

public interface IPostProductNameBuilder
{
	IPostProductManufacturerBuilder ManufacturedBy(string manufacturer);
}

public interface IPostProductManufacturerBuilder
{
	IBuilder<Product> Priced(decimal price);
}

public class ProductBuilder : IBuilder<Product>,
	IPreProductNameBuilder, IPostProductNameBuilder, IPostProductManufacturerBuilder
{
	private string? Manufacturer { get; set; }
	private string? Name { get; set; }
	private decimal Price { get; set; }

	public IPostProductNameBuilder Named(string name)
	{
		Name = name;
		return this;
	}

	public IPostProductManufacturerBuilder ManufacturedBy(string manufacturer)
	{
		Manufacturer = manufacturer;
		return this;
	}

	public IBuilder<Product> Priced(decimal price)
	{
		Price = price;
		return this;
	}

	public static implicit operator Product(ProductBuilder builder)
	{
		return builder.Build();
	}

	public Product Build()
	{
		return new Product(Price, Manufacturer ?? string.Empty, Name ?? string.Empty);
	}
}
