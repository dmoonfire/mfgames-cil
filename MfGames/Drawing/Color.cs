using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MfGames.Drawing
{
	public class Color<T>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Color&lt;T&gt;"/> class.
		/// </summary>
		public Color()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Color&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="a">A.</param>
		/// <param name="r">The r.</param>
		/// <param name="g">The g.</param>
		/// <param name="b">The b.</param>
		public Color(T a, T r, T g, T b)
		{
			this.a = a;
			this.r = r;
			this.g = g;
			this.b = b;
		}
		#endregion Constructors

		#region Color Channels
		private T r, g, b, a;

		public T A
		{
			get { return a; }
			set { a = value; }
		}

		public T B
		{
			get { return b; }
			set { b = value; }
		}

		public T G
		{
			get { return g; }
			set { g = value; }
		}

		public T R
		{
			get { return r; }
			set { r = value; }
		}
		#endregion Color Channels

		#region Opacity
		/// <summary>
		/// Gets or sets the opacity of the color channel.
		/// </summary>
		/// <value>The opacity.</value>
		public double Opacity
		{
			get
			{
				if (typeof(T) == typeof(byte))
					return Convert.ToDouble(A) / 255.0;
				return Convert.ToDouble(A);
			}

			set
			{
				if (typeof(T) == typeof(byte))
					A = (T) Convert.ChangeType(value * 255, typeof(T));
				else
					A = (T) Convert.ChangeType(value, typeof(T));
			}
		}
		#endregion
	}
}
