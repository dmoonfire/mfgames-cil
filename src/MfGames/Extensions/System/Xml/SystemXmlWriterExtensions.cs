// <copyright file="SystemXmlWriterExtensions.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System.Xml;

namespace MfGames.Extensions.System.Xml
{
	/// <summary>
	/// Extensions for <see cref="XmlWriter"/>.
	/// </summary>
	public static class SystemXmlWriterExtensions
	{
		#region Public Methods and Operators

		/// <summary>
		/// Writes the element string with a local ane and value, but
		/// only if the value is non-null.
		/// </summary>
		/// <param name="writer">
		/// The writer to write out the results.
		/// </param>
		/// <param name="localName">
		/// The local name of the element.
		/// </param>
		/// <param name="value">
		/// The value of the element.
		/// </param>
		public static void WriteNonNullElementString(
			this XmlWriter writer,
			string localName,
			string value)
		{
			if (value != null)
			{
				writer.WriteElementString(
					localName,
					value);
			}
		}

		/// <summary>
		/// Writes the element string with a namespace, local, and value, but
		/// only if the value is non-null.
		/// </summary>
		/// <param name="writer">
		/// The writer to write out the results.
		/// </param>
		/// <param name="localName">
		/// The local name of the element.
		/// </param>
		/// <param name="ns">
		/// The namespace of the element.
		/// </param>
		/// <param name="value">
		/// The value of the element.
		/// </param>
		public static void WriteNonNullElementString(
			this XmlWriter writer,
			string localName,
			string ns,
			string value)
		{
			if (value != null)
			{
				writer.WriteElementString(
					localName,
					ns,
					value);
			}
		}

		/// <summary>
		/// Writes the element string with a prefix, namespace, local, and value, but
		/// only if the value is non-null.
		/// </summary>
		/// <param name="writer">
		/// The writer to write out the results.
		/// </param>
		/// <param name="prefix">
		/// The XML prefix to use.
		/// </param>
		/// <param name="localName">
		/// The local name of the element.
		/// </param>
		/// <param name="ns">
		/// The namespace of the element.
		/// </param>
		/// <param name="value">
		/// The value of the element.
		/// </param>
		public static void WriteNonNullElementString(
			this XmlWriter writer,
			string prefix,
			string localName,
			string ns,
			string value)
		{
			if (value != null)
			{
				writer.WriteElementString(
					prefix,
					localName,
					ns,
					value);
			}
		}

		#endregion
	}
}
