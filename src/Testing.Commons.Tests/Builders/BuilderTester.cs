using System;
using System.Collections;
using System.Collections.Specialized;
using NUnit.Framework;
using Testing.Commons.Builders;

namespace Testing.Commons.Tests.Builders
{
	[TestFixture]
	public class BuilderTester
	{
		[Test, Category("Example")]
		public void Of_WithNamedMethods_Style()
		{
			Exception exception = Builder.Of.Exceptions()
				.WithMessage("message")
				.WithInner(new ArrayTypeMismatchException())
				.WithData("something", "happened")
				.WithData(
					new DictionaryEntry(1, DateTime.UtcNow),
					new DictionaryEntry(2, 42m))
				.Build();
		}

		[Test, Category("Example")]
		public void For_NamedArgumentsStyle()
		{
			ArgumentException exception = Builder.For.ArgumentExceptions()
				.With(message: "message")
				.With(inner: new ArrayTypeMismatchException())
				.WithArgument("param")
				.Build();
		}
	}

	internal static class BuilderExtensions
	{
		public static ExceptionBuilder Exceptions(this Builder.OfEntryPoint entry)
		{
			return new ExceptionBuilder();
		}

		public static ArgumentExceptionBuilder ArgumentExceptions(this Builder.ForEntryPoint entry)
		{
			return new ArgumentExceptionBuilder();
		}
	}

	internal class ExceptionBuilder : IBuilder<Exception>
	{
		private string _message = string.Empty;
		private Exception _inner;
		private readonly IDictionary _data = new ListDictionary();

		public ExceptionBuilder WithMessage(string message)
		{
			_message = message;
			return this;
		}

		public ExceptionBuilder WithInner(Exception inner)
		{
			_inner = inner;
			return this;
		}

		public ExceptionBuilder WithData(object key, object value)
		{
			_data.Add(key, value);
			return this;
		}

		public ExceptionBuilder WithData(params DictionaryEntry[] entries)
		{
			foreach (var entry in entries)
			{
				_data.Add(entry.Key, entry.Value);
			}
			return this;
		}

		public Exception Build()
		{
			var e = new Exception(_message, _inner);
			foreach (DictionaryEntry entry in _data)
			{
				e.Data.Add(entry.Key, entry.Value);
			}
			return e;
		}
	}

	internal class ArgumentExceptionBuilder : IBuilder<ArgumentException>
	{
		private string _message = string.Empty;
		private Exception _inner;
		private string _paramName;

		public ArgumentExceptionBuilder With(string message)
		{
			_message = message;
			return this;
		}

		public ArgumentExceptionBuilder With(Exception inner)
		{
			_inner = inner;
			return this;
		}

		public ArgumentExceptionBuilder WithArgument(string paramName)
		{
			_paramName = paramName;
			return this;
		}

		public ArgumentException Build()
		{
			var e = new ArgumentException(_message, _paramName, _inner);
			return e;
		}
	}
}