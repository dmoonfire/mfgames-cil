using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using MfGames.Utility;

namespace MfGames.Settings.Design
{
	/// <summary>
	/// XML reader that reads in the common XML design settings and creates the
	/// internal structures to represent it.
	/// </summary>
	public class XmlDesignReader
	{
		#region Reading
		/// <summary>
		/// Reads in the design settings from the given file.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public DesignConfiguration Read(FileInfo file)
		{
			// Make sure the file exists
			if (!file.Exists)
				throw new Exception("Cannot read settings from non-existent file: " + file);

			// Open it up
			using (FileStream stream = file.OpenRead())
			{
				// Wrap it in a reader
				XmlReader xml = XmlReader.Create(stream);
				DesignConfiguration settings = Read(xml);
				xml.Close();
				return settings;
			}
		}

		/// <summary>
		/// Reads the design settings from a given XML stream.
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public DesignConfiguration Read(XmlReader xml)
		{
			// Create the settings
			DesignConfiguration settings = new DesignConfiguration();

			// Read through the file
			while (xml.Read())
			{
				// Check for the end
				if (xml.NodeType == XmlNodeType.EndElement &&
					xml.LocalName == "configuration")
				{
					// We are done parsing it
					break;
				}

				// Check for group
				else if (xml.NodeType == XmlNodeType.Element)
				{
					if (xml.LocalName == "class" && !xml.IsEmptyElement)
					{
						settings.ClassName = xml.ReadString();
					}
					else if (xml.LocalName == "namespace" && !xml.IsEmptyElement)
					{
						settings.Namespace = xml.ReadString();
					}

					else if (xml.LocalName == "group")
					{
						// Read in a group
						DesignGroup group = new DesignGroup();
						Read(xml, group);
						settings.Groups.Add(group);
					}
				}
			}

			// Return the results
			return settings;
		}

		/// <summary>
		/// Reads in a design group into memory.
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="group"></param>
		private void Read(XmlReader xml, DesignGroup group)
		{
			// Read through the file
			while (xml.Read())
			{
				// Check for the end
				if (xml.NodeType == XmlNodeType.EndElement &&
					xml.LocalName == "group")
				{
					// We are done parsing it
					break;
				}

				// Figure out what to do based on the open tag
				else if (xml.NodeType == XmlNodeType.Element)
				{
					if (xml.LocalName == "name" && !xml.IsEmptyElement)
					{
						// Read in the name of the group
						group.Name = xml.ReadString();
					}
					else if (xml.LocalName == "setting" || xml.LocalName == "constant" || xml.LocalName == "transient")
					{
						// Create the setting object
						DesignSetting setting = new DesignSetting();

						// Set up the usage type for non-settings
						if (xml.LocalName == "constant")
							setting.Usage = UsageType.Constant;
						if (xml.LocalName == "transient")
							setting.Usage = UsageType.Transient;

						// Read in the setting
						Read(xml, setting);
						setting.Group = group.Name;
						group.Settings.Add(setting);
					}
				}
			}
		}

		/// <summary>
		/// Reads in a design setting into memory.
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="group"></param>
		private void Read(XmlReader xml, DesignSetting setting)
		{
			// Ignore empty
			if (xml.IsEmptyElement)
				return;

			// Read through the file
			while (xml.Read())
			{
				// Check for the end
				if (xml.NodeType == XmlNodeType.EndElement &&
					(xml.LocalName == "setting" || xml.LocalName == "constant" || xml.LocalName == "transient"))
				{
					// We are done parsing it
					break;
				}

				// Figure out what to do based on the open tag
				else if (xml.NodeType == XmlNodeType.Element)
				{
					if (xml.LocalName == "name" && !xml.IsEmptyElement)
					{
						// Read in the name of the group
						setting.Name = xml.ReadString();
					}
					else if (xml.LocalName == "default" && !xml.IsEmptyElement)
					{
						// Read in the name of the group
						setting.Default = xml.ReadString();
					}
					else if (xml.LocalName == "type" && !xml.IsEmptyElement)
					{
						// See if we have an attribute
						if (xml["format"] != null)
						{
							// Parse the format
							setting.Format = Enum<FormatType>.Parse(xml["format"]);
						}

						// Read in the name of the group
						setting.TypeName = xml.ReadString();
					}
				}
			}
		}
		#endregion
	}
}
