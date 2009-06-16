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
using MfGames.Utility.Annotations;

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

			if (!typeSettings.ContainsKey(typeName))
			{
				// Nothing in the dictionary for this.
				return false;
			}

			// We have a settings object, so grab that. We use reflection to scan the settings object
			// and populate any fields or properties within that object.
			TypeSettingsCollection typeSettingCollection = typeSettings[typeName];
			return Load(settingsObject, typeSettingCollection);
		}

		/// <summary>
		/// Loads the specified settings object from the collection and returns the results.
		/// </summary>
		/// <param name="settingsObject">The settings object.</param>
		/// <param name="collection">The collection.</param>
		/// <returns></returns>
		private static bool Load(object settingsObject, ObjectSettingsCollection collection)
		{
			// Use reflection to scan through the settings. We know this isn't null because of the calling method.
			Type type = settingsObject.GetType();
			MemberInfo[] members =
				type.GetMembers(BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic);

			foreach (MemberInfo memberInfo in members)
			{
				// Check for the presents of the settings custom attribute.
				if (!memberInfo.HasCustomAttribute(typeof(SettingsAttribute)))
				{
					// No attribute, no processing.
					continue;
				}

				// Use the name to get the value.
				if (!collection.ContainsKey(memberInfo.Name))
				{
					// We don't have it in our collection, so move on.
					continue;
				}

				// Pull out the field and map it to the appropriate type.
				string value = collection[memberInfo.Name];

				// Depending on the type, we have to use different methods.
				if (memberInfo is PropertyInfo)
				{
					var propertyInfo = (PropertyInfo) memberInfo;
					Type convertType = propertyInfo.PropertyType;
					object convertedValue = ExtendedConvert.ChangeType(value, convertType);
					propertyInfo.SetValue(settingsObject, convertedValue, null);
				}

				if (memberInfo is FieldInfo)
				{
					var fieldInfo = (FieldInfo) memberInfo;
					Type convertType = fieldInfo.FieldType;
					object convertedValue = ExtendedConvert.ChangeType(value, convertType);
					fieldInfo.SetValue(settingsObject, convertedValue);
				}
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
			TypeSettingsCollection typeSettingsCollection = null;

			if (!typeSettings.ContainsKey(typeName))
			{
				// Create a new collection object to store.
				typeSettingsCollection = new TypeSettingsCollection();
				typeSettings.Add(typeName, typeSettingsCollection);
			}
			else
			{
				typeSettingsCollection = typeSettings[typeName];
			}

			// Persist the contents of the settings object into the collection.
			Store(settingsObject, typeSettingsCollection);
		}

		private static void Store(object settingsObject, ObjectSettingsCollection collection)
		{
			// Use reflection to scan through the settings. We know this isn't null because of the calling method.
			Type type = settingsObject.GetType();
			MemberInfo[] members =
				type.GetMembers(BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic);

			foreach (MemberInfo memberInfo in members)
			{
				// Check for the presents of the settings custom attribute.
				if (!memberInfo.HasCustomAttribute(typeof(SettingsAttribute)))
				{
					// No attribute, no processing.
					continue;
				}

				// Use the name to get the value.
				if (!collection.ContainsKey(memberInfo.Name))
				{
					// We don't have it in our collection, so move on.
					continue;
				}

				// Depending on the type, we have to use different methods to get the data.
				object value = null;

				if (memberInfo is PropertyInfo)
				{
					var propertyInfo = (PropertyInfo) memberInfo;
					value = propertyInfo.GetValue(settingsObject, null);
				}

				if (memberInfo is FieldInfo)
				{
					var fieldInfo = (FieldInfo) memberInfo;
					value = fieldInfo.GetValue(settingsObject);
				}

				// Remove the contents of the old settings.
				collection.Remove(memberInfo.Name);

				if (value == null)
				{
					continue;
				}

				// Otherwise, we set the value within the collection.
				collection.Add(memberInfo.Name, ExtendedConvert.ToString(value));
			}
		}

		#endregion

		#region Nested type: ObjectSettingsCollection

		private class ObjectSettingsCollection : Dictionary<string, string>
		{
		}

		#endregion

		#region Nested type: TypeSettingsCollection

		private sealed class TypeSettingsCollection : ObjectSettingsCollection
		{
		}

		#endregion
	}
}