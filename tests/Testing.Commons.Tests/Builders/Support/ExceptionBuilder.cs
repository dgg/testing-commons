using System.Collections;
using System.Collections.Specialized;
using Testing.Commons.Builders;

namespace Testing.Commons.Tests.Builders.Support;

internal class ExceptionBuilder : IBuilder<Exception>
{
	private string _message = string.Empty;
	private Exception? _inner;
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
