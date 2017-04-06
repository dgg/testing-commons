using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Testing.Commons.Web.Support
{
	internal static class AnonymousExtensions
	{
		public static IDictionary<string, object> AsDictionary(this object anonymousObject)
		{
			IDictionary<string, object> dictionary = anonymousObject == null ?
				new Dictionary<string, object>(0) :
				fillDictionary(anonymousObject);

			return dictionary;
		}

		private static IDictionary<string, object> fillDictionary(this object anonymousObject)
		{
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(anonymousObject);
			IDictionary<string, object> dictionary = new Dictionary<string, object>(
				props.Count,
				StringComparer.InvariantCulture);

			foreach (PropertyDescriptor prop in props)
			{
				object val = prop.GetValue(anonymousObject);
				dictionary.Add(prop.Name, val);
			}
			return dictionary;
		}
	}


}
