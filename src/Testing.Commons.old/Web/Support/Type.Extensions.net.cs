using System;

namespace Testing.Commons.Web.Support
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
