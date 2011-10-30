namespace Testing.Commons.Serialization
{
	public interface IDeserializer
	{
		T Deserialize<T>(string toDeserialize);
	}
}