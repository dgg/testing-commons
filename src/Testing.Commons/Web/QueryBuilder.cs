using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Testing.Commons.Web
{
	internal class QueryBuilder
	{
		private const string AMPERSAND = "&", EQUALS = "=";
		public QueryBuilder(NameValueCollection collection) 
		{
			Query = collection == null ?
				string.Empty : 
				string.Join(AMPERSAND,
					collection.Cast<string>()
					.Where(key => !string.IsNullOrEmpty(key))
					.Select(key => encode(key)+ EQUALS +  encode(collection[key])));
		}

		private string encode(string s)
		{
			return HttpUtility.UrlEncode(s ?? string.Empty);
		}

		public string Query { get; private set; }
	}
}