using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MfGames.Settings.Design
{
	/// <summary>
	/// Generates out a XML stream with a given design configuration.
	/// </summary>
	public class XmlDesignGenerater
	{
		#region Writing
		/// <summary>
		/// Generates out to the given file.
		/// </summary>
		/// <param name="file"></param>
		/// <param name="configuration"></param>
		public void Generate(FileInfo inputXml, FileInfo csFile)
		{
			// Make sure the XML file exists
			if (!inputXml.Exists)
				throw new Exception("Cannot read the given XML file");

			// At this point, the positional variables are properly set
			// and we have everything setup. The basic functionality is to
			// take the input XML, transform it using the given
			// stylesheet, and output the results.

			// Set up the XSLT
			XslCompiledTransform trans = new XslCompiledTransform();

			using (Stream s = GetType().Assembly.GetManifestResourceStream(
					"MfGames.Settings.Design.XmlSettings.xsl"))
			{
				TextReader tr = new StreamReader(s);
				XmlReader xr = new XmlTextReader(tr);

				trans.Load(xr);
			}

			// Build up the XSLT arguments
			XsltArgumentList xargs = new XsltArgumentList();

			// Load in the input XML
			XPathDocument input = new XPathDocument(inputXml.FullName);

			// Set up the output to properly open and close
			using (FileStream fs = csFile.Open(FileMode.Create))
			{
				// Write out the document
				trans.Transform(input, xargs, fs);
			}
		}
		#endregion
	}
}
