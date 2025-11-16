using NUnit.Framework.Constraints;

namespace Testing.Commons.Tests;

[TestFixture]
public class GuidBuilderTester
{
	#region from char

	[TestCase('1', "11111111-1111-1111-1111-111111111111")]
	[TestCase('2', "22222222-2222-2222-2222-222222222222")]
	[TestCase('3', "33333333-3333-3333-3333-333333333333")]
	[TestCase('4', "44444444-4444-4444-4444-444444444444")]
	[TestCase('5', "55555555-5555-5555-5555-555555555555")]
	[TestCase('6', "66666666-6666-6666-6666-666666666666")]
	[TestCase('7', "77777777-7777-7777-7777-777777777777")]
	[TestCase('8', "88888888-8888-8888-8888-888888888888")]
	[TestCase('9', "99999999-9999-9999-9999-999999999999")]
	[TestCase('0', "00000000-0000-0000-0000-000000000000")]
	public void Build_CharNumber_GuidCreated(char number, string guid)
	{
		Assert.That(GuidBuilder.Build(number), Is.EqualTo(new Guid(guid)));
	}

	[TestCase('a', "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
	[TestCase('A', "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
	[TestCase('b', "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")]
	[TestCase('B', "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")]
	[TestCase('c', "cccccccc-cccc-cccc-cccc-cccccccccccc")]
	[TestCase('C', "cccccccc-cccc-cccc-cccc-cccccccccccc")]
	[TestCase('d', "dddddddd-dddd-dddd-dddd-dddddddddddd")]
	[TestCase('D', "dddddddd-dddd-dddd-dddd-dddddddddddd")]
	[TestCase('e', "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee")]
	[TestCase('E', "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee")]
	[TestCase('f', "ffffffff-ffff-ffff-ffff-ffffffffffff")]
	[TestCase('F', "ffffffff-ffff-ffff-ffff-ffffffffffff")]
	public void Build_CharLetter_GuidCreatedRegardlessOfCase(char lowerCaseFigure, string guid)
	{
		Assert.That(GuidBuilder.Build(lowerCaseFigure), Is.EqualTo(new Guid(guid)));
		Assert.That(GuidBuilder.Build(char.ToUpperInvariant(lowerCaseFigure)), Is.EqualTo(new Guid(guid)));
	}

	[Test]
	public void Build_NotACharFigure_Exception()
	{
		Assert.That(() => GuidBuilder.Build('q'), throwsNotHexException('q', "q"));
	}

	private static Constraint throwsNotHexException(char notAFigure, string notAFigureRepresentation)
	{
		string figureDomain = "[0..9] U [a..f]";

		return Throws.InstanceOf<NotHexadecimalException>().With
			.Property("ActualValue").EqualTo(notAFigure)
			.And.Message.Contain(notAFigureRepresentation)
			.And.Message.Contain(figureDomain);
	}

	#endregion

	#region from byte

	[TestCase(1, "11111111-1111-1111-1111-111111111111")]
	[TestCase(2, "22222222-2222-2222-2222-222222222222")]
	[TestCase(3, "33333333-3333-3333-3333-333333333333")]
	[TestCase(4, "44444444-4444-4444-4444-444444444444")]
	[TestCase(5, "55555555-5555-5555-5555-555555555555")]
	[TestCase(6, "66666666-6666-6666-6666-666666666666")]
	[TestCase(7, "77777777-7777-7777-7777-777777777777")]
	[TestCase(8, "88888888-8888-8888-8888-888888888888")]
	[TestCase(9, "99999999-9999-9999-9999-999999999999")]
	[TestCase(0, "00000000-0000-0000-0000-000000000000")]
	public void Build_Number_GuidCreated(int number, string guid)
	{
		// weird casting as NUnit cannot direcly convert the int literal into a byte
		Assert.That(GuidBuilder.Build((byte)number), Is.EqualTo(new Guid(guid)));
	}

	[TestCase(0xa, "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
	[TestCase(10, "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
	[TestCase(0xb, "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")]
	[TestCase(11, "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")]
	[TestCase(0xc, "cccccccc-cccc-cccc-cccc-cccccccccccc")]
	[TestCase(12, "cccccccc-cccc-cccc-cccc-cccccccccccc")]
	[TestCase(0xd, "dddddddd-dddd-dddd-dddd-dddddddddddd")]
	[TestCase(13, "dddddddd-dddd-dddd-dddd-dddddddddddd")]
	[TestCase(0xe, "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee")]
	[TestCase(14, "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee")]
	[TestCase(0xf, "ffffffff-ffff-ffff-ffff-ffffffffffff")]
	[TestCase(15, "ffffffff-ffff-ffff-ffff-ffffffffffff")]
	public void Build_Letter_GuidCreated(int lowerCaseFigure, string guid)
	{
		// weird casting as NUnit cannot direcly convert the int literal into a byte
		Assert.That(GuidBuilder.Build((byte)lowerCaseFigure), Is.EqualTo(new Guid(guid)));
	}

	[Test]
	public void Build_OutOfBoundsANumber_Exception()
	{
		Assert.That(() => GuidBuilder.Build(16), throwsNotHexException(16, "16"));
	}

	private static Constraint throwsNotHexException(byte notAFigure, string notAFigureRepresentation)
	{
		string figureDomain = "[0..15]";

		return Throws.InstanceOf<NotHexadecimalException>().With
			.Property("ActualValue").EqualTo(notAFigure)
			.And.Message.Contain(notAFigureRepresentation)
			.And.Message.Contain(figureDomain);
	}

	#endregion

	#region documentation

	[Test]
	public void Documentation_Wiki_ForCharacters()
	{
		Assert.That(GuidBuilder.Build('7'), Is.EqualTo(new Guid("77777777-7777-7777-7777-777777777777")));
		Assert.That(GuidBuilder.Build('a'), Is.EqualTo(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")));
		Assert.That(GuidBuilder.Build('B'), Is.EqualTo(new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")));
	}

	[Test]
	public void Documentation_Wiki_ForNumbers()
	{
		Assert.That(GuidBuilder.Build(1), Is.EqualTo(new Guid("11111111-1111-1111-1111-111111111111")));
		Assert.That(GuidBuilder.Build(11), Is.EqualTo(new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")));
		Assert.That(GuidBuilder.Build(0x3), Is.EqualTo(new Guid("33333333-3333-3333-3333-333333333333")));
		Assert.That(GuidBuilder.Build(0xd), Is.EqualTo(new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd")));
	}

	#endregion
}
