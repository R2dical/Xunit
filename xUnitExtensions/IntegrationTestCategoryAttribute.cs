namespace Xunit
{
	public class IntegrationTestCategoryAttribute : TestCategoryAttribute
	{
		public IntegrationTestCategoryAttribute()
			: base("Integration")
		{ }
	}
}
