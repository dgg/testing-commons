namespace Testing.Commons.Serialization
{
	public class JsonString
	{
		private readonly string _json;
		public JsonString(string json)
		{
			_json = jsonify(json);
		}

		internal static string jsonify(string s)
		{
			return s == null ? null : s.Replace("'", "\"");
		}

		public override string ToString()
		{
			return _json;
		}

		public static implicit operator string(JsonString instance)
		{
			return instance._json;
		}
	}
}