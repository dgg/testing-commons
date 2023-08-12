using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints
{
	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="DeserializationConstraint{T}"/> that allows checking the data contract deserialization of an object.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize. The default is <c>4</c>.</param>
		/// <param name="ignoreExtensionDataObject">true to ignore the <see cref="IExtensibleDataObject"/> interface upon serialization and ignore unexpected data upon deserialization; otherwise, false. The default is false.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="IDataContractSurrogate"/> to customize the serialization process.</param>
		/// <param name="alwaysEmitTypeInformation">true to emit type information; otherwise, false. The default is false.</param>
		/// <returns>Instance built.</returns>
		public static Constraint DataContractJsonDeserializable<T>(this Must.BeEntryPoint entry,
			Constraint constraintOverDeserialized, int maxItemsInObjectGraph = 4, bool ignoreExtensionDataObject = false,
			IDataContractSurrogate dataContractSurrogate = null, bool alwaysEmitTypeInformation = false)
		{
			return new DeserializationConstraint<T>(
				new DataContractJsonDeserializer(
					maxItemsInObjectGraph,
					ignoreExtensionDataObject,
					dataContractSurrogate,
					alwaysEmitTypeInformation),
				constraintOverDeserialized);
		}

		/// <summary>
		/// Builds an instance of <see cref="DeserializationConstraint{T}"/> that allows checking the JSON deserialization of an object.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <param name="converters">An array that contains the custom converters to be registered.</param>
		/// <returns>Instance built.</returns>
		public static Constraint JsonDeserializable<T>(this Must.BeEntryPoint entry, Constraint constraintOverDeserialized, params JavaScriptConverter[] converters)
		{
			return new DeserializationConstraint<T>(new JsonDeserializer(converters), constraintOverDeserialized);
		}
	}
}