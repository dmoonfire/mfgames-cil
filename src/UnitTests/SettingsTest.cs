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

using MfGames.Settings;

using NUnit.Framework;

#endregion

namespace UnitTests
{
	[TestFixture]
	public class SettingsTest
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
		public void LoadInt32()
		{
			var settings = new SettingsCollection();
			int value = 23;
			settings.Load(value);
		}

		[Test]
		public void StoreLoadSettings1()
		{
			var value = new Settings1(111);
			value.Property1 = -1;

			var settings = new SettingsCollection();
			settings.Store(value);

			var value2 = new Settings1(111);
			settings.Load(value2);

			Assert.AreEqual(-1, value2.Property1);
		}

		[Test]
		public void StoreLoadSettings1Field1()
		{
			var value = new Settings1(222.1f);

			var settings = new SettingsCollection();
			settings.Store(value);

			var value2 = new Settings1(111);
			settings.Load(value2);

			Assert.AreEqual(222.1f, value2.Field1);
		}

		[Test]
		public void StoreLoadSettings1Property2()
		{
			var value = new Settings1(222.1f);
			value.Property2 = "gary";

			var settings = new SettingsCollection();
			settings.Store(value);

			var value2 = new Settings1(111);
			value2.Property2 = "steve";
			settings.Load(value2);

			Assert.AreEqual("gary", value2.Property2);
		}

		[Test]
		public void StoreLoadSettings1Property2Null()
		{
			var value = new Settings1(222.1f);
			value.Property2 = null;

			var settings = new SettingsCollection();
			settings.Store(value);

			var value2 = new Settings1(111);
			value2.Property2 = "steve";
			settings.Load(value2);

			Assert.AreEqual("steve", value2.Property2);
		}

		[Test]
		public void StoreSettings1()
		{
			var value = new Settings1(111);
			value.Property1 = -1;
			value.Property2 = "bob";

			var settings = new SettingsCollection();
			settings.Store(value);
		}
	}
}