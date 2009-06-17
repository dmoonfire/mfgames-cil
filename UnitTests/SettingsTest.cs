using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MfGames.Settings;

using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class SettingsTest
	{
		[Test]
		public void LoadInt32()
		{
			var settings = new SettingsCollection();
			int value = 23;
			settings.Load(value);
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

		private class Settings1
		{
			public Settings1(float value)
			{
				field1 = value;
			}

			[Settings]
			public int Property1 { get; set; }

			[Settings]
			public string Property2 { get; set; }

			[Settings]
			private float field1;

			public float Field1
			{
				get
				{
					return field1;
				}
			}
		}
	}
}
