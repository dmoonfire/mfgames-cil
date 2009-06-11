#region Namespaces

using MfGames.Numerics;

using NUnit.Framework;

#endregion

namespace UnitTests
{
	/// <summary>
	/// Summary description for Point2Tests
	/// </summary>
	[TestFixture]
	public class Point2Tests
	{
		[Test]
		public void DefinedCreateInt32()
		{
			var point = new Point2<int>(7, 19);
			Assert.AreEqual(7, point.X);
			Assert.AreEqual(19, point.Y);
		}

		[Test]
		public void EmptyCreateInt32()
		{
			var point = new Point2<int>();
			Assert.AreEqual(0, point.X);
			Assert.AreEqual(0, point.Y);
		}
	}
}