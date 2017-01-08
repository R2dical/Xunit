namespace Xunit.Google.Sheets
{
	/// <summary>
	/// The major dimension that results should use. For example, if the spreadsheet
	/// data is: `A1=1,B1=2,A2=3,B2=4`, then requesting `range=A1:B2,majorDimension=ROWS`
	/// will return `[[1,2],[3,4]]`, whereas requesting `range=A1:B2,majorDimension=COLUMNS`
	/// will return `[[1,3],[2,4]]`.
	/// </summary>
	public enum MajorDimension
	{
		DIMENSIONUNSPECIFIED = 0,
		ROWS = 1,
		COLUMNS = 2
	}
}