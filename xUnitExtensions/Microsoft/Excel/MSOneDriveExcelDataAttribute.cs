using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Xunit.Microsoft.Excel
{
	public sealed class OneDriveExcelDataAttribute : DataAttribute
	{
		public override IEnumerable<object[]> GetData(MethodInfo testMethod)
		{
			throw new NotImplementedException();
		}
	}
}
