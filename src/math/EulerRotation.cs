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
	/// Represents a Euler rotation. All of this code is stolen from
	/// gtkglarea-sharp.
	/// </summary>
    public class EulerRotation
	{
#region Constants
		/// <summary>
		/// Contains the identity rotation.
		/// </summary>
		public static readonly EulerRotation Identity =
			new EulerRotation(0, 0, 0);
#endregion

#region Constructors
		/// <summary>
		/// Constructs a default identity rotation.
		/// </summary>
		public EulerRotation()
		{
			X = Y = Z = 0;
		}
		
		/// <summary>
		/// Constructs a rotation from the given values.
		/// </summary>
		public EulerRotation(double x, double y, double z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}
#endregion

#region Operators
		/// <summary>
		/// Subtracts a rotation from another rotation and returns the
		/// results.
		/// </summary>
        public static EulerRotation operator -
		(EulerRotation rot1, EulerRotation rot2)
        {
        	EulerRotation newRotation = new EulerRotation(0.0, 0.0, 0.0);
			
        	newRotation.X = Math.Abs( rot1.X - rot2.X );
        	newRotation.Y = Math.Abs( rot1.Y - rot2.Y );
        	newRotation.Z = Math.Abs( rot1.Z - rot2.Z );
        	
        	return newRotation;
        }
        
		/// <summary>
		/// Adds two rotations together.
		/// </summary>
        public static EulerRotation operator +
		(EulerRotation rot1, EulerRotation rot2)
        {
        	if(rot1 == null)
        		if(rot2 == null)
        			return null;
        		else
        			return rot2;
       		
       		if(rot2 == null)
        		return rot1;
        
        	EulerRotation newRotation = new EulerRotation(0.0, 0.0, 0.0);

        	newRotation.X = ( (rot1.X + rot2.X) % 360 );
        	newRotation.Y = ( (rot1.Y + rot2.Y) % 360 );
        	newRotation.Z = ( (rot1.Z + rot2.Z) % 360 );

        	return newRotation;
        }
        
		/// <summary>
		/// Determines if the two rotations are equal.
		/// </summary>
        public static bool operator ==(EulerRotation rot1, EulerRotation rot2)
        {
        	if(Object.Equals(rot1, null))
	        	if(Object.Equals(rot2, null))
        			return true;
        		else
        			return false;
        			
        	if(Object.Equals(rot2, null))
        		return false;
        		        
			// We consider values within 0.001 of each other to be equal
			if(Math.Abs(rot1.X - rot2.X) <= 0.001 &&
				Math.Abs(rot1.Y - rot2.Y) <= 0.001 &&
				Math.Abs(rot1.Z - rot2.Z) <= 0.001 )
				return true;
				
			return false;
        }

		/// <summary>
		/// Returns true if the two rotations are not equal.
		/// </summary>
        public static bool operator !=
			(EulerRotation rot1, EulerRotation rot2)
		{
			bool isEqual = (rot1 == rot2);
			
			if(isEqual)
				return false;
			else
				return true;
        }

		/// <summary>
		/// Implicit cast into a matrix.
		/// </summary>
		public static implicit operator Matrix4D(EulerRotation e)
		{
			// Create the new matrix
        	double[] matrix = new double [16];
        	
        	// Convert to radians
        	double Xradians = e.X * 2 * Math.PI / 360.0;
        	double Yradians = e.Y * 2 * Math.PI / 360.0;
        	double Zradians = e.Z * 2 * Math.PI / 360.0;
        	
		    double ch = Math.Cos(Xradians);
		    double sh = Math.Sin(Xradians);
		    
		    double ca = Math.Cos(Yradians);
		    double sa = Math.Sin(Yradians);
		    
		    double cb = Math.Cos(Zradians);
		    double sb = Math.Sin(Zradians); 

			// Populate the matrix doubles
		    matrix[0*4 + 0] = ch * ca;
		    matrix[0*4 + 1] = sh*sb - ch*sa*cb;
		    matrix[0*4 + 2] = ch*sa*sb + sh*cb;
		    matrix[1*4 + 0] = sa;
		    matrix[1*4 + 1] = ca*cb;
		    matrix[1*4 + 2] = -ca*sb;
		    matrix[2*4 + 0] = -sh*ca;
		    matrix[2*4 + 1] =  sh*sa*cb + ch*sb;
		    matrix[2*4 + 2] = -sh*sa*sb + ch*cb;
		    
			// Return the results
		    return new Matrix4D(matrix); 
		}

		/// <summary>
		/// Implicit cast to convert to a quanterion.
		/// </summary>
		public static implicit operator Quaternion(EulerRotation e) 
		{
			// Assuming the angles are in radians.
		    double c1 = Math.Cos(e.X / 2);
		    double s1 = Math.Sin(e.X / 2);
		    double c2 = Math.Cos(e.Y / 2);
		    double s2 = Math.Sin(e.Y / 2);
		    double c3 = Math.Cos(e.Z / 2);
		    double s3 = Math.Sin(e.Z / 2);
		    double c1c2 = c1 * c2;
		    double s1s2 = s1 * s2;
        	
        	return new Quaternion(c1c2*c3 - s1s2*s3,
				c1c2*s3 + s1s2*c3,
				s1*c2*c3 + c1*s2*s3,
				c1*s2*c3 - s1*c2*s3);
		}

		/// <summary>
		/// Determines if the two items are equal.
		/// </summary>
		public override bool Equals(object o)
		{
			if (o is EulerRotation)
				return this == (EulerRotation) o;
			else
				return false;
		}

		/// <summary>
		/// Returns the hash code of this value.
		/// </summary>
		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}
#endregion

#region Properties
		/// <remarks>
		/// These are public fields for speed reasons.
		/// </remarks>
		public double X, Y, Z;
#endregion
    }
}
