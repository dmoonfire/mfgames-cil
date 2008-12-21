using System;
using System.Collections.Generic;

namespace MfGames.Settings.Design
{
	/// <summary>
	/// Represents a group within the design system.
	/// </summary>
	public class DesignGroup
		: IComparable<DesignGroup>
	{
		#region Properties
		private List<DesignSetting> settings = new List<DesignSetting>();

		/// <summary>
		/// Gets or sets the name for this group.
		/// </summary>
		public string Name {
			get;
			set;
		}
		#endregion

		#region Settings
		/// <summary>
		/// Retrives a design setting of the given name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public DesignSetting this[string name]
		{
			get
			{
				foreach (DesignSetting setting in settings)
					if (setting.Name == name)
						return setting;

				throw new Exception("Cannot find setting: " + name);
			}
		}

		/// <summary>
		/// Contains an unsorted list of setting objects in this group.
		/// </summary>
		public List<DesignSetting> Settings
		{
			get
			{
				return settings;
			}
		}
		#endregion

		#region IComparable<DesignGroup> Members
		/// <summary>
		/// Compares the design group to another group.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(DesignGroup other)
		{
			return Name.CompareTo(other.Name);
		}
		#endregion
	}
}
