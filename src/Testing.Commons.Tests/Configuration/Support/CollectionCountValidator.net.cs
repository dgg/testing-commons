using System;
using System.Configuration;

namespace Testing.Commons.Tests.Configuration.Support
{
	public class CollectionCountValidator : ConfigurationValidatorBase
	{
		private readonly uint _minimumCount;

		public CollectionCountValidator(uint minCount)
		{
			_minimumCount = minCount;
		}

		public override bool CanValidate(Type type)
		{
			return type.IsSubclassOf(typeof(ConfigurationElementCollection));
		}

		public override void Validate(object value)
		{
			ConfigurationElementCollection collection = (ConfigurationElementCollection)value;

			if (collection.Count < _minimumCount)
			{
				throw new ConfigurationErrorsException(string.Format(
					"The collection of type '{0}' must contain at least {1} elements, but contained {2}.",
					collection.GetType().Name,
					_minimumCount.ToString(),
					collection.Count.ToString()));
			}
		}
	}
}