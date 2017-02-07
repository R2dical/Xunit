namespace Xunit.Google.Sheets
{
	/// <summary>
	/// How dates, times, and durations should be represented in the output. This is
	/// ignored if value_render_option is FORMATTED_VALUE.The default dateTime render
	/// option is [DateTimeRenderOption.SERIAL_NUMBER].
	/// </summary>
	public enum DateTimeRenderOption
	{
		SERIALNUMBER = 0,
		FORMATTEDSTRING = 1
	}
}