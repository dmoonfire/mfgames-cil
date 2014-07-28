// <copyright file="SystemTypeExtensions.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Extensions.System
{
    using global::System;

    /// <summary>
    /// Defines extensions for System.Type.
    /// </summary>
    public static class SystemTypeExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the custom attribute or null if one doesn't exist.
        /// </summary>
        /// <typeparam name="TAttribute">
        /// The type of the attribute.
        /// </typeparam>
        /// <param name="type">
        /// The member info.
        /// </param>
        /// <returns>
        /// </returns>
        public static TAttribute GetCustomAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            object[] attributes = type.GetCustomAttributes(
                typeof(TAttribute), true);

            if (attributes.Length == 0)
            {
                return null;
            }

            return (TAttribute)attributes[0];
        }

        /// <summary>
        /// Extends the Type class to return a flag if there is the presence of a custom
        /// attribute.
        /// </summary>
        /// <param name="type">
        /// The member info.
        /// </param>
        /// <param name="attributeType">
        /// Type of the attribute.
        /// </param>
        /// <returns>
        /// <c>true</c> if [has custom attribute] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasCustomAttribute(
            this Type type, Type attributeType)
        {
            return HasCustomAttribute(type, attributeType, true);
        }

        /// <summary>
        /// Extends the Type class to return a flag if there is the presence of a custom
        /// attribute.
        /// </summary>
        /// <param name="type">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool HasCustomAttribute<TAttribute>(this Type type)
        {
            return type.HasCustomAttribute(typeof(TAttribute));
        }

        /// <summary>
        /// Extends the Type class to return a flag if there is the presence of a custom
        /// attribute.
        /// </summary>
        /// <param name="type">
        /// </param>
        /// <param name="inherited">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool HasCustomAttribute<TAttribute>(
            this Type type, bool inherited)
        {
            return type.HasCustomAttribute(typeof(TAttribute), inherited);
        }

        /// <summary>
        /// Extends the Type class to return a flag if there is the presence of a custom
        /// attribute.
        /// </summary>
        /// <param name="type">
        /// The member info.
        /// </param>
        /// <param name="attributeType">
        /// Type of the attribute.
        /// </param>
        /// <param name="inherited">
        /// if set to <c>true</c> [inherited].
        /// </param>
        /// <returns>
        /// <c>true</c> if [has custom attribute] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasCustomAttribute(
            this Type type, Type attributeType, bool inherited)
        {
            // Check for null parameters.
            if (attributeType == null)
            {
                throw new ArgumentNullException("attributeType");
            }

            // Go through the attributes of the member type and look for at least one.
            return type.GetCustomAttributes(attributeType, inherited).Length
                > 0;
        }

        #endregion
    }
}