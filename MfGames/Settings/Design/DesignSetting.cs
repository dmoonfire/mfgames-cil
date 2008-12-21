using System;
namespace MfGames.Settings.Design
{
	/// <summary>
	/// Design class for settings. This contains the various properties common with
	/// all settings and is used for the design stylesheet to create a custom, typesafe
	/// class.
	/// </summary>
	public class DesignSetting
		: IComparable<DesignSetting>
	{
		#region Properties
		/// <summary>
		/// Contains the default value for this setting. If this is null, then
		/// there is no default.
		/// </summary>
		public string Default {
			get;
			set;
		}

		/// <summary>
		/// Contains the format of this setting object.
		/// </summary>
		public FormatType Format {
			get;
			set;
		}

		/// <summary>
		/// The group name for the settings. All settings are organized on a
		/// group/name basis where "bob/gary" and "steve/gary" are separate
		/// properties (but must be in two different setting containers).
		/// </summary>
		public string Group {
			get;
			set;
		}

		/// <summary>
		/// Sets or gets the name of the setting. This is the public property
		/// of the settings object, so it must be a valid C# name.
		/// </summary>
		public string Name {
			get;
			set;
		}

		/// <summary>
		/// Contains a string version of the type of this setting.
		/// </summary>
		public string TypeName {
			get;
			set;
		}

		/// <summary>
		/// This determines the usage of this setting, either a settable
		/// value or a constant.
		/// </summary>
		public UsageType Usage {
			get;
			set;
		}
		#endregion

		#region IComparable<DesignSetting> Members
		public int CompareTo(DesignSetting other)
		{
			return Name.CompareTo(other.Name);
		}
		#endregion
	}
}
