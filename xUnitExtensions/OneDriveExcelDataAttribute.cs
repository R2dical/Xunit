using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Xunit
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class OneDriveExcelDataAttribute : DataAttribute
	{
		public OneDriveExcelDataAttribute(string url, string queryString)
		{
			throw new NotImplementedException();
		}

		public override IEnumerable<object[]> GetData(MethodInfo testMethod)
		{
			throw new NotImplementedException();
		}
	}
}