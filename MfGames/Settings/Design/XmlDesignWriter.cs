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

using System.IO;
using System.Text;
using System.Xml;

#endregion

namespace MfGames.Settings.Design
{
	/// <summary>
	/// Writes out a XML stream with a given design configuration.
	/// </summary>
	public class XmlDesignWriter
	{
		#region Writing

		/// <summary>
		/// Writes out to the given file.
		/// </summary>
		/// <param name="file"></param>
		/// <param name="configuration"></param>
		public void Write(FileInfo file, DesignConfiguration configuration)
		{
			using (FileStream stream = file.Open(FileMode.Create))
			{
				// Set up the settings
				var settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.CloseOutput = true;
				settings.Encoding = Encoding.Unicode;

				// Open up the stream
				XmlWriter xml = XmlWriter.Create(stream, settings);
				xml.WriteStartDocument();
				Write(xml, configuration);
				xml.WriteEndDocument();
				xml.Close();
			}
		}

		/// <summary>
		/// Writes out the configuration to the given XML writer.
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="configuration"></param>
		public void Write(XmlWriter xml, DesignConfiguration configuration)
		{
			// Write out the various tags
			xml.WriteStartElement("configuration",
			                      "http://mfgames.com/2008/mfgames-utility-configuration");

			xml.WriteStartElement("class");
			xml.WriteString(configuration.ClassName);
			xml.WriteEndElement();
			xml.WriteStartElement("namespace");
			xml.WriteString(configuration.Namespace);
			xml.WriteEndElement();

			// Write out the groups
			foreach (DesignGroup group in configuration.Groups)
			{
				// Write out this tag
				xml.WriteStartElement("group");
				xml.WriteStartElement("name");
				xml.WriteString(group.Name);
				xml.WriteEndElement();

				// Write out the settings
				foreach (DesignSetting setting in group.Settings)
				{
					// The starting tag is based on type
					xml.WriteStartElement(setting.Usage.ToString().ToLower());

					xml.WriteStartElement("name");
					xml.WriteString(setting.Name);
					xml.WriteEndElement();

					xml.WriteStartElement("default");
					xml.WriteString(setting.Default);
					xml.WriteEndElement();

					xml.WriteStartElement("type");
					xml.WriteAttributeString("format", setting.Format.ToString());
					xml.WriteString(setting.TypeName);
					xml.WriteEndElement();

					// Finish up
					xml.WriteEndElement();
				}

				// Finish up
				xml.WriteEndElement();
			}

			// Finish up
			xml.WriteEndElement();
		}

		#endregion
	}
}