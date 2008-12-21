namespace MfGames.Numerics
{
	/// <summary>
	/// Defines a 1D numeric range.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRange1<T>
	{
		#region Bounds
		/// <summary>
		/// Gets the maximum value for this numeric range.
		/// </summary>
		/// <value>The maximum.</value>
		T Maximum {
			get;
		}

		/// <summary>
		/// Gets the minimum value for this range.
		/// </summary>
		/// <value>The minimum.</value>
		T Minimum {
			get;
		}
		#endregion Bounds

		#region Selection
		/// <summary>
		/// Gets a random number that is valid for this range.
		/// </summary>
		/// <returns></returns>
		T GetRandom();
		#endregion Selection
	}
}
