using System;
using System.Runtime.Serialization;

namespace Testing.Commons
{
	public static partial class GuidBuilder
	{
		[Serializable]
		public partial class NotHexadecimalException
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="ArgumentOutOfRangeException"/> class with serialized data.
			/// </summary>
			/// <param name="info">The object that holds the serialized object data.</param>
			/// <param name="context">An object that describes the source or destination of the serialized data.</param>
			protected NotHexadecimalException(SerializationInfo info, StreamingContext context) : base(info, context) { }
		}
	}
}