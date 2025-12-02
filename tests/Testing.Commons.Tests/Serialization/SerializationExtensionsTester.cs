using System.Text;
using NUnit.Framework;
using Testing.Commons.Serialization;

namespace Testing.Commons.Tests.Serialization;

[TestFixture]
public class SerializationExtensionsTester
{
	[Test]
	public void AsStream_GivenAString_CanBeReadBack()
	{
		string subject = "a string";

		// act
		using var stream = subject.AsStream();

		// assert
		using var reader = new StreamReader(stream, Encoding.UTF8);
		Assert.That(reader.ReadToEnd(), Is.EqualTo(subject));
	}

	[Test]
	public void ReadString_CanBeReadBack()
	{
		var subject = "a string";

		using var stream = new MemoryStream();
		using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
		{
			writer.Write(subject);
		}

		// act & assert
		Assert.That(stream.ReadString(), Is.EqualTo(subject));
	}

	[Test]
	public void ReadString_StreamIsAtTheEnd_CanBeReadBack()
	{
		var subject = "a string";

		using var stream = new MemoryStream();
		using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
		{
			writer.Write(subject);
			writer.Flush();
		}

		Assert.That(stream.ReadString(), Is.EqualTo(subject));
	}

	[Test]
	public void ReadString_EmptyStream_Empty()
	{
		using var stream = new MemoryStream();
		Assert.That(stream.ReadString(), Is.Empty);
	}
}
