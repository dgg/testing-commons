namespace Testing.Commons.Tests.Time.Support
{
	internal class Iz : NUnit.Framework.Iz
	{
		public static TimeComponentsConstraint TimeWith(int year, int month, int day,
			int hour = 0, int minute = 0, int second = 0, int milliseconds = 0,
			TimeSpan? offset = null) =>
			new (year, month, day, hour, minute, second, milliseconds, offset ?? TimeSpan.Zero);
	}
}
