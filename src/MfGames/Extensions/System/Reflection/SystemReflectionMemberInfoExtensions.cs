// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;
using System.Reflection;

namespace MfGames.Extensions.System.Reflection
{
	/// <summary>
	/// Extends the MemberInfo class with additional methods.
	/// </summary>
	public static class SystemReflectionMemberInfoExtensions
	{
		#region Methods

		/// <summary>
		/// Gets the custom attribute or null if one doesn't exist.
		/// </summary>
		/// <typeparam name="TAttribute">The type of the attribute.</typeparam>
		/// <param name="memberInfo">The member info.</param>
		/// <returns></returns>
		public static TAttribute GetCustomAttribute<TAttribute>(
			this MemberInfo memberInfo) where TAttribute: Attribute
		{
			object[] attributes = memberInfo.GetCustomAttributes(
				typeof (TAttribute), true);

			if (attributes.Length == 0)
			{
				return null;
			}

			return (TAttribute) attributes[0];
		}

		/// <summary>
		/// Extends the MemberInfo class to return a flag if there is the presence of a custom
		/// attribute.
		/// </summary>
		public static bool HasCustomAttribute<TAttribute>(this MemberInfo memberInfo)
		{
			return memberInfo.HasCustomAttribute(typeof (TAttribute));
		}

		/// <summary>
		/// Extends the MemberInfo class to return a flag if there is the presence of a custom
		/// attribute.
		/// </summary>
		public static bool HasCustomAttribute<TAttribute>(
			this MemberInfo memberInfo,
			bool inherited)
		{
			return memberInfo.HasCustomAttribute(typeof (TAttribute), inherited);
		}

		/// <summary>
		/// Extends the Type class to return a flag if there is the presence of a custom
		/// attribute.
		/// </summary>
		/// <param name="memberInfo">The member info.</param>
		/// <param name="attributeType">Type of the attribute.</param>
		/// <returns>
		/// 	<c>true</c> if [has custom attribute] [the specified type]; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasCustomAttribute(
			this MemberInfo memberInfo,
			Type attributeType)
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
		public static bool HasCustomAttribute(
			this MemberInfo memberInfo,
			Type attributeType,
			bool inherited)
		{
			// Check for null parameters.
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}

			// Go through the attributes of the member type and look for at least one.
			return (memberInfo.GetCustomAttributes(attributeType, inherited).Length > 0);
		}

		#endregion
	}
}
