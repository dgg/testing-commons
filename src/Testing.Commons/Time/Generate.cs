using System.Globalization;
using Testing.Commons.Resources;

namespace Testing.Commons.Time;

/// <summary>
/// Allows random generation of constrained dates.
/// </summary>
public static class Generate
{
	/// <summary>
	/// Infrastructure class that allows setting the upper bound of the dates generated.
	/// </summary>
	public class DateGeneratorBuilder
	{
		private readonly DateTime _from;

		internal DateGeneratorBuilder(DateTime from)
		{
			_from = @from;
		}

		/// <summary>
		/// Sets the upper bound of the dates generated.
		/// </summary>
		/// <param name="to">Maximum date to be generated.</param>
		/// <returns></returns>
		public DateGenerator And(DateTime to)
		{
			return new DateGenerator(_from, to);
		}

		/// <summary>
		/// Infrastructure class that allows access to the generation methods.
		/// </summary>
#pragma warning disable CA5394
		public class DateGenerator
		{
			internal DateGenerator(DateTime from, DateTime to)
			{
				assertBounds(from, to);

				From = from;
				To = to;
			}

			private static void assertBounds(DateTime from, DateTime to)
			{
				if (to <= from) throw new ArgumentOutOfRangeException(nameof(to), to, string.Format(CultureInfo.InvariantCulture, Exceptions.InvertedRange_Template, from));
			}

			private DateTime From { get; set; }
			private DateTime To { get; set; }

			/// <summary>
			/// Generates a single random date between the defined lower and upper bounds.
			/// </summary>
			/// <returns>A date between the defined ranges.</returns>
#pragma warning disable CA1720
			public DateTime Single()
			{
				Random rnd = new();
				int dayRange = (To - From).Days;

				return From.AddDays(rnd.Next(dayRange));
			}
#pragma warning restore CA1720

			/// <summary>
			/// Generated an infinite succession of random dates between the defined lower and upper bounds.
			/// </summary>
			/// <returns>An infinite series of dates between the defined ranges.</returns>
			public IEnumerable<DateTime> Stream()
			{
				Random rnd = new();
				int dayRange = (To - From).Days;
				while (true) yield return From.AddDays(rnd.Next(dayRange));
			}

			/// <summary>
			/// Generated a finite succession of random dates between the defined lower and upper bounds.
			/// </summary>
			/// <param name="count">The number of dates to be generated.</param>
			/// <returns>A finite series of dates between the defined ranges.</returns>
			public IEnumerable<DateTime> Stream(int count)
			{
				return Stream().Take(count);
			}
		}
#pragma warning restore CA5394
	}

	/// <summary>
	/// Allows setting the lower bound of the dates generated.
	/// </summary>
	/// <param name="from">Minimum date to be generated.</param>
	/// <returns></returns>
	public static DateGeneratorBuilder Between(DateTime from)
	{
		return new DateGeneratorBuilder(from);
	}
}
