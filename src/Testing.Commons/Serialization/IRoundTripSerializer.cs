using System;

namespace Testing.Commons.Serialization
{
	public interface IRoundTripSerializer<T> : IDisposable
	{
		string Serialize(T toSerialize);
		T Deserialize();
	}
}