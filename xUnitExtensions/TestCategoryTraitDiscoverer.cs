using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit
{
	public class TestCategoryTraitDiscoverer : ITraitDiscoverer
	{
		public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
		{
			var attributeInfo = traitAttribute as ReflectionAttributeInfo;
			var testCategoryAttribute = attributeInfo?.Attribute as TestCategoryAttribute;
			yield return new KeyValuePair<string, string>("TestCategory", testCategoryAttribute?.TestCategory ?? string.Empty);
		}
	}
}
