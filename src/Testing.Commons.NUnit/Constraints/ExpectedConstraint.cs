using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public abstract class ExpectedConstraint : Constraint
	{
		protected readonly ExpectedObject _expected;
		private readonly ExposingWriter _writer;
		protected WritableEqualityResult _exposed;

		protected ExpectedConstraint(object expected)
		{
			_writer = new ExposingWriter(new ShouldWriter());
			_expected = expected.ToExpectedObject().Configure(ctx =>
			{
				ctx.IgnoreTypes();
				ctx.SetWriter(_writer);
			});
		}

		/// <summary>
		/// Test whether the constraint is satisfied by a given value.
		/// </summary>
		/// <param name="current">The value to be tested</param>
		/// <returns>True for success, false for failure</returns>
		public override bool Matches(object current)
		{
			actual = current;
			bool matched = _expected.Equals(actual);
			if (!matched)
			{
				_writer.GetFormattedResults();
				_exposed = _writer.Exposed;
			}
			return matched;
		}
	}

	public class WritableEqualityResult : EqualityResult
	{
		public WritableEqualityResult(string member, object expected, object actual) : base(false, member, expected, actual) { }

		public void WriteMember(MessageWriter writer)
		{
			writer.WriteMessageLine(0, Member);
		}

		public void WriteActual(MessageWriter writer)
		{
			if (Actual is IMissingMember)
			{
				writer.Write("member was missing");
			}
			else if (Actual is IMissingElement)
			{
				writer.Write("element was missing");
			}
			else
			{
				writer.WriteActualValue(Actual);
			}
		}

		public void WriteExpected(MessageWriter writer)
		{
			if (Actual is IUnexpectedElement)
			{
				writer.Write("nothing");
			}
			else
			{
				writer.WriteExpectedValue(Expected);
			}
		}
	}

	public class ExposingWriter : IWriter
	{
		private readonly IWriter _decoree;
		private readonly List<EqualityResult> _written;

		public ExposingWriter(IWriter decoree)
		{
			_decoree = decoree;
			_written = new List<EqualityResult>();
		}

		internal WritableEqualityResult Exposed { get; private set; }
		
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
			return !_written.Any(x => x.Member.StartsWith(result.Member + ".") || x.Member.StartsWith(result.Member + "["));
		}
	}
}
