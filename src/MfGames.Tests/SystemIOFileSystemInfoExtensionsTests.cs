// <copyright file="SystemIOFileSystemInfoExtensionsTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace UnitTests
{
    using System.IO;

    using MfGames.Extensions.System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Exercises the functionality of the extension classes.
    /// </summary>
    [TestFixture]
    public class SystemIOFileSystemInfoExtensionsTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        [Test]
        public void TestFramework()
        {
        }

        /// <summary>
        /// </summary>
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