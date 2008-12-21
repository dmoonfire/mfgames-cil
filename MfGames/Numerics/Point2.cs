using System;
using System.Drawing;

namespace MfGames.Numerics
{
	/// <summary>
	/// Generics-based 2D coordinates.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class Point2<T>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Point2&lt;T&gt;"/> class.
		/// </summary>
		public Point2()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Point2&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public Point2(T x, T y)
		{
			this.x = x;
			this.y = y;
		}
		#endregion

		#region Coordinates
		private T x;
		private T y;

		/// <summary>
		/// Gets or sets the X coordinate.
		/// </summary>
		/// <value>The X.</value>
		public T X
		{
			get
			{
				return x;
			}
			set
			{
				x = value;
			}
		}

		/// <summary>
		/// Gets or sets the Y coordinate.
		/// </summary>
		/// <value>The Y.</value>
		public T Y
		{
			get
			{
				return y;
			}
			set
			{
				y = value;
			}
		}
		#endregion

		#region Conversions
		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
			return String.Format("({0},{1}", x, y);
		}
		#endregion Conversions

		#region Operators
		/// <summary>
		/// Performs an implicit conversion from <see cref="MfGames.Numerics.Point2&lt;T&gt;"/> to <see cref="System.Drawing.Point"/>.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator Point(Point2<T> point)
		{
			return new Point(Convert.ToInt32(point.x), Convert.ToInt32(point.y));
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="MfGames.Numerics.Point2&lt;T&gt;"/> to <see cref="System.Drawing.PointF"/>.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator PointF(Point2<T> point)
		{
			return new PointF(Convert.ToSingle(point.x), Convert.ToSingle(point.y));
		}
		#endregion
	}
}
