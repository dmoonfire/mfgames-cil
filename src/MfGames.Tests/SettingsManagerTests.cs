// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using MfGames.HierarchicalPaths;
using MfGames.Settings;
using MfGames.Settings.Enumerations;
using NUnit.Framework;

namespace UnitTests
{
	/// <summary>
	/// Tests various functionality of the <see cref="SettingsManager"/>.
	/// </summary>
	[TestFixture]
	public class SettingsManagerTests
	{
		#region Methods

		/// <summary>
		/// Tests adding one to the manager.
		/// </summary>
		[Test]
		public void AddOneToManager()
		{
			// Setup
			var settingsManager = new SettingsManager();

			// Operation
			settingsManager.Set(new HierarchicalPath("/a"), new SettingsA1(2, "two"));

			// Verification
			Assert.AreEqual(1, settingsManager.Count);
		}

		/// <summary>
		/// Tests adding one to the manager then flushing it.
		/// </summary>
		[Test]
		public void AddOneToManagerAndFlush()
		{
			// Setup
			var settingsManager = new SettingsManager();

			// Operation
			settingsManager.Set(new HierarchicalPath("/a"), new SettingsA1(3, "three"));
			settingsManager.Flush();

			// Verification
			Assert.AreEqual(1, settingsManager.Count);
		}

		/// <summary>
		/// Saves a setting A and loads it as B, but without mapping.
		/// </summary>
		[Test]
		public void ConvertFromAtoB()
		{
			// Setup
			var settingsManager = new SettingsManager();
			settingsManager.Set(new HierarchicalPath("/a"), new SettingsA1(1, "one"));

			// Operation
			var b = settingsManager.Get<SettingsA2>(
				"/a", SettingSearchOptions.SerializeDeserializeMapping);

			// Verification
			Assert.AreEqual(1, settingsManager.Count);
			Assert.AreEqual(1, b.A);
			Assert.AreEqual("one", b.B);
		}

		/// <summary>
		/// Tests the initial state of an empty manager.
		/// </summary>
		[Test]
		public void EmptyManager()
		{
			// Setup

			// Operation
			var settingsManager = new SettingsManager();

			// Verification
			Assert.AreEqual(0, settingsManager.Count);
		}

		[Test]
		public void GetAllFromParents()
		{
			// Arrange
			var settings1 = new SettingsManager();
			var settings2 = new SettingsManager();
			var settings3 = new SettingsManager();

			settings1.Set("/a", new SettingsA1(99, "settings1"));
			settings1.Flush();
			settings1.Parent = settings2;

			settings2.Parent = settings3;

			settings3.Set("/a", new SettingsA2(11, "settings3"));
			settings3.Flush();

			// Act
			IList<SettingsA1> settings = settings1.GetAll<SettingsA1>("/a");

			// Assert
			Assert.AreEqual(2, settings.Count);

			Assert.AreEqual(99, settings[0].A);
			Assert.AreEqual("settings1", settings[0].B);

			Assert.AreEqual(11, settings[1].A);
			Assert.AreEqual("settings3", settings[1].B);
		}

		/// <summary>
		/// Tests serializing, then deserializing an empty manager.
		/// </summary>
		[Test]
		public void SerializeEmptyManager()
		{
			// Setup
			var settingsManager = new SettingsManager();

			// Operation
			var writer = new StringWriter();
			settingsManager.Save(writer);

			var reader = new StringReader(writer.ToString());
			settingsManager = new SettingsManager();
			settingsManager.Load(reader);

			// Verification
			Assert.AreEqual(0, settingsManager.Count);
		}

		[Test]
		public void SerializeWrappedEmptyManager()
		{
			// Arrange
			var settingsManager = new SettingsManager();
			var stringWriter = new StringWriter();

			using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
			{
				xmlWriter.WriteStartElement("wrapper");
				settingsManager.Save(xmlWriter);
				xmlWriter.WriteStartElement("guard");
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
			}

			// Act
			var stringReader = new StringReader(stringWriter.ToString());
			var xmlReader = XmlReader.Create(stringReader);
			settingsManager = new SettingsManager();
			settingsManager.Load(xmlReader);

			// Assert
			Assert.AreEqual(SettingsManager.SettingsNamespace, xmlReader.NamespaceURI);
			Assert.AreEqual("settings", xmlReader.LocalName);
			Assert.AreEqual(XmlNodeType.Element, xmlReader.NodeType);
			Assert.IsTrue(xmlReader.IsEmptyElement);
		}

		[Test]
		public void SerializeWrappedManager()
		{
			// Arrange: Set up the settings manager.
			var settingsManager = new SettingsManager();

			settingsManager.Set("/a",new SettingsA1(99,"settings1"));

			// Arrange: Write out a wrapper, the settings, and a guard element.
			var stringWriter = new StringWriter();

			using(XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
			{
				xmlWriter.WriteStartElement("wrapper");
				settingsManager.Save(xmlWriter);
				xmlWriter.WriteStartElement("guard");
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
			}

			// Act
			var stringReader = new StringReader(stringWriter.ToString());
			var xmlReader = XmlReader.Create(stringReader);
			settingsManager = new SettingsManager();
			settingsManager.Load(xmlReader);

			// Assert
			Assert.AreEqual(SettingsManager.SettingsNamespace,xmlReader.NamespaceURI);
			Assert.AreEqual("settings",xmlReader.LocalName);
			Assert.AreEqual(XmlNodeType.EndElement,xmlReader.NodeType);
		}
		#endregion

		#region Nested Type: SettingsA1

		[XmlRoot("SettingsA")]
		public class SettingsA1
		{
			#region Constructors

			public SettingsA1()
			{
			}

			public SettingsA1(
				int a,
				string b)
			{
				A = a;
				B = b;
			}

			#endregion

			#region Fields

			public int A;
			public string B;

			#endregion
		}

		#endregion

		#region Nested Type: SettingsA2

		[XmlRoot("SettingsA")]
		public class SettingsA2
		{
			#region Constructors

			public SettingsA2()
			{
				A = -123;
				B = "uninitialized";
			}

			public SettingsA2(
				int a,
				string b)
			{
				A = a;
				B = b;
			}

			#endregion

			#region Fields

			public int A;
			public string B;

			#endregion
		}

		#endregion

		#region Nested Type: SettingsC

		public class SettingsC
		{
		}

		#endregion
	}
}
