// <copyright file="SettingsManager.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    using MfGames.HierarchicalPaths;

    /// <summary>
    /// Defines a manager of settings. A SettingsManager can zero or more parent
    /// settings to allow a recursive looking of values. It uses 
    /// <see cref="HierarchicalPath"/>to identify the key in the settings and uses
    /// generics to provide type-safe settings.
    /// </summary>
    public class SettingsManager
    {
        #region Constants

        /// <summary>
        /// Contains the namespace used for the settings XML.
        /// </summary>
        public const string SettingsNamespace =
            "urn:mfgames.com/mfgames-cil/settings/0";

        #endregion

        #region Fields

        /// <summary>
        /// </summary>
        private readonly Dictionary<HierarchicalPath, object> objectSettings;

        /// <summary>
        /// </summary>
        private readonly Dictionary<HierarchicalPath, string> xmlSettings;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsManager"/> class.
        /// </summary>
        public SettingsManager()
        {
            this.xmlSettings = new Dictionary<HierarchicalPath, string>();
            this.objectSettings = new Dictionary<HierarchicalPath, object>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of settings in the manager.
        /// </summary>
        public int Count
        {
            get
            {
                return this.objectSettings.Count + this.xmlSettings.Count;
            }
        }

        /// <summary>
        /// Gets the number of deserialized settings.
        /// </summary>
        public int LoadedCount
        {
            get
            {
                return this.objectSettings.Count;
            }
        }

        /// <summary>
        /// Gets or sets the parent settings object, used for searching.
        /// </summary>
        /// <value>
        /// The parent settings manager.
        /// </value>
        public SettingsManager Parent { get; set; }

        /// <summary>
        /// Gets the number of settings that are in serialized form.
        /// </summary>
        public int StoredCount
        {
            get
            {
                return this.xmlSettings.Count;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Determines whether the settings contain the specified path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// <c>true</c> if [contains] [the specified path]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string path)
        {
            var hierarchicalPath = new HierarchicalPath(path);
            return Contains(hierarchicalPath);
        }

        /// <summary>
        /// Determines whether the settings contain the specified path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// <c>true</c> if [contains] [the specified path]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(HierarchicalPath path)
        {
            return this.Contains(
                path, 
                SettingSearchOptions.None);
        }

        /// <summary>
        /// Determines whether the settings contain the specified path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="searchOptions">
        /// The search options.
        /// </param>
        /// <returns>
        /// <c>true</c> if [contains] [the specified path]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(
            HierarchicalPath path, 
            SettingSearchOptions searchOptions)
        {
            // If this settings has a setting for this specific path, then
            // attempt to map it. If we can't map it, return false.
            if (this.ContainsInternal(path))
            {
                return true;
            }

            // We aren't in the exact path of this item, so we need to search
            // for the parent paths or up the HierarchicalPath reference.
            bool searchParent = (searchOptions
                & SettingSearchOptions.SearchParentSettings)
                == SettingSearchOptions.SearchParentSettings;
            bool searchPaths = (searchOptions
                & SettingSearchOptions.SearchHierarchicalParents)
                == SettingSearchOptions.SearchHierarchicalParents;
            bool parentSettingsFirst = (searchOptions
                & SettingSearchOptions.ParentSettingsFirst)
                == SettingSearchOptions.ParentSettingsFirst;

            // If we are searching neither, then we couldn't find it.
            if (!searchParent && !searchPaths)
            {
                return false;
            }

            // Check to see if we are searching parents first.
            if (searchParent && parentSettingsFirst && this.Parent != null)
            {
                // We are searching the parent. We only have the parent search
                // its parents since we'll be searching the path in the next
                // step.
                if (this.Parent.Contains(
                    path, 
                    SettingSearchOptions.SearchParentSettings))
                {
                    return true;
                }
            }

            // Search the path three until we get to the top.
            if (searchPaths && path.Count != 1)
            {
                // Grab the current path.
                HierarchicalPath currentPath = path.Parent;

                while (true)
                {
                    // Try to retrieve the object.
                    if (this.Contains(
                        currentPath, 
                        searchOptions))
                    {
                        // We found it through recursion.
                        return true;
                    }

                    // We couldn't find it, so move up a level.
                    if (currentPath.Count == 1)
                    {
                        break;
                    }

                    currentPath = currentPath.Parent;
                }
            }

            // We have found it so far, so check to see if we are searching
            // parents
            if (searchParent && !parentSettingsFirst && this.Parent != null)
            {
                // We are searching the parent. We only have the parent search
                // its parents since we'll be searching the path in the next
                // step.
                if (this.Contains(
                    path, 
                    SettingSearchOptions.SearchParentSettings))
                {
                    return true;
                }
            }

            // We couldn't find it.
            return false;
        }

        /// <summary>
        /// Flushes this instance and serialized all deserialized settings.
        /// </summary>
        public void Flush()
        {
            // Pull out all the keys.
            var keys = new List<HierarchicalPath>(this.objectSettings.Keys);

            foreach (HierarchicalPath path in keys)
            {
                this.Flush(path);
            }
        }

        /// <summary>
        /// Gets a settings at the specific path.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// </returns>
        public TSetting Get<TSetting>(string path) where TSetting : class, new()
        {
            var hierarchicalPath = new HierarchicalPath(path);
            return Get<TSetting>(hierarchicalPath);
        }

        /// <summary>
        /// Gets a settings at the specific path.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// </returns>
        public TSetting Get<TSetting>(HierarchicalPath path)
            where TSetting : class, new()
        {
            return Get<TSetting>(
                path, 
                SettingSearchOptions.None);
        }

        /// <summary>
        /// Gets a settings at the specific path.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="searchOptions">
        /// The search options.
        /// </param>
        /// <returns>
        /// </returns>
        public TSetting Get<TSetting>(
            string path, 
            SettingSearchOptions searchOptions) where TSetting : class, new()
        {
            var hierarchicalPath = new HierarchicalPath(path);
            return Get<TSetting>(
                hierarchicalPath, 
                searchOptions);
        }

        /// <summary>
        /// Gets a settings at the specific path.
        /// </summary>
        /// <param name="path">
        /// </param>
        /// <param name="searchOptions">
        /// </param>
        /// <returns>
        /// </returns>
        public TSetting Get<TSetting>(
            HierarchicalPath path, 
            SettingSearchOptions searchOptions) where TSetting : class, new()
        {
            SettingsManager containingManager;
            return this.Get<TSetting>(
                path, 
                searchOptions, 
                out containingManager);
        }

        /// <summary>
        /// Gets a settings at the specific path.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="searchOptions">
        /// The search options.
        /// </param>
        /// <param name="containingManager">
        /// The containing manager.
        /// </param>
        /// <returns>
        /// </returns>
        public TSetting Get<TSetting>(
            HierarchicalPath path, 
            SettingSearchOptions searchOptions, 
            out SettingsManager containingManager) where TSetting : class, new()
        {
            // Attempt to retrieve the item from the collection.
            SettingSearchOptions newSearchOptions = searchOptions
                | SettingSearchOptions.SerializeDeserializeMapping;

            TSetting output;

            if (this.TryGet(
                path, 
                newSearchOptions, 
                out output, 
                out containingManager))
            {
                return output;
            }

            // We couldn't find it, so create a new one and store it.
            output = new TSetting();
            this.objectSettings[path] = output;
            this.xmlSettings.Remove(path);

            return output;
        }

        /// <summary>
        /// Retrives all the settings of the given path including all the parents.
        /// The order of the resulting enumerable is from the current one with each
        /// parent added to the end. If a given SettingsManager doesn't include the
        /// path, then no item will be included.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// </returns>
        public IList<TSetting> GetAll<TSetting>(string path)
            where TSetting : class, new()
        {
            var hierarchicalPath = new HierarchicalPath(path);
            IList<TSetting> settings = GetAll<TSetting>(hierarchicalPath);
            return settings;
        }

        /// <summary>
        /// Retrives all the settings of the given path including all the parents.
        /// The order of the resulting enumerable is from the current one with each
        /// parent added to the end. If a given SettingsManager doesn't include the
        /// path, then no item will be included.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// </returns>
        public IList<TSetting> GetAll<TSetting>(HierarchicalPath path)
            where TSetting : class, new()
        {
            // Go through the manager, adding each setting, until we run out of
            // parent managers.
            var settings = new List<TSetting>();
            SettingsManager manager = this;

            while (manager != null)
            {
                // See if this manager has that item.
                TSetting setting;

                if (manager.TryGet(
                    path, 
                    SettingSearchOptions.None, 
                    out setting))
                {
                    settings.Add(setting);
                }

                // Move to the parent setting manager.
                manager = manager.Parent;
            }

            // Return the resulting managers.
            return settings;
        }

        /// <summary>
        /// Loads the specified file into the settings.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        public void Load(FileInfo file)
        {
            // Make sure the file exists first.
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            if (!file.Exists)
            {
                throw new FileNotFoundException(
                    "Cannot find settings file.", 
                    file.Name);
            }

            // Open the file as a stream and load it.
            using (Stream stream = file.Open(
                FileMode.Open, 
                FileAccess.Read, 
                FileShare.Read))
            {
                Load(stream);
            }
        }

        /// <summary>
        /// Loads the specified stream into the settings.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        public void Load(Stream stream)
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                Load(reader);
            }
        }

        /// <summary>
        /// Loads the specified reader into the settings.
        /// </summary>
        /// <param name="textReader">
        /// </param>
        public void Load(TextReader textReader)
        {
            using (XmlReader xmlReader = XmlReader.Create(textReader))
            {
                Load(xmlReader);
            }
        }

        /// <summary>
        /// Loads the specified reader into the settings.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        public void Load(XmlReader reader)
        {
            // Read until we find the close tag for the settings.
            while (true)
            {
                // If we aren't in the right namespace, just skip it.
                if (reader.NamespaceURI != SettingsNamespace)
                {
                    // Advance the reader, breaking out if we hit the end of the
                    // XML.
                    if (!reader.Read())
                    {
                        break;
                    }

                    // Loop again to process the next read.
                    continue;
                }

                // Look to see if we have a close tag.
                if (reader.LocalName == "settings")
                {
                    if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        return;
                    }

                    if (reader.NodeType == XmlNodeType.Element
                        && reader.IsEmptyElement)
                    {
                        return;
                    }
                }

                // Look for the opening setting tag.
                if (reader.NodeType == XmlNodeType.Element
                    && reader.LocalName == "setting")
                {
                    // Get the path from the attribute.
                    var path = new HierarchicalPath(reader["path"]);

                    // Pull in the XML directly.
                    string xml = reader.ReadInnerXml();

                    // Put them into the settings.
                    this.xmlSettings[path] = xml;

                    // Because we used the ReadInnerXml(), we don't want to advance
                    // the path.
                    continue;
                }

                // Read the next node.
                if (!reader.Read())
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Removes the specified path from the settings.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public void Remove(HierarchicalPath path)
        {
            this.xmlSettings.Remove(path);
            this.objectSettings.Remove(path);
        }

        /// <summary>
        /// Saves the settings into the specified file.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        public void Save(FileInfo file)
        {
            using (Stream stream = file.Open(
                FileMode.Create, 
                FileAccess.Write))
            {
                Save(stream);
            }
        }

        /// <summary>
        /// Saves the settings into the specified stream.
        /// </summary>
        /// <param name="stream">
        /// </param>
        public void Save(Stream stream)
        {
            using (XmlWriter writer = XmlWriter.Create(stream))
            {
                Save(writer);
            }
        }

        /// <summary>
        /// Saves the settings into the specified writer.
        /// </summary>
        /// <param name="textWriter">
        /// The text writer.
        /// </param>
        public void Save(TextWriter textWriter)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(textWriter))
            {
                Save(xmlWriter);
            }
        }

        /// <summary>
        /// Saves the settings into the specified writer.
        /// </summary>
        /// <param name="writer">
        /// </param>
        public void Save(XmlWriter writer)
        {
            // Start by flushing out all the object settings.
            this.Flush();

            // Write out the starting XML tag.
            writer.WriteStartElement(
                "settings", 
                "settings", 
                SettingsNamespace);

            // Go through all the paths in the settings.
            foreach (HierarchicalPath path in this.xmlSettings.Keys)
            {
                // Write out the setting object.
                writer.WriteStartElement(
                    "settings", 
                    "setting", 
                    SettingsNamespace);
                writer.WriteAttributeString(
                    "path", 
                    path.ToString());

                // Read in the XML into a document.
                var reader = new StringReader(this.xmlSettings[path]);

                var document = new XmlDocument();
                document.Load(reader);

                // Write out the root element of the document but without the
                // XML PI or docutype.
                document.DocumentElement.WriteTo(writer);

                // Finish the setting object.
                writer.WriteEndElement();
            }

            // Close the tag.
            writer.WriteEndElement();
        }

        /// <summary>
        /// Sets the setting to the specified path.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="setting">
        /// The setting.
        /// </param>
        public void Set<TSetting>(
            HierarchicalPath path, 
            TSetting setting)
        {
            this.xmlSettings.Remove(path);
            this.objectSettings[path] = setting;
        }

        /// <summary>
        /// Sets the setting to the specified path.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="setting">
        /// The setting.
        /// </param>
        public void Set<TSetting>(
            string path, 
            TSetting setting)
        {
            var hierarchicalPath = new HierarchicalPath(path);

            Set(
                hierarchicalPath, 
                setting);
        }

        /// <summary>
        /// Tries to get settings at the given path without creating it if
        /// missing.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="searchOptions">
        /// The search options.
        /// </param>
        /// <param name="containingManager">
        /// The containing manager.
        /// </param>
        /// <returns>
        /// </returns>
        public bool TryGet<TSetting>(
            HierarchicalPath path, 
            SettingSearchOptions searchOptions, 
            out SettingsManager containingManager) where TSetting : class, new()
        {
            TSetting output;
            return this.TryGet(
                path, 
                searchOptions, 
                out output, 
                out containingManager);
        }

        /// <summary>
        /// Tries to get settings at the given path without creating it if
        /// missing.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="searchOptions">
        /// The search options.
        /// </param>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <returns>
        /// </returns>
        public bool TryGet<TSetting>(
            HierarchicalPath path, 
            SettingSearchOptions searchOptions, 
            out TSetting output) where TSetting : class, new()
        {
            SettingsManager manager;
            return this.TryGet(
                path, 
                searchOptions, 
                out output, 
                out manager);
        }

        /// <summary>
        /// Tries to get settings at the given path without creating it if missing.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="searchOptions">
        /// The search options.
        /// </param>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <param name="containingManager">
        /// The manager that actually contained the settings retrieved.
        /// </param>
        /// <returns>
        /// </returns>
        public bool TryGet<TSetting>(
            HierarchicalPath path, 
            SettingSearchOptions searchOptions, 
            out TSetting output, 
            out SettingsManager containingManager) where TSetting : class, new()
        {
            // Try the current settings for the key.
            if (this.ContainsInternal(path))
            {
                // Attempt to map the current path item. Depending on option
                // settings, this will serialize/deserialize objects that don't
                // match.
                if (this.TryMap(
                    path, 
                    searchOptions, 
                    out output))
                {
                    containingManager = this;
                    return true;
                }

                // We couldn't map it.
                output = null;
                containingManager = null;
                return false;
            }

            // We aren't in the exact path of this item, so we need to search
            // for the parent paths or up the path for an entry.
            bool searchParent = GetSearchParent(searchOptions);
            bool searchPaths = this.GetSearchPaths(searchOptions);
            bool parentSettingsFirst = this.GetParentSettingsFirst(
                searchOptions);

            // If we are searching neither, then we couldn't find it.
            if (!searchParent && !searchPaths)
            {
                containingManager = this;
                output = default(TSetting);
                return false;
            }

            // Check to see if we are searching parents first.
            if (searchParent && parentSettingsFirst && this.Parent != null)
            {
                // We are searching the parent. We only have the parent search
                // its parents since we'll be searching the path in the next
                // step.
                if (this.Parent.TryGet(
                    path, 
                    SettingSearchOptions.SearchParentSettings, 
                    out output, 
                    out containingManager))
                {
                    return true;
                }
            }

            // Search the path three until we get to the top.
            if (searchPaths && path.Count != 1)
            {
                // Grab the current path.
                HierarchicalPath currentPath = path.Parent;

                while (true)
                {
                    // Try to retrieve the object.
                    if (this.TryGet(
                        currentPath, 
                        searchOptions, 
                        out output, 
                        out containingManager))
                    {
                        // We found it through recursion.
                        return true;
                    }

                    // We couldn't find it, so move up a level.
                    if (currentPath.Count == 1)
                    {
                        break;
                    }

                    currentPath = currentPath.Parent;
                }
            }

            // We have found it so far, so check to see if we are searching
            // parents
            if (searchParent && !parentSettingsFirst && this.Parent != null)
            {
                // We are searching the parent. We only have the parent search
                // its parents since we'll be searching the path in the next
                // step.
                if (this.Parent.TryGet(
                    path, 
                    SettingSearchOptions.SearchParentSettings, 
                    out output, 
                    out containingManager))
                {
                    return true;
                }
            }

            // If we got this far, we couldn't find it with the given settings. So, we set the default
            // value and return false.
            output = default(TSetting);
            containingManager = null;
            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        /// <param name="searchOptions">
        /// </param>
        /// <returns>
        /// </returns>
        private static bool GetSearchParent(SettingSearchOptions searchOptions)
        {
            return (searchOptions & SettingSearchOptions.SearchParentSettings)
                == SettingSearchOptions.SearchParentSettings;
        }

        /// <summary>
        /// </summary>
        /// <param name="searchOptions">
        /// </param>
        /// <returns>
        /// </returns>
        private static bool GetSerializeDeserializeMapping(
            SettingSearchOptions searchOptions)
        {
            return (searchOptions
                & SettingSearchOptions.SerializeDeserializeMapping)
                == SettingSearchOptions.SerializeDeserializeMapping;
        }

        /// <summary>
        /// Determines whether the specified path is in either of the internal
        /// dictionaries.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified path contains internal; otherwise, <c>false</c>.
        /// </returns>
        private bool ContainsInternal(HierarchicalPath path)
        {
            return this.xmlSettings.ContainsKey(path)
                || this.objectSettings.ContainsKey(path);
        }

        /// <summary>
        /// Flushes data from the object settings back into XML.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        private void Flush(HierarchicalPath path)
        {
            // See if we have the path in the object settings.
            if (!this.objectSettings.ContainsKey(path))
            {
                return;
            }

            // We have it in the object settings, so serialize it.
            object input = this.objectSettings[path];
            var inputSerializer = new XmlSerializer(input.GetType());
            var writer = new StringWriter();

            inputSerializer.Serialize(
                writer, 
                input);

            // Put it into the XML settings and remove it from the object.
            this.xmlSettings[path] = writer.ToString();
            this.objectSettings.Remove(path);
        }

        /// <summary>
        /// </summary>
        /// <param name="searchOptions">
        /// </param>
        /// <returns>
        /// </returns>
        private bool GetParentSettingsFirst(SettingSearchOptions searchOptions)
        {
            return (searchOptions & SettingSearchOptions.ParentSettingsFirst)
                == SettingSearchOptions.ParentSettingsFirst;
        }

        /// <summary>
        /// </summary>
        /// <param name="searchOptions">
        /// </param>
        /// <returns>
        /// </returns>
        private bool GetSearchPaths(SettingSearchOptions searchOptions)
        {
            return (searchOptions
                & SettingSearchOptions.SearchHierarchicalParents)
                == SettingSearchOptions.SearchHierarchicalParents;
        }

        /// <summary>
        /// Attempts to map the setting in the collection to the desired goal.
        /// </summary>
        /// <typeparam name="TSetting">
        /// The type of the setting.
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="searchOptions">
        /// The options.
        /// </param>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <returns>
        /// </returns>
        private bool TryMap<TSetting>(
            HierarchicalPath path, 
            SettingSearchOptions searchOptions, 
            out TSetting output) where TSetting : class, new()
        {
            // Check to see if we have an object already deserialized.
            bool mapObject = GetSerializeDeserializeMapping(searchOptions);

            // Check to see if we have an object already deserialized.
            if (this.objectSettings.ContainsKey(path))
            {
                // Pull it out and see if we can do anything with it.
                object input = this.objectSettings[path];

                if (input == null)
                {
                    output = null;
                    return false;
                }

                // Check to see if we can cast the object into the new type.
                output = input as TSetting;

                if (output != null)
                {
                    return true;
                }

                // If we aren't mapping, then we can't do anything so we need
                // to return false.
                if (!mapObject)
                {
                    return false;
                }

                // We cannot cast it directly, but we are going to attempt to
                // deserialize it in a different type. So, we need to flush it
                // back to XML for the deserialization.
                this.Flush(path);
            }

            // We either never had this object deserialized or we couldn't map
            // it to the input, so deserialize it from the XML stream.
            var outputDeserializer = new XmlSerializer(typeof(TSetting));
            var reader = new StringReader(this.xmlSettings[path]);

            try
            {
                output = (TSetting)outputDeserializer.Deserialize(reader);
            }
            catch
            {
                output = null;
                return false;
            }

            // Remove it from the XML dictionary and put it into the object store.
            this.objectSettings[path] = output;
            this.xmlSettings.Remove(path);

            // Return the results.
            return true;
        }

        #endregion
    }
}