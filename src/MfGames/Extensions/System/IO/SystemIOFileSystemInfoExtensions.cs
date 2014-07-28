// <copyright file="SystemIOFileSystemInfoExtensions.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Extensions.System.IO
{
    using global::System;

    using global::System.IO;

    using global::System.Text;

    /// <summary>
    /// Common extension methods for System.IO.FileSystemInfo objects.
    /// </summary>
    public static class SystemIOFileSystemInfoExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Ensures that the parent directory exists for the given object.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        public static void EnsureParentExists(this FileSystemInfo info)
        {
            // If we have a null, then don't bother doing anything.
            if (info == null)
            {
                return;
            }

            // Grab the parent item for this info.
            DirectoryInfo parent = Directory.GetParent(info.FullName);

            if (parent == null)
            {
                return;
            }

            // Check to see if the parent exists, if it doesn't, then make it.
            if (!parent.Exists)
            {
                parent.Create();
            }
        }

        /// <summary>
        /// Gets a string that represents the relative path between two items.
        /// </summary>
        /// <param name="targetInfo">
        /// </param>
        /// <param name="relatedInfo">
        /// </param>
        /// <returns>
        /// </returns>
        public static string GetRelativePathTo(
            this FileSystemInfo targetInfo, FileSystemInfo relatedInfo)
        {
            string targetPath = relatedInfo.FullName;
            string relatedPath = targetInfo.FullName;
            string[] absoluteDirectories =
                targetPath.Split(Path.DirectorySeparatorChar);
            string[] relativeDirectories =
                relatedPath.Split(Path.DirectorySeparatorChar);

            // Get the shortest of the two paths
            int length = absoluteDirectories.Length < relativeDirectories.Length
                ? absoluteDirectories.Length
                : relativeDirectories.Length;

            // Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            // Find common root
            for (index = 0; index < length; index++)
            {
                if (absoluteDirectories[index] == relativeDirectories[index])
                {
                    lastCommonRoot = index;
                }
                else
                {
                    break;
                }
            }

            // If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
            {
                throw new ArgumentException("Paths do not have a common base");
            }

            // Build up the relative path
            var relativePath = new StringBuilder();

            // Add on the ..
            for (index = lastCommonRoot + 1;
                index < absoluteDirectories.Length;
                index++)
            {
                if (absoluteDirectories[index].Length > 0)
                {
                    relativePath.Append(".." + Path.DirectorySeparatorChar);
                }
            }

            // Add on the folders
            for (index = lastCommonRoot + 1;
                index < relativeDirectories.Length - 1;
                index++)
            {
                relativePath.Append(
                    relativeDirectories[index] + Path.DirectorySeparatorChar);
            }

            relativePath.Append(
                relativeDirectories[relativeDirectories.Length - 1]);

            return relativePath.ToString();
        }

        #endregion
    }
}