using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MfGames.Numerics
{
	/// <summary>
	/// Extension methods for Int32 (regular int).
	/// </summary>
	public static class ExtensionsInt32
	{
		public static int RaiseToPower2(this Int32 originalValue)
		{
			double power = 0;
			int nextValue = originalValue;
			while (nextValue > System.Math.Pow(2.0, power))
				power++;
			return (int) System.Math.Pow(2.0, power);
		}
	}
}
