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
using System.IO;

#endregion

namespace MfGames
{
    /// <summary>
    /// Contains a series of static methods used to finding and managing directories
    /// and files inside the configuration area. On a Unix platform, this will be
    /// located in $HOME/.config and in Application Data on the Windows platform.
    /// </summary>
    public static class ConfigStorage
    {
        /// <summary>
        /// Contains the configuration directory for the current user.
        /// </summary>
        public static string ConfigurationDirectory
        {
            get
            {
                return
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.ApplicationData);
            }
        }

        /// <summary>
        /// Contains the configuration directory for the current user.
        /// </summary>
        public static DirectoryInfo ConfigurationDirectoryInfo
        {
            get { return new DirectoryInfo(ConfigurationDirectory); }
        }

        /// <summary>
        /// Retrieves, creating if needed, a directory inside the user's configuration area.
        /// </summary>
        public static string GetDirectory(params string[] relativeDirectories)
        {
            return GetDirectoryInfo(true, relativeDirectories).FullName;
        }

        /// <summary>
        /// Retrieves, creating if requested, a directory inside the user's configuration area.
        /// </summary>
        public static string GetDirectory(
            bool create,
            params string[] relativeDirectories)
        {
            return GetDirectoryInfo(create, relativeDirectories).FullName;
        }

        /// <summary>
        /// Retrieves, creating if needed, a directory inside the user's configuration area.
        /// </summary>
        public static DirectoryInfo GetDirectoryInfo(
            params string[] relativeDirectories)
        {
            return GetDirectoryInfo(true, relativeDirectories);
        }

        /// <summary>
        /// Retrieves, creating if requested, a directory inside the user's configuration area.
        /// </summary>
        public static DirectoryInfo GetDirectoryInfo(
            bool create,
            params string[] relativeDirectories)
        {
            // Start at the top-level configuration directory and create all the relative
            // directories before returning the inner-mode directory.
            DirectoryInfo directory = ConfigurationDirectoryInfo;

            foreach (string relativeDirectory in relativeDirectories)
            {
                // Create the new child directory.
                directory =
                    new DirectoryInfo(
                        Path.Combine(directory.FullName, relativeDirectory));

                // Create the directory if we were requested to create it and it doesn't exist.
                if (create && !directory.Exists)
                {
                    directory.Create();
                }
            }

            // Return the resulting directory.
            return directory;
        }
    }
}