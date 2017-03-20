using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Linq;

namespace Xunit
{
	//public class ExcelDataAttribute
	//{
	//	public ExcelDataAttribute(string filepath, string sheetName, string addressName)
	//	{
	//		using (var spreadsheetDocument = SpreadsheetDocument.Open(filepath, false))
	//		{
	//			// Retrieve a reference to the workbook part.
	//			var workbookPart = spreadsheetDocument.WorkbookPart;

	//			// Find the sheet with the supplied name, and then use that 
	//			// Sheet object to retrieve a reference to the first worksheet.
	//			var sheet = workbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName).FirstOrDefault();

	//			// Throw an exception if there is no sheet.
	//			if (sheet == null)
	//				throw new ArgumentException("sheetName");

	//			// Retrieve a reference to the worksheet part.
	//			var worksheetPart = workbookPart.GetPartById(sheet.Id) as WorksheetPart;

	//			// For shared strings, look up the value in the
	//			// shared strings table.
	//			var sharedStringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
	//			// If the shared string table is missing, something 
	//			// is wrong. Return the index that is in
	//			// the cell. Otherwise, look up the correct text in 
	//			// the table.
	//			if (sharedStringTablePart == null)
	//				throw new Exception();

	//			// Use its Worksheet property to get a reference to the cell 
	//			// whose address matches the address you supplied.
	//			var cell = worksheetPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference == addressName).FirstOrDefault();

	//		}
	//	}

	//	static object ParseCell(Cell cell, SharedStringTablePart sharedStringTablePart)
	//	{
	//		string value = null;
	//		// If the cell does not exist, return an empty string.
	//		if (cell == null)
	//		{
	//			value = cell.InnerText;

	//			// If the cell represents an integer number, you are done. 
	//			// For dates, this code returns the serialized value that 
	//			// represents the date. The code handles strings and 
	//			// Booleans individually. For shared strings, the code 
	//			// looks up the corresponding value in the shared string 
	//			// table. For Booleans, the code converts the value into 
	//			// the words TRUE or FALSE.
	//			if (cell.DataType != null)
	//			{
	//				switch (cell.DataType.Value)
	//				{
	//					case CellValues.Boolean:
	//						return value == "1";
	//					case CellValues.Number:
	//						break;
	//					case CellValues.Error:
	//						break;
	//					case CellValues.SharedString:
	//						value = sharedStringTablePart.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
	//						break;
	//					case CellValues.String:
	//						break;
	//					case CellValues.InlineString:
	//						break;
	//					case CellValues.Date:
	//						break;
	//					default:
	//						break;
	//				}
	//			}
	//		}
	//	}
	//}
}
