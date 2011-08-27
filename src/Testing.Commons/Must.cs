namespace Testing.Commons
{
	/// <summary>
	/// Allows easy extensibility of assertions for testing frameworks.
	/// </summary>
	/// <example>In NUnit:
	/// <code>Assert.That(2, Must.Be.Even())</code>
	/// where <c>.Even()</c> returns a custom assertion.</example>
	public static class Must
	{
		/// <summary>
		/// Allows meaningful extensions for elements of a group, for example, collections.
		/// </summary>
		public class ContainEntryPoint
		{
			internal ContainEntryPoint() { }
		}

		/// <summary>
		/// Allows meaningful negative extensions for elements of a group, for example, collections.
		/// </summary>
		public class NotContainEntryPoint
		{
			internal NotContainEntryPoint() { }
		}

		private static readonly ContainEntryPoint _containEntryPoint = new ContainEntryPoint();
		/// <summary>
		/// Allows meaningful extensions for elements of a group, for example, collections.
		/// </summary>
		/// <example>In NUnit:
		/// <code>Assert.That(new[]{1, 2, 3}, Must.Contain.ASingleEvenNumber())</code>
		/// where <c>.ASingleEvenNumber()</c> returns a custom assertion.</example>
		public static ContainEntryPoint Contain { get { return _containEntryPoint; } }

		/// <summary>
		/// Allows meaningful extensions for elements of a group, for example, a property out of many.
		/// </summary>
		public class HaveEntryPoint
		{
			internal HaveEntryPoint() { }
		}

		/// <summary>
		/// Allows meaningful negative extensions for elements of a group, for example, a property out of many.
		/// </summary>
		public class NotHaveEntryPoint
		{
			internal NotHaveEntryPoint() { }
		}

		private static readonly HaveEntryPoint _haveEntryPoint = new HaveEntryPoint();
		/// <summary>
		/// Allows meaningful extensions for elements of a group, for example, a property out of many.
		/// </summary>
		/// <example>In NUnit:
		/// <code>Assert.That(aUri, Must.Have.Query())</code>
		/// where <c>.Query()</c> returns a custom assertion.</example>
		public static HaveEntryPoint Have { get { return _haveEntryPoint; } }

		/// <summary>
		/// Allows meaningful extensions for individual elements.
		/// </summary>
		public class BeEntryPoint
		{
			internal BeEntryPoint() { }
		}

		/// <summary>
		/// Allows meaningful negative extensions for individual elements.
		/// </summary>
		public class NotBeEntryPoint
		{
			internal NotBeEntryPoint() { }
		}

		private static readonly BeEntryPoint _beEntryPoint = new BeEntryPoint();
		/// <summary>
		/// Allows meaningful extensions for individual elements.
		/// </summary>
		/// <example>In NUnit:
		/// <code>Assert.That(2, Must.Be.Even())</code>
		/// where <c>.Even()</c> returns a custom assertion.</example>
		public static BeEntryPoint Be { get { return _beEntryPoint; } }

		/// <summary>
		/// Allows meaningful extensions for constraints of individual elements.
		/// </summary>
		public class SatisfyEntryPoint
		{
			internal SatisfyEntryPoint() { }
		}

		/// <summary>
		/// Allows meaningful negative extensions for constraints of individual elements.
		/// </summary>
		public class NotSatisfyEntryPoint
		{
			internal NotSatisfyEntryPoint() { }
		}

		private static readonly SatisfyEntryPoint _satisfyEntryPoint = new SatisfyEntryPoint();
		/// <summary>
		/// Allows meaningful extensions for constraints of individual elements.
		/// </summary>
		/// <example>In NUnit:
		/// <code>Assert.That(collection, Must.Satisfy.Uniqueness())</code>
		/// where <c>.Uniqueness()</c> returns a custom assertion.</example>
		public static SatisfyEntryPoint Satisfy { get { return _satisfyEntryPoint; } }

		/// <summary>
		/// Allows meaningful extensions for partial constraints of individual elements.
		/// </summary>
		public class MatchEntryPoint
		{
			internal MatchEntryPoint() { }
		}

		/// <summary>
		/// Allows meaningful negative extensions for partial constraints of individual elements.
		/// </summary>
		public class NotMatchEntryPoint
		{
			internal NotMatchEntryPoint() { }
		}

		private static readonly MatchEntryPoint _matchEntryPoint = new MatchEntryPoint();
		/// <summary>
		/// Allows meaningful extensions for partial constraints of individual elements.
		/// </summary>
		/// <example>In NUnit:
		/// <code>Assert.That("asd", Must.Match.String("as*"))</code>
		/// where <c>.String()</c> returns a custom assertion.</example>
		public static MatchEntryPoint Match { get { return _matchEntryPoint; } }

		/// <summary>
		/// Allows easy extensibility of negative assertions for testing frameworks.
		/// </summary>
		/// <example>In NUnit:
		/// <code>Assert.That(2, Must.Not.Be.Even())</code>
		/// where <c>.Even()</c> returns a custom assertion.</example>
		// ReSharper disable MemberHidesStaticFromOuterClass
		public static class Not
		{

			private static readonly NotContainEntryPoint _containEntryPoint = new NotContainEntryPoint();
			/// <summary>
			/// Allows meaningful negative extensions for elements of a group, for example, collections.
			/// </summary>
			/// <example>In NUnit:
			/// <code>Assert.That(new[]{1, 3, 5}, Must.Not.Contain.ASingleEvenNumber())</code>
			/// where <c>.ASingleEvenNumber()</c> returns a custom assertion.</example>
			public static NotContainEntryPoint Contain { get { return _containEntryPoint; } }

			private static readonly NotHaveEntryPoint _haveEntryPoint = new NotHaveEntryPoint();
			/// <summary>
			/// Allows meaningful negative extensions for elements of a group, for example, a property out of many.
			/// </summary>
			/// <example>In NUnit:
			/// <code>Assert.That(aUri, Must.Not.Have.Query())</code>
			/// where <c>.Query()</c> returns a custom assertion.</example>
			public static NotHaveEntryPoint Have { get { return _haveEntryPoint; } }

			private static readonly NotBeEntryPoint _beEntryPoint = new NotBeEntryPoint();
			/// <summary>
			/// Allows meaningful negative extensions for individual elements.
			/// </summary>
			/// <example>In NUnit:
			/// <code>Assert.That(3, Must.Not.Be.Even())</code>
			/// where <c>.Even()</c> returns a custom assertion.</example>
			public static NotBeEntryPoint Be { get { return _beEntryPoint; } }

			private static readonly NotSatisfyEntryPoint _satisfyEntryPoint = new NotSatisfyEntryPoint();
			/// <summary>
			/// Allows meaningful negative extensions for constraints of individual elements.
			/// </summary>
			/// <example>In NUnit:
			/// <code>Assert.That(collection, Must.Not.Satisfy.Uniqueness())</code>
			/// where <c>.Uniqueness()</c> returns a custom assertion.</example>
			public static NotSatisfyEntryPoint Satisfy { get { return _satisfyEntryPoint; } }

			private static readonly NotMatchEntryPoint _matchEntryPoint = new NotMatchEntryPoint();
			/// <summary>
			/// Allows meaningful negative extensions for partial constraints of individual elements.
			/// </summary>
			/// <example>In NUnit:
			/// <code>Assert.That("asd", Must.Not.Match.String("ds*"))</code>
			/// where <c>.String()</c> returns a custom assertion.</example>
			public static NotMatchEntryPoint Match { get { return _matchEntryPoint; } }
		}
		// ReSharper restore MemberHidesStaticFromOuterClass
	}
}
