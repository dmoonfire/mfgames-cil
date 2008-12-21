using System.IO;
using System.Text;
using System.Xml;

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
				      XmlWriterSettings settings = new XmlWriterSettings();
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
			xml.WriteStartElement("configuration", "http://mfgames.com/2008/mfgames-utility-configuration");

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
