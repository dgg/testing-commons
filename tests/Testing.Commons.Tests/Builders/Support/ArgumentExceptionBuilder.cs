using Testing.Commons.Builders;

namespace Testing.Commons.Tests.Builders.Support;

internal class ArgumentExceptionBuilder : IBuilder<ArgumentException>
{
	private string _message = string.Empty;
	private Exception? _inner;
	private string? _paramName;

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
