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
using System.Reflection;

#endregion

namespace MfGames.Extensions.Reflection
{
	/// <summary>
	/// Extends the MemberInfo class with additional methods.
	/// </summary>
	public static class SystemReflectionMemberInfoExtensions
	{
		/// <summary>
		/// Extends the Type class to return a flag if there is the presence of a custom
		/// attribute.
		/// </summary>
		/// <param name="memberInfo">The member info.</param>
		/// <param name="attributeType">Type of the attribute.</param>
		/// <returns>
		/// 	<c>true</c> if [has custom attribute] [the specified type]; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasCustomAttribute(this MemberInfo memberInfo, Type attributeType)
		{
			return HasCustomAttribute(memberInfo, attributeType, true);
		}

		/// <summary>
		/// Extends the Type class to return a flag if there is the presence of a custom
		/// attribute.
		/// </summary>
		/// <param name="memberInfo">The member info.</param>
		/// <param name="attributeType">Type of the attribute.</param>
		/// <param name="inherited">if set to <c>true</c> [inherited].</param>
		/// <returns>
		/// 	<c>true</c> if [has custom attribute] [the specified type]; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasCustomAttribute(this MemberInfo memberInfo, Type attributeType, bool inherited)
		{
			// Check for null parameters.
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}

			// Go through the attributes of the member type and look for at least one.
			return (memberInfo.GetCustomAttributes(attributeType, true).Length > 0);
		}
	}
}