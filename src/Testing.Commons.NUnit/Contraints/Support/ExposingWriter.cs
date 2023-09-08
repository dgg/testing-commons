using System.Diagnostics.CodeAnalysis;
using ExpectedObjects;

namespace Testing.Commons.NUnit.Constraints.Support;

/// <summary>
/// Exposes the instance of <see cref="WritableEqualityResult"/> which output would be formatted.
/// </summary>
internal class ExposingWriter : IWriter
{
	private readonly IWriter _decoree;
	private readonly List<EqualityResult> _written;

	public ExposingWriter([NotNull] IWriter decoree)
	{
		_decoree = decoree;
		_written = new List<EqualityResult>();
	}

	internal WritableEqualityResult Exposed { get; private set; } = default!;

	public void Write(EqualityResult content)
	{
		if (!content.Status) _written.Add(content);
		_decoree.Write(content);
	}

	public string GetFormattedResults()
	{
		Exposed = _written.Where(isLeaf).Select(r => new WritableEqualityResult(r.Member, r.Expected, r.Actual)).First();
		return _decoree.GetFormattedResults();
	}

	private bool isLeaf(EqualityResult result)
	{
		return !_written.Any(x =>
			x.Member.StartsWith(result.Member + ".", StringComparison.Ordinal) ||
			x.Member.StartsWith(result.Member + "[", StringComparison.Ordinal)
		);
	}
}
