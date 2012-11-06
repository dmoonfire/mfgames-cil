// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

#region Namespaces

using System;
using System.Reflection;

#endregion

namespace MfGames.Extensions.System.Reflection
{
	/// <summary>
	/// Extensions to System.Reflection.ParameterInfo.
	/// </summary>
	public static class SystemReflectionParameterInfoExtensions
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
		public static bool HasCustomAttribute(
			this ParameterInfo memberInfo,
			Type attributeType)
		{
			return HasCustomAttribute(
				memberInfo,
				attributeType,
				true);
		}

		/// <summary>
		/// Extends the PropertyInfo class to return a flag if there is the presence of a custom
		/// attribute.
		/// </summary>
		public static bool HasCustomAttribute<TAttribute>(
			this PropertyInfo propertyInfo)
		{
			return propertyInfo.HasCustomAttribute(typeof (TAttribute));
		}

		/// <summary>
		/// Extends the PropertyInfo class to return a flag if there is the presence of a custom
		/// attribute.
		/// </summary>
		public static bool HasCustomAttribute<TAttribute>(
			this PropertyInfo propertyInfo,
			bool inherited)
		{
			return propertyInfo.HasCustomAttribute(
				typeof (TAttribute),
				inherited);
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
			this ParameterInfo memberInfo,
			Type attributeType,
			bool inherited)
		{
			// Check for null parameters.
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}

			// Go through the attributes of the member type and look for at least one.
			return (memberInfo.GetCustomAttributes(
				attributeType,
				true).Length > 0);
		}
	}
}
