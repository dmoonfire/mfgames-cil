using System;
using System.Collections.Generic;

namespace MfGames.Settings.Design
{
	/// <summary>
	/// Represents the top-level configuration object for various settings.
	/// </summary>
	public class DesignConfiguration
	{
		#region Properties
		/// <summary>
		/// Contains the name of this configuration class.
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// Contains the namespace to generate for a file.
		/// </summary>
		public string Namespace { get; set; }
		#endregion

		#region Groups
		private List<DesignGroup> groups = new List<DesignGroup>();

		/// <summary>
		/// Retrives a design group of the given name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public DesignGroup this[string name]
		{
			get
			{
				foreach (DesignGroup group in groups)
					if (group.Name == name)
						return group;

				throw new Exception("Cannot find group: " + name);
			}
		}

		/// <summary>
		/// Contains a list of groups in this settings object.
		/// </summary>
		public List<DesignGroup> Groups
		{
			get { return groups; }
		}

		/// <summary>
		/// Returns true if there is a group with the given name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool Contains(string name)
		{
			foreach (DesignGroup group in groups)
				if (group.Name == name)
					return true;

			return false;
		}
		#endregion
	}
}
