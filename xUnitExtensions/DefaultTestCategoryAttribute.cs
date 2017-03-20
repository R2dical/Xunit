namespace Xunit
{
	public class DefaultTestCategoryAttribute : TestCategoryAttribute
	{
		public DefaultTestCategoryAttribute()
			: base("Default")
		{ }
	}
}
