using System;

namespace MfGames.Numerics
{
	/// <summary>
	/// Additional mathematics and algorithims.
	/// </summary>
	public static class Mathematics
	{
		/// <summary>
		/// Returns the greatest common factory (GCF) of two integers.
		/// </summary>
		/// <param name="a">A non-zero integer.</param>
		/// <param name="b">A non-zero integer.</param>
		/// <returns></returns>
		public static int GreatestCommonFactor(int a, int b)
		{
			int high = Math.Max(a, b);
			int low = Math.Min(a, b);
			int tmp = high % low;

			while (tmp != 0)
			{
				high = low;
				low = tmp;
				tmp = high % low;
			}

			return low;
		}
	}
}
