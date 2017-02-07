namespace Xunit.Google.Sheets
{
	/// <summary>
	/// How values should be represented in the output. The default render option is
	/// ValueRenderOption.FORMATTED_VALUE.
	/// </summary>
	public enum ValueRenderOption
	{
		FORMATTEDVALUE = 0,
		UNFORMATTEDVALUE = 1,
		FORMULA = 2
	}
}