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
using System.Collections.Generic;
using System.Reflection;

using MfGames.Extensions;
using MfGames.Utility;

#endregion

namespace MfGames.Settings
{
	/// <summary>
	/// Implements a generic collection of settings and properties which other classes can retrieve data from. This
	/// allows for arbitrary objects to be used as settings with a centralized repository of the actual settings.
	/// </summary>
	public class SettingsCollection
	{
		#region Collection Methods

		private readonly Dictionary<string, TypeSettingsCollection> typeSettings = new Dictionary<string, TypeSettingsCollection>();

		/// <summary>
		/// Gets the type settings for the entire collection.
		/// </summary>
		/// <value>The type settings.</value>
		public Dictionary<string, TypeSettingsCollection> TypeSettings
		{
			get { return typeSettings; }
		}

		/// <summary>
		/// Gets the specified settings and populates the settingsObject.
		/// </summary>
		/// <param name="settingsObject">The settings object.</param>
		/// <returns></returns>
		public bool Load(object settingsObject)
		{
			// If we have a null value, then just indicate we couldn't do anything.
			if (settingsObject == null)
			{
				return false;
			}

			// Grab the type, as a string because we might not have it next time, and see if we have
			// a type in the dictionary.
			string typeName = settingsObject.GetType().ToString();

			if (!TypeSettings.ContainsKey(typeName))
			{
				// Nothing in the dictionary for this.
				return false;
			}

			// We have a settings object, so grab that. We use reflection to scan the settings object
			// and populate any fields or properties within that object.
			TypeSettingsCollection typeSettingCollection = TypeSettings[typeName];
			return Load(settingsObject, typeSettingCollection);
		}

		/// <summary>
		/// Loads the specified settings object from the collection and returns the results.
		/// </summary>
		/// <param name="settingsObject">The settings object.</param>
		/// <param name="collection">The collection.</param>
		/// <returns></returns>
		private static bool Load(object settingsObject, TypeSettingsCollection collection)
		{
			// Use reflection to scan through the settings. We know this isn't null because of the calling method.
			Type type = settingsObject.GetType();

			// Go through the fields.
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				// Check for the presents of the settings custom attribute.
				if (!fieldInfo.HasCustomAttribute(typeof(SettingsAttribute)))
				{
					// No attribute, no processing.
					continue;
				}

				// Use the name to get the value.
				if (!collection.ContainsKey(fieldInfo.Name))
				{
					// We don't have it in our collection, so move on.
					continue;
				}

				// Pull out the field and map it to the appropriate type.
				string value = collection[fieldInfo.Name];

				// Depending on the type, we have to use different methods.
				Type convertType = fieldInfo.FieldType;
				object convertedValue = ExtendedConvert.ChangeType(value, convertType);
				fieldInfo.SetValue(settingsObject, convertedValue);
			}

			// Go through the properties.
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				// Check for the presents of the settings custom attribute.
				if (!propertyInfo.HasCustomAttribute(typeof(SettingsAttribute)))
				{
					// No attribute, no processing.
					continue;
				}

				// Use the name to get the value.
				if (!collection.ContainsKey(propertyInfo.Name))
				{
					// We don't have it in our collection, so move on.
					continue;
				}

				// Pull out the property and map it to the appropriate type.
				string value = collection[propertyInfo.Name];

				// Depending on the type, we have to use different methods.
				Type convertType = propertyInfo.PropertyType;
				object convertedValue = ExtendedConvert.ChangeType(value, convertType);
				propertyInfo.SetValue(settingsObject, convertedValue, null);
			}

			// We are good, so return success.
			return true;
		}

		/// <summary>
		/// Sets the specified settings from the given object.
		/// </summary>
		/// <param name="settingsObject">The settings object.</param>
		public void Store(object settingsObject)
		{
			// Ignore null objects since we can't do anything with those.
			if (settingsObject == null)
			{
				return;
			}

			// Pull out the type for this object and create a collection if needed.
			string typeName = settingsObject.GetType().ToString();
			TypeSettingsCollection typeSettingsCollection;

			if (!TypeSettings.ContainsKey(typeName))
			{
				// Create a new collection object to store.
				typeSettingsCollection = new TypeSettingsCollection();
				typeSettingsCollection.TypeName = typeName;
				TypeSettings.Add(typeName, typeSettingsCollection);
			}
			else
			{
				typeSettingsCollection = TypeSettings[typeName];
			}

			// Persist the contents of the settings object into the collection.
			Store(settingsObject, typeSettingsCollection);
		}

		/// <summary>
		/// Stores the specified settings object into the given second-level collection.
		/// </summary>
		/// <param name="settingsObject">The settings object.</param>
		/// <param name="collection">The collection.</param>
		private static void Store(object settingsObject, TypeSettingsCollection collection)
		{
			// Use reflection to scan through the settings. We know this isn't null because of the calling method.
			Type type = settingsObject.GetType();

			// Scan through the fields of the settings object.
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				// Check for the presents of the settings custom attribute.
				if (!fieldInfo.HasCustomAttribute(typeof(SettingsAttribute)))
				{
					// No attribute, no processing.
					continue;
				}

				// Depending on the type, we have to use different methods to get the data.
				object value = fieldInfo.GetValue(settingsObject);

				// Remove the contents of the old settings.
				collection.Remove(fieldInfo.Name);

				if (value == null)
				{
					continue;
				}

				// Otherwise, we set the value within the collection.
				collection.Add(fieldInfo.Name, ExtendedConvert.ToString(value));
			}

			// Scan through the properties of the settings object.
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				// Check for the presents of the settings custom attribute.
				if (!propertyInfo.HasCustomAttribute(typeof(SettingsAttribute)))
				{
					// No attribute, no processing.
					continue;
				}

				// Depending on the type, we have to use different methods to get the data.
				object value = propertyInfo.GetValue(settingsObject, null);

				// Remove the contents of the old settings.
				collection.Remove(propertyInfo.Name);

				if (value == null)
				{
					continue;
				}

				// Otherwise, we set the value within the collection.
				collection.Add(propertyInfo.Name, ExtendedConvert.ToString(value));
			}
		}

		#endregion

		#region Nested type: TypeSettingsCollection

		/// <summary>
		/// The collected settings for a specific type, stored in a dictionary. This is used for
		/// the "top level" settings objects.
		/// </summary>
		public sealed class TypeSettingsCollection : Dictionary<string, string>
		{
			/// <summary>
			/// Gets or sets the name of the type associated with this collection.
			/// </summary>
			/// <value>The name of the type.</value>
			public string TypeName { get; set; }
		}

		#endregion
	}
}