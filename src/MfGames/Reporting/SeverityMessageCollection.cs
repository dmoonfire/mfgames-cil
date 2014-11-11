// <copyright file="SeverityMessageCollection.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Reporting
{
    using System;
    using System.Collections.Generic;

    using MfGames.Enumerations;

    /// <summary>
    /// Contains an unordered collection of messages along with various query
    /// methods for determining the contents of the collection
    /// </summary>
    public class SeverityMessageCollection : HashSet<SeverityMessage>
    {
        #region Public Properties

        /// <summary>
        /// Gets the highest severity in the collection. If there are no elements
        /// in the collection, this returns Severity.Debug.
        /// </summary>
        public Severity? HighestSeverity
        {
            get
            {
                // Check for an empty collection.
                if (this.Count == 0)
                {
                    return null;
                }

                // Go through and gather the severity from the collection.
                var highest = (int)Severity.Debug;

                foreach (SeverityMessage message in this)
                {
                    highest = Math.Max(
                        (int)message.Severity, 
                        highest);
                }

                // Return the resulting severity.
                return (Severity)highest;
            }
        }

        #endregion
    }
}