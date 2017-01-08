using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Xunit.Google.Sheets
{
	/// <summary>
	/// Provides a data source for a data theory, with the data coming from a Google sheets spreadsheet.
	/// </summary>
	/// <example>
	/// https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit#gid=0
	/// </example>
	public sealed class GoogleSheetDataAttribute : DataAttribute
	{
		readonly Lazy<IEnumerable<object[]>> _data;

		/// <summary>
		/// Initializes a new instance of the Xunit.Google.Sheets.GoogleSheetDataAttribute class.
		/// </summary>
		/// <param name="apiKey">The Google developer API key. Eg. 'AIzaFyB8Tf54Xd1AQaUkVz0a5DLPq1pClEHpJvZ'. See https://developers.google.com/sheets/api/guides/authorizing#APIKey .</param>
		/// <param name="spreadsheetId">The id of the spreadsheet. Eg '1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms'.</param>
		/// <param name="range">The range of cells to get data from. Eg 'Class Data!A2:E'</param>
		/// <param name="majorDimension">The major dimension that results should use.</param>
		/// <param name="valueRenderOption">How values should be represented in the output.</param>
		/// <param name="dateTimeRenderOption">How dates, times, and durations should be represented in the output. This is ignored if valueRenderOption is FORMATTED_VALUE.</param>
		public GoogleSheetDataAttribute(string apiKey, string spreadsheetId, string range, MajorDimension majorDimension = MajorDimension.DIMENSIONUNSPECIFIED, ValueRenderOption valueRenderOption = ValueRenderOption.FORMATTEDVALUE, DateTimeRenderOption dateTimeRenderOption = DateTimeRenderOption.SERIALNUMBER)
		{
			_data = new Lazy<IEnumerable<object[]>>(() =>
			{
				using (var service = new SheetsService(new BaseClientService.Initializer
				{
					ApiKey = apiKey,
					ApplicationName = "Xunit.Google.Sheets",
				}))
				{
					var request = service.Spreadsheets.Values.Get(spreadsheetId, range);
					request.MajorDimension = (SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum?)majorDimension;
					request.ValueRenderOption = (SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum?)valueRenderOption;
					request.DateTimeRenderOption = (SpreadsheetsResource.ValuesResource.GetRequest.DateTimeRenderOptionEnum?)dateTimeRenderOption;
					var response = request.Execute();
					return response.Values.Select(value => value.ToArray());
				}
			});
		}

		/// <summary>
		/// Initializes a new instance of the Xunit.Google.Sheets.GoogleSheetDataAttribute class.
		/// </summary>
		/// <param name="apiKey">The Google developer API key. Eg. 'AIzaFyB8Tf54Xd1AQaUkVz0a5DLPq1pClEHpJvZ'. See https://developers.google.com/sheets/api/guides/authorizing#APIKey .</param>
		/// <param name="spreadsheetId">The id of the spreadsheet. Eg '1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms'.</param>
		/// <param name="ranges">Multiple range of cells to get data from. Eg [ 'Class Data!A2:E', 'Class Data!A42:E' ]</param>
		/// <param name="majorDimension">The major dimension that results should use.</param>
		/// <param name="valueRenderOption">How values should be represented in the output.</param>
		/// <param name="dateTimeRenderOption">How dates, times, and durations should be represented in the output. This is ignored if valueRenderOption is FORMATTED_VALUE.</param>
		public GoogleSheetDataAttribute(string apiKey, string spreadsheetId, IEnumerable<string> ranges, MajorDimension majorDimension = MajorDimension.DIMENSIONUNSPECIFIED, ValueRenderOption valueRenderOption = ValueRenderOption.FORMATTEDVALUE, DateTimeRenderOption dateTimeRenderOption = DateTimeRenderOption.SERIALNUMBER)
		{
			_data = new Lazy<IEnumerable<object[]>>(() =>
			{
				using (var service = new SheetsService(new BaseClientService.Initializer
				{
					ApiKey = apiKey,
					ApplicationName = "Xunit.Google.Sheets",
				}))
				{
					var request = service.Spreadsheets.Values.BatchGet(spreadsheetId);
					request.MajorDimension = (SpreadsheetsResource.ValuesResource.BatchGetRequest.MajorDimensionEnum?)majorDimension;
					request.ValueRenderOption = (SpreadsheetsResource.ValuesResource.BatchGetRequest.ValueRenderOptionEnum?)valueRenderOption;
					request.DateTimeRenderOption = (SpreadsheetsResource.ValuesResource.BatchGetRequest.DateTimeRenderOptionEnum?)dateTimeRenderOption;
					request.Ranges = new Repeatable<string>(ranges);
					var response = request.Execute();
					return response.ValueRanges.SelectMany(valueRange => valueRange.Values.Select(value => value.ToArray()));
				}
			});
		}

		public override IEnumerable<object[]> GetData(MethodInfo testMethod) => _data.Value;
	}
}
