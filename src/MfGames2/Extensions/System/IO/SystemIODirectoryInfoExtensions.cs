// <copyright file="SystemIODirectoryInfoExtensions.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Extensions.System.IO
{
    using global::System.IO;

    /// <summary>
    /// Defines various extensions to System.IO.DirectoryInfo.
    /// </summary>
    public static class SystemIODirectoryInfoExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the directory info from the directory.
        /// </summary>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <param name="dirname">
        /// The dirname.
        /// </param>
        /// <returns>
        /// </returns>
        public static DirectoryInfo GetDirectoryInfo(
            this DirectoryInfo directory, 
            string dirname)
        {
            return new DirectoryInfo(
                Path.Combine(
                    directory.FullName, 
                    dirname));
        }

        /// <summary>
        /// Gets a child FileInfo object from the directory.
        /// </summary>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// </returns>
        public static FileInfo GetFileInfo(
            this DirectoryInfo directory, 
            string filename)
        {
            return new FileInfo(
                Path.Combine(
                    directory.FullName, 
                    filename));
        }

        #endregion
    }
}