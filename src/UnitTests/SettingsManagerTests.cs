#region Copyright and License

// Copyright (C) 2005-2011 by Moonfire Games
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
using System.Xml.Serialization;

using MfGames;
using MfGames.HierarchicalPaths;
using MfGames.Settings;
using MfGames.Settings.Enumerations;

using NUnit.Framework;

#endregion

namespace UnitTests
{
    /// <summary>
    /// Tests various functionality of the <see cref="SettingsManager"/>.
    /// </summary>
    [TestFixture]
    public class SettingsManagerTests
    {
        #region Settings Test

        /// <summary>
        /// Tests adding one to the manager.
        /// </summary>
        [Test]
        public void AddOneToManager()
        {
            // Setup
            var settingsManager = new SettingsManager();

            // Operation
            settingsManager.Set(
                new HierarchicalPath("/a"), new SettingsA1(2, "two"));

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
            settingsManager.Set(
                new HierarchicalPath("/a"), new SettingsA1(3, "three"));
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
            settingsManager.Set(
                new HierarchicalPath("/a"), new SettingsA1(1, "one"));

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

        #endregion

        #region Serialization Tests

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

        #endregion

        #region Nested type: SettingsA1

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

            #region Properties

            public int A;
            public string B;

            #endregion
        }

        #endregion

        #region Nested type: SettingsA2

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

            #region Properties

            public int A;
            public string B;

            #endregion
        }

        #endregion

        #region Nested type: SettingsC

        public class SettingsC
        {
        }

        #endregion
    }
}