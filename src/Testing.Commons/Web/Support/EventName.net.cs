using System;

namespace Testing.Commons.Web.Support
{
	internal class EventName
	{
		private EventName(string name)
		{
			OfEvent = name;
			OfInvocator = "On" + OfEvent;
		}

		public string OfEvent { get; private set; }
		public string OfInvocator { get; private set; }

		public static EventName Parse(string operationName)
		{
			return Parser.Parse(operationName);
		}

		abstract class Parser
		{
			protected abstract string OperationPrefix { get; }

			protected virtual bool canParse(string operationName)
			{
				return operationName.StartsWith(OperationPrefix);
			}

			protected virtual string parse(string operationName)
			{
				return operationName.Substring(OperationPrefix.Length);
			}

			private bool tryParse(string methodName, out EventName name)
			{
				bool result = canParse(methodName);
				name = result ? new EventName(parse(methodName)) : default(EventName);
				return result;
			}

			private static readonly Parser[] _parsers;
			static Parser()
			{
				_parsers = new Parser[] { new SubscriptionParser(), new UnsubscriptionParser() };
			}

			public static EventName Parse(string operationName)
			{
				EventName name = null;
				foreach (var parser in _parsers)
				{
					if (parser.tryParse(operationName, out name)) break;
				}
				if (name == null) throw new ArgumentException(Resources.Messages.ParseableEventOperation, "operationName");
				return name;
			}
		}

		class SubscriptionParser : Parser
		{
			protected override string OperationPrefix { get { return "add_"; } }
		}

		class UnsubscriptionParser : Parser
		{
			protected override string OperationPrefix { get { return "remove_"; } }
		}
	}
}