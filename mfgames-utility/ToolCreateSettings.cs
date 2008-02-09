#region Copyright
/*
 * Copyright (C) 2005-2008, Moonfire Games
 *
 * This file is part of MfGames.Utility.
 *
 * The MfGames.Utility library is free software; you can redistribute
 * it and/or modify it under the terms of the GNU Lesser General
 * Public License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

using MfGames.Utility;
using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

/// <summary>
/// Implements a tool that takes a settings.xml file template and
/// creates the settings object around it. This settings file is used
/// to wrap around a settings store and gives a compile-time checking
/// of properties.
///
/// This is done by taking the given settings xml file and
/// transforming it using the embedded stylesheet in a manner that
/// will allow the resulting output to be compiled as a specific class
/// in the system.
/// </summary>
public class ToolCreateSettings
: ITool
{
	/// <summary>
	/// Executes the service with the given parameters.
	/// </summary>
	public void Process(string [] args)
	{
		// At this point, the positional variables are properly set
		// and we have everything setup. The basic functionality is to
		// take the input XML, transform it using the given
		// stylesheet, and output the results.

		// Set up the XSLT
		XslTransform trans = new XslTransform();

		if (inputXsl == null)
		{
			using (Stream s = GetType().Assembly.GetManifestResourceStream(
					"mfgames_utility.ToolCreateSettings.xsl"))
			{
				TextReader tr = new StreamReader(s);
				XmlReader xr = new XmlTextReader(tr);
				
				trans.Load(xr);
			}
		}
		else
		{
			using (FileStream fs = inputXsl.OpenRead())
			{
				TextReader tr = new StreamReader(fs);
				XmlReader xr = new XmlTextReader(tr);
				
				trans.Load(xr);
			}
		}

		// Build up the XSLT arguments
		XsltArgumentList xargs = new XsltArgumentList();
		
		xargs.AddParam("namespace", "", Namespace);
		xargs.AddParam("class", "", Class);
		xargs.AddParam("access", "", Access);

		// Load in the input XML
		XPathDocument input = new XPathDocument(inputXml.FullName);

		// Set up the output to properly open and close
		using (FileStream fs = 
			outputClass.Open(FileMode.Create, FileAccess.Write))
		{
			// Write out the document
			trans.Transform(input, xargs, fs);
		}
	}

	#region Tool Properties
	private FileInfo inputXml, outputClass, inputXsl;
	private string useNamespace = "";
	private string access = "public";
	private string useClassname = "GeneratedSettings";

	/// <summary>
	/// Contains the access level for the settings.
	/// </summary>
	public string Access
	{
		get { return access; }
		set { access = value ?? "public"; }
	}

	/// <summary>
	/// Contains the name of the class to generate.
	/// </summary>
	public string Class
	{
		get { return useClassname; }
		set { useClassname = value ?? "GeneratedSettings"; }
	}

	/// <summary>
	/// Contains the input XML which the settings file will be
	/// generated from.
	/// </summary>
	[Positional(0)]
	public FileInfo InputXml
	{
		get { return inputXml; }
		set
		{
			if (value == null)
				throw new Exception("Cannot assign a null input XML");

			inputXml = value;

			if (!inputXml.Exists)
				throw new Exception("Input XML " + inputXml
					+ " does not exist");
		}
	}

	/// <summary>
	/// Output file to generate. If the file exists, it will be
	/// overwritten when this tool is run.
	/// </summary>
	[Positional(1)]
	public FileInfo OutputClass
	{
		get { return outputClass; }
		set
		{
			if (value == null)
				throw new Exception("Cannot assign a null output file");

			outputClass = value;
		}
	}

	/// <summary>
	/// Contains the namespace to use in the output.
	/// </summary>
	public string Namespace
	{
		get { return useNamespace; }
		set { useNamespace = value ?? ""; }
	}

	/// <summary>
	/// Contains the XSL to use instead of the embedded one.
	/// </summary>
	public FileInfo Xsl
	{
		get { return inputXsl; }
		set
		{
			if (value != null && !value.Exists)
				throw new Exception("XSL doesn't exist: " + value);

			inputXsl = value;
		}
	}
	#endregion

	#region Metadata Properties
	/// <summary>
	/// Returns a service description. Typically this is a single phrase
	/// or sentance, with a period at the end.
	/// </summary>
	public string Description
	{
		get { return "Generates a C# class from a given settings XML."; }
	}

	/// <summary>
	/// Returns a list of service names that this service handles. These
	/// are the second argument of the system, which is a string name,
	/// typically dash-delimeted for words.
	/// </summary>
	public string ToolName { get { return "create-settings"; } }
	#endregion
}
