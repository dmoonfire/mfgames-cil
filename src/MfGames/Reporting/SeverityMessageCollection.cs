// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;
using System.Collections.Generic;

namespace MfGames.Reporting
{
	/// <summary>
	/// Contains an unordered collection of messages along with various query
	/// methods for determining the contents of the collection
	/// </summary>
	public class SeverityMessageCollection: HashSet<SeverityMessage>
	{
		#region Properties

		/// <summary>
		/// Gets the highest severity in the collection. If there are no elements
		/// in the collection, this returns Severity.Debug.
		/// </summary>
		public Severity? HighestSeverity
		{
			get
			{
				// Check for an empty collection.
				if (Count == 0)
				{
					return null;
				}

				// Go through and gather the severity from the collection.
				var highest = (int) Severity.Debug;

				foreach (SeverityMessage message in this)
				{
					highest = Math.Max((int) message.Severity, highest);
				}

				// Return the resulting severity.
				return (Severity) highest;
			}
		}

		#endregion
	}
}
