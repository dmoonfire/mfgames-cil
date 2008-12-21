using System.Collections.Generic;
using System.Linq;
using System.Text;

using MfGames.Utility;

using Convert=System.Convert;

namespace MfGames.Numerics
{
	/// <summary>
	/// Implements a simple linear range.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LinearRange1<T>
		: IRange1<T>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="LinearRange1&lt;T&gt;"/> class.
		/// </summary>
		public LinearRange1()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LinearRange1&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="min">The min.</param>
		/// <param name="max">The max.</param>
		public LinearRange1(T min, T max)
		{
			Maximum = max;
			Minimum = min;
		}
		#endregion Constructors

		#region Bounds
		/// <summary>
		/// Gets the maximum value for this numeric range.
		/// </summary>
		/// <value>The maximum.</value>
		public T Maximum {
			get;
			set;
		}

		/// <summary>
		/// Gets the minimum value for this range.
		/// </summary>
		/// <value>The minimum.</value>
		public T Minimum {
			get;
			set;
		}
		#endregion Bounds

		#region Selection
		/// <summary>
		/// Gets a random number that is valid for this range.
		/// </summary>
		/// <returns></returns>
		public T GetRandom()
		{
			double d1 = Convert.ToDouble(Minimum);
			double d2 = Convert.ToDouble(Maximum);
			double r = Entropy.NextDouble(d1, d2);
			return (T) Convert.ChangeType(r, typeof(T));
		}
		#endregion Selection
	}
}
