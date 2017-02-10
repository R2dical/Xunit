using Microsoft.Extensions.FileSystemGlobbing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit.Sdk;

namespace Xunit
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class FileDataAttribute : DataAttribute
	{
		const char GLOB_EXCLUDE_CHAR = '!';

		readonly object[] _data;
		readonly string[] _filePaths;
		readonly Encoding _encoding;
		readonly string[] _delimiters;
		readonly StringSplitOptions _stringSplitOptions;

		public FileDataAttribute(string rootPath, string[] globPatterns, string[] delimiters, StringSplitOptions stringSplitOptions, params object[] data)
			: this(GetFilePathsFromGlobPatterns(rootPath, globPatterns), delimiters, stringSplitOptions, data)
		{ }

		public FileDataAttribute(string filePath, string[] delimiters, StringSplitOptions stringSplitOptions, params object[] data)
			: this(new[] { filePath }, delimiters, stringSplitOptions, data)
		{ }

		public FileDataAttribute(string[] filePaths, string[] delimiters, StringSplitOptions stringSplitOptions, params object[] data)
			: this(delimiters, stringSplitOptions, data)
		{
			_filePaths = filePaths;
		}

		public FileDataAttribute(string rootPath, string[] globPatterns, params object[] data)
			: this(GetFilePathsFromGlobPatterns(rootPath, globPatterns), data)
		{ }

		public FileDataAttribute(string[] filePaths, params object[] data)
			: this(data)
		{
			_filePaths = filePaths;
		}

		public FileDataAttribute(string filePath, params object[] data)
			: this(new[] { filePath }, data)
		{ }

		private FileDataAttribute(string[] delimiters, StringSplitOptions stringSplitOptions, object[] data)
			: this(data)
		{
			_delimiters = delimiters;
			_stringSplitOptions = stringSplitOptions;
		}

		private FileDataAttribute(object[] data)
		{
			_data = data;
		}

		public override IEnumerable<object[]> GetData(MethodInfo testMethod)
		{
			if (testMethod == null)
				throw new ArgumentNullException(nameof(testMethod));

			foreach (var filePath in _filePaths)
			{
				yield return _delimiters != null
					? File.ReadAllText(filePath).Split(_delimiters, _stringSplitOptions).Concat(_data).ToArray()
					: new[] { File.ReadAllText(filePath) }.Concat(_data).ToArray();
			}
		}

		static string[] GetFilePathsFromGlobPatterns(string rootPath, string[] globPatterns)
		{
			var matcher = new Matcher(StringComparison.CurrentCultureIgnoreCase);
			matcher.AddExcludePatterns(globPatterns.Where(globPattern => globPattern.StartsWith(GLOB_EXCLUDE_CHAR.ToString())).Select(globPattern => globPattern.TrimStart(GLOB_EXCLUDE_CHAR)));
			matcher.AddIncludePatterns(globPatterns.Where(globPattern => !globPattern.StartsWith(GLOB_EXCLUDE_CHAR.ToString())));
			return matcher.GetResultsInFullPath(rootPath).ToArray();
		}
	}
}
