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
using System.Runtime.InteropServices;

namespace MfGames.Utility
{
	/// <summary>
	/// Yet another library for handling matrix operations, but this
	/// one fits the coding guidelines. Most of the code was stolen
	/// from Axiom.MathLib and adjusted for doubles, which are easier
	/// to work with in C# and also removed the unsafe code.
	///
	/// This stores values in row-dominate order, so the internal
	/// coordinates are y, x. OpenGL uses this order.
	/// </summary>
    [StructLayout(LayoutKind.Sequential)]
	public class Matrix4D
	{
#region Constants
		/// <summary>
		/// Contains a read-only identity matrix.
		/// </summary>
		public static readonly Matrix4D Identity = new Matrix4D(new double [] {
			1, 0, 0, 0,
			0, 1, 0, 0,
			0, 0, 1, 0,
			0, 0, 0, 1});
			
		/// <summary>
		/// Contains a read-only zero metrix.
		/// </summary>
		public static readonly Matrix4D Zero = new Matrix4D(new double [] {
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0});
#endregion

#region Constructors
		/// <summary>
		/// Constructs an identity matrix.
		/// </summary>
		public Matrix4D()
		: this(Matrix4D.Identity)
		{
		}

		/// <summary>
		/// Constructs an identity matrix from another matrix.
		/// </summary>
		public Matrix4D(Matrix4D matrix)
		{
			values = (double [,]) matrix.values.Clone();
		}

		/// <summary>
		/// Constructs the matrix from 16 doubles.
		/// </summary>
		public Matrix4D(double [] values)
		{
			if (values.Length != 16)
				throw new UtilityException("Can only assign a 16 double");

			this.values[0, 0] = values[ 0];
			this.values[1, 0] = values[ 1];
			this.values[2, 0] = values[ 2];
			this.values[3, 0] = values[ 3];
			this.values[0, 1] = values[ 4];
			this.values[1, 1] = values[ 5];
			this.values[2, 1] = values[ 6];
			this.values[3, 1] = values[ 7];
			this.values[0, 2] = values[ 8];
			this.values[1, 2] = values[ 9];
			this.values[2, 2] = values[10];
			this.values[3, 2] = values[11];
			this.values[0, 3] = values[12];
			this.values[1, 3] = values[13];
			this.values[2, 3] = values[14];
			this.values[3, 3] = values[15];
		}
#endregion

#region Operators
		/// <summary>
		/// Converts the entire matrix into an array of 16 doubles.
		/// </summary>
		public double [] ToDoubleArray()
		{
			double [] i = new double [16];
			i[ 0] = this.values[0, 0];
			i[ 1] = this.values[1, 0];
			i[ 2] = this.values[2, 0];
			i[ 3] = this.values[3, 0];
			i[ 4] = this.values[0, 1];
			i[ 5] = this.values[1, 1];
			i[ 6] = this.values[2, 1];
			i[ 7] = this.values[3, 1];
			i[ 8] = this.values[0, 2];
			i[ 9] = this.values[1, 2];
			i[10] = this.values[2, 2];
			i[11] = this.values[3, 2];
			i[12] = this.values[0, 3];
			i[13] = this.values[1, 3];
			i[14] = this.values[2, 3];
			i[15] = this.values[3, 3];
			return i;
		}

