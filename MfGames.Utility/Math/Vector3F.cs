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
 * class for double, float, and int. The source file is Vector3.cs.in.
 */

using System;

namespace MfGames.Utility
{
	/// <summary>
	/// A simple three-dimensional float vector.
	/// </summary>
    public class Vector3F
	{
		#region Constructors
		/// <summary>
		/// Constructs a zero vector.
		/// </summary>
		public Vector3F()
		{
		}

		/// <summary>
		/// Constructs the vector from another one.
		/// </summary>
		public Vector3F(Vector2F v)
		{
			this.X = v.X;
			this.Y = v.Y;
			this.Z = 0;
		}

		/// <summary>
		/// Constructs the vector from another one.
		/// </summary>
		public Vector3F(Vector3F v)
		{
			this.X = v.X;
			this.Y = v.Y;
			this.Z = v.Z;
		}

		/// <summary>
		/// Constructs the vector from the given points.
		/// </summary>
        public Vector3F(float x, float y, float z)
		{
        	this.X = x;
        	this.Y = y;
        	this.Z = z;
        }
        
		/// <summary>
		/// Constructs the vector from the list of floats.
		/// </summary>
        public Vector3F(float[] v)
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
        public static Vector3F operator +(Vector3F v1, Vector3F v2)
        {
        	return new Vector3F(v1.X + v2.X,
				v1.Y + v2.Y,
				v1.Z + v2.Z);
        }
        
		/// <summary>
		/// Performs the subtractive operator on two vectors.
		/// </summary>
        public static Vector3F operator -(Vector3F v1, Vector3F v2)
        {
        	return new Vector3F(v1.X - v2.X,
				v1.Y - v2.Y,
				v1.Z - v2.Z);
        }
        
		/// <summary>
		/// Multiplies two vectors to each other.
		/// </summary>
		public static float operator *(Vector3F v1, Vector3F v2)
		{
			return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
		}

		/// <summary>
		/// Multiplies a float against a vector.
		/// </summary>
        public static Vector3F operator *(Vector3F v1, float factor)
        {
        	return new Vector3F(
				v1.X * factor,
				v1.Y * factor,
				v1.Z * factor);
        }

		/// <summary>
		/// Gets the dot product of two vectors.
		/// </summary>
        public static float Dot(Vector3F v1, Vector3F v2)
        {
        	return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
        }

		/// <summary>
		/// Gets the cross product of two vectors.
		/// </summary>
        public static Vector3F Cross(Vector3F v1, Vector3F v2)
        {
        	return new Vector3F(
				(v1.Y * v2.Z) - (v1.Z * v2.Y),
				(v1.Z * v2.X) - (v1.X * v2.Z),
				(v1.X * v2.Y) - (v1.Y * v2.X));
        }

		/// <summary>
		/// Normalizes the vector.
		/// </summary>
		public void Normalize()
		{
			float l = (float) Math.Sqrt(X*X+Y*Y+Z*Z);
			X /= l;
			Y /= l;
			Z /= l;
		}

		/// <summary>
		/// Converts to an array of floats automatically.
		/// </summary>
		public double [] ToDoubleArray()
		{
			return new double [] { (double) X, (double) Y, (double) Z };
		}

		/// <summary>
		/// Converts to an array of floats automatically.
		/// </summary>
		public float [] ToFloatArray()
		{
			return new float [] { (float) X, (float) Y, (float) Z };
		}

		/// <summary>
		/// Converts to an array of floats automatically.
		/// </summary>
		public int [] ToIntArray()
		{
			return new int [] { (int) X, (int) Y, (int) Z };
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
    	public float X, Y, Z;

		/// <summary>
		/// Returns the length of the vector.
		/// </summary>
		public float Length
		{
			get
			{
				return (float) Math.Sqrt(X * X + Y * Y + Z * Z);
			}
		}
		#endregion
	}
}
