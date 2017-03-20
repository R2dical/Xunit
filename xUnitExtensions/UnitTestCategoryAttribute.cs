namespace Xunit
{
	public class UnitTestCategoryAttribute : TestCategoryAttribute
	{
		public UnitTestCategoryAttribute()
			: base("Unit")
		{ }
	}
}
