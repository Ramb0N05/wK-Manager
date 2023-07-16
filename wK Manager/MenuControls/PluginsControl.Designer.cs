namespace wK_Manager.MenuControls {
    partial class PlugInsControl {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            splitContainer1 = new SplitContainer();
            pluginsListView = new ListView();
            plugInInfoTableLayoutPanel = new TableLayoutPanel();
            pluginNameLabel = new Label();
            disableButton = new Button();
            pluginVersionLabel = new Label();
            pluginDescriptionTextBox = new TextBox();
            pluginLogoPictureBox = new PictureBox();
            openPluginDirButton = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            plugInInfoTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pluginLogoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pluginsListView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(plugInInfoTableLayoutPanel);
            splitContainer1.Size = new Size(1000, 650);
            splitContainer1.SplitterDistance = 450;
            splitContainer1.TabIndex = 0;
            // 
            // pluginsListView
            // 
            pluginsListView.Dock = DockStyle.Fill;
            pluginsListView.Location = new Point(0, 0);
            pluginsListView.MultiSelect = false;
            pluginsListView.Name = "pluginsListView";
            pluginsListView.Size = new Size(1000, 450);
            pluginsListView.TabIndex = 0;
            pluginsListView.UseCompatibleStateImageBehavior = false;
            pluginsListView.View = View.List;
            pluginsListView.ItemSelectionChanged += plugInsListView_ItemSelectionChanged;
            // 
            // plugInInfoTableLayoutPanel
            // 
            plugInInfoTableLayoutPanel.ColumnCount = 4;
            plugInInfoTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            plugInInfoTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            plugInInfoTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            plugInInfoTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            plugInInfoTableLayoutPanel.Controls.Add(pluginNameLabel, 1, 0);
            plugInInfoTableLayoutPanel.Controls.Add(disableButton, 3, 2);
            plugInInfoTableLayoutPanel.Controls.Add(pluginVersionLabel, 2, 0);
            plugInInfoTableLayoutPanel.Controls.Add(pluginDescriptionTextBox, 1, 1);
            plugInInfoTableLayoutPanel.Controls.Add(pluginLogoPictureBox, 0, 0);
            plugInInfoTableLayoutPanel.Controls.Add(openPluginDirButton, 1, 2);
            plugInInfoTableLayoutPanel.Dock = DockStyle.Fill;
            plugInInfoTableLayoutPanel.Location = new Point(0, 0);
            plugInInfoTableLayoutPanel.Name = "plugInInfoTableLayoutPanel";
            plugInInfoTableLayoutPanel.RowCount = 3;
            plugInInfoTableLayoutPanel.RowStyles.Add(new RowStyle());
            plugInInfoTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            plugInInfoTableLayoutPanel.RowStyles.Add(new RowStyle());
            plugInInfoTableLayoutPanel.Size = new Size(1000, 196);
            plugInInfoTableLayoutPanel.TabIndex = 0;
            plugInInfoTableLayoutPanel.Visible = false;
            // 
            // pluginNameLabel
            // 
            pluginNameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pluginNameLabel.AutoSize = true;
            pluginNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            pluginNameLabel.Location = new Point(103, 0);
            pluginNameLabel.Name = "pluginNameLabel";
            pluginNameLabel.Size = new Size(134, 21);
            pluginNameLabel.TabIndex = 1;
            pluginNameLabel.Text = "%PluginName%";
            pluginNameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // disableButton
            // 
            disableButton.Anchor = AnchorStyles.None;
            disableButton.AutoSize = true;
            disableButton.Enabled = false;
            disableButton.Location = new Point(915, 168);
            disableButton.Name = "disableButton";
            disableButton.Size = new Size(82, 25);
            disableButton.TabIndex = 0;
            disableButton.Text = "Deaktivieren";
            disableButton.UseVisualStyleBackColor = true;
            // 
            // pluginVersionLabel
            // 
            pluginVersionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pluginVersionLabel.AutoSize = true;
            pluginVersionLabel.Location = new Point(243, 6);
            pluginVersionLabel.Name = "pluginVersionLabel";
            pluginVersionLabel.Size = new Size(666, 15);
            pluginVersionLabel.TabIndex = 2;
            pluginVersionLabel.Text = "(%PluginVersion%)";
            // 
            // pluginDescriptionTextBox
            // 
            pluginDescriptionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plugInInfoTableLayoutPanel.SetColumnSpan(pluginDescriptionTextBox, 3);
            pluginDescriptionTextBox.Location = new Point(103, 24);
            pluginDescriptionTextBox.Multiline = true;
            pluginDescriptionTextBox.Name = "pluginDescriptionTextBox";
            pluginDescriptionTextBox.ReadOnly = true;
            pluginDescriptionTextBox.ScrollBars = ScrollBars.Vertical;
            pluginDescriptionTextBox.Size = new Size(894, 138);
            pluginDescriptionTextBox.TabIndex = 3;
            // 
            // pluginLogoPictureBox
            // 
            pluginLogoPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pluginLogoPictureBox.Location = new Point(3, 3);
            pluginLogoPictureBox.Name = "pluginLogoPictureBox";
            plugInInfoTableLayoutPanel.SetRowSpan(pluginLogoPictureBox, 3);
            pluginLogoPictureBox.Size = new Size(94, 190);
            pluginLogoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pluginLogoPictureBox.TabIndex = 4;
            pluginLogoPictureBox.TabStop = false;
            // 
            // openPluginDirButton
            // 
            openPluginDirButton.Anchor = AnchorStyles.Left;
            openPluginDirButton.AutoSize = true;
            openPluginDirButton.Location = new Point(103, 168);
            openPluginDirButton.Name = "openPluginDirButton";
            openPluginDirButton.Size = new Size(108, 25);
            openPluginDirButton.TabIndex = 5;
            openPluginDirButton.Text = "Öffne Verzeichnis";
            openPluginDirButton.UseVisualStyleBackColor = true;
            openPluginDirButton.Click += openPlugInDirButton_Click;
            // 
            // PlugInsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            MenuImageKey = "brick";
            MenuItemName = "Erweiterungen verwalten";
            MenuItemOrder = 997;
            Name = "PlugInsControl";
            Size = new Size(1000, 650);
            Load += plugInsControl_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            plugInInfoTableLayoutPanel.ResumeLayout(false);
            plugInInfoTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pluginLogoPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private ListView pluginsListView;
        private TableLayoutPanel plugInInfoTableLayoutPanel;
        private Button disableButton;
        private Label pluginNameLabel;
        private Label pluginVersionLabel;
        private TextBox pluginDescriptionTextBox;
        private PictureBox pluginLogoPictureBox;
        private Button openPluginDirButton;
    }
}
