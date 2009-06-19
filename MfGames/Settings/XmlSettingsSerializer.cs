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
using System.IO;
using System.Text;
using System.Xml;

#endregion

namespace MfGames.Settings
{
	public class XmlSettingsSerializer
	{
		#region Constants

		/// <summary>
		/// Contains the namespace for settings object.
		/// </summary>
		private const string Namespace = "http://mfgames.com/2009/mfgames-settings";

		#endregion

		#region Reading

		/// <summary>
		/// Reads the specified file and returns the resulting settings.
		/// </summary>
		/// <param name="fileInfo">The file info.</param>
		/// <returns></returns>
		public SettingsCollection Read(FileInfo fileInfo)
		{
			var settings = new SettingsCollection();
			Read(fileInfo, settings);
			return settings;
		}

		/// <summary>
		/// Reads the specified file and places them into the settings object.
		/// </summary>
		/// <param name="fileInfo">The file info.</param>
		/// <param name="settings">The settings.</param>
		public void Read(FileInfo fileInfo, SettingsCollection settings)
		{
			// Check for valid properties.
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}

			if (fileInfo == null)
			{
				throw new ArgumentNullException("fileInfo");
			}

			// Open up a stream into this file while letting other files read it also.
			using (FileStream fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				Read(fileStream, settings);
			}
		}

		/// <summary>
		/// Reads the specified stream and populates the given collections object.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="settings">The settings collection.</param>
		public void Read(Stream stream, SettingsCollection settings)
		{
			// Wrap this in a XML reader.
			using (XmlReader xml = XmlReader.Create(stream))
			{
				Read(xml, settings);
			}
		}

		/// <summary>
		/// Reads the specified XML and populates the settings object.
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <param name="settings">The settings.</param>
		public void Read(XmlReader xml, SettingsCollection settings)
		{
			// Loop through the XML stream.
			while (xml.Read())
			{
				// We care about open tags at this level.
				if (xml.NodeType == XmlNodeType.Element)
				{
					// Check for the local name.
					if (xml.LocalName == "type-settings")
					{
						// We have a settings object, so grab the type and make sure we have it.
						string typeName = xml["type"];

						if (!settings.TypeSettings.ContainsKey(typeName))
						{
							settings.TypeSettings.Add(typeName, new SettingsCollection.TypeSettingsCollection());
						}

						// Grab the collection and read in its contents.
						Read(xml, settings.TypeSettings[typeName]);
					}
				}
			}
		}

		/// <summary>
		/// Reads the specified XML and populates the type settings collection.
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <param name="settings">The settings.</param>
		private void Read(XmlReader xml, SettingsCollection.TypeSettingsCollection settings)
		{
			while (xml.Read())
			{
				// Check for closing our element out.
				if (xml.NodeType == XmlNodeType.EndElement &&
					xml.LocalName == "type-settings")
				{
					// We are done.
					break;
				}

				// If we have an open tag, check for the elements we care about.
				if (xml.NodeType == XmlNodeType.Element &&
					xml.LocalName == "key-value")
				{
					// Read in the key value.
					string key = xml["key"];
					string value = xml["value"];

					// Add it to the collection.
					if (settings.ContainsKey(key))
					{
						settings.Remove(key);
					}

					settings.Add(key, value);
				}
			}
		}

		#endregion

		#region Writing

		/// <summary>
		/// Writes the specified settings to the given file.
		/// </summary>
		/// <param name="fileInfo">The file info.</param>
		/// <param name="settings">The settings.</param>
		public void Write(FileInfo fileInfo, SettingsCollection settings)
		{
			using (FileStream fileStream = fileInfo.Open(FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				Write(fileStream, settings);
			}
		}

		/// <summary>
		/// Writes the specified stream and writes out the given settings object.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="settings">The settings.</param>
		public void Write(Stream stream, SettingsCollection settings)
		{
			// It takes up more space, but indented looks a bit neater to debug.
			var xmlSettings = new XmlWriterSettings();
			xmlSettings.ConformanceLevel = ConformanceLevel.Document;
			xmlSettings.Encoding = Encoding.UTF8;
			xmlSettings.Indent = true;
			xmlSettings.IndentChars = "\t";

			// Create the XML writer and write out the data.
			using (XmlWriter xml = XmlWriter.Create(stream, xmlSettings))
			{
				Write(xml, settings);
			}
		}

		/// <summary>
		/// Writes the specified XML using the given settings object.
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <param name="settings">The settings.</param>
		public void Write(XmlWriter xml, SettingsCollection settings)
		{
			// Write the opening tag.
			xml.WriteStartElement("settings", Namespace);

			// Go through the type settings.
			foreach (string type in settings.TypeSettings.Keys)
			{
				Write(xml, settings.TypeSettings[type]);
			}

			// Write out the ending tag.
			xml.WriteEndElement();
		}

		/// <summary>
		/// Writes the specified XML the components of the given settings object.
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <param name="settings">The settings.</param>
		private static void Write(XmlWriter xml, SettingsCollection.TypeSettingsCollection settings)
		{
			// Write the opening tag.
			xml.WriteStartElement("type-settings", Namespace);
			xml.WriteAttributeString("type", settings.TypeName);
			
			// Write out the top-level elements.
			if (settings.Count > 0)
			{
				// Write out a starting tag.
				xml.WriteStartElement("key-values", Namespace);

				// Go through all the elements.
				foreach (string key in settings.Keys)
				{
					xml.WriteStartElement("key-value", Namespace);
					xml.WriteAttributeString("key", key);
					xml.WriteAttributeString("value", settings[key]);
					xml.WriteEndElement();
				}

				// Finish up the tag.
				xml.WriteEndElement();
			}

			// Write out the ending tag.
			xml.WriteEndElement();
		}

		#endregion
	}
}