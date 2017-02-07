using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xunit.Google.Sheets
{
	public class GoogleSheetDataAttributeTests
	{
		const string API_KEY = "AIzaSyB8Tf54Xd1AQbUkVz0a5DLPq1pClEHpJvU";
		const string SPREADSHEET_ID = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";

		[Fact]
		public void GoogleSheetDataAttributeTest()
		{
			Assert.NotNull(new GoogleSheetDataAttribute(API_KEY, SPREADSHEET_ID, "Class Data!A2:E"));
		}

		[Theory]
		[InlineData(API_KEY, SPREADSHEET_ID, "Class Data!A2:E", 30, 5)]
		public void GetDataTest_SingleRange(string apiKey, string spreadsheetId, string range, int expectedColumns, int expectedRows)
		{
			var googleSheetDataAttribute = new GoogleSheetDataAttribute(apiKey, spreadsheetId, range);
			var actual = googleSheetDataAttribute.GetData(new Mock<MethodInfo>().Object);

			Assert.Equal(expectedColumns, actual.Count());
			Assert.Equal(expectedRows, actual.FirstOrDefault()?.Count() ?? 0);
		}

		[Theory]
		[InlineData(API_KEY, SPREADSHEET_ID, new[] { "Class Data!A2:E16", "Class Data!A17:E31" }, 30, 5)]
		public void GetDataTest_MultipleRange(string apiKey, string spreadsheetId, IEnumerable<string> ranges, int expectedColumns, int expectedRows)
		{
			var googleSheetDataAttribute = new GoogleSheetDataAttribute(apiKey, spreadsheetId, ranges);
			var actual = googleSheetDataAttribute.GetData(new Mock<MethodInfo>().Object);

			Assert.Equal(expectedColumns, actual.Count());
			Assert.Equal(expectedRows, actual.FirstOrDefault()?.Count() ?? 0);
		}
	}
	public class Test
	{
		const string API_KEY = "AIzaSyB8Tf54Xd1AQbUkVz0a5DLPq1pClEHpJvU";
		const string SPREADSHEET_ID = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";

		[Theory]
		[GoogleSheetData(API_KEY, SPREADSHEET_ID, "Class Data!A2:E")]
		public void Test1(string studentName, string gender, string classLevel, string homeState, string major)
		{
			//https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit#gid=0
		}
	}
}