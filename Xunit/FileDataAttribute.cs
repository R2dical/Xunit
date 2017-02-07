using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Xunit
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class FileDataAttribute : DataAttribute
	{
		readonly FileData _fileData;
		readonly object[] _data;

		public FileDataAttribute(string[] filePaths, params object[] data)
			: this(new FileData(filePaths), data)
		{ }

		public FileDataAttribute(string filePath, params object[] data)
			: this(new FileData(filePath), data)
		{ }

		public FileDataAttribute(string rootPath, string[] globPatterns, params object[] data)
			: this(new FileData(rootPath, globPatterns), data)
		{ }

		public FileDataAttribute(string[] filePaths, string[] delimiters, StringSplitOptions stringSplitOptions, params object[] data)
			: this(new FileData(filePaths, delimiters, stringSplitOptions), data)
		{ }

		public FileDataAttribute(string filePath, string[] delimiters, StringSplitOptions stringSplitOptions, params object[] data)
			: this(new FileData(filePath, delimiters, stringSplitOptions), data)
		{ }

		public FileDataAttribute(string rootPath, string[] globPatterns, string[] delimiters, StringSplitOptions stringSplitOptions, params object[] data)
			: this(new FileData(rootPath, globPatterns, delimiters, stringSplitOptions), data)
		{ }

		private FileDataAttribute(FileData fileData, object[] data)
		{
			_fileData = fileData;
			_data = data;
		}

		public override IEnumerable<object[]> GetData(MethodInfo testMethod)
		{
			if (testMethod == null)
				throw new ArgumentNullException(nameof(testMethod));

			foreach (var fileData in _fileData)
				yield return fileData.Concat(_data).ToArray();
		}
	}
}
