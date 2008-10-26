using System;
using System.Drawing;

namespace MfGames.Numerics
{
	/// <summary>
	/// Generics-based 2D size and dimensions.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class Size2<T>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Size2&lt;T&gt;"/> class.
		/// </summary>
		public Size2()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Size2&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		public Size2(T width, T height)
		{
			this.width = width;
			this.height = height;
		}
		#endregion

		#region Dimensions
		private T width;
		private T height;

		/// <summary>
		/// Gets or sets the width dimension.
		/// </summary>
		/// <value>The width.</value>
		public T Width
		{
			get { return width; }
			set { width = value; }
		}

		/// <summary>
		/// Gets or sets the height dimension.
		/// </summary>
		/// <value>The height.</value>
		public T Height
		{
			get { return height; }
			set { height = value; }
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
			return String.Format("({0},{1}", width, height);
		}
		#endregion Conversions

		#region Operators
		/// <summary>
		/// Performs an implicit conversion from <see cref="MfGames.Numerics.Size2&lt;T&gt;"/> to <see cref="System.Drawing.Point"/>.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator Point(Size2<T> point)
		{
			return new Point(Convert.ToInt32(point.width), Convert.ToInt32(point.height));
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="MfGames.Numerics.Size2&lt;T&gt;"/> to <see cref="System.Drawing.PointF"/>.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator PointF(Size2<T> point)
		{
			return new PointF(Convert.ToSingle(point.width), Convert.ToSingle(point.height));
		}
		#endregion
	}
}
