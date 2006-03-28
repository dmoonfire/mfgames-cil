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
	/// Stolen and reformatted from the gtkglarea, this makes some
	/// slight changes to the stolen class.
	/// </summary>
    public class Quaternion
	{
#region Constants
		public static readonly Quaternion Identity =
			new Quaternion(1, 0, 0, 0);
#endregion

#region Constructors
		/// <summary>
		/// Constructs the quaternion as an identity one.
		/// </summary>
		public Quaternion()
		: this(1, 0, 0, 0)
		{
		}

		/// <summary>
		/// Constructs the quaternion from floats.
		/// </summary>
        public Quaternion(float[] q)
        {
        	this.W = (double) q[0];
        	this.X = (double) q[1];
        	this.Y = (double) q[2];
        	this.Z = (double) q[3];
        }
        
		/// <summary>
		/// Constructs the quaternion from an array of doubles.
		/// </summary>
        public Quaternion(double[] q)
        {
        	this.W = q[0];
        	this.X = q[1];
        	this.Y = q[2];
        	this.Z = q[3];
        }
        
		/// <summary>
		/// Constructs the quaternion from the given values.
		/// </summary>
        public Quaternion(double w, double x, double y, double z)
        {
        	this.W = w;
        	this.X = x;
        	this.Y = y;
        	this.Z = z;
        }
#endregion

#region Operators
		/// <summary>
		/// Multiplies two quaternions together.
		/// </summary>
        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
			// Ignore nulls
        	if(q1 == null) return null;
        		
        	if(q2 == null) return null;

			// Create vectors from the values
        	Vector3D v1 = new Vector3D(q1.X, q1.Y, q1.Z);
        	Vector3D v2 = new Vector3D(q2.X, q2.Y, q2.Z);
        	
        	double angle = ((q1.W * q2.W) - Vector3D.Dot(v1, v2));
        	
        	Vector3D cross = Vector3D.Cross(v1, v2);
        	
        	v1 *= q2.W;
        	v2 *= q1.W;
        	
        	Quaternion result = new Quaternion(angle,
				(v1.X + v2.X + cross.X),
				(v1.Y + v2.Y + cross.Y),
				(v1.Z + v2.Z + cross.Z));
			
			return result;
        }

		/// <summary>
		/// Implicit cast into a Euler rotation.
		/// </summary>
		public static implicit operator EulerRotation(Quaternion q)
		{
			double heading;
			double attitude;
			double bank;
		    
			double magnitude = q.GetMagnitude();
	        double test = q.X * q.Y + q.Z * q.W;
	        
			// Check for singularity at the north pole
		    if (test > 0.499 * magnitude)
			{
		      heading = 2 * Math.Atan2(q.X, q.W);
		      attitude = Math.PI/2;
		      bank = 0;
		      return new EulerRotation(heading, attitude, bank);
		    }
		    
			// Check for signularity at the south pole
		    if (test < -0.499 * magnitude)
			{
		      heading = -2 * Math.Atan2(q.X, q.W);
		      attitude = - Math.PI/2;
		      bank = 0;
		      return new EulerRotation(heading, attitude, bank);
		    }

			// Otherwise, calculate it with math
		    heading = Math.Atan2(2 * q.Y * q.W - 2 * q.X * q.Z ,
				1 - 2 * q.Y * q.Y - 2 * q.Z * q.Z);
		    attitude = Math.Asin(2 * test / magnitude);
		    bank = Math.Atan2(2 * q.X * q.W - 2 * q.Y * q.Z,
				1 - 2 * q.X * q.X - 2 * q.Z * q.Z);

			return new EulerRotation(heading, attitude, bank);
		}

		/// <summary>
		/// Implicit cast into a matrix.
		/// </summary>
		public static implicit operator Matrix4D(Quaternion q)
		{
			double[] transMatrix = new double[16];
			
	        transMatrix[4 * 0 + 0] = 1.0 - 2.0 * (q.Y * q.Y + q.Z * q.Z);
		    transMatrix[4 * 0 + 1] = 2.0 * (q.X * q.Y - q.Z * q.W);
		    transMatrix[4 * 0 + 2] = 2.0 * (q.Z * q.X + q.Y * q.W);

		    transMatrix[4 * 1 + 0] = 2.0 * (q.X * q.Y + q.Z * q.W);
		    transMatrix[4 * 1 + 1]= 1.0 - 2.0f * (q.Z * q.Z + q.X * q.X);
		    transMatrix[4 * 1 + 2] = 2.0 * (q.Y * q.Z - q.X * q.W);

		    transMatrix[4 * 2 + 0] = 2.0 * (q.Z * q.X - q.Y * q.W);
	    	transMatrix[4 * 2 + 1] = 2.0 * (q.Y * q.Z + q.X * q.W);
		    transMatrix[4 * 2 + 2] = 1.0 - 2.0 * (q.Y * q.Y + q.X * q.X);

			transMatrix[3] = transMatrix[7] = transMatrix[11] =
			transMatrix[12] = transMatrix[13] = transMatrix[14] = 0.0;
			
			transMatrix[15] = 1.0;
		    
		    return new Matrix4D(transMatrix);
		}

		/// <summary>
		/// Gets the magnitude of this quaternion.
		/// </summary>
        double GetMagnitude()
        {
        	return (W*W + X*X + Y*Y + Z*Z);
        }

	   	/*
	 	 * Quaternions always obey:  a^2 + b^2 + c^2 + d^2 = 1.0
	 	 * If they don't add up to 1.0, dividing by their magnitued will
	 	 * renormalize them.
	 	 *
	 	 * Note: See the following for more information on quaternions:
	 	 *
	 	 * - Shoemake, K., Animating rotation with quaternion curves, Computer
	 	 *   Graphics 19, No 3 (Proc. SIGGRAPH'85), 245-254, 1985.
	 	 * - Pletinckx, D., Quaternion calculus as a basic tool in computer
	 	 *   graphics, The Visual Computer 5, 2-13, 1989.
	 	 */
        public Quaternion Normalize()
        {
		    double mag = GetMagnitude();
			return new Quaternion(W / mag, X / mag, Y / mag, Z / mag);
		}

		/// <summary>
		/// Formats the quaterion as a string.
		/// </summary>
		public override string ToString()
		{
			return String.Format("Q({0},{1},{2},{3})", W, X, Y, Z);
		}
#endregion

#region Properties
		/// <remarks>
		/// We keep these as public fields for speed.
		/// </remarks>
        public double X, Y, Z, W;
#endregion
    }
}
