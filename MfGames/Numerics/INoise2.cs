using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MfGames.Numerics
{
	/// <summary>
	/// Defines the common interface of 2D noise generation routines.
	/// </summary>
	public interface INoise2
	{
		/// <summary>
		/// Gets the noise for the given X and Y coordinate.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns></returns>
		double GetNoise(int x, int y);
	}
}
