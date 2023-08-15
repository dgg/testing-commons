using System.Linq.Expressions;
using System.Reflection;
using Testing.Commons.NUnit.Resources;

namespace Testing.Commons.NUnit.Constraints
{
	internal static class Name
	{
		public static string Of<T>(Expression<Func<T, object>> property)
		{
			return propertyInfo(property).Name;
		}

		private static PropertyInfo propertyInfo<TObject>(Expression<Func<TObject, object>> property)
		{
			return (PropertyInfo)getMemberExpression(property).Member;
		}

		private static MemberExpression getMemberExpression<TObject>(Expression<Func<TObject, object>> property)
		{
			MemberExpression? memberExpression = null;
			if (property.Body.NodeType == ExpressionType.Convert)
			{
				var body = (UnaryExpression)property.Body;
				memberExpression = body.Operand as MemberExpression;
			}
			else if (property.Body.NodeType == ExpressionType.MemberAccess)
			{
				memberExpression = property.Body as MemberExpression;
			}
			if (memberExpression == null) throw new ArgumentException(Exceptions.NotMemberExpression, nameof(property));

			return memberExpression;
		}
	}
}
