using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints
{
	/// <summary>
	/// Provides a set of static methods to create custom constraints.
	/// </summary>
	public static class MustExtensions
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
		/// Builds an instance of <see cref="PropertyChangedConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanged.PropertyChanged"/> when a property is set.
		/// </summary>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</typeparam>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="property">Expression that represents the name of a property.</param>
		/// <returns>Instance built.</returns>
		public static PropertyChangedConstraint<TSubject> PropertyChanged<TSubject>(this Must.RaiseEntryPoint entry, TSubject subject, Expression<Func<TSubject, object>> property) where TSubject : INotifyPropertyChanged
		{
			return new PropertyChangedConstraint<TSubject>(subject, property);
		}

		/// <summary>
		/// Builds an instance of <see cref="PropertyChangingConstraint{TSubject}"/> that allows checking whether a type raises a
		/// <see cref="INotifyPropertyChanging.PropertyChanging"/> when a property is set.
		/// </summary>
		/// <param name="entry">Extension entry point.</param>
		/// <param name="subject"> Instance of the event raising type.</param>
		/// <param name="property">Expression that represents the name of a property.</param>
		/// <typeparam name="TSubject">Type that raises the <see cref="INotifyPropertyChanging.PropertyChanging"/> event.</typeparam>
		/// <returns>Instance built.</returns>
		public static PropertyChangingConstraint<TSubject> PropertyChanging<TSubject>(this Must.RaiseEntryPoint entry, TSubject subject, Expression<Func<TSubject, object>> property) where TSubject : INotifyPropertyChanging
		{
			return new PropertyChangingConstraint<TSubject>(subject, property);
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
	}
}
