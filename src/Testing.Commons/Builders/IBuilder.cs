namespace Testing.Commons.Builders
{
	public interface IBuilder<out T>
	{
		T Build();
	}

	public static class Builder
	{
		public class OfEntryPoint
		{
			internal OfEntryPoint() { }
		}

		public class ForEntryPoint
		{
			internal ForEntryPoint() { }
		}

		public static readonly OfEntryPoint _ofEntryPoint = new OfEntryPoint();
		public static OfEntryPoint Of { get { return _ofEntryPoint; } }

		public static readonly ForEntryPoint _forEntryPoint = new ForEntryPoint();
		public static ForEntryPoint For { get { return _forEntryPoint; } }
	}
}