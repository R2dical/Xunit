﻿using System;
using Xunit.Sdk;

namespace Xunit
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	[TraitDiscoverer("Xunit.TestCategoryTraitDiscoverer", "xUnitExtensions.Core")]
	public abstract class TestCategoryAttribute : Attribute, ITraitAttribute
	{
		public string TestCategory { get; }

		protected TestCategoryAttribute(string testCategory)
		{
			TestCategory = testCategory;
		}
	}
}
