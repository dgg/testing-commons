using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints
{
	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="SerializationConstraint{T}"/> that allows checking the binary serialization/deserialization of an object.
		/// </summary>
		/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <returns>Instance built.</returns>
		public static Constraint BinarySerializable<T>(this Must.BeEntryPoint entry, Constraint constraintOverDeserialized)
		{
			return new SerializationConstraint<T>(new BinaryRoundtripSerializer<T>(), constraintOverDeserialized);
		}

		/// <summary>
		/// Builds an instance of <see cref="SerializationConstraint{T}"/> that allows checking the JSON serialization/deserialization of an object.
		/// </summary>
		/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <param name="converters">An array that contains the custom converters to be registered.</param>
		/// <returns>Instance built.</returns>
		public static Constraint JsonSerializable<T>(this Must.BeEntryPoint entry, Constraint constraintOverDeserialized, params JavaScriptConverter[] converters)
		{
			return new SerializationConstraint<T>(new JsonRoundtripSerializer<T>(converters), constraintOverDeserialized);
		}

		/// <summary>
		/// Builds an instance of <see cref="SerializationConstraint{T}"/> that allows checking the data contract serialization/deserialization of an object.
		/// </summary>
		/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize. The default is <c>4</c>.</param>
		/// <param name="ignoreExtensionDataObject">true to ignore the <see cref="IExtensibleDataObject"/> interface upon serialization and ignore unexpected data upon deserialization; otherwise, false. The default is false.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="IDataContractSurrogate"/> to customize the serialization process.</param>
		/// <param name="alwaysEmitTypeInformation">true to emit type information; otherwise, false. The default is false.</param>
		/// <returns>Instance built.</returns>
		public static Constraint DataContractJsonSerializable<T>(this Must.BeEntryPoint entry, Constraint constraintOverDeserialized, int maxItemsInObjectGraph = 4, bool ignoreExtensionDataObject = false, IDataContractSurrogate dataContractSurrogate = null, bool alwaysEmitTypeInformation = false)
		{
			return new SerializationConstraint<T>(
				new DataContractJsonRoundtripSerializer<T>(
					maxItemsInObjectGraph,
					ignoreExtensionDataObject,
					dataContractSurrogate,
					alwaysEmitTypeInformation),
				constraintOverDeserialized);
		}
	}
}