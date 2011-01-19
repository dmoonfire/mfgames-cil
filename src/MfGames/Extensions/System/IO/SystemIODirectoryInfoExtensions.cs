#region Copyright and License

// Copyright (c) 2005-2011, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System.IO;

#endregion

namespace MfGames.Extensions.System.IO
{
	/// <summary>
	/// Defines various extensions to System.IO.DirectoryInfo.
	/// </summary>
	public static class SystemIODirectoryInfoExtensions
	{
		/// <summary>
		/// Gets the directory info from the directory.
		/// </summary>
		/// <param name="directory">The directory.</param>
		/// <param name="dirname">The dirname.</param>
		/// <returns></returns>
		public static DirectoryInfo GetDirectoryInfo(
			this DirectoryInfo directory,
			string dirname)
		{
			return new DirectoryInfo(Path.Combine(directory.FullName, dirname));
		}

		/// <summary>
		/// Gets a child FileInfo object from the directory.
		/// </summary>
		/// <param name="directory">The directory.</param>
		/// <param name="filename">The filename.</param>
		/// <returns></returns>
		public static FileInfo GetFileInfo(
			this DirectoryInfo directory,
			string filename)
		{
			return new FileInfo(Path.Combine(directory.FullName, filename));
		}
	}
}