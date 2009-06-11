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
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

#endregion

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
			var trans = new XslCompiledTransform();

			using (
				Stream s =
					GetType().Assembly.GetManifestResourceStream(
						"MfGames.Settings.Design.XmlSettings.xsl"))
			{
				TextReader tr = new StreamReader(s);
				XmlReader xr = new XmlTextReader(tr);

				trans.Load(xr);
			}

			// Build up the XSLT arguments
			var xargs = new XsltArgumentList();

			// Load in the input XML
			var input = new XPathDocument(inputXml.FullName);

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