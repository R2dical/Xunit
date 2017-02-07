using Microsoft.Extensions.FileSystemGlobbing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Xunit
{
	/// <summary>
	/// Represents a collection of data read from file(s), then optionally split again by a delimiter.
	/// </summary>
	sealed class FileData : IEnumerable<object[]>
	{
		readonly string[] _filePaths;
		readonly Encoding _encoding;
		readonly string[] _delimiters;
		readonly StringSplitOptions _stringSplitOptions;

		public const char GlobExcludeChar = '!';

		/// <summary>
		/// Create a new file text data collection from a set of paths.
		/// </summary>
		/// <param name="filePaths">An array of paths to read from.</param>
		public FileData(string[] filePaths)
		{
			_filePaths = filePaths;
		}

		/// <summary>
		/// Create a new file text data collection from a single path.
		/// </summary>
		/// <param name="filePath">A path to read from.</param>
		public FileData(string filePath)
			: this(new[] { filePath })
		{ }

		/// <summary>
		/// Create a new file text data collection from a set of glob paths.
		/// </summary>
		/// <param name="rootPath">The root directory to use.</param>
		/// <param name="globPatterns">An array of relative glob path patterns to read from.</param>
		public FileData(string rootPath, string[] globPatterns)
			: this(GetFilePathsFromGlobPatterns(rootPath, globPatterns))
		{ }

		/// <summary>
		/// Create a new file text data collection from a set of paths and delimiters.
		/// </summary>
		/// <param name="filePaths">An array of paths to read from.</param>
		/// <param name="delimiters">The delimiters to split the file text data.</param>
		/// <param name="stringSplitOptions">Split options for the delimiters.</param>
		public FileData(string[] filePaths, string[] delimiters, StringSplitOptions stringSplitOptions)
			: this(filePaths)
		{
			_delimiters = delimiters;
			_stringSplitOptions = stringSplitOptions;
		}

		/// <summary>
		/// Create a new file text data collection from a path and delimiters.
		/// </summary>
		/// <param name="filePath">A path to read from.</param>
		/// <param name="delimiters">The delimiters to split the file text data.</param>
		/// <param name="stringSplitOptions">Split options for the delimiters.</param>
		public FileData(string filePath, string[] delimiters, StringSplitOptions stringSplitOptions)
			: this(new[] { filePath }, delimiters, stringSplitOptions)
		{ }

		/// <summary>
		/// Create a new file text data collection from a set of glob paths and delimiters.
		/// </summary>
		/// <param name="rootPath">The root directory to use.</param>
		/// <param name="globPatterns">An array of relative glob path patterns to read from.</param>
		/// <param name="delimiters">The delimiters to split the file text data.</param>
		/// <param name="stringSplitOptions">Split options for the delimiters.</param>
		public FileData(string rootPath, string[] globPatterns, string[] delimiters, StringSplitOptions stringSplitOptions)
			: this(GetFilePathsFromGlobPatterns(rootPath, globPatterns))
		{
			_delimiters = delimiters;
			_stringSplitOptions = stringSplitOptions;
		}

		public IEnumerator<object[]> GetEnumerator() => GetData(_filePaths, _delimiters, _stringSplitOptions).GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		static IEnumerable<object[]> GetData(string[] filePaths, string[] delimiters, StringSplitOptions stringSplitOptions)
		{
			foreach (var filePath in filePaths)
			{
				yield return delimiters != null
					? File.ReadAllText(filePath).Split(delimiters, stringSplitOptions) as object[]
					: new[] { File.ReadAllText(filePath) } as object[];
			}
		}

		static string[] GetFilePathsFromGlobPatterns(string rootPath, string[] globPatterns)
		{
			var matcher = new Matcher(StringComparison.CurrentCultureIgnoreCase);
			matcher.AddExcludePatterns(globPatterns.Where(globPattern => globPattern.StartsWith(GlobExcludeChar.ToString())).Select(globPattern => globPattern.TrimStart(GlobExcludeChar)));
			matcher.AddIncludePatterns(globPatterns.Where(globPattern => !globPattern.StartsWith(GlobExcludeChar.ToString())));
			return matcher.GetResultsInFullPath(rootPath).ToArray();
		}
	}
}