namespace Testing.Commons.Serialization;

/// <summary>
/// Provides a set of extension methods for testing serialization scenarios, where streams are widely used.
/// </summary>
public static class SerializationExtensions
{
	/// <summary>
	/// Converts a string into a <see cref="MemoryStream"/>. This method is useful for preparing string data to be consumed by stream-based serializers or other stream-processing components.
	/// </summary>
	/// <param name="s">The string to convert to a stream.</param>
	/// <returns>A new <see cref="MemoryStream"/> containing the content of the input string.</returns>
	public static Stream AsStream(this string s)
	{
		var stream = new MemoryStream();
		using var sw = new StreamWriter(stream);
		sw.Write(s);
		sw.Flush();
		return stream;
	}

	/// <summary>
	/// Reads the entire content of a <see cref="Stream"/> into a string. This method is useful for retrieving serialized data from a stream for inspection or further processing.
	/// </summary>
	/// <param name="stream">The stream to read from.</param>
	/// <returns>A string containing the content of the stream, or <see langword="null"/> if the stream's length is zero.</returns>
	/// <remarks>
	/// The method flushes the stream and resets its position to the beginning before reading.
	/// </remarks>
	public static string? ReadString(this Stream stream)
	{
		if (stream.Length <= 0) return null;

		stream.Flush();
		stream.Position = 0;
		using var sr = new StreamReader(stream);
		return sr.ReadToEnd();
	}
}
