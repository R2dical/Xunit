using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Xunit.Sdk;

namespace Xunit
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class NewExcelDataAttribute : DataAttribute
	{
		readonly string _filePath;
		readonly int _sheetIndex;

		public NewExcelDataAttribute(string filePath, int sheetIndex)
		{
			_filePath = filePath;
			_sheetIndex = sheetIndex;
		}

		public override IEnumerable<object[]> GetData(MethodInfo testMethod)
		{
			Application excel = null;
			Workbooks workbooks = null;
			Workbook workbook = null;
			Sheets sheets = null;
			Worksheet worksheet = null;
			Range usedRange = null;

			try
			{
				excel = new Application();
				workbooks = excel.Workbooks;
				workbook = workbooks.Open(_filePath, ReadOnly: true);
				if (workbook == null)
					throw new XunitException($"Failed to open workbook '{_filePath}'.");
				sheets = workbook.Sheets;
				if (_sheetIndex <= 0 || _sheetIndex > sheets.Count)
					throw new ArgumentOutOfRangeException(nameof(_sheetIndex));
				worksheet = sheets[_sheetIndex];
				usedRange = worksheet.UsedRange;

				var valueArray = usedRange.get_Value(XlRangeValueDataType.xlRangeValueDefault) as object[,];

				for (int rowIndex = 1; rowIndex <= valueArray.GetLength(0); rowIndex++)
				{
					var row = new List<object>(valueArray.GetLength(1));
					for (int colIndex = 1; colIndex <= valueArray.GetLength(1); colIndex++)
						row.Add(valueArray[rowIndex, colIndex]);
					yield return row.ToArray();
				}
			}
			finally
			{
				if (usedRange != null)
				{
					Marshal.ReleaseComObject(usedRange);
					usedRange = null;
				}
				if (worksheet != null)
				{
					Marshal.ReleaseComObject(worksheet);
					worksheet = null;
				}
				if (sheets != null)
				{
					Marshal.ReleaseComObject(sheets);
					sheets = null;
				}
				if (workbook != null)
				{
					workbook.Close(false, _filePath);
					Marshal.ReleaseComObject(workbook);
					workbook = null;
				}
				if (workbooks != null)
				{
					Marshal.ReleaseComObject(workbooks);
					workbooks = null;
				}
				if (excel != null)
				{
					excel.Quit();
					Marshal.ReleaseComObject(excel);
					excel = null;
				}
			}
		}
	}
}