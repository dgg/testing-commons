using System;

namespace Testing.Commons
{
	internal static class TypeExtensions
	{
		internal static object Default(this Type t)
		{
			if (!t.IsValueType) return null;
			return Activator.CreateInstance(t);
		}
	}
}
