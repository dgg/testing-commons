using NUnit.Framework;

namespace Testing.Commons.Tests
{
	[TestFixture]
	public class MustTester
	{
		[Test]
		public void Must_Be_CanBeExtended_ByAddingExtensionsMethodsToEntryPoint()
		{
			Assert.That(() => Must.Be.Extended(), Throws.Nothing);
			Assert.That(() => Must.Be.Extended(3), Throws.Nothing);
		}

		[Test]
		public void Must_Have_CanBeExtended_ByAddingExtensionsMethodsToEntryPoint()
		{
			Assert.That(() => Must.Have.Extended(), Throws.Nothing);
			Assert.That(() => Must.Have.Extended(3), Throws.Nothing);
		}

		[Test]
		public void Must_Satisfy_CanBeExtended_ByAddingExtensionsMethodsToEntryPoint()
		{
			Assert.That(() => Must.Satisfy.Extended(), Throws.Nothing);
			Assert.That(() => Must.Satisfy.Extended(3), Throws.Nothing);
		}

		[Test]
		public void Must_Match_CanBeExtended_ByAddingExtensionsMethodsToEntryPoint()
		{
			Assert.That(() => Must.Match.Extended(), Throws.Nothing);
			Assert.That(() => Must.Match.Extended(3), Throws.Nothing);
		}

		[Test]
		public void Must_NotBe_CanBeExtended_ByAddingExtensionsMethodsToEntryPoint()
		{
			Assert.That(() => Must.Not.Be.Extended(), Throws.Nothing);
			Assert.That(() => Must.Not.Be.Extended(3), Throws.Nothing);
		}

		[Test]
		public void Must_NotHave_CanBeExtended_ByAddingExtensionsMethodsToEntryPoint()
		{
			Assert.That(() => Must.Not.Have.Extended(), Throws.Nothing);
			Assert.That(() => Must.Not.Have.Extended(3), Throws.Nothing);
		}

		[Test]
		public void Must_NotSatisfy_CanBeExtended_ByAddingExtensionsMethodsToEntryPoint()
		{
			Assert.That(() => Must.Not.Satisfy.Extended(), Throws.Nothing);
			Assert.That(() => Must.Not.Satisfy.Extended(3), Throws.Nothing);
		}

		[Test]
		public void Must_NotMatch_CanBeExtended_ByAddingExtensionsMethodsToEntryPoint()
		{
			Assert.That(() => Must.Not.Match.Extended(), Throws.Nothing);
			Assert.That(() => Must.Not.Match.Extended(3), Throws.Nothing);
		}
	}

	internal static class MustExtensions
	{
		public static void Extended(this Must.BeEntryPoint entry) { }
		public static void Extended(this Must.BeEntryPoint entry, int argument) { }

		public static void Extended(this Must.HaveEntryPoint entry) { }
		public static void Extended(this Must.HaveEntryPoint entry, int argument) { }

		public static void Extended(this Must.SatisfyEntryPoint entry) { }
		public static void Extended(this Must.SatisfyEntryPoint entry, int argument) { }

		public static void Extended(this Must.MatchEntryPoint entry) { }
		public static void Extended(this Must.MatchEntryPoint entry, int argument) { }

		public static void Extended(this Must.NotBeEntryPoint entry) { }
		public static void Extended(this Must.NotBeEntryPoint entry, int argument) { }

		public static void Extended(this Must.NotHaveEntryPoint entry) { }
		public static void Extended(this Must.NotHaveEntryPoint entry, int argument) { }

		public static void Extended(this Must.NotSatisfyEntryPoint entry) { }
		public static void Extended(this Must.NotSatisfyEntryPoint entry, int argument) { }

		public static void Extended(this Must.NotMatchEntryPoint entry) { }
		public static void Extended(this Must.NotMatchEntryPoint entry, int argument) { }
	}
}
