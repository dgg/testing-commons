using System;
using System.IO;
using System.Reflection;

namespace Testing.Commons.Web.Support
{
	// code based on Castle.Facilities.EventWiring.NaiveMethodNameExtractor
	// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
	internal class EventOperation
	{
		internal enum OpCodeValues : ushort
		{
			Nop = 0x0000,
			Ldarg_0 = 0x0002,
			Ldnull = 0x0014,
			Call = 0x0028,
			Ret = 0x002A,
			Callvirt = 0x006F,
		}

		private readonly MethodInfo _delegateMethod;
		private readonly Module _module;
		private readonly MemoryStream _stream;

		public EventOperation(Delegate assignation)
		{
			_delegateMethod = assignation.Method;
			MethodBody body = _delegateMethod.GetMethodBody();
			_stream = new MemoryStream(body.GetILAsByteArray());
			_module = _delegateMethod.Module;
		}

		public EventName EventName
		{
			get
			{
				string methodName = null;
				OpCodeValues currentOpCode;
				while (readOpCode(out currentOpCode))
				{
					if (currentOpCode == OpCodeValues.Callvirt || currentOpCode == OpCodeValues.Call)
					{
						var method = getCalledMethod(readOperand(32));
						methodName = method.Name;
					}
				}

				return EventName.Parse(methodName);
			}
		}

		private bool readOpCode(out OpCodeValues opCodeValue)
		{
			var valueInt = _stream.ReadByte();
			if (valueInt == -1)
			{
				opCodeValue = 0;
				return false;
			}
			var xByteValue = (byte)valueInt;
			if (xByteValue == 0xFE)
			{
				valueInt = _stream.ReadByte();
				if (valueInt == -1)
				{
					opCodeValue = 0;
					return false;
				}
				opCodeValue = (OpCodeValues)(xByteValue << 8 | valueInt);
			}
			else
			{
				opCodeValue = (OpCodeValues)xByteValue;
			}
			return true;
		}

		private MethodBase getCalledMethod(byte[] rawOperand)
		{
			Type[] genericTypeArguments = null;
			Type[] genericMethodArguments = null;
			if (_delegateMethod.DeclaringType.IsGenericType)
			{
				genericTypeArguments = _delegateMethod.DeclaringType.GetGenericArguments();
			}
			if (_delegateMethod.IsGenericMethod)
			{
				genericMethodArguments = _delegateMethod.GetGenericArguments();
			}
			var methodBase = _module.ResolveMethod(operandValueAsInt32(rawOperand), genericTypeArguments, genericMethodArguments);
			return methodBase;
		}

		private static int operandValueAsInt32(byte[] rawOperand)
		{
			var value = new byte[4];
			Array.Copy(rawOperand, value, Math.Min(4, rawOperand.Length));
			return BitConverter.ToInt32(value, 0);
		}

		private byte[] readOperand(byte operandSize)
		{
			var bytes = new byte[operandSize / 8];
			var actualSize = _stream.Read(bytes, 0, bytes.Length);
			if (actualSize < bytes.Length)
			{
				throw new NotSupportedException();
			}
			return bytes;
		}
	}
}