#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System;
using System.Drawing;

#endregion

namespace MfGames.Numerics
{
	/// <summary>
	/// Generics-based 2D size and dimensions.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Size2<T>
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

		private T height;
		private T width;

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
		/// Converts the size to a different type.
		/// </summary>
		/// <typeparam name="T2">The second type to convert to.</typeparam>
		/// <returns></returns>
		public Size2<T2> ToSize2<T2>()
		{
			return new Size2<T2>((T2) Convert.ChangeType(width, typeof(T2)),
			                     (T2) Convert.ChangeType(height, typeof(T2)));
		}

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
			return new PointF(Convert.ToSingle(point.width),
			                  Convert.ToSingle(point.height));
		}

		#endregion
	}
}