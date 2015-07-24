namespace Testing.Commons.Serialization
{
	public static class JsonStringExtensions
	{
		public static string Jsonify(this string json)
		{
			return JsonString.jsonify(json);
		}
	}
}
