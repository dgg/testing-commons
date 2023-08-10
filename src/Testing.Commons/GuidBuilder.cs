using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace Testing.Commons;

/// <summary>
/// Allows creation of deterministic <see cref="Guid"/> instances.
/// </summary>
public static partial class GuidBuilder
{
	private static readonly Range<char> _hexLetters = new('a', 'f');

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
		if (!isHex(hexFigure)) throw new NotHexadecimalException(nameof(hexFigure), hexFigure);
	}

	private static bool isHex(char c)
	{
		bool isHex = char.IsNumber(c) ||
					 (char.IsLetter(c) && _hexLetters.Contains(char.ToLower(c, CultureInfo.InvariantCulture)));
		return isHex;
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

		// TODO: check netstandard target
		char safeChar = char.Parse(hexFigure.ToString("x", CultureInfo.InvariantCulture));
		return buildGuid(safeChar);
	}

	private static void ensureHex(byte hexFigure)
	{
		if (!isHex(hexFigure)) throw new NotHexadecimalException(nameof(hexFigure), hexFigure);
	}

	private static bool isHex(byte i)
	{
		bool isHex = i <= 15;
		return isHex;
	}

	/// <summary>
	/// The exception that is thrown when the value of an argument is outside the allowable range of values as defined by the invoked method.
	/// </summary>
	/// <remarks>When a number or a number representation is not a hexadecimal figure.</remarks>
	[Serializable]
	// TODO: check netstandard
	public partial class NotHexadecimalException : ArgumentOutOfRangeException
	{
		private static readonly string _allowedCharacters = new Range<char>('0', '9') + " U " + _hexLetters;
		private static readonly string _allowedNumbers = new Range<byte>(0, 15).ToString();

		/// <summary>
		/// Initializes a new instance of the <see cref="NotHexadecimalException"/> class.
		/// </summary>
		public NotHexadecimalException() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="NotHexadecimalException"/> class with the name of the parameter that causes this exception.
		/// </summary>
		/// <param name="paramName">The parameter name string.</param>
		public NotHexadecimalException(string paramName) : base(paramName) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="NotHexadecimalException"/> class with a specified error message and the name of the parameter that causes this exception.
		/// </summary>
		/// <param name="paramName">The parameter name string.</param>
		/// <param name="message">The error message string.</param>
		public NotHexadecimalException(string paramName, string message) : base(paramName, message) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="NotHexadecimalException"/> class with a specified error message and the exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message string.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
		public NotHexadecimalException(string message, Exception innerException) : base(message, innerException) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="NotHexadecimalException"/> class with a specified error message, the parameter name, and the value of the argument.
		/// </summary>
		/// <param name="paramName">The parameter name string.</param>
		/// <param name="actualValue">The argument value.</param>
		/// <param name="message">The error message string.</param>
		public NotHexadecimalException(string paramName, object actualValue, string message) : base(paramName, actualValue, message) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="NotHexadecimalException"/> class with the parameter name and the value of the argument.
		/// </summary>
		/// <param name="paramName">The parameter name string.</param>
		/// <param name="notHex">The argument value.</param>
		public NotHexadecimalException(string paramName, char notHex) : this(paramName, notHex, _allowedCharacters) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="NotHexadecimalException"/> class with the parameter name and the value of the argument.
		/// </summary>
		/// <param name="paramName">The parameter name string.</param>
		/// <param name="notHex">The argument value.</param>
		public NotHexadecimalException(string paramName, byte notHex) : this(paramName, notHex, _allowedNumbers) { }

		// TODO: check netstandard
		/// <summary>
		/// Initializes a new instance of the <see cref="ArgumentOutOfRangeException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">An object that describes the source or destination of the serialized data.</param>
		protected NotHexadecimalException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
