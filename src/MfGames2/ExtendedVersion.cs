// <copyright file="ExtendedVersion.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Implements a full-featured version parsing and comparison class. This, like 
    /// <see cref="string">String</see> is an immutable class. Any methods that would 
    /// somehow change a field instead return a new ExtendedVersion object.
    /// </summary>
    [Serializable]
    public class ExtendedVersion
    {
        #region Static Fields

        /// <summary>
        /// Contains the simple matcher for string (number, followed
        /// by... stuff)
        /// </summary>
        private static readonly Regex RegexPart = new Regex(@"(\d+)([\d\w]*)");

        /// <summary>
        /// Contains the regex for whitespace matching.
        /// </summary>
        private static readonly Regex RegexSpace = new Regex(@"\s");

        #endregion

        #region Fields

        /// <summary>
        /// Contains the numeric parts of the version
        /// </summary>
        private readonly int[] numerics;

        /// <summary>
        /// Contains the string parts of the version.
        /// </summary>
        private readonly string[] strings;

        /// <summary>
        /// Contains the string version.
        /// </summary>
        private readonly string version;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Constructs an empty version with a version of zero ("0").
        /// </summary>
        public ExtendedVersion()
            : this("0")
        {
        }

        /// <summary>
        /// Constructs a version using the given string as the
        /// version. This breaks up the version into version parts (broken
        /// down by periods (".") and slashes ("-"). A version part
        /// consists of a number, followed optionally by a string. If the
        /// version cannot be parsed, it throws an MfGamesException.
        /// </summary>
        /// <param name="version">
        /// </param>
        public ExtendedVersion(string version)
        {
            // Check for null and blank
            if (version == null || version.Trim() == string.Empty)
            {
                throw new Exception("Cannot parse a null or blank version");
            }

            // Save the string version and remove the spaces
            this.version = version = version.Trim();

            // Check for spaces
            if (RegexSpace.IsMatch(version))
            {
                throw new Exception("Versions cannot have whitespace");
            }

            // Split the version into parts. We also allocate the space for
            // everything before parsing.
            string[] parts = version.Split(
                '.', 
                '-');
            this.numerics = new int[parts.Length];
            this.strings = new string[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                // Check for match and sanity checking
                if (!RegexPart.IsMatch(parts[i]))
                {
                    throw new Exception(
                        "Cannot parse part '" + parts[i] + "' of '" + version
                            + "'");
                }

                // Pull out the parts
                Match match = RegexPart.Match(parts[i]);
                string strNumber = match.Groups[1].Value;
                this.strings[i] = match.Groups[2].Value;

                try
                {
                    // Try to parse the integer
                    this.numerics[i] = int.Parse(strNumber);
                }
                catch
                {
                    throw new Exception(
                        "Cannot numerically parse '" + parts[i] + "'");
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Determines if the two versions are syntactically equal. If all
        /// the version parts are identical, then so is the entire version.
        /// </summary>
        public static bool operator ==(ExtendedVersion v1, 
            ExtendedVersion v2)
        {
            return v1.ToString() == v2.ToString();
        }

        /// <summary>
        /// Determines if the first version is greater than the second
        /// version. See the &lt; operator for more conditions.
        /// </summary>
        public static bool operator >(ExtendedVersion v1, 
            ExtendedVersion v2)
        {
            // Just do the reverse, its easier
            return v2 < v1;
        }

        /// <summary>
        /// Determines if the first version is greater than or equal to
        /// the second version. See the &lt; operator for more conditions.
        /// </summary>
        public static bool operator >=(ExtendedVersion v1, 
            ExtendedVersion v2)
        {
            // Just do the reverse, its easier
            return v1 == v2 || v2 < v1;
        }

        /// <summary>
        /// Determines if the two versions are syntactically equal. If all
        /// the version parts are identical, then so is the entire version.
        /// </summary>
        public static bool operator !=(ExtendedVersion v1, 
            ExtendedVersion v2)
        {
            return v1.ToString() != v2.ToString();
        }

        /// <summary>
        /// Determines if the first is less than the second one. There are
        /// some conditions where a version is neither less than or greater
        /// than another version, specifcally with version parts that have
        /// text in it.
        /// </summary>
        public static bool operator <(ExtendedVersion v1, 
            ExtendedVersion v2)
        {
            // Make sure v1 has the less parts, for simplicicity.
            bool swapped = false;

            if (v1.numerics.Length > v2.numerics.Length)
            {
                ExtendedVersion v3 = v2;
                v2 = v1;
                v1 = v3;
                swapped = true;
            }

            // Go through the various parts
            for (int i = 0; i < v1.numerics.Length; i++)
            {
                // Get the parts
                int num1 = v1.numerics[i];
                string str1 = v1.strings[i];
                int num2 = v2.numerics[i];
                string str2 = v2.strings[i];

                // Make sure strings match. If they do not, then the versions
                // will never match.
                if (str1 != str2)
                {
                    return swapped;
                }

                // Compare the numbers. If num1 is less than num2, then the
                // rest of the version will be less. If it is the reverse,
                // return it.
                if (num1 < num2)
                {
                    return !swapped;
                }

                if (num1 > num2)
                {
                    return swapped;
                }
            }

            // We never got something that explicitly was less or invalid,
            // so assume false (equals).
            return swapped;
        }

        /// <summary>
        /// Determines if the first version is less than or equal to
        /// the second version. See the &lt; operator for more conditions.
        /// </summary>
        public static bool operator <=(ExtendedVersion v1, 
            ExtendedVersion v2)
        {
            // Just do the reverse, its easier
            return v1 == v2 || v1 < v2;
        }

        /// <summary>
        /// A Debian-like parsing of version numbers that encodes the
        /// operation into the string. For example, "&gt; 2.3.4" would be
        /// true if the ExtendedVersion object was 2.3.5 but not 2.3.3 or 2.3.4.
        /// 
        /// The following operations are allowed:
        ///   "&lt;", "&lt;=", "=", "&gt;", "&gt;="
        /// 
        /// There may be any number of spaces between the op and the version.
        /// </summary>
        /// <param name="operation">
        /// </param>
        /// <returns>
        /// </returns>
        public bool Compare(string operation)
        {
            // Pull out the parts using substrings.
            string op;
            string ver;

            if (operation.StartsWith(">="))
            {
                op = ">=";
                ver = operation.Substring(2)
                    .Trim();
            }
            else if (operation.StartsWith("<="))
            {
                op = "<=";
                ver = operation.Substring(2)
                    .Trim();
            }
            else if (operation.StartsWith("<"))
            {
                op = "<";
                ver = operation.Substring(1)
                    .Trim();
            }
            else if (operation.StartsWith(">"))
            {
                op = ">";
                ver = operation.Substring(1)
                    .Trim();
            }
            else if (operation.StartsWith("="))
            {
                op = "=";
                ver = operation.Substring(1)
                    .Trim();
            }
            else
            {
                return false;
            }

            // Check the op
            var v = new ExtendedVersion(ver);

            switch (op)
            {
                case "<":
                    return this < v;
                case "<=":
                    return this <= v;
                case "=":
                    return this == v;
                case ">":
                    return this > v;
                case ">=":
                    return this >= v;
                default:
                    throw new Exception("Cannot identify operation: " + op);
            }
        }

        /// <summary>
        /// Determines if the object is equal to the current one. In cases
        /// where the object is not a ExtendedVersion class, it returns false.
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <returns>
        /// </returns>
        public override bool Equals(object obj)
        {
            try
            {
                var v = obj as ExtendedVersion;
                return this == v;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Overrides the hash code for the version, which is based on all
        /// the version parts.
        /// </summary>
        /// <returns>
        /// </returns>
        public override int GetHashCode()
        {
            return this.ToString()
                .GetHashCode();
        }

        /// <summary>
        /// Returns the text version of the string.
        /// </summary>
        /// <returns>
        /// </returns>
        public override string ToString()
        {
            return this.version;
        }

        #endregion
    }
}