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

using MfGames.Settings;

using NUnit.Framework;

#endregion

namespace UnitTests
{
	[TestFixture]
	public class XmlSettingsSerializerTests
	{
		private sealed class Settings1
		{
			[Settings]
			private readonly float field1;

			public Settings1(float value)
			{
				field1 = value;
			}

			[Settings]
			public int Property1 { get; set; }

			[Settings]
			public string Property2 { get; set; }

			public float Field1
			{
				get { return field1; }
			}
		}

		[Test]
		public void ReadFromFile()
		{
			// Write out the object to the settings collection.
			var settings = new SettingsCollection();
			var value = new Settings1(1234567);
			value.Property1 = -123;
			value.Property2 = "gary";
			settings.Store(value);

			// Save the results to a temporary file.
			var file = new FileInfo(Path.GetTempFileName());

			try
			{
				// Create a serializer and write out the file.
				var serializer = new XmlSettingsSerializer();
				serializer.Write(file, settings);

				// Create a new serializer to read in the contents.
				var serializer2 = new XmlSettingsSerializer();
				SettingsCollection settings2 = serializer2.Read(file);
				var value2 = new Settings1(0);
				settings2.Load(value2);

				// Assert that our contents were identical.
				Assert.AreEqual(-123, value2.Property1, "Property2 did not match");
				Assert.AreEqual("gary", value2.Property2, "Property1 did not match");
				Assert.AreEqual(1234567, value2.Field1, "Field1 did not match");
			}
			finally
			{
				// Delete the file to clean up after ourselves.
				file.Delete();
			}
		}

		[Test]
		public void SaveToFile()
		{
			// Write out the object to the settings collection.
			var settings = new SettingsCollection();
			var value = new Settings1(1234567);
			value.Property1 = -123;
			value.Property2 = "gary";
			settings.Store(value);

			// Save the results to a temporary file.
			var file = new FileInfo(Path.GetTempFileName());

			try
			{
				// Create a serializer and write out the file.
				var serializer = new XmlSettingsSerializer();
				serializer.Write(file, settings);
			}
			finally
			{
				// Delete the file to clean up after ourselves.
				file.Delete();
			}
		}
	}
}