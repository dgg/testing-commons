using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Provides a set of static methods to create custom constraints.
	/// </summary>
	public static partial class MustExtensions
	{
		/// <summary>
		/// Builds an instance of <see cref="Testing.Commons.NUnit.Constraints.ConstrainedEnumerable"/> with the provided constraints.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Constrained(this Must.BeEntryPoint entry, params Constraint[] constraints)
		{
			return new ConstrainedEnumerable(constraints);
		}

		/// <summary>
		/// Builds an instance of <see cref="Testing.Commons.NUnit.Constraints.ConstrainedEnumerable"/> with the provided constraints.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraints">Constraints to apply to the enumerable elements.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Constrained(this Must.BeEntryPoint entry, IEnumerable<Constraint> constraints)
		{
			return new ConstrainedEnumerable(constraints);
		}

		/// <summary>
		/// Builds an instance of <see cref="MatchingConstraint"/> to match the provided expected object.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="expected">The object to match the actual value against.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Expected(this Must.MatchEntryPoint entry, object expected)
		{
			return new MatchingConstraint(expected);
		}

		/// <summary>
		/// Builds an instance of <see cref="LambdaPropertyConstraint{T}"/> to check <paramref name="constraint"/> over the value of the property expressed by <paramref name="property"/>
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="property">A member expression representing a property.</param>
		/// <param name="constraint">The constraint to apply to the property.</param>
		/// <returns>Instance built.</returns>
		public static LambdaPropertyConstraint<T> Property<T>(this Must.HaveEntryPoint entry, Expression<Func<T, object>> property, Constraint constraint)
		{
			return new LambdaPropertyConstraint<T>(property, constraint);
		}

		/// <summary>
		/// Builds an instance of <see cref="EnumerableCountConstraint"/> that allows asserting on the number of elements of any instance of <see cref="System.Collections.IEnumerable"/>.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="countConstraint">The constraint to be applied to the element count.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Count(this Must.HaveEntryPoint entry, Constraint countConstraint)
		{
			return new EnumerableCountConstraint(countConstraint);
		}

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
		/// Builds an instance of <see cref="SerializationConstraint{T}"/> that allows checking the XML serialization/deserialization of an object.
		/// </summary>
		/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <returns>Instance built.</returns>
		public static Constraint XmlSerializable<T>(this Must.BeEntryPoint entry, Constraint constraintOverDeserialized)
		{
			return new SerializationConstraint<T>(new XmlRoundtripSerializer<T>(), constraintOverDeserialized);
		}

		/// <summary>
		/// Builds an instance of <see cref="SerializationConstraint{T}"/> that allows checking the data contract serialization/deserialization of an object.
		/// </summary>
		/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <returns>Instance built.</returns>
		public static Constraint DataContractSerializable<T>(this Must.BeEntryPoint entry, Constraint constraintOverDeserialized)
		{
			return new SerializationConstraint<T>(new DataContractRoundtripSerializer<T>(), constraintOverDeserialized);
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
		/// Builds an instance of <see cref="SerializationConstraint{T}"/> that allows checking the serialization/deserialization of an object.
		/// </summary>
		/// <typeparam name="T">Type to be serialized and deserialized.</typeparam>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="serializer"></param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Serializable<T>(this Must.BeEntryPoint entry, IRoundtripSerializer<T> serializer, Constraint constraintOverDeserialized)
		{
			return new SerializationConstraint<T>(serializer, constraintOverDeserialized);
		}

		/// <summary>
		/// Builds an instance of <see cref="DeserializationConstraint{T}"/> that allows checking the XML deserialization of an object.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <returns>Instance built.</returns>
		public static Constraint XmlDeserializable<T>(this Must.BeEntryPoint entry, Constraint constraintOverDeserialized)
		{
			return new DeserializationConstraint<T>(new XmlDeserializer(), constraintOverDeserialized);
		}

		/// <summary>
		/// Builds an instance of <see cref="DeserializationConstraint{T}"/> that allows checking the data contract deserialization of an object.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <returns>Instance built.</returns>
		public static Constraint DataContractDeserializable<T>(this Must.BeEntryPoint entry, Constraint constraintOverDeserialized)
		{
			return new DeserializationConstraint<T>(new DataContractDeserializer(), constraintOverDeserialized);
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

		/// <summary>
		/// Builds an instance of <see cref="DeserializationConstraint{T}"/> that allows checking the deserialization of an object.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="deserializer">Deserializer used to deserialize the tested value.</param>
		/// <param name="constraintOverDeserialized">Constraint to apply to the deserialized object.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Deserializable<T>(this Must.BeEntryPoint entry, IDeserializer deserializer, Constraint constraintOverDeserialized)
		{
			return new DeserializationConstraint<T>(deserializer, constraintOverDeserialized);
		}

		/// <summary>
		/// Builds an instance of <see cref="ImplementsComparableConstraint{T}"/> that allows checking the implementation of
		/// <see cref="IComparable{T}"/> when <typeparamref name="T"/> is the same type.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="strictlyLessThan">An instance of <typeparamref name="T"/> that is strictly less than the value tested.</param>
		/// <param name="strictlyGreaterThan">An instance of <typeparamref name="T"/> that is strictly greater than the value tested.</param>
		/// <returns>Instance built.</returns>
		public static Constraint ComparableSpecificationAgainst<T>(this Must.SatisfyEntryPoint entry, T strictlyLessThan, T strictlyGreaterThan)
		{
			return new ImplementsComparableConstraint<T>(strictlyLessThan, strictlyGreaterThan);
		}

		/// <summary>
		/// Builds an instance of <see cref="ImplementsComparableConstraint{T}"/> that allows checking the implementation of
		/// <see cref="IComparable{T}"/> when <typeparamref name="T"/> is another type.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="strictlyLessThan">An instance of <typeparamref name="T"/> that is strictly less than the value tested.</param>
		/// <param name="strictlyGreaterThan">An instance of <typeparamref name="T"/> that is strictly greater than the value tested.</param>
		/// <param name="equal">An instance of <typeparamref name="T"/> that has the same value as the value tested.</param>
		/// <returns>Instance built.</returns>
		public static Constraint ComparableSpecificationAgainst<T>(this Must.SatisfyEntryPoint entry, T strictlyLessThan, T strictlyGreaterThan, T equal)
		{
			return new ImplementsComparableConstraint<T>(strictlyLessThan, strictlyGreaterThan, equal);
		}

		/// <summary>
		/// Builds an instance of <see cref="ImplementsComparisonConstraint{T}"/> that allows checking the implementation of
		/// comparison operators against the same type.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="strictlyLessThan">An instance of <typeparamref name="T"/> that is strictly less than the value tested.</param>
		/// <param name="strictlyGreaterThan">An instance of <typeparamref name="T"/> that is strictly greater than the value tested.</param>
		/// <returns>Instance built.</returns>
		public static Constraint ComparisonSpecificationAgainst<T>(this Must.SatisfyEntryPoint entry, T strictlyLessThan, T strictlyGreaterThan)
		{
			return new ImplementsComparisonConstraint<T>(strictlyLessThan, strictlyGreaterThan);
		}

		/// <summary>
		/// Builds an instance of <see cref="ImplementsComparisonConstraint{T, U}"/> that allows checking the implementation of
		/// comparison operators against another type.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="strictlyLessThan">An instance of <typeparamref name="U"/> that is strictly less than the value tested.</param>
		/// <param name="strictlyGreaterThan">An instance of <typeparamref name="U"/> that is strictly greater than the value tested.</param>
		/// <param name="equal">An instance of <typeparamref name="U"/> that has the same value as the value tested.</param>
		/// <returns>Instance built.</returns>
		public static Constraint ComparisonSpecificationAgainst<T, U>(this Must.SatisfyEntryPoint entry, U equal, U strictlyLessThan, U strictlyGreaterThan)
		{
			return new ImplementsComparisonConstraint<T, U>(equal, strictlyLessThan, strictlyGreaterThan);
		}

		/*/// <summary>
		/// Builds an instance of <see cref="ImplementsEquatableConstraint{T}"/> that allows checking the implementation of
		/// <see cref="IEquatable{T}"/> when <typeparamref name="T"/> is the same type.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="equalTo">An instance of <typeparamref name="T"/> that is equal to the value tested.</param>
		/// <param name="notEqualTo">An instance of <typeparamref name="T"/> that is not equal to the value tested.</param>
		/// <returns>Instance built.</returns>
		public static Constraint EquatableSpecificationAgainst<T>(this Must.SatisfyEntryPoint entry, T equalTo, T notEqualTo)
		{
			return new ImplementsEquatableConstraint<T>(equalTo, notEqualTo);
		}*/

		/// <summary>
		/// Builds an instance of <see cref="ConjunctionConstraint"/> that allows joining multiple constraints
		/// while reporting the specific constraint that failed.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraints">The list of constraints to evaluate.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Conjunction(this Must.SatisfyEntryPoint entry, params Constraint[] constraints)
		{
			return new ConjunctionConstraint(constraints);
		}

		/// <summary>
		/// Builds an instance of <see cref="ConjunctionConstraint"/> that allows joining multiple constraints
		/// while reporting the specific constraint that failed.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="constraints">The list of constraints to evaluate.</param>
		/// <returns>Instance built.</returns>
		public static Constraint Conjunction(this Must.SatisfyEntryPoint entry, IEnumerable<Constraint> constraints)
		{
			return new ConjunctionConstraint(constraints);
		}

	}
}
