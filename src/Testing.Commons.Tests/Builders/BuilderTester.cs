using System;
using System.Collections;
using NUnit.Framework;
using Testing.Commons.Builders;
using Testing.Commons.Tests.Builders.Support;

namespace Testing.Commons.Tests.Builders
{
	[TestFixture]
	public class BuilderTester
	{
		[Test, Category("Example")]
		public void Of_WithNamedMethods_Style()
		{
			Exception exception = Builder.Of.Exceptions()
				.WithMessage("message")
				.WithInner(new ArrayTypeMismatchException())
				.WithData("something", "happened")
				.WithData(
					new DictionaryEntry(1, DateTime.UtcNow),
					new DictionaryEntry(2, 42m))
				.Build();
		}

		[Test, Category("Example")]
		public void For_NamedArgumentsStyle()
		{
			ArgumentException exception = Builder.For.ArgumentExceptions()
				.With(message: "message")
				.With(inner: new ArrayTypeMismatchException())
				.WithArgument("param")
				.Build();
		}

		[Test, Category("Example")]
		public void For_ProgressiveInterfaceStyle()
		{
			Product product = Builder.For.Products()
				.Named("testing.commons")
				.ManufacturedBy("dgg")
				.Priced(0m)
				.Build();
		}
	}
}