using System;
using System.Configuration;

namespace Testing.Commons.Configuration.Messages
{
	/// <summary>
	/// Provides access to some fragments of common messages of <see cref="ConfigurationErrorsException"/> in English.
	/// </summary>
	public static class MessageExtensions
	{
		/// <summary>
		/// The value does not correspond to the defined type.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="memberName">Name of the </param>
		/// <returns>A formatted substring of the extencion message.</returns>
		public static string WrongMemberValue(this ExceptionMessagePart.ForEntryPoint entry, string memberName)
		{
			return String.Format("value of the property '{0}' cannot be parsed", memberName);
		}

		/// <summary>
		/// The element is not defined.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="attributeName"></param>
		/// <returns>A formatted substring of the extencion message.</returns>
		public static string UndefinedAttribute(this ExceptionMessagePart.ForEntryPoint entry, string attributeName)
		{
			return String.Format("Unrecognized attribute '{0}'", attributeName);
		}

		/// <summary>
		/// An element could not be recognized.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="elementName"></param>
		/// <returns>A formatted substring of the extencion message.</returns>
		public static string UndefinedElement(this ExceptionMessagePart.ForEntryPoint entry, string elementName)
		{
			return String.Format("Unrecognized element '{0}'", elementName);
		}

		/// <summary>
		/// More than one ocurrence of an element is detected.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="elementName"></param>
		/// <returns>A formatted substring of the extencion message.</returns>
		public static string DuplicatedElement(this ExceptionMessagePart.ForEntryPoint entry, string elementName)
		{
			return String.Format("<{0}> may only appear once", elementName);
		}

		/// <summary>
		/// The value of an attribute does not correspond with its definition.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="memberName"></param>
		/// <returns>A formatted substring of the extencion message.</returns>
		public static string InvalidAttributeValue(this ExceptionMessagePart.ForEntryPoint entry, string memberName)
		{
			return String.Format("'{0}' is not valid", memberName);
		}

		/// <summary>
		/// A mandatory member is not present.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="memberName"></param>
		/// <returns>A formatted substring of the extencion message.</returns>
		public static string MissingRequiredMember(this ExceptionMessagePart.ForEntryPoint entry, string memberName)
		{
			return String.Format("Required attribute '{0}' not found", memberName);
		}

		/// <summary>
		/// A required child of a collection is not present.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <returns>A formatted substring of the extencion message.</returns>
		public static string MissingRequiredChildElement<TCollection>(this ExceptionMessagePart.ForEntryPoint entry) where TCollection : ConfigurationElementCollection
		{
			return string.Format("The collection of type '{0}' must contain at least 1 elements", typeof(TCollection).Name);
		}
	}
}
