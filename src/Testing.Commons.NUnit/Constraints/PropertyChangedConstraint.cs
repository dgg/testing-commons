using System;
using System.ComponentModel;
using System.Linq.Expressions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Testing.Commons.NUnit.Constraints
{
	public class PropertyChangedConstraint<T> : Constraint where T : INotifyPropertyChanged
	{
		private readonly T _source;
		private readonly Constraint _matchingPropertyName;

		public PropertyChangedConstraint(T source, Expression<Func<T, object>> property)
		{
			_source = source;
			_matchingPropertyName = Must.Have.Property<PropertyChangedEventArgs>(e => e.PropertyName, Is.EqualTo(Name.Of(property)));
		}

		bool _eventRaised;

		bool _matched;
		public override bool Matches(ActualValueDelegate del)
		{
			_matched = false;
			_source.PropertyChanged += (sender, e) =>
			{
				_eventRaised = true;
				_matched = _matchingPropertyName.Matches(e);
			};
			del();
			return _matched;
		}
		public override bool Matches(object current)
		{
			return _matched;
		}

		public override void WriteDescriptionTo(MessageWriter writer)
		{
			writer.Write("raise event 'PropertyChanged' and ");
			_matchingPropertyName.WriteDescriptionTo(writer);
		}

		public override void WriteActualValueTo(MessageWriter writer)
		{
			if (!_eventRaised)
			{
				writer.Write("event 'PropertyChanged' not raised");
			}
			else
			{
				_matchingPropertyName.WriteActualValueTo(writer);
			}
		}
	}
}
