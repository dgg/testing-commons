﻿namespace Testing.Commons.Builders
{
	/// <summary>
	/// Allows easy extensibility of complex test objects.
	/// </summary>
	/// <example><code>Builder.For.User()... .Build()</code>
	/// where <c>.Exceotion()</c> returns a custom object builder.</example>
	public static class Builder
	{
		/// <summary>
		/// Allows meaningful extensions for complex object creation.
		/// </summary>
		public class OfEntryPoint
		{
			internal OfEntryPoint() { }
		}

		/// <summary>
		/// Allows meaningful extensions for complex object creation.
		/// </summary>
		public class ForEntryPoint
		{
			internal ForEntryPoint() { }
		}

		private static readonly OfEntryPoint _ofEntryPoint = new OfEntryPoint();
		/// <summary>
		/// Allows meaningful extensions for complex object creation.
		/// </summary>
		/// <example><code>Builder.Of.Users()... .Build()</code>
		/// where <c>.Exceotion()</c> returns a custom builder of user instances.</example>
		public static OfEntryPoint Of { get { return _ofEntryPoint; } }

		private static readonly ForEntryPoint _forEntryPoint = new ForEntryPoint();
		/// <summary>
		/// Allows meaningful extensions for complex object creation.
		/// </summary>
		/// <example><code>Builder.For.User()... .Build()</code>
		/// where <c>.Exception()</c> returns a custom builder of user instances.</example>
		public static ForEntryPoint For { get { return _forEntryPoint; } }
	}
}