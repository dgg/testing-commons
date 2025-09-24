using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Testing.Commons.Serialization;

/// <summary>
/// Extension methods that provide help testing implementations of <see cref="JsonConverter{T}"/>.
/// </summary>
public static class JsonConverterExtensions
{
	/// <summary>
	/// Writes the value to a string using the provided <see cref="JsonConverter" /> instance
	/// </summary>
	/// <typeparam name="T">The type of object or value handled by the converter.</typeparam>
	/// <param name="subject">The converter being tested.</param>
	/// <param name="toWrite">The instance to convert.</param>
	/// <param name="options">An object that specifies serialization options to use or <c>null</c>.</param>
	/// <returns>A string containing the JSON representation of <paramref name="toWrite"/> as specified by <paramref name="subject"/>.</returns>
	public static string WriteToString<T>(this JsonConverter<T> subject, T toWrite, JsonSerializerOptions? options = null)
	{
		using var ms = new MemoryStream();
		using var writer = new Utf8JsonWriter(ms);
		
		options ??= new JsonSerializerOptions()
		{
			Converters = { subject}
		};
		subject.Write(writer, toWrite, options);
		writer.Flush();
		var result = Encoding.UTF8.GetString(ms.ToArray());
		return result;
	}

	/// <summary>
	/// Reads the value using the provided <see cref="JsonConverter"/> from a JSON string.
	/// </summary>
	/// <typeparam name="T">The type of object or value handled by the converter.</typeparam>
	/// <param name="subject">The converter being tested.</param>
	/// <param name="json">The JSON representation of what the <paramref name="subject"/> will read.</param>
	/// <param name="options">An object that specifies serialization options to use or <c>null</c>.</param>
	/// <returns>The instance of <typeparamref name="T"/> read by <paramref name="subject"/>.</returns>
	/// <exception cref="ArgumentException">The JSON string provided by <paramref name="json"/> could not be read by <paramref name="subject"/>.</exception>
	public static T ReadFromString<T>(this JsonConverter<T> subject, string json, JsonSerializerOptions? options = null)
	{
		var bytes = Encoding.UTF8.GetBytes(json);
		var reader = new Utf8JsonReader(bytes);
		
		reader.Read(); // advance before reading
		options ??= new JsonSerializerOptions()
		{
			Converters = { subject}
		};
		var result = subject.Read(ref reader, typeof(T), options);
		if (result is null)
		{
			throw new ArgumentException("Read value is 'null'", nameof(json));
		}
		return result;
	}

	/// <summary>
	/// Parses text representing a single <see cref="JsonObject"/> value.
	/// </summary>
	/// <param name="json">JSON text to parse.</param>
	/// <returns>A <see cref="JsonObject"/> representation of the JSON value.</returns>
	/// <exception cref="InvalidOperationException">The text provided by <paramref name="json"/> is not a <see cref="JsonObject"/>.</exception>
	public static JsonObject ParseObject(this string json)
	{
		var nodeOpts = new JsonNodeOptions
		{
			PropertyNameCaseInsensitive = true
		};

		JsonNode? objNode = JsonNode.Parse(json, nodeOpts);
		if (objNode is null)
		{
			throw new InvalidOperationException($"The node must be of type '{nameof(JsonObject)}'");
		}

		JsonObject obj = objNode.AsObject();
		return obj;
	}
}

