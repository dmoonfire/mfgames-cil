#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
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
using System.Collections.Generic;
using System.IO;
using System.Xml;

using MfGames.Logging;

#endregion

namespace MfGames.Settings
{
	/// <summary>
	/// Implements a basic XML-backed settings store that saves
	/// properties in a simple format using a given FileInfo object as
	/// the location for the XML.
	/// </summary>
	public class XmlSettingsStore : ISettingsStore
	{
		#region Constructors

		/// <summary>
		/// Constructs an empty store.
		/// </summary>
		public XmlSettingsStore()
		{
		}

		/// <summary>
		/// Constructs an XmlSettingStore object and associated it
		/// with a file, but does not actually load the settings into
		/// memory.
		/// </summary>
		public XmlSettingsStore(FileInfo file)
		{
			// Valid the variable and save it
			if (file == null)
				throw new Exception("Cannot back a XML settings object with a null file");

			this.file = file;
			log = new Log(file.Name);
		}

		#endregion

		#region Backing Properties

		private FileInfo file;
		private Log log;

		/// <summary>
		/// Clears out all variables stored within the store.
		/// </summary>
		public void Clear()
		{
			groups.Clear();
		}

		/// <summary>
		/// Loads a setting object from the stored file object. This
		/// does not remove any pre-existing variables.
		/// </summary>
		public void Load()
		{
			Load(file);
		}

		/// <summary>
		/// Loads a setting object from the given file object. This
		/// does not remove any pre-existing variables.
		/// </summary>
		public void Load(FileInfo file)
		{
			// Sanity checking
			if (file == null)
				throw new Exception("Cannot load from a null file");

			// We load the XML, but ignore errors
			try
			{
				// Open up the stream
				using (
					FileStream fs = file.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					// Create an XML reader
					XmlReader xml = XmlReader.Create(fs);
					Load(xml);
				}
			}
			catch (Exception e)
			{
				log.Alert("Error loading file: {0}", e.Message);
			}
		}

		/// <summary>
		/// Reads configuration/settings data from a given XML stream
		/// and properly returns when they are completed.
		/// </summary>
		public void Load(XmlReader xml)
		{
			// Sanity checking
			if (xml == null)
				throw new Exception("Cannot load from a null XML stream");

			// Parse through it
			string group = null;

			while (xml.Read())
			{
				// Check for setting objects
				if (xml.NodeType == XmlNodeType.Element)
				{
					// Load the group tag
					if (xml.LocalName == "group")
						group = xml["name"];

					// Load the settings
					if (xml.LocalName == "setting")
					{
						string name = xml["name"];
						string value = xml["value"];
						Logger.Debug(GetType(), "Setting: {0}.{1} = {2}", group, name, value);

						this[group, name] = value;
					}
				}
				else if (xml.NodeType == XmlNodeType.EndElement)
				{
					// Check for the end
					if (xml.LocalName == "settings")
						break;
				}
			}
		}

		/// <summary>
		/// Writes out the configuration object to the stored file.
		/// </summary>
		public void Save()
		{
			Save(file);
		}

		/// <summary>
		/// Writes out the configuration object to the given
		/// file. This does not change the public file object.
		/// </summary>
		public void Save(FileInfo file)
		{
			// We load the XML, but ignore errors
			try
			{
				// Open up the stream
				using (FileStream fs = file.Open(FileMode.Create))
				{
					// Create an XML writer
					XmlWriter xml = XmlWriter.Create(fs);
					xml.WriteStartDocument();
					Save(xml);
					xml.WriteEndDocument();
					xml.Close();
					fs.Close();
				}
			}
			catch (Exception e)
			{
				log.Alert("Error writing file: {0}", e.Message);
			}
		}

		/// <summary>
		/// Writes out the settings information to the given stream.
		/// </summary>
		public void Save(XmlWriter xml)
		{
			// Start with the tag
			xml.WriteStartElement("settings");

			// Go through the group
			foreach (string group in groups.Keys)
			{
				// Write out the group
				xml.WriteStartElement("group");
				xml.WriteAttributeString("name", group);

				// Go through the keys
				foreach (string var in groups[group].Keys)
				{
					// Write it out
					xml.WriteStartElement("setting");
					xml.WriteAttributeString("name", var);
					xml.WriteAttributeString("value", groups[group][var]);
					xml.WriteEndElement();
				}

				// Finish up
				xml.WriteEndElement();
			}

			// Finish the settings
			xml.WriteEndElement();
		}

		#endregion

		#region Accessing

		private Dictionary<string, Dictionary<string, string>> groups =
			new Dictionary<string, Dictionary<string, string>>();

		/// <summary>
		/// This is the primary method for getting and setting values
		/// in the system. If a null is set, then the value is
		/// removed.
		/// </summary>
		public string this[string group, string variable]
		{
			get
			{
				// See if we have the group
				if (!groups.ContainsKey(group))
					return null;

				// Get the group
				Dictionary<string, string> d = groups[group];

				if (!d.ContainsKey(variable))
					return null;

				// Return the results
				return d[variable];
			}

			set
			{
				// If we are null, then we are removing
				if (value == null)
				{
					// Make sure we have a group
					if (!groups.ContainsKey(group))
						// We don't, so don't bother
						return;

					// Get the group
					Dictionary<string, string> d = groups[group];
					d.Remove(variable);

					// If 'd' is empty, then remove it from the group
					if (d.Count == 0)
						groups.Remove(group);
				}
				else
				{
					// Make sure we have a group
					if (!groups.ContainsKey(group))
						groups[group] = new Dictionary<string, string>();

					// Set the value
					Dictionary<string, string> d = groups[group];
					d[variable] = value;
				}
			}
		}

		/// <summary>
		/// This method returns true if a specific group/variable
		/// exists within the system.
		/// </summary>
		public bool Contains(string group, string variable)
		{
			return groups.ContainsKey(group) && groups[group].ContainsKey(variable);
		}

		#endregion
	}
}