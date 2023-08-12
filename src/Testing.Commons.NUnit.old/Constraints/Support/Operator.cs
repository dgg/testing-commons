using System;
using System.Linq.Expressions;

namespace Testing.Commons.NUnit.Constraints.Support
{
	// Based on:
	// "Miscellaneous Utility Library" Software Licence
	//
	// Version 1.0
	// Copyright (c) 2004-2008 Jon Skeet and Marc Gravell.
	// All rights reserved.

	/// <summary>
	/// Provides standard operators over a single type
	/// </summary>
	internal static class Operator<T>
	{
		static readonly Func<T, T, bool> _equal, _notEqual, _greaterThan, _lessThan, _greaterThanOrEqual, _lessThanOrEqual;
		static Operator()
		{
			_greaterThan = ExpressionBuilder.Binary<T, T, bool>(Expression.GreaterThan);
			_greaterThanOrEqual = ExpressionBuilder.Binary<T, T, bool>(Expression.GreaterThanOrEqual);
			_lessThan = ExpressionBuilder.Binary<T, T, bool>(Expression.LessThan);
			_lessThanOrEqual = ExpressionBuilder.Binary<T, T, bool>(Expression.LessThanOrEqual);
			_equal = ExpressionBuilder.Binary<T, T, bool>(Expression.Equal);
			_notEqual = ExpressionBuilder.Binary<T, T, bool>(Expression.NotEqual);
		}

		/// <summary>
		/// Returns a delegate to evaluate binary equality (==) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, T, bool> Equal { get { return _equal; } }
		/// <summary>
		/// Returns a delegate to evaluate binary inequality (!=) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, T, bool> NotEqual { get { return _notEqual; } }
		/// <summary>
		/// Returns a delegate to evaluate binary greater-then (&gt;) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, T, bool> GreaterThan { get { return _greaterThan; } }
		/// <summary>
		/// Returns a delegate to evaluate binary less-than (&lt;) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, T, bool> LessThan { get { return _lessThan; } }
		/// <summary>
		/// Returns a delegate to evaluate binary greater-than-or-equal (&gt;=) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, T, bool> GreaterThanOrEqual { get { return _greaterThanOrEqual; } }
		/// <summary>
		/// Returns a delegate to evaluate binary less-than-or-equal (&lt;=) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, T, bool> LessThanOrEqual { get { return _lessThanOrEqual; } }
	}

	/// <summary>
	/// Provides standard operators that operate over operands of different types.
	/// </summary>
	public static class Operator<T, U>
	{
		static readonly Func<T, U, bool> _equal, _notEqual, _greaterThan, _lessThan, _greaterThanOrEqual, _lessThanOrEqual;
		static Operator()
		{
			_greaterThan = ExpressionBuilder.Binary<T, U, bool>(Expression.GreaterThan);
			_greaterThanOrEqual = ExpressionBuilder.Binary<T, U, bool>(Expression.GreaterThanOrEqual);
			_lessThan = ExpressionBuilder.Binary<T, U, bool>(Expression.LessThan);
			_lessThanOrEqual = ExpressionBuilder.Binary<T, U, bool>(Expression.LessThanOrEqual);
			_equal = ExpressionBuilder.Binary<T, U, bool>(Expression.Equal);
			_notEqual = ExpressionBuilder.Binary<T, U, bool>(Expression.NotEqual);
		}

		/// <summary>
		/// Returns a delegate to evaluate binary equality (==) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, U, bool> Equal { get { return _equal; } }
		/// <summary>
		/// Returns a delegate to evaluate binary inequality (!=) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, U, bool> NotEqual { get { return _notEqual; } }
		/// <summary>
		/// Returns a delegate to evaluate binary greater-then (&gt;) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, U, bool> GreaterThan { get { return _greaterThan; } }
		/// <summary>
		/// Returns a delegate to evaluate binary less-than (&lt;) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, U, bool> LessThan { get { return _lessThan; } }
		/// <summary>
		/// Returns a delegate to evaluate binary greater-than-or-equal (&gt;=) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, U, bool> GreaterThanOrEqual { get { return _greaterThanOrEqual; } }
		/// <summary>
		/// Returns a delegate to evaluate binary less-than-or-equal (&lt;=) for the given type; this delegate will throw
		/// an InvalidOperationException if the type T does not provide this operator, or for
		/// Nullable&lt;TInner&gt; if TInner does not provide this operator.
		/// </summary>
		public static Func<T, U, bool> LessThanOrEqual { get { return _lessThanOrEqual; } }
	}

	/// <summary>
	/// General purpose Expression utilities
	/// </summary>
	internal static class ExpressionBuilder
	{
		/// <summary>
		/// Create a function delegate representing a binary operation
		/// </summary>
		/// <typeparam name="T">The first parameter type</typeparam>
		/// <typeparam name="U">The second parameter type</typeparam>
		/// <typeparam name="TResult">The return type</typeparam>
		/// <param name="body">Body factory</param>
		/// <returns>Compiled function delegate</returns>
		public static Func<T, U, TResult> Binary<T, U, TResult>(Func<Expression, Expression, BinaryExpression> body)
		{
			ParameterExpression lhs = Expression.Parameter(typeof(T), "lhs");
			ParameterExpression rhs = Expression.Parameter(typeof(U), "rhs");
			try
			{
				return Expression.Lambda<Func<T, U, TResult>>(body(lhs, rhs), lhs, rhs).Compile();
			}
			catch (Exception ex)
			{
				string msg = ex.Message; // avoid capture of ex itself
				return delegate { throw new InvalidOperationException(msg); };
			}
		}
	}
}
