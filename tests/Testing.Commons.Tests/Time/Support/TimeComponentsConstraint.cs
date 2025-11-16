using NUnit.Framework.Constraints;
using Testing.Commons.Time;

namespace Testing.Commons.Tests.Time.Support;

public class TimeComponentsConstraint(int year, int month, int day, int hour, int minute, int second, int millisecond, TimeSpan offset) : Constraint
{
	/*{

		_constraints =
		[
			,
			,
			,

		];
	}*/

	private EqualConstraint? _current;
	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{
		TimeComponents components = (TimeComponents)(object)actual!;

		_current = new EqualConstraint(new DateOnly(year, month, day));
		DateOnly actualDo = components;
		var result = _current.ApplyTo(actualDo);
		if (result.IsSuccess)
		{
			TimeOnly actualTo = components;
			_current = new EqualConstraint(new TimeOnly(hour, minute, second, millisecond));
			if (result.IsSuccess)
			{
				DateTime actualDt = components;
				DateTimeKind kind = offset.Equals(TimeSpan.Zero) ? DateTimeKind.Utc : DateTimeKind.Local;
				_current = new EqualConstraint(new DateTime(year, month, day, hour, minute, second, kind));
				result = _current.ApplyTo(actualDt);
				if (result.IsSuccess)
				{
					DateTimeOffset actualDto = components;
					_current = new EqualConstraint(new DateTimeOffset(year, month, day, hour, minute, second, millisecond, offset));
					result = _current.ApplyTo(actualDto);
				}
			}
		}

		return result;
	}

	public override string Description => _current?.Description ?? string.Empty;
}
