using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MfGames.Settings.Design;
using System.IO;

namespace SettingsEditor
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		#region Control Properties
		/// <remarks>This is prefixed with a _ because it has internal processing.</remarks>
		private DesignConfiguration _configuration;

		/// <remarks>This is prefixed with a _ because it has internal processing.</remarks>
		private DesignSetting _setting;

		private string filename;

		/// <summary>
		/// Gets or sets the currently loaded configuration. Setting this
		/// automatically updates the various controls.
		/// </summary>
		public DesignConfiguration Configuration
		{
			get { return _configuration; }
			set
			{
				// Set the value
				_configuration = value;

				// Determine what to do based on value
				if (_configuration == null)
				{
					// Clear out the settings
					settings.Enabled = false;
					settings.Nodes.Clear();

					// Clear out the settings object
					Setting = null;

					// Disable the menu items
					saveToolStripMenuItem.Enabled = false;
					closeToolStripMenuItem.Enabled = false;
					newToolStripMenuItem.Enabled = false;
					generateToolStripMenuItem.Enabled = false;

					// Disable the top-level
					configClass.Enabled = false;
					configNamespace.Enabled = false;
					configClass.Text = "";
					configNamespace.Text = "";
				}
				else
				{
					// Populate the nodes
					settings.Enabled = true;
					Rebuild();

					// Enable the menu items
					saveToolStripMenuItem.Enabled = true;
					closeToolStripMenuItem.Enabled = true;
					newToolStripMenuItem.Enabled = true;
					generateToolStripMenuItem.Enabled = true;

					// Disable the top-level to prevent loop
					configClass.Enabled = false;
					configNamespace.Enabled = false;
					configClass.Text = _configuration.ClassName;
					configNamespace.Text = _configuration.Namespace;
					configClass.Enabled = true;
					configNamespace.Enabled = true;
				}
			}
		}

		/// <summary>
		/// Contains the current selected setting.
		/// </summary>
		public DesignSetting Setting
		{
			get { return _setting; }
			set
			{
				// Set the value
				_setting = value;

				// See if we need to set the values
				if (_setting == null)
				{
					// Disable everything
					name.Enabled = false;
					group.Enabled = false;
					type.Enabled = false;
					defaultValue.Enabled = false;
					enumeration.Enabled = false;
					constant.Enabled = false;

					// Clear out the fields
					name.Text = "";
					group.Text = "";
					type.Text = "";
					defaultValue.Text = "";
					enumeration.Checked = false;
				}
				else
				{
					// Disable everything to prevent events
					name.Enabled = false;
					group.Enabled = false;
					type.Enabled = false;
					defaultValue.Enabled = false;
					enumeration.Enabled = false;
					constant.Enabled = false;

					// Clear out the fields
					name.Text = _setting.Name;
					group.Text = _setting.Group;
					type.Text = _setting.TypeName;
					defaultValue.Text = _setting.Default;
					enumeration.Checked = _setting.Format == FormatType.Enumeration ? true : false;
					constant.Checked = _setting.Usage == UsageType.Constant;

					// Enable everything
					name.Enabled = true;
					group.Enabled = true;
					type.Enabled = true;
					defaultValue.Enabled = true;
					enumeration.Enabled = true;
					constant.Enabled = true;
				}
			}
		}
		#endregion

		#region Tree Display
		/// <summary>
		/// Rebuilds the contents of the tree.
		/// </summary>
		private void Rebuild()
		{
			// Go through the groups
			settings.Nodes.Clear();
			_configuration.Groups.Sort();

			foreach (DesignGroup group in _configuration.Groups)
			{
				// Add a node for each one
				TreeNode groupNode = settings.Nodes.Add(group.Name);
				groupNode.Tag = null;

				// Go through each setting
				group.Settings.Sort();

				foreach (DesignSetting setting in group.Settings)
				{
					// Create a node for this one
					TreeNode settingNode = groupNode.Nodes.Add(setting.Name);
					settingNode.Tag = setting;
				}
			}

			// Expand everything
			settings.ExpandAll();
		}
		#endregion

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Clear out the configuration and wipe out everything
			Configuration = null;
			filename = null;

			// Open up the input box
			DialogResult result = openSettingsDialog.ShowDialog();

			if (result != DialogResult.OK)
				return;

			// Open up the XML
			XmlDesignReader reader = new XmlDesignReader();
			Configuration = reader.Read(new FileInfo(openSettingsDialog.FileName));
			filename = openSettingsDialog.FileName;
		}

		private void settings_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			// Get the node and make sure it is a setting node
			DesignSetting setting = (DesignSetting) e.Node.Tag;

			if (setting == null)
				return;

			// Set it
			Setting = setting;
		}

		private void name_TextChanged(object sender, EventArgs e)
		{
			// Ignore if we aren't enabled to prevent infinite loops
			if (!name.Enabled)
				return;

			// Ignore blanks
			if (name.Text.Trim().Length == 0)
				return;

			// Update the settings
			Setting.Name = name.Text.Trim();

			// Rebuild the list
			Rebuild();
		}

		private void group_TextChanged(object sender, EventArgs e)
		{
			// Ignore if we aren't enabled to prevent loops
			if (!group.Enabled)
				return;

			// Ignores blanks
			if (group.Text.Trim().Length == 0)
				return;

			// Get the group name and stop processing if we haven't changed
			string name = group.Text.Trim();
			string oldname = Setting.Group;

			if (name == oldname)
				return;

			// We have a new group, so move this setting
			if (!Configuration.Contains(name))
				Configuration.Groups.Add(new DesignGroup() { Name = name });

			// Get the design groups
			DesignGroup newGroup = Configuration[name];

			Setting.Group = name;
			newGroup.Settings.Add(Setting);

			// Remove it from the old one
			DesignGroup oldGroup = Configuration[oldname];

			oldGroup.Settings.Remove(Setting);

			// See if the group is now empty
			if (oldGroup.Settings.Count == 0)
				Configuration.Groups.Remove(oldGroup);

			// Rebuild the list
			Rebuild();
		}

		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Get (or create) the group
			if (!Configuration.Contains("New Group"))
				Configuration.Groups.Add(new DesignGroup() { Name = "New Group" });

			DesignGroup group = Configuration["New Group"];

			// Set up the default settings
			DesignSetting setting = new DesignSetting() { Name = "New Setting", Group = "New Group" };
			group.Settings.Add(setting);
			Setting = setting;

			// Rebuild the list
			Rebuild();
		}

		private void constant_CheckedChanged(object sender, EventArgs e)
		{
			if (!constant.Enabled)
				return;

			Setting.Usage = constant.Checked ? UsageType.Constant : UsageType.Setting;
		}

		private void enumeration_CheckedChanged(object sender, EventArgs e)
		{
			if (!enumeration.Enabled)
				return;

			Setting.Format = enumeration.Checked ? FormatType.Enumeration : FormatType.Normal;
		}

		private void defaultValue_TextChanged(object sender, EventArgs e)
		{
			if (!defaultValue.Enabled)
				return;

			Setting.Default = defaultValue.Text.Trim();
		}

		private void type_TextChanged(object sender, EventArgs e)
		{
			if (!type.Enabled)
				return;

			Setting.TypeName = type.Text.Trim();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			XmlDesignWriter writer = new XmlDesignWriter();
			writer.Write(new FileInfo(filename), Configuration);
		}

		private void generateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Figure out the CS filename
			string csFile = filename.Replace(".xml", ".cs");

			// Create the file
			XmlDesignGenerater generator = new XmlDesignGenerater();
			generator.Generate(new FileInfo(filename), new FileInfo(csFile));
		}

		private void configClass_TextChanged(object sender, EventArgs e)
		{
			if (!configClass.Enabled)
				return;

			if (configClass.Text.Trim().Length == 0)
				return;

			Configuration.ClassName = configClass.Text.Trim();
		}

		private void configNamespace_TextChanged(object sender, EventArgs e)
		{
			if (!configNamespace.Enabled)
				return;

			if (configNamespace.Text.Trim().Length == 0)
				return;

			Configuration.Namespace = configNamespace.Text.Trim();
		}
	}
}
