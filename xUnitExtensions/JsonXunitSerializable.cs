using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace Xunit
{
	public class JsonXunitSerializable<T> : IXunitSerializable where T : new()
	{
		T _obj;

		public JsonXunitSerializable(T obj)
		{
			_obj = obj;
		}

		public void Deserialize(IXunitSerializationInfo info)
		{
			var jObject = new JObject();
			foreach (var item in JObject.FromObject(_obj))
				jObject.Add(item.Key, info.GetValue<string>(item.Key));
			_obj = jObject.ToObject<T>();
		}

		public void Serialize(IXunitSerializationInfo info)
		{
			foreach (var item in JObject.FromObject(_obj))
				info.AddValue(item.Key, item.Value.ToString(), typeof(string));
		}

		public static implicit operator T(JsonXunitSerializable<T> xunitSerializable)
		{
			return xunitSerializable._obj;
		}

		public static implicit operator JsonXunitSerializable<T>(T obj)
		{
			return new JsonXunitSerializable<T>(new T());
		}
	}
}
