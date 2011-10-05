#region Copyright and License

// Copyright (C) 2005-2011 by Moonfire Games
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

#endregion

namespace MfGames.HierarchicalPaths
{
	/// <summary>
	/// Defines a basic 2-tuple that uses the HierarchicalPath as the key and
	/// with a given value. This is an immutable object and cannot be alterated
	/// once created.
	/// </summary>
	public class HierarchicalPathKeyValue<TValue>
		: IHierarchicalPathContainer, IComparable<HierarchicalPath>,
		  IComparable<IHierarchicalPathContainer>,
		  IEquatable<HierarchicalPathKeyValue<TValue>>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalPathKeyValue&lt;TValue&gt;"/> class.
		/// </summary>
		/// <param name="hierarchialPath">The hierarchial path.</param>
		/// <param name="value">The value.</param>
		public HierarchicalPathKeyValue(
			string hierarchialPath,
			TValue value)
			: this(new HierarchicalPath(hierarchialPath), value)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalPathKeyValue&lt;TValue&gt;"/> class.
		/// </summary>
		/// <param name="hierarchicalPath">The hierarchical path.</param>
		/// <param name="value">The value.</param>
		public HierarchicalPathKeyValue(
			HierarchicalPath hierarchicalPath,
			TValue value)
		{
			if (hierarchicalPath == null)
			{
				throw new ArgumentNullException("hierarchicalPath");
			}

			HierarchicalPath = hierarchicalPath;
			Value = value;
		}

		#endregion

		#region Hierarchical Path

		/// <summary>
		/// Gets the hierarchical path associated with the instance.
		/// </summary>
		public HierarchicalPath HierarchicalPath { get; private set; }

		#endregion

		#region Value

		/// <summary>
		/// Gets the value associated with this key/value pair.
		/// </summary>
		public TValue Value { get; private set; }

		#endregion

		#region Equality

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.
		///                 </param>
		public bool Equals(HierarchicalPathKeyValue<TValue> other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}
			return Equals(other.HierarchicalPath, HierarchicalPath);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
		///                 </param><exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.
		///                 </exception><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != typeof(HierarchicalPathKeyValue<TValue>))
			{
				return false;
			}
			return Equals((HierarchicalPathKeyValue<TValue>) obj);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return HierarchicalPath.GetHashCode();
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator ==(HierarchicalPathKeyValue<TValue> left,
		                               HierarchicalPathKeyValue<TValue> right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator !=(HierarchicalPathKeyValue<TValue> left,
		                               HierarchicalPathKeyValue<TValue> right)
		{
			return !Equals(left, right);
		}

		#endregion

		#region Comparison

		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
		/// Value
		/// Meaning
		/// Less than zero
		/// This object is less than the <paramref name="other"/> parameter.
		/// Zero
		/// This object is equal to <paramref name="other"/>.
		/// Greater than zero
		/// This object is greater than <paramref name="other"/>.
		/// </returns>
		public int CompareTo(HierarchicalPath other)
		{
			return HierarchicalPath.CompareTo(other);
		}

		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
		/// Value
		/// Meaning
		/// Less than zero
		/// This object is less than the <paramref name="other"/> parameter.
		/// Zero
		/// This object is equal to <paramref name="other"/>.
		/// Greater than zero
		/// This object is greater than <paramref name="other"/>.
		/// </returns>
		public int CompareTo(IHierarchicalPathContainer other)
		{
			return HierarchicalPath.CompareTo(other.HierarchicalPath);
		}

		#endregion

		#region Conversion

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return String.Format("{0} => {1}", HierarchicalPath, Value);
		}

		#endregion
	}
}