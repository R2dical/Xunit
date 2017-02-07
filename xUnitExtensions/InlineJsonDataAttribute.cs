using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Xunit
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class InlineJsonDataAttribute : DataAttribute
	{
		readonly string[] _jsonData;
		readonly JsonSerializerSettings _jsonSerializerSettings;

		public InlineJsonDataAttribute(params string[] jsonData)
		{
			_jsonData = jsonData;
		}

		public InlineJsonDataAttribute(Type jsonSerializerSettingsType, params string[] jsonData)
			: this(jsonData)
		{
			_jsonSerializerSettings = Activator.CreateInstance(jsonSerializerSettingsType) as JsonSerializerSettings;
		}

		public override IEnumerable<object[]> GetData(MethodInfo testMethod)
		{
			var testMethodParameters = testMethod.GetParameters();
			if (_jsonData.Length != testMethodParameters.Length)
				throw new XunitException("Json data length does not match method parameters length");

			yield return _jsonData.Zip(testMethodParameters, (data, parameterInfo) =>
			{
				if (parameterInfo.ParameterType.Equals(typeof(string)))
					return data;

				if (_jsonSerializerSettings != null)
					return JsonConvert.DeserializeObject(data, parameterInfo.ParameterType, _jsonSerializerSettings);
				else
					return JsonConvert.DeserializeObject(data, parameterInfo.ParameterType);
			}).ToArray();
		}
	}
}