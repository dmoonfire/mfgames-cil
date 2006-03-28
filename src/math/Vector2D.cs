/*
 * Copyright (C) 2005, Moonfire Games
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
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307
 * USA
 */

using System;

namespace MfGames.Utility
{
	/// <summary>
	/// A simple two-dimensional double vector.
	/// </summary>
    public class Vector2D
	{
#region Constructors
		/// <summary>
		/// Constructs a zero vector.
		/// </summary>
		public Vector2D()
		{
		}

		/// <summary>
		/// Constructs the vector from the given points.
		/// </summary>
        public Vector2D(double x, double y)
		{
        	this.X = x;
        	this.Y = y;
        }
        
		/// <summary>
		/// Constructs the vector from the list of doubles.
		/// </summary>
        public Vector2D(double[] v)
		{
        	if(v != null && v.Length == 2)
			{
        		this.X = v[0];
        		this.Y = v[1];
        	}
        }
#endregion

#region Operators
		/// <summary>
		/// Performs the additive operator on two vectors.
		/// </summary>
        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
        	return new Vector2D(v1.X + v2.X,
				v1.Y + v2.Y);
        }
        
		/// <summary>
		/// Multiplies a double against a vector.
		/// </summary>
        public static Vector2D operator *(Vector2D v1, double factor)
        {
        	return new Vector2D(
				v1.X * factor,
				v1.Y * factor);
        }

		/// <summary>
		/// Gets the dot product of two vectors.
		/// </summary>
        public static double Dot(Vector2D v1, Vector2D v2)
        {
        	return (v1.X * v2.X) + (v1.Y * v2.Y);
        }

		/// <summary>
		/// Converts to an array of doubles automatically.
		/// </summary>
		public double [] ToDoubleArray()
		{
			return new double [] { X, Y };
		}

		/// <summary>
		/// Converts to an array of floats automatically.
		/// </summary>
		public float [] ToFloatArray()
		{
			return new float [] { (float) X, (float) Y };
		}
#endregion

#region Properties
		/// <remarks>
		/// We make these public since the overhead for the property
		/// access will just increase the processing time.
		/// </remarks>
    	public double X, Y;
#endregion
	}
}