        /// <summary>
		/// Swaps the rows and columns of the matrix and returns a new
		/// value.
        /// </summary>
        /// <returns>A transposed matrix.</returns>
        public Matrix4D Transpose()
		{
            return new Matrix4D(new double [] {
				values[0, 0], values[0, 1], values[0, 2], values[0, 3],
				values[1, 0], values[1, 1], values[1, 2], values[1, 3],
				values[2, 0], values[2, 1], values[2, 2], values[2, 3],
				values[3, 0], values[3, 1], values[3, 2], values[3, 3]});
        }
#endregion

#region Properties
		private double [,] values = new double [4,4];
#endregion

#region Multiplication
        /// <summary>
        ///	Used to multiply (concatenate) two 4x4 Matrices.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4D operator * (Matrix4D left, Matrix4D right)
		{
            Matrix4D result = new Matrix4D();

            result.values[0, 0] = left.values[0, 0] * right.values[0, 0] + left.values[0, 1] * right.values[1, 0] + left.values[0, 2] * right.values[2, 0] + left.values[0, 3] * right.values[3, 0];
            result.values[0, 1] = left.values[0, 0] * right.values[0, 1] + left.values[0, 1] * right.values[1, 1] + left.values[0, 2] * right.values[2, 1] + left.values[0, 3] * right.values[3, 1];
            result.values[0, 2] = left.values[0, 0] * right.values[0, 2] + left.values[0, 1] * right.values[1, 2] + left.values[0, 2] * right.values[2, 2] + left.values[0, 3] * right.values[3, 2];
            result.values[0, 3] = left.values[0, 0] * right.values[0, 3] + left.values[0, 1] * right.values[1, 3] + left.values[0, 2] * right.values[2, 3] + left.values[0, 3] * right.values[3, 3];

            result.values[1, 0] = left.values[1, 0] * right.values[0, 0] + left.values[1, 1] * right.values[1, 0] + left.values[1, 2] * right.values[2, 0] + left.values[1, 3] * right.values[3, 0];
            result.values[1, 1] = left.values[1, 0] * right.values[0, 1] + left.values[1, 1] * right.values[1, 1] + left.values[1, 2] * right.values[2, 1] + left.values[1, 3] * right.values[3, 1];
            result.values[1, 2] = left.values[1, 0] * right.values[0, 2] + left.values[1, 1] * right.values[1, 2] + left.values[1, 2] * right.values[2, 2] + left.values[1, 3] * right.values[3, 2];
            result.values[1, 3] = left.values[1, 0] * right.values[0, 3] + left.values[1, 1] * right.values[1, 3] + left.values[1, 2] * right.values[2, 3] + left.values[1, 3] * right.values[3, 3];

            result.values[2, 0] = left.values[2, 0] * right.values[0, 0] + left.values[2, 1] * right.values[1, 0] + left.values[2, 2] * right.values[2, 0] + left.values[2, 3] * right.values[3, 0];
            result.values[2, 1] = left.values[2, 0] * right.values[0, 1] + left.values[2, 1] * right.values[1, 1] + left.values[2, 2] * right.values[2, 1] + left.values[2, 3] * right.values[3, 1];
            result.values[2, 2] = left.values[2, 0] * right.values[0, 2] + left.values[2, 1] * right.values[1, 2] + left.values[2, 2] * right.values[2, 2] + left.values[2, 3] * right.values[3, 2];
            result.values[2, 3] = left.values[2, 0] * right.values[0, 3] + left.values[2, 1] * right.values[1, 3] + left.values[2, 2] * right.values[2, 3] + left.values[2, 3] * right.values[3, 3];

            result.values[3, 0] = left.values[3, 0] * right.values[0, 0] + left.values[3, 1] * right.values[1, 0] + left.values[3, 2] * right.values[2, 0] + left.values[3, 3] * right.values[3, 0];
            result.values[3, 1] = left.values[3, 0] * right.values[0, 1] + left.values[3, 1] * right.values[1, 1] + left.values[3, 2] * right.values[2, 1] + left.values[3, 3] * right.values[3, 1];
            result.values[3, 2] = left.values[3, 0] * right.values[0, 2] + left.values[3, 1] * right.values[1, 2] + left.values[3, 2] * right.values[2, 2] + left.values[3, 3] * right.values[3, 2];
            result.values[3, 3] = left.values[3, 0] * right.values[0, 3] + left.values[3, 1] * right.values[1, 3] + left.values[3, 2] * right.values[2, 3] + left.values[3, 3] * right.values[3, 3];

            return result;
        }
#endregion

#region Scaling
		/// <summary>
		/// Scales the matrix by the given amount.
		/// </summary>
		public void Scale(double sx, double sy, double sz)
		{
			values[0, 0] *= sx;
			values[1, 1] *= sy;
			values[2, 2] *= sz;
		}
#endregion

#region Rotation
		/// <summary>
		/// Rotates the matrix by the given amount.
		/// </summary>
		public void Rotate(EulerRotation rot)
		{
			Matrix4D res = this * rot;
			values = res.values;
		}

