using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace Testing.Commons;

/// <summary>
/// Allows creation of deterministic <see cref="Guid"/> instances.
/// </summary>
public static class GuidBuilder
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Guid"/> structure using the values represented by the specified representation of an hex figure.
	/// </summary>
	/// <param name="hexFigure">A hexadecimal figure: '0' to '1' or 'A' to 'F' or 'a' to 'f'.</param>
	/// <returns>A new instance with the information provided.</returns>
	/// <exception cref="NotHexadecimalException"><paramref name="hexFigure"/> is not a hexadecimal figure.</exception>
	public static Guid Build(char hexFigure)
	{
		ensureHexFigure(hexFigure);
		return buildGuid(hexFigure);
	}

	private static Guid buildGuid(char safeHexFigure)
	{
		var sb = new StringBuilder(32);
		sb.Append(safeHexFigure, 32);
		return new Guid(sb.ToString());
	}

	private static void ensureHexFigure(char hexFigure)
	{
		if (!char.IsAsciiHexDigit(hexFigure)) throw NotHexadecimalException.From(nameof(hexFigure), hexFigure);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Guid"/> structure using the values represented by the specified hex values.
	/// </summary>
	/// <param name="hexFigure">A hexadecimal figure: 0 to 15 or 0x0 to 0xf.</param>
	/// <returns>A new instance with the information provided.</returns>
	/// /// <exception cref="NotHexadecimalException"><paramref name="hexFigure"/> is not a hexadecimal figure.</exception>
	public static Guid Build(byte hexFigure)
	{
		ensureHex(hexFigure);

		char safeChar = hexFigure <= 9 ? Convert.ToChar(hexFigure + 48) : Convert.ToChar(hexFigure + 55);
		return buildGuid(safeChar);
	}

	private static void ensureHex(byte hexFigure)
	{
		if (!isHex(hexFigure)) throw NotHexadecimalException.From(nameof(hexFigure), hexFigure);
	}

	private static bool isHex(byte i) => i <= 15;
}

/// <summary>
/// The exception that is thrown when the value of an argument is outside the allowable range of values as defined by the invoked method.
/// </summary>
/// <remarks>When a number or a number representation is not a hexadecimal figure.</remarks>
public class NotHexadecimalException : ArgumentOutOfRangeException
{
	/// <inheritdoc />
	public NotHexadecimalException() { }

	/// <inheritdoc />
	public NotHexadecimalException(string paramName) : base(paramName) { }

	/// <inheritdoc />
	public NotHexadecimalException(string message, Exception inner) : base(message, inner) { }

	/// <inheritdoc />
	public NotHexadecimalException(string paramName, string message) : base(paramName, message) { }

	/// <inheritdoc />
	public NotHexadecimalException(string paramName, object actualValue, string message) : base(paramName, actualValue, message) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="NotHexadecimalException"/> class with the parameter name and the value of the argument.
	/// </summary>
	/// <param name="paramName">The name of the parameter that caused the exception.</param>
	/// <param name="notHex">The value of the argument that causes this exception.</param>
	/// <returns></returns>
	public static NotHexadecimalException From(string paramName, char notHex) => new(paramName, notHex, "[0..9] U [a..f]");

	/// <summary>
	/// Initializes a new instance of the <see cref="NotHexadecimalException"/> class with the parameter name and the value of the argument.
	/// </summary>
	/// <param name="paramName">The name of the parameter that caused the exception.</param>
	/// <param name="notHex">The value of the argument that causes this exception.</param>
	/// <returns></returns>
	public static NotHexadecimalException From(string paramName, byte notHex) => new(paramName, notHex, "[0..15]");
}
