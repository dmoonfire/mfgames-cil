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
	/// A simple three-dimensional double vector.
	/// </summary>
    public class Vector3D
	{
#region Constructors
		/// <summary>
		/// Constructs a zero vector.
		/// </summary>
		public Vector3D()
		{
		}

		/// <summary>
		/// Constructs the vector from another one.
		/// </summary>
		public Vector3D(Vector3D v)
		{
			this.X = v.X;
			this.Y = v.Y;
			this.Z = v.Z;
		}

		/// <summary>
		/// Constructs the vector from the given points.
		/// </summary>
        public Vector3D(double x, double y, double z)
		{
        	this.X = x;
        	this.Y = y;
        	this.Z = z;
        }
        
		/// <summary>
		/// Constructs the vector from the list of doubles.
		/// </summary>
        public Vector3D(double[] v)
		{
        	if(v != null && v.Length == 3)
			{
        		this.X = v[0];
        		this.Y = v[1];
        		this.Z = v[2];
        	}
        }
#endregion

#region Operators
		/// <summary>
		/// Performs the additive operator on two vectors.
		/// </summary>
        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
        	return new Vector3D(v1.X + v2.X,
				v1.Y + v2.Y,
				v1.Z + v2.Z);
        }
        
		/// <summary>
		/// Performs the subtractive operator on two vectors.
		/// </summary>
        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
        	return new Vector3D(v1.X - v2.X,
				v1.Y - v2.Y,
				v1.Z - v2.Z);
        }
        
		/// <summary>
		/// Multiplies two vectors to each other.
		/// </summary>
		public static double operator *(Vector3D v1, Vector3D v2)
		{
			return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
		}

		/// <summary>
		/// Multiplies a double against a vector.
		/// </summary>
        public static Vector3D operator *(Vector3D v1, double factor)
        {
        	return new Vector3D(
				v1.X * factor,
				v1.Y * factor,
				v1.Z * factor);
        }

		/// <summary>
		/// Gets the dot product of two vectors.
		/// </summary>
        public static double Dot(Vector3D v1, Vector3D v2)
        {
        	return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
        }

		/// <summary>
		/// Gets the cross product of two vectors.
		/// </summary>
        public static Vector3D Cross(Vector3D v1, Vector3D v2)
        {
        	return new Vector3D(
				(v1.Y * v2.Z) - (v1.Z * v2.Y),
				(v1.Z * v2.X) - (v1.X * v2.Z),
				(v1.X * v2.Y) - (v1.Y * v2.X));
        }

		/// <summary>
		/// Normalizes the vector.
		/// </summary>
		public void Normalize()
		{
			double l = Math.Sqrt(X*X+Y*Y+Z*Z);
			X /= l;
			Y /= l;
			Z /= l;
		}

		/// <summary>
		/// Converts to an array of doubles automatically.
		/// </summary>
		public double [] ToDoubleArray()
		{
			return new double [] { X, Y, Z };
		}

		/// <summary>
		/// Converts to an array of floats automatically.
		/// </summary>
		public float [] ToFloatArray()
		{
			return new float [] { (float) X, (float) Y, (float) Z };
		}

		/// <summary>
		/// Converts this into a string.
		/// </summary>
		public override string ToString()
		{
			return String.Format("({0:N3},{1:N3},{2:N3})", X, Y, Z);
		}
#endregion

#region Properties
		/// <remarks>
		/// We make these public since the overhead for the property
		/// access will just increase the processing time.
		/// </remarks>
    	public double X, Y, Z;

		/// <summary>
		/// Returns the length of the vector.
		/// </summary>
		public double Length
		{
			get
			{
				return Math.Sqrt(X * X + Y * Y + Z * Z);
			}
		}
#endregion
	}
}
