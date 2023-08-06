using System;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Testing.Commons.Web.Support;

namespace Testing.Commons.Web
{
	/// <summary>
	/// Eases testing implementations of <see cref="Control"/>.
	/// </summary>
	/// <remarks>Controls react to different events during their lifecycle. Those events are usually raisedv by implementations of protected methods and,
	/// therefore, cannot be directly invoked from outside the class. By hiding the complexity of those reflection calls, testing of controls is easier to achieve.</remarks>
	public class ControlLifecycle
	{
		/// <summary>
		/// Simulates the raise of a given event in a strongly-typed fashion.
		/// </summary>
		/// <remarks>In reality, when the event is fired, some methods in the implementation are called.
		/// <para>When testing, the non-public method that would be called when the real event fired is called using reflection with default values for each argument.</para></remarks>
		/// <param name="control">The subject of the test.</param>
		/// <param name="step">The event that we want to simulate.</param>
		/// <typeparam name="TControl">Type of the subject of the test.</typeparam>
		public static void Fake<TControl>(TControl control, Action<TControl> step) where TControl : Control
		{
			var name = new EventOperation(step).EventName;
			
			Call(control, name.OfInvocator);
		}

		/// <summary>
		/// Simulates the raise of a given event in a strongly-typed fashion.
		/// </summary>
		/// <remarks>In reality, when the event is fired, some methods in the implementation are called.
		/// <para>When testing, the non-public method that would be called when the real event fired is called using reflection with the provided argument values.</para></remarks>
		/// <param name="control">The subject of the test.</param>
		/// <param name="step">The event that we want to simulate.</param>
		/// <param name="stepArguments">Arguments for the non-public method that will be called when the event is simulated.</param>
		/// <typeparam name="TControl">Type of the subject of the test.</typeparam>
		public static void Fake<TControl>(TControl control, Action<TControl> step, params object[] stepArguments) where TControl : Control
		{
			var name = new EventOperation(step).EventName;

			Call(control, name.OfInvocator, stepArguments);
		}

		/// <summary>
		/// Calls a non-public method directly.
		/// </summary>
		/// <remarks>Sometimes, important methods are not the direct result of an event and yet they are called during the lyfecycle of the control.
		/// This method eases calling them with default values for each argument.</remarks>
		/// <typeparam name="TControl">Type of the subject of the test.</typeparam>
		/// <param name="control">The subject of the test.</param>
		/// <param name="methodName">Name of the method to invoke.</param>
		public static void Call<TControl>(TControl control, string methodName) where TControl : Control
		{
			var method = getMethodForStep(control, methodName);
			invoke(method, control, getDefaultParameters(method));
		}

		/// <summary>
		/// Calls a non-public method directly.
		/// </summary>
		/// <remarks>Sometimes, important methods are not the direct result of an event and yet they are called during the lyfecycle of the control.
		/// This method eases calling them with default values for each argument.</remarks>
		/// <typeparam name="TControl">Type of the subject of the test.</typeparam>
		/// <param name="control">The subject of the test.</param>
		/// <param name="methodName">Name of the method to invoke.</param>
		/// <param name="methodArguments">Arguments of the method.</param>
		public static void Call<TControl>(TControl control, string methodName, params object[] methodArguments) where TControl : Control
		{
			var method = getMethodForStep(control, methodName);
			invoke(method, control, methodArguments);
		}

		private static MethodInfo getMethodForStep(object control, string methodName)
		{
			var flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod;
			Type type = control.GetType();
			MethodInfo method = type.GetMethod(methodName, flags);
			if (method == null)
			{
				throw new MissingMemberException(type.Name, methodName);
			}

			return method;
		}

		private static object[] getDefaultParameters(MethodInfo method)
		{
			ParameterInfo[] parameters = method.GetParameters();

			return parameters
				.Select(p => p.GetType().Default())
				.ToArray();
		}

		private static void invoke(MethodInfo method, object control, object[] parameters)
		{
			method.Invoke(control, parameters);
		}
	}
}
