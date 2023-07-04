namespace wK_Manager.MenuControls
{
    partial class SettingsControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsControl));
            settingsTableLayoutPanel = new TableLayoutPanel();
            label2 = new Label();
            userConfigPathTextBox = new TextBox();
            label3 = new Label();
            userConfigPathButton = new Button();
            label1 = new Label();
            sevenZipPathTextBox = new TextBox();
            sevenZipPathButton = new Button();
            startWindowComboBox = new ComboBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            saveButton = new Button();
            defaultsButton = new Button();
            checkPermissionPictureBox = new PictureBox();
            checkPermissionLabel = new Label();
            settingsTableLayoutPanel.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checkPermissionPictureBox).BeginInit();
            SuspendLayout();
            // 
            // settingsTableLayoutPanel
            // 
            settingsTableLayoutPanel.ColumnCount = 10;
            settingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            settingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            settingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            settingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            settingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            settingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            settingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            settingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            settingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            settingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            settingsTableLayoutPanel.Controls.Add(label2, 0, 0);
            settingsTableLayoutPanel.Controls.Add(userConfigPathTextBox, 1, 0);
            settingsTableLayoutPanel.Controls.Add(label3, 0, 2);
            settingsTableLayoutPanel.Controls.Add(userConfigPathButton, 9, 0);
            settingsTableLayoutPanel.Controls.Add(label1, 0, 1);
            settingsTableLayoutPanel.Controls.Add(sevenZipPathTextBox, 1, 1);
            settingsTableLayoutPanel.Controls.Add(sevenZipPathButton, 9, 1);
            settingsTableLayoutPanel.Controls.Add(startWindowComboBox, 1, 2);
            settingsTableLayoutPanel.Dock = DockStyle.Top;
            settingsTableLayoutPanel.Location = new Point(0, 0);
            settingsTableLayoutPanel.Name = "settingsTableLayoutPanel";
            settingsTableLayoutPanel.RowCount = 4;
            settingsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            settingsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            settingsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            settingsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            settingsTableLayoutPanel.Size = new Size(1096, 121);
            settingsTableLayoutPanel.TabIndex = 0;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(123, 30);
            label2.TabIndex = 3;
            label2.Text = "Benutzer\r\nKonfigurationsordner:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // userConfigPathTextBox
            // 
            userConfigPathTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            settingsTableLayoutPanel.SetColumnSpan(userConfigPathTextBox, 8);
            userConfigPathTextBox.Location = new Point(132, 3);
            userConfigPathTextBox.Name = "userConfigPathTextBox";
            userConfigPathTextBox.ReadOnly = true;
            userConfigPathTextBox.Size = new Size(874, 23);
            userConfigPathTextBox.TabIndex = 4;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(3, 67);
            label3.Name = "label3";
            label3.Size = new Size(123, 15);
            label3.TabIndex = 6;
            label3.Text = "Startfenster:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // userConfigPathButton
            // 
            userConfigPathButton.Anchor = AnchorStyles.None;
            userConfigPathButton.AutoSize = true;
            userConfigPathButton.Location = new Point(1015, 3);
            userConfigPathButton.Name = "userConfigPathButton";
            userConfigPathButton.Size = new Size(75, 24);
            userConfigPathButton.TabIndex = 5;
            userConfigPathButton.Text = "...";
            userConfigPathButton.UseVisualStyleBackColor = true;
            userConfigPathButton.Click += userConfigPathButton_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(3, 37);
            label1.Name = "label1";
            label1.Size = new Size(123, 15);
            label1.TabIndex = 0;
            label1.Text = "7-Zip Pfad:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sevenZipPathTextBox
            // 
            sevenZipPathTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            settingsTableLayoutPanel.SetColumnSpan(sevenZipPathTextBox, 8);
            sevenZipPathTextBox.Location = new Point(132, 33);
            sevenZipPathTextBox.Name = "sevenZipPathTextBox";
            sevenZipPathTextBox.ReadOnly = true;
            sevenZipPathTextBox.Size = new Size(874, 23);
            sevenZipPathTextBox.TabIndex = 1;
            // 
            // sevenZipPathButton
            // 
            sevenZipPathButton.Anchor = AnchorStyles.None;
            sevenZipPathButton.AutoSize = true;
            sevenZipPathButton.Location = new Point(1015, 33);
            sevenZipPathButton.Name = "sevenZipPathButton";
            sevenZipPathButton.Size = new Size(75, 24);
            sevenZipPathButton.TabIndex = 2;
            sevenZipPathButton.Text = "...";
            sevenZipPathButton.UseVisualStyleBackColor = true;
            sevenZipPathButton.Click += sevenZipPathButton_Click;
            // 
            // startWindowComboBox
            // 
            startWindowComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            settingsTableLayoutPanel.SetColumnSpan(startWindowComboBox, 8);
            startWindowComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            startWindowComboBox.FormattingEnabled = true;
            startWindowComboBox.Location = new Point(132, 63);
            startWindowComboBox.Name = "startWindowComboBox";
            startWindowComboBox.Size = new Size(874, 23);
            startWindowComboBox.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(saveButton, 3, 0);
            tableLayoutPanel2.Controls.Add(defaultsButton, 2, 0);
            tableLayoutPanel2.Controls.Add(checkPermissionPictureBox, 0, 0);
            tableLayoutPanel2.Controls.Add(checkPermissionLabel, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Bottom;
            tableLayoutPanel2.Location = new Point(0, 615);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1096, 30);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.None;
            saveButton.AutoSize = true;
            saveButton.Location = new Point(1018, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 24);
            saveButton.TabIndex = 1;
            saveButton.Text = "Speichern";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // defaultsButton
            // 
            defaultsButton.Anchor = AnchorStyles.None;
            defaultsButton.AutoSize = true;
            defaultsButton.Location = new Point(925, 3);
            defaultsButton.Name = "defaultsButton";
            defaultsButton.Size = new Size(87, 24);
            defaultsButton.TabIndex = 0;
            defaultsButton.Text = "Zurücksetzen";
            defaultsButton.UseVisualStyleBackColor = true;
            defaultsButton.Click += defaultsButton_Click;
            // 
            // checkPermissionPictureBox
            // 
            checkPermissionPictureBox.Dock = DockStyle.Fill;
            checkPermissionPictureBox.Image = (Image)resources.GetObject("checkPermissionPictureBox.Image");
            checkPermissionPictureBox.Location = new Point(3, 3);
            checkPermissionPictureBox.Name = "checkPermissionPictureBox";
            checkPermissionPictureBox.Size = new Size(24, 24);
            checkPermissionPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            checkPermissionPictureBox.TabIndex = 2;
            checkPermissionPictureBox.TabStop = false;
            // 
            // checkPermissionLabel
            // 
            checkPermissionLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            checkPermissionLabel.AutoSize = true;
            checkPermissionLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            checkPermissionLabel.ForeColor = Color.DarkRed;
            checkPermissionLabel.Location = new Point(33, 0);
            checkPermissionLabel.Name = "checkPermissionLabel";
            checkPermissionLabel.Size = new Size(886, 30);
            checkPermissionLabel.TabIndex = 3;
            checkPermissionLabel.Text = "Kein Schreibzugriff auf die Einstellungen möglich!\r\nStarte die Anwendung mit erhöten Rechten neu.";
            // 
            // SettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(tableLayoutPanel2);
            Controls.Add(settingsTableLayoutPanel);
            MenuImageKey = "cog";
            MenuItemName = "Einstellungen";
            MenuItemOrder = 990;
            Name = "SettingsControl";
            Size = new Size(1096, 645);
            Load += SettingsControl_Load;
            settingsTableLayoutPanel.ResumeLayout(false);
            settingsTableLayoutPanel.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)checkPermissionPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel settingsTableLayoutPanel;
        private Label label1;
        private TextBox sevenZipPathTextBox;
        private Button sevenZipPathButton;
        private TableLayoutPanel tableLayoutPanel2;
        private Button defaultsButton;
        private Button saveButton;
        private Label label2;
        private TextBox userConfigPathTextBox;
        private Button userConfigPathButton;
        private Label label3;
        private ComboBox startWindowComboBox;
        private PictureBox checkPermissionPictureBox;
        private Label checkPermissionLabel;
    }
}
