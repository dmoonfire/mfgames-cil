namespace SettingsEditor
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.settings = new System.Windows.Forms.TreeView();
			this.label1 = new System.Windows.Forms.Label();
			this.group = new System.Windows.Forms.TextBox();
			this.name = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.type = new System.Windows.Forms.ComboBox();
			this.enumeration = new System.Windows.Forms.CheckBox();
			this.defaultValue = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.editorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.openSettingsDialog = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.configClass = new System.Windows.Forms.TextBox();
			this.configNamespace = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.usageCombo = new System.Windows.Forms.ComboBox();
			this.menuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			//
			// settings
			//
			this.settings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.settings.Enabled = false;
			this.settings.Location = new System.Drawing.Point(5, 5);
			this.settings.Name = "settings";
			this.settings.Size = new System.Drawing.Size(249, 259);
			this.settings.TabIndex = 0;
			this.settings.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.settings_NodeMouseDoubleClick);
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Property Name:";
			//
			// group
			//
			this.group.Enabled = false;
			this.group.Location = new System.Drawing.Point(92, 13);
			this.group.Name = "group";
			this.group.Size = new System.Drawing.Size(249, 20);
			this.group.TabIndex = 0;
			this.group.TextChanged += new System.EventHandler(this.group_TextChanged);
			//
			// name
			//
			this.name.Enabled = false;
			this.name.Location = new System.Drawing.Point(92, 40);
			this.name.Name = "name";
			this.name.Size = new System.Drawing.Size(249, 20);
			this.name.TabIndex = 1;
			this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(47, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Group:";
			//
			// type
			//
			this.type.Enabled = false;
			this.type.FormattingEnabled = true;
			this.type.Items.AddRange(new object[] {
			                                 "int",
			                                 "string",
			                                 "float",
			                                 "double",
			                                 "char",
			                                 "numeric",
			                                 "System.IO.FileInfo",
			                                 "System.IO.DirectoryInfo"
						 });
			this.type.Location = new System.Drawing.Point(92, 93);
			this.type.Name = "type";
			this.type.Size = new System.Drawing.Size(249, 21);
			this.type.TabIndex = 3;
			this.type.TextChanged += new System.EventHandler(this.type_TextChanged);
			//
			// enumeration
			//
			this.enumeration.AutoSize = true;
			this.enumeration.Enabled = false;
			this.enumeration.Location = new System.Drawing.Point(92, 120);
			this.enumeration.Name = "enumeration";
			this.enumeration.Size = new System.Drawing.Size(91, 17);
			this.enumeration.TabIndex = 4;
			this.enumeration.Text = "Enumeration?";
			this.enumeration.UseVisualStyleBackColor = true;
			this.enumeration.CheckedChanged += new System.EventHandler(this.enumeration_CheckedChanged);
			//
			// defaultValue
			//
			this.defaultValue.Enabled = false;
			this.defaultValue.Location = new System.Drawing.Point(92, 143);
			this.defaultValue.Name = "defaultValue";
			this.defaultValue.Size = new System.Drawing.Size(249, 20);
			this.defaultValue.TabIndex = 5;
			this.defaultValue.TextChanged += new System.EventHandler(this.defaultValue_TextChanged);
			//
			// label3
			//
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(52, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Type:";
			//
			// label4
			//
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 145);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(74, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Default Value:";
			//
			// menuStrip1
			//
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                       this.editorToolStripMenuItem
						       });
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(620, 24);
			this.menuStrip1.TabIndex = 10;
			this.menuStrip1.Text = "menuStrip1";
			//
			// editorToolStripMenuItem
			//
			this.editorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                                            this.openToolStripMenuItem,
			                                                            this.saveToolStripMenuItem,
			                                                            this.generateToolStripMenuItem,
			                                                            this.closeToolStripMenuItem,
			                                                            this.newToolStripMenuItem,
			                                                            this.quitToolStripMenuItem
									    });
			this.editorToolStripMenuItem.Name = "editorToolStripMenuItem";
			this.editorToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.editorToolStripMenuItem.Text = "&Editor";
			//
			// openToolStripMenuItem
			//
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.openToolStripMenuItem.Text = "&Open...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			//
			// saveToolStripMenuItem
			//
			this.saveToolStripMenuItem.Enabled = false;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			//
			// generateToolStripMenuItem
			//
			this.generateToolStripMenuItem.Enabled = false;
			this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
			this.generateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.generateToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.generateToolStripMenuItem.Text = "&Generate";
			this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateToolStripMenuItem_Click);
			//
			// closeToolStripMenuItem
			//
			this.closeToolStripMenuItem.Enabled = false;
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			//
			// newToolStripMenuItem
			//
			this.newToolStripMenuItem.Enabled = false;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			//
			// quitToolStripMenuItem
			//
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.quitToolStripMenuItem.Text = "&Quit";
			this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
			//
			// panel1
			//
			this.panel1.Controls.Add(this.settings);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(5);
			this.panel1.Size = new System.Drawing.Size(259, 269);
			this.panel1.TabIndex = 11;
			//
			// openSettingsDialog
			//
			this.openSettingsDialog.DefaultExt = "xml";
			this.openSettingsDialog.Filter = "XML|*.xml|All files|*.*";
			this.openSettingsDialog.Title = "Open Settings XML";
			//
			// groupBox1
			//
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.configClass);
			this.groupBox1.Controls.Add(this.configNamespace);
			this.groupBox1.Location = new System.Drawing.Point(266, 28);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(347, 76);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Configuration";
			//
			// label6
			//
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(20, 50);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(66, 13);
			this.label6.TabIndex = 3;
			this.label6.Text = "Class Name:";
			//
			// label5
			//
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(19, 23);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Namespace:";
			//
			// configClass
			//
			this.configClass.Location = new System.Drawing.Point(93, 46);
			this.configClass.Name = "configClass";
			this.configClass.Size = new System.Drawing.Size(248, 20);
			this.configClass.TabIndex = 1;
			this.configClass.TextChanged += new System.EventHandler(this.configClass_TextChanged);
			//
			// configNamespace
			//
			this.configNamespace.Location = new System.Drawing.Point(92, 20);
			this.configNamespace.Name = "configNamespace";
			this.configNamespace.Size = new System.Drawing.Size(249, 20);
			this.configNamespace.TabIndex = 0;
			this.configNamespace.TextChanged += new System.EventHandler(this.configNamespace_TextChanged);
			//
			// groupBox2
			//
			this.groupBox2.Controls.Add(this.usageCombo);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.group);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.name);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.type);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.enumeration);
			this.groupBox2.Controls.Add(this.defaultValue);
			this.groupBox2.Location = new System.Drawing.Point(266, 110);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(347, 172);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Setting";
			//
			// label7
			//
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(45, 69);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 13);
			this.label7.TabIndex = 10;
			this.label7.Text = "Usage:";
			//
			// usageCombo
			//
			this.usageCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.usageCombo.Enabled = false;
			this.usageCombo.FormattingEnabled = true;
			this.usageCombo.Items.AddRange(new object[] {
			                                       "Setting",
			                                       "Constant",
			                                       "Transient"
						       });
			this.usageCombo.Location = new System.Drawing.Point(92, 66);
			this.usageCombo.Name = "usageCombo";
			this.usageCombo.Size = new System.Drawing.Size(249, 21);
			this.usageCombo.TabIndex = 2;
			this.usageCombo.SelectedValueChanged += new System.EventHandler(this.usageCombo_SelectedValueChanged);
			//
			// Form1
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(620, 293);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Settings Editor";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TreeView settings;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox group;
		private System.Windows.Forms.TextBox name;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox type;
		private System.Windows.Forms.CheckBox enumeration;
		private System.Windows.Forms.TextBox defaultValue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem editorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.OpenFileDialog openSettingsDialog;
		private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox configClass;
		private System.Windows.Forms.TextBox configNamespace;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox usageCombo;
		private System.Windows.Forms.Label label7;
	}
}

