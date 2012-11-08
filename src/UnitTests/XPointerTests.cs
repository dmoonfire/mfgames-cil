// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using MfGames.Xml;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class XPointerTests
	{
		[Test]
		public void TestFramework()
		{
		}

		[Test]
		public void ParseXPointer()
		{
			// Arrange
			const string input = "xpointer(//bob)";

			// Act
			var xpointer = new XPointerInfo(input);
		}
	}
}