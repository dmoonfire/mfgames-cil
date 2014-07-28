// <copyright file="XPointerTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace UnitTests
{
    using MfGames.Xml;

    using NUnit.Framework;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class XPointerTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        [Test]
        public void ParseXPointer()
        {
            // Arrange
            const string input = "xpointer(//bob)";

            // Act
            var xpointer = new XPointerInfo(input);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void TestFramework()
        {
        }

        #endregion
    }
}