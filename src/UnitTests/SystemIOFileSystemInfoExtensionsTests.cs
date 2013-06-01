// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System.IO;
using MfGames.Extensions.System.IO;
using NUnit.Framework;

namespace UnitTests
{
	/// <summary>
	/// Exercises the functionality of the extension classes.
	/// </summary>
	[TestFixture]
	public class SystemIOFileSystemInfoExtensionsTests
	{
		#region Methods

		[Test]
		public void TestFramework()
		{
		}

		[Test]
		public void TestRelativePath()
		{
			// Arrange
			var file1 = new FileInfo(Path.Combine("Temp", "bob.jpg"));
			var file2 = new FileInfo(Path.Combine("Program Files", "gary.jpg"));

			// Act
			string relative = file1.GetRelativePathTo(file2);

			// Assert
			Assert.AreEqual(@"..\..\Temp\bob.jpg", relative);
		}

		#endregion
	}
}
