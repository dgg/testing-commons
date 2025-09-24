using System.Text.Json;
using System.Text.Json.Serialization;

namespace Testing.Commons.Tests.Serialization.Subjects;

/// <summary>
///  Serializes the double (2x) amount of the provided number.
/// </summary>
internal class CustomDoublingConverter : JsonConverter<Doubling>
{
	public override Doubling Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		int doubledValue = JsonSerializer.Deserialize<int>(ref reader, options);
		return new Doubling(doubledValue / 2);
	}

	public override void Write(Utf8JsonWriter writer, Doubling value, JsonSerializerOptions options)
	{
		JsonSerializer.Serialize(writer, value.Value * 2, options);
	}
}
internal readonly record struct Doubling(int Value);

