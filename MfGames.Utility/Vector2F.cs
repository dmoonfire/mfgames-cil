#region Copyright
/*
 * Copyright (C) 2005-2008, Moonfire Games
 *
 * This file is part of MfGames.Utility.
 *
 * The MfGames.Utility library is free software; you can redistribute
 * it and/or modify it under the terms of the GNU Lesser General
 * Public License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

/*
 * WARNING:
 * This class is dynamically generated. C# doesn't allow for generics
 * with value types, such as float or int, so this creates one custom
 * class for double, float, and int. The source file is Vector2.cs.in.
 */

using System;
using System.Drawing;

namespace MfGames.Utility
{
	/// <summary>
	/// A simple two-dimensional float vector.
	/// </summary>
    public class Vector2F
	{
		#region Constructors
		/// <summary>
		/// Constructs a zero vector.
		/// </summary>
		public Vector2F()
		{
		}

		/// <summary>
		/// Constructs the vector from the given points.
		/// </summary>
        public Vector2F(float x, float y)
		{
        	this.X = x;
        	this.Y = y;
        }
        
		/// <summary>
		/// Constructs the vector from the given Vector2F.
		/// </summary>
        public Vector2F(Vector2F v)
		{
        	this.X = v.X;
        	this.Y = v.Y;
        }
        
		/// <summary>
		/// Constructs the vector from the given Vector3F.
		/// </summary>
        public Vector2F(Vector3F v)
		{
        	this.X = v.X;
        	this.Y = v.Y;
        }
        
		/// <summary>
		/// Constructs the vector from the list of floats.
		/// </summary>
        public Vector2F(float [] v)
		{
        	if(v != null && v.Length == 2)
			{
        		this.X = v[0];
        		this.Y = v[1];
        	}
        }

		/// <summary>
		/// Constructs the vector from the list of floats.
		/// </summary>
        public Vector2F(PointF p)
		{
			this.X = (float) p.X;
			this.Y = (float) p.Y;
        }
		#endregion

		#region Operators
		/// <summary>
		/// Performs the additive operator on two vectors.
		/// </summary>
        public static Vector2F operator +(Vector2F v1, Vector2F v2)
        {
        	return new Vector2F(v1.X + v2.X, v1.Y + v2.Y);
        }
        
		/// <summary>
		/// Multiplies a float against a vector.
		/// </summary>
        public static Vector2F operator *(Vector2F v1, float factor)
        {
        	return new Vector2F(v1.X * factor, v1.Y * factor);
        }

		/// <summary>
		/// Gets the dot product of two vectors.
		/// </summary>
        public static float Dot(Vector2F v1, Vector2F v2)
        {
        	return (v1.X * v2.X) + (v1.Y * v2.Y);
        }

		/// <summary>
		/// Converts to an array of floats automatically.
		/// </summary>
		public double [] ToDoubleArray()
		{
			return new double [] { (double) X, (double) Y };
		}

		/// <summary>
		/// Converts to an array of floats automatically.
		/// </summary>
		public float [] ToFloatArray()
		{
			return new float [] { (float) X, (float) Y };
		}

		/// <summary>
		/// Converts to an array of floats automatically.
		/// </summary>
		public int [] ToIntArray()
		{
			return new int [] { (int) X, (int) Y };
		}

		#endregion

		#region Properties
		/// <remarks>
		/// We make these public since the overhead for the property
		/// access will just increase the processing time.
		/// </remarks>
    	public float X, Y;

		/// <summary>
		/// Returns the length of the vector.
		/// </summary>
		public float Length
		{
			get
			{
				return (float) Math.Sqrt(X * X + Y * Y);
			}
		}
		#endregion
	}
}