		/// <summary>
		/// Rotates the matrix by the given amount.
		/// </summary>
		public void Rotate(Quaternion rot)
		{
			Matrix4D res = this * rot;
			values = res.values;
		}
#endregion

#region Translation
		/// <summary>
		/// Translates the matrix by the given amount.
		/// </summary>
		public void Translate(double dx, double dy, double dz)
		{
			values[0, 3] += dx;
			values[1, 3] += dy;
			values[2, 3] += dz;
		}
#endregion


#if REMOVED
        #region Public methods

        /// <summary>
        ///    Returns a 3x3 portion of this 4x4 matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix3 GetMatrix3() {
            return
                new Matrix3(
                    this.m00, this.m01, this.m02,
                    this.m10, this.m11, this.m12,
                    this.m20, this.m21, this.m22);
        }

        /// <summary>
        ///    Returns an inverted 4d matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix4 Inverse() {
            return Adjoint() * (1.0f / this.Determinant);
        }


        #endregion

        #region Operator overloads + CLS compliant method equivalents

        /// <summary>
        ///		Used to multiply (concatenate) two 4x4 Matrices.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4 Multiply (Matrix4 left, Matrix4 right) {
        	return left * right;
        }
        

        /// <summary>
        ///		Transforms the given 3-D vector by the matrix, projecting the 
        ///		result back into <i>w</i> = 1.
        ///		<p/>
        ///		This means that the initial <i>w</i> is considered to be 1.0,
        ///		and then all the tree elements of the resulting 3-D vector are
        ///		divided by the resulting <i>w</i>.
        /// </summary>
        /// <param name="matrix">A Matrix4.</param>
        /// <param name="vector">A Vector3.</param>
        /// <returns>A new vector.</returns>
        public static Vector3 Multiply (Matrix4 matrix, Vector3 vector) {
        	return matrix * vector;
        }

		/// <summary>
		///		Transforms a plane using the specified transform.
		/// </summary>
		/// <param name="matrix">Transformation matrix.</param>
		/// <param name="plane">Plane to transform.</param>
		/// <returns>A transformed plane.</returns>
		public static Plane Multiply(Matrix4 matrix, Plane plane) {
			return matrix * plane;
		}
        
        /// <summary>
        ///		Transforms the given 3-D vector by the matrix, projecting the 
        ///		result back into <i>w</i> = 1.
        ///		<p/>
        ///		This means that the initial <i>w</i> is considered to be 1.0,
        ///		and then all the tree elements of the resulting 3-D vector are
        ///		divided by the resulting <i>w</i>.
        /// </summary>
        /// <param name="matrix">A Matrix4.</param>
        /// <param name="vector">A Vector3.</param>
        /// <returns>A new vector.</returns>
        public static Vector3 operator * (Matrix4 matrix, Vector3 vector) {
            Vector3 result = new Vector3();

            float inverseW = 1.0f / ( matrix.m30 + matrix.m31 + matrix.m32 + matrix.m33 );

            result.x = ( (matrix.m00 * vector.x) + (matrix.m01 * vector.y) + (matrix.m02 * vector.z) + matrix.m03 ) * inverseW;
            result.y = ( (matrix.m10 * vector.x) + (matrix.m11 * vector.y) + (matrix.m12 * vector.z) + matrix.m13 ) * inverseW;
            result.z = ( (matrix.m20 * vector.x) + (matrix.m21 * vector.y) + (matrix.m22 * vector.z) + matrix.m23 ) * inverseW;

            return result;
        }

        /// <summary>
        ///		Used to multiply a Matrix4 object by a scalar value..
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4 operator * ( Matrix4 left, float scalar) {
            Matrix4 result = new Matrix4();

            result.m00 = left.m00 * scalar;
            result.m01 = left.m01 * scalar;
            result.m02 = left.m02 * scalar;
            result.m03 = left.m03 * scalar;

            result.m10 = left.m10 * scalar;
            result.m11 = left.m11 * scalar;
            result.m12 = left.m12 * scalar;
            result.m13 = left.m13 * scalar;

            result.m20 = left.m20 * scalar;
            result.m21 = left.m21 * scalar;
            result.m22 = left.m22 * scalar;
            result.m23 = left.m23 * scalar;

            result.m30 = left.m30 * scalar;
            result.m31 = left.m31 * scalar;
            result.m32 = left.m32 * scalar;
            result.m33 = left.m33 * scalar;

            return result;
        }

		/// <summary>
		///		Used to multiply a transformation to a Plane.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="plane"></param>
		/// <returns></returns>
		public static Plane operator * (Matrix4 left, Plane plane) {
			Plane result = new Plane();

			Vector3 planeNormal = plane.Normal;

			result.Normal = new Vector3(
				left.m00 * planeNormal.x + left.m01 * planeNormal.y + left.m02 * planeNormal.z,
				left.m10 * planeNormal.x + left.m11 * planeNormal.y + left.m12 * planeNormal.z,
				left.m20 * planeNormal.x + left.m21 * planeNormal.y + left.m22 * planeNormal.z);

			Vector3 pt = planeNormal * -plane.D;
			pt = left * pt;

			result.D = -pt.Dot(result.Normal);

			return result;
		}

        /// <summary>
        ///		Used to add two matrices together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4 Add ( Matrix4 left, Matrix4 right ) {
        	return left + right;
        }
        
        /// <summary>
        ///		Used to add two matrices together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4 operator + ( Matrix4 left, Matrix4 right ) {
            Matrix4 result = new Matrix4();

            result.m00 = left.m00 + right.m00;
            result.m01 = left.m01 + right.m01;
            result.m02 = left.m02 + right.m02;
            result.m03 = left.m03 + right.m03;

            result.m10 = left.m10 + right.m10;
            result.m11 = left.m11 + right.m11;
            result.m12 = left.m12 + right.m12;
            result.m13 = left.m13 + right.m13;

            result.m20 = left.m20 + right.m20;
            result.m21 = left.m21 + right.m21;
            result.m22 = left.m22 + right.m22;
            result.m23 = left.m23 + right.m23;

            result.m30 = left.m30 + right.m30;
            result.m31 = left.m31 + right.m31;
            result.m32 = left.m32 + right.m32;
            result.m33 = left.m33 + right.m33;

            return result;
        }

        /// <summary>
        ///		Used to subtract two matrices.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4 Subtract ( Matrix4 left, Matrix4 right ) {
        	return left - right;
        }
        
        /// <summary>
        ///		Used to subtract two matrices.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4 operator - ( Matrix4 left, Matrix4 right ) {
            Matrix4 result = new Matrix4();

            result.m00 = left.m00 - right.m00;
            result.m01 = left.m01 - right.m01;
            result.m02 = left.m02 - right.m02;
            result.m03 = left.m03 - right.m03;

            result.m10 = left.m10 - right.m10;
            result.m11 = left.m11 - right.m11;
            result.m12 = left.m12 - right.m12;
            result.m13 = left.m13 - right.m13;

            result.m20 = left.m20 - right.m20;
            result.m21 = left.m21 - right.m21;
            result.m22 = left.m22 - right.m22;
            result.m23 = left.m23 - right.m23;

            result.m30 = left.m30 - right.m30;
            result.m31 = left.m31 - right.m31;
            result.m32 = left.m32 - right.m32;
            result.m33 = left.m33 - right.m33;

            return result;
        }

        /// <summary>
        /// Compares two Matrix4 instances for equality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true if the Matrix 4 instances are equal, false otherwise.</returns>
        public static bool operator == (Matrix4 left, Matrix4 right) {
            if( 
                left.m00 == right.m00 && left.m01 == right.m01 && left.m02 == right.m02 && left.m03 == right.m03 &&
                left.m10 == right.m10 && left.m11 == right.m11 && left.m12 == right.m12 && left.m13 == right.m13 &&
                left.m20 == right.m20 && left.m21 == right.m21 && left.m22 == right.m22 && left.m23 == right.m23 &&
                left.m30 == right.m30 && left.m31 == right.m31 && left.m32 == right.m32 && left.m33 == right.m33 )
                return true;

            return false;
        }

        /// <summary>
        /// Compares two Matrix4 instances for inequality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true if the Matrix 4 instances are not equal, false otherwise.</returns>
        public static bool operator != (Matrix4 left, Matrix4 right) {
            return !(left == right);
        }

        /// <summary>
        ///		Used to allow assignment from a Matrix3 to a Matrix4 object.
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4 FromMatrix3(Matrix3 right) {
        	return right;
        }
        
        /// <summary>
        ///		Used to allow assignment from a Matrix3 to a Matrix4 object.
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        public static implicit operator Matrix4(Matrix3 right) {
            Matrix4 result = Matrix4.Identity;

            result.m00 = right.m00; result.m01 = right.m01; result.m02 = right.m02;
            result.m10 = right.m10; result.m11 = right.m11; result.m12 = right.m12;
            result.m20 = right.m20; result.m21 = right.m21; result.m22 = right.m22;	

            return result;
        }

        /// <summary>
        ///    Allows the Matrix to be accessed like a 2d array (i.e. matrix[2,3])
        /// </summary>
        /// <remarks>
        ///    This indexer is only provided as a convenience, and is <b>not</b> recommended for use in
        ///    intensive applications.  
        /// </remarks>
        public float this[int row, int col] {
            get {
                //Debug.Assert((row >= 0 && row < 4) && (col >= 0 && col < 4), "Attempt to access Matrix4 indexer out of bounds.");

                unsafe {
                    fixed(float* pM = &m00)
                        return *(pM + ((4*row) + col)); 
                }
            }
            set { 	
                //Debug.Assert((row >= 0 && row < 4) && (col >= 0 && col < 4), "Attempt to access Matrix4 indexer out of bounds.");

                unsafe {
                    fixed(float* pM = &m00)
                        *(pM + ((4*row) + col)) = value;
                }
            }
        }

        /// <summary>
        ///		Allows the Matrix to be accessed linearly (m[0] -> m[15]).  
        /// </summary>
        /// <remarks>
        ///    This indexer is only provided as a convenience, and is <b>not</b> recommended for use in
        ///    intensive applications.  
        /// </remarks>
        public float this[int index] {
            get {
                //Debug.Assert(index >= 0 && index < 16, "Attempt to access Matrix4 linear indexer out of bounds.");

                unsafe {
                    fixed(float* pMatrix = &this.m00) {			
                        return *(pMatrix + index);
                    }
                }
            }
            set {
                //Debug.Assert(index >= 0 && index < 16, "Attempt to access Matrix4 linear indexer out of bounds.");

                unsafe {
                    fixed(float* pMatrix = &this.m00) {			
                        *(pMatrix + index) = value;
                    }
                }
            }
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void MakeFloatArray(float[] floats) {
            unsafe {
                fixed(float* p = &m00) {
                    for(int i = 0; i < 16; i++)
                        floats[i] = *(p + i);
                }
            }
        }

        /// <summary>
        ///    Gets the determinant of this matrix.
        /// </summary>
        public float Determinant {
            get {
				// note: this is an expanded version of the Ogre determinant() method, to give better performance in C#. Generated using a script
                float result = m00 * (m11 * (m22 * m33 - m32 * m23) - m12 * (m21 * m33 - m31 * m23) + m13 * (m21 * m32 - m31 * m22)) - 
	                m01 * (m10 * (m22 * m33 - m32 * m23) - m12 * (m20 * m33 - m30 * m23) + m13 * (m20 * m32 - m30 * m22)) + 
	                m02 * (m10 * (m21 * m33 - m31 * m23) - m11 * (m20 * m33 - m30 * m23) + m13 * (m20 * m31 - m30 * m21)) - 
	                m03 * (m10 * (m21 * m32 - m31 * m22) - m11 * (m20 * m32 - m30 * m22) + m12 * (m20 * m31 - m30 * m21));

                return result;
            }
        }

        /// <summary>
        ///    Used to generate the adjoint of this matrix.  Used internally for <see cref="Inverse"/>.
        /// </summary>
        /// <returns>The adjoint matrix of the current instance.</returns>
        private Matrix4 Adjoint() {
            // note: this is an expanded version of the Ogre adjoint() method, to give better performance in C#. Generated using a script
            float val0 = m11 * (m22 * m33 - m32 * m23) - m12 * (m21 * m33 - m31 * m23) + m13 * (m21 * m32 - m31 * m22);
            float val1 = -(m01 * (m22 * m33 - m32 * m23) - m02 * (m21 * m33 - m31 * m23) + m03 * (m21 * m32 - m31 * m22));
            float val2 = m01 * (m12 * m33 - m32 * m13) - m02 * (m11 * m33 - m31 * m13) + m03 * (m11 * m32 - m31 * m12);
            float val3 = -(m01 * (m12 * m23 - m22 * m13) - m02 * (m11 * m23 - m21 * m13) + m03 * (m11 * m22 - m21 * m12));
            float val4 = -(m10 * (m22 * m33 - m32 * m23) - m12 * (m20 * m33 - m30 * m23) + m13 * (m20 * m32 - m30 * m22));
            float val5 = m00 * (m22 * m33 - m32 * m23) - m02 * (m20 * m33 - m30 * m23) + m03 * (m20 * m32 - m30 * m22);
            float val6 = -(m00 * (m12 * m33 - m32 * m13) - m02 * (m10 * m33 - m30 * m13) + m03 * (m10 * m32 - m30 * m12));
            float val7 = m00 * (m12 * m23 - m22 * m13) - m02 * (m10 * m23 - m20 * m13) + m03 * (m10 * m22 - m20 * m12);
            float val8 = m10 * (m21 * m33 - m31 * m23) - m11 * (m20 * m33 - m30 * m23) + m13 * (m20 * m31 - m30 * m21);
            float val9 = -(m00 * (m21 * m33 - m31 * m23) - m01 * (m20 * m33 - m30 * m23) + m03 * (m20 * m31 - m30 * m21));
            float val10 = m00 * (m11 * m33 - m31 * m13) - m01 * (m10 * m33 - m30 * m13) + m03 * (m10 * m31 - m30 * m11);
            float val11 = -(m00 * (m11 * m23 - m21 * m13) - m01 * (m10 * m23 - m20 * m13) + m03 * (m10 * m21 - m20 * m11));
            float val12 = -(m10 * (m21 * m32 - m31 * m22) - m11 * (m20 * m32 - m30 * m22) + m12 * (m20 * m31 - m30 * m21));
            float val13 = m00 * (m21 * m32 - m31 * m22) - m01 * (m20 * m32 - m30 * m22) + m02 * (m20 * m31 - m30 * m21);
            float val14 = -(m00 * (m11 * m32 - m31 * m12) - m01 * (m10 * m32 - m30 * m12) + m02 * (m10 * m31 - m30 * m11));
            float val15 = m00 * (m11 * m22 - m21 * m12) - m01 * (m10 * m22 - m20 * m12) + m02 * (m10 * m21 - m20 * m11);

            return new Matrix4(val0, val1, val2, val3, val4, val5, val6, val7, val8, val9, val10, val11, val12, val13, val14, val15);
        }

        #endregion

        #region Object overloads

        /// <summary>
        ///		Overrides the Object.ToString() method to provide a text representation of 
        ///		a Matrix4.
        /// </summary>
        /// <returns>A string representation of a vector3.</returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
			
            sb.AppendFormat(" | {0} {1} {2} {3} |\n", this.m00, this.m01, this.m02, this.m03);
            sb.AppendFormat(" | {0} {1} {2} {3} |\n", this.m10, this.m11, this.m12, this.m13);
            sb.AppendFormat(" | {0} {1} {2} {3} |\n", this.m20, this.m21, this.m22, this.m23);
            sb.AppendFormat(" | {0} {1} {2} {3} |\n", this.m30, this.m31, this.m32, this.m33);

            return sb.ToString();
        }

        /// <summary>
        ///		Provides a unique hash code based on the member variables of this
        ///		class.  This should be done because the equality operators (==, !=)
        ///		have been overriden by this class.
        ///		<p/>
        ///		The standard implementation is a simple XOR operation between all local
        ///		member variables.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            int hashCode = 0;

            unsafe {
                fixed(float* pM = &m00) {
                    for(int i = 0; i < 16; i++)
                        hashCode ^= (int)(*(pM + i));
                }
            }
					
            return hashCode;
        }

        /// <summary>
        ///		Compares this Matrix to another object.  This should be done because the 
        ///		equality operators (==, !=) have been overriden by this class.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            if(obj is Matrix4)
                return (this == (Matrix4)obj);
            else
                return false;
        }

        #endregion
    }
#endif
	}
}
