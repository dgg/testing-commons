using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using Testing.Commons.Globalization;

namespace Testing.Commons.Tests.Globalization
{
	[TestFixture]
	public class CultureReseterTester
	{
		[Test]
		public void Reset_NoChange_ResetToPreviousValues()
		{
			CultureInfo culture = Culture.GetFromThread();
			CultureInfo uICulture = Culture.GetUIFromThread();

			using (new CultureReseter())
			{
				// do nothing
			}
			Assert.That(Culture.GetFromThread(), Is.EqualTo(culture));
			Assert.That(Culture.GetUIFromThread(), Is.EqualTo(uICulture));
		}

		[Test]
		public void Reset_CultureChange_ResetToPreviousValues()
		{
			CultureInfo culture = Culture.GetFromThread();
			CultureInfo uICulture = Culture.GetUIFromThread();

			using (new CultureReseter())
			{
				// valid test point if code does not run in Maldives
				Culture.SetOnThread(Culture.Get("dv-MV"));
				Assert.That(Culture.GetFromThread(), Is.Not.EqualTo(culture));
			}
			Assert.That(Culture.GetFromThread(), Is.EqualTo(culture));
			Assert.That(Culture.GetUIFromThread(), Is.EqualTo(uICulture));
		}

		[Test]
		public void Reset_CultureUiChange_ResetToPreviousValues()
		{
			CultureInfo culture = Culture.GetFromThread();
			CultureInfo uICulture = Culture.GetUIFromThread();

			using (new CultureReseter())
			{
				// valid test point if code does not run in Maldives
				Culture.SetUIOnThread(Culture.Get("dv"));
				Assert.That(Culture.GetUIFromThread(), Is.Not.EqualTo(uICulture));
			}
			Assert.That(Culture.GetFromThread(), Is.EqualTo(culture));
			Assert.That(Culture.GetUIFromThread(), Is.EqualTo(uICulture));
		}

		[Test]
		public void Reset_BothChange_ResetToPreviousValues()
		{
			CultureInfo culture = Culture.GetFromThread();
			CultureInfo uICulture = Culture.GetUIFromThread();

			using (new CultureReseter())
			{
				// valid test point if code does not run in Maldives
				Culture.SetOnThread(Culture.Get("dv-MV"), Culture.Get("dv"));

				Assert.That(Culture.GetFromThread(), Is.Not.EqualTo(culture));
				Assert.That(Culture.GetUIFromThread(), Is.Not.EqualTo(uICulture));
			}
			Assert.That(Culture.GetFromThread(), Is.EqualTo(culture));
			Assert.That(Culture.GetUIFromThread(), Is.EqualTo(uICulture));
		}

		[Test]
		public void Set_BothChange_ResetToPreviousValues()
		{
			CultureInfo culture = Culture.GetFromThread();
			CultureInfo uICulture = Culture.GetUIFromThread();

			// valid test point if code does not run in Maldives
			using (CultureReseter.Set(Culture.Get("dv-MV"), Culture.Get("dv")))
			{
				Assert.That(Culture.GetFromThread(), Is.Not.EqualTo(culture));
				Assert.That(Culture.GetUIFromThread(), Is.Not.EqualTo(uICulture));
			}
			Assert.That(Culture.GetFromThread(), Is.EqualTo(culture));
			Assert.That(Culture.GetUIFromThread(), Is.EqualTo(uICulture));
		}

		[Test]
		public void Set_Both_CultureAndUiSetToSame()
		{
			CultureInfo ci = Culture.Get("dv-MV");
			using (CultureReseter.Set(ci))
			{
				Assert.That(CultureInfo.CurrentCulture, Is.EqualTo(ci));
				Assert.That(CultureInfo.CurrentUICulture, Is.EqualTo(ci));
			}
		}

		[Test]
		public void Set_NeutralNonNeutralCombinations_NoException()
		{
			CultureInfo neutral = Culture.Get("en"), nonNeutral = Culture.Get("en-GB");
			Assert.That(() => CultureReseter.Set(nonNeutral, nonNeutral), Throws.Nothing);
			Assert.That(() => CultureReseter.Set("en-GB", "es-ES"), Throws.Nothing);
			Assert.That(() => CultureReseter.Set(nonNeutral, neutral), Throws.Nothing);
			Assert.That(() => CultureReseter.Set("en-US", "es"), Throws.Nothing);

			Assert.That(() => CultureReseter.Set(neutral, neutral), Throws.Nothing);
			Assert.That(() => CultureReseter.Set(neutral), Throws.Nothing);
			Assert.That(() => CultureReseter.Set("es", "es"), Throws.Nothing);
			Assert.That(() => CultureReseter.Set("es"), Throws.Nothing);

			Assert.That(()=> CultureReseter.Set(CultureInfo.InvariantCulture), Throws.Nothing);
			Assert.That(()=> CultureReseter.Set(CultureInfo.InvariantCulture, CultureInfo.InvariantCulture), Throws.Nothing);
			
		}

		[TestCaseSource(nameof(cultureCombinations))]
		public void Set_BothCultures_CanBeNeutralOrNot(CultureInfo culture, CultureInfo uiCulture)
		{
			Assert.That(() => CultureReseter.Set(culture, uiCulture), Throws.Nothing);
		}

		private static IEnumerable<TestCaseData> cultureCombinations
		{
			get
			{
				CultureInfo neutral = Culture.Get("en"), nonNeutral = Culture.Get("en-GB");
				yield return new TestCaseData(nonNeutral, nonNeutral);
				yield return new TestCaseData(nonNeutral, neutral);
				yield return new TestCaseData(neutral, neutral).SetDescription("no longer throws");
			}
		}

		[TestCaseSource(nameof(validCultureNameCombinations))]
		public void Set_BothCultureNAmes_CultureMustBeNeutralAndUiCultureCanBeNeutralOrNot(string cultureName, string uiCultureName)
		{
			Assert.That(() => CultureReseter.Set(cultureName, uiCultureName), Throws.Nothing);
		}

		private static IEnumerable<TestCaseData> validCultureNameCombinations
		{
			get
			{
				string neutral = "en", nonNeutral = "en-GB";
				yield return new TestCaseData(nonNeutral, nonNeutral);
				yield return new TestCaseData(nonNeutral, neutral);
			}
		}
	}
}
