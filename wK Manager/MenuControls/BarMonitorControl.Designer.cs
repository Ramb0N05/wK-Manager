namespace wK_Manager.MenuControls
{
    partial class BarMonitorControl
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
            displaysGroupBox = new GroupBox();
            displaysFlowLayoutPanel = new FlowLayoutPanel();
            identifyLabel = new Label();
            diashowGroupBox = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            localPathButton = new Button();
            remotePathTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            autoGetCheckBox = new CheckBox();
            getButton = new Button();
            localPathTextBox = new TextBox();
            remotePathStatusLabel = new Label();
            presentGroupBox = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            vlcPathLabel = new Label();
            vlcPathTextBox = new TextBox();
            vlcPathButton = new Button();
            stopButton = new Button();
            presentButton = new Button();
            lockSettingsCheckBox = new CheckBox();
            displaysGroupBox.SuspendLayout();
            diashowGroupBox.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            presentGroupBox.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // displaysGroupBox
            // 
            displaysGroupBox.Controls.Add(displaysFlowLayoutPanel);
            displaysGroupBox.Dock = DockStyle.Top;
            displaysGroupBox.Location = new Point(2, 5);
            displaysGroupBox.Name = "displaysGroupBox";
            displaysGroupBox.Size = new Size(1182, 100);
            displaysGroupBox.TabIndex = 0;
            displaysGroupBox.TabStop = false;
            displaysGroupBox.Text = "Monitor Auswahl";
            // 
            // displaysFlowLayoutPanel
            // 
            displaysFlowLayoutPanel.Dock = DockStyle.Fill;
            displaysFlowLayoutPanel.Location = new Point(3, 19);
            displaysFlowLayoutPanel.Name = "displaysFlowLayoutPanel";
            displaysFlowLayoutPanel.Size = new Size(1176, 78);
            displaysFlowLayoutPanel.TabIndex = 0;
            // 
            // identifyLabel
            // 
            identifyLabel.Dock = DockStyle.Top;
            identifyLabel.Location = new Point(2, 105);
            identifyLabel.Name = "identifyLabel";
            identifyLabel.Size = new Size(1182, 15);
            identifyLabel.TabIndex = 1;
            identifyLabel.Text = "(Rechtsklick zum identifizieren)";
            identifyLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // diashowGroupBox
            // 
            diashowGroupBox.Controls.Add(tableLayoutPanel1);
            diashowGroupBox.Dock = DockStyle.Top;
            diashowGroupBox.Location = new Point(2, 120);
            diashowGroupBox.Name = "diashowGroupBox";
            diashowGroupBox.Size = new Size(1182, 125);
            diashowGroupBox.TabIndex = 2;
            diashowGroupBox.TabStop = false;
            diashowGroupBox.Text = "Diashow beziehen";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(localPathButton, 2, 0);
            tableLayoutPanel1.Controls.Add(remotePathTextBox, 1, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 1);
            tableLayoutPanel1.Controls.Add(autoGetCheckBox, 1, 2);
            tableLayoutPanel1.Controls.Add(getButton, 2, 2);
            tableLayoutPanel1.Controls.Add(localPathTextBox, 1, 0);
            tableLayoutPanel1.Controls.Add(remotePathStatusLabel, 2, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(1176, 103);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // localPathButton
            // 
            localPathButton.Anchor = AnchorStyles.None;
            localPathButton.AutoSize = true;
            localPathButton.Location = new Point(1090, 4);
            localPathButton.Name = "localPathButton";
            localPathButton.Size = new Size(75, 25);
            localPathButton.TabIndex = 3;
            localPathButton.Text = "...";
            localPathButton.UseVisualStyleBackColor = true;
            localPathButton.Click += localPathButton_Click;
            // 
            // remotePathTextBox
            // 
            remotePathTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            remotePathTextBox.Location = new Point(87, 39);
            remotePathTextBox.Name = "remotePathTextBox";
            remotePathTextBox.Size = new Size(990, 23);
            remotePathTextBox.TabIndex = 3;
            remotePathTextBox.TextChanged += remotePathTextBox_TextChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(3, 9);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 0;
            label2.Text = "Lokaler Pfad:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(3, 43);
            label3.Name = "label3";
            label3.Size = new Size(78, 15);
            label3.TabIndex = 1;
            label3.Text = "Remote Pfad:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // autoGetCheckBox
            // 
            autoGetCheckBox.Anchor = AnchorStyles.Right;
            autoGetCheckBox.AutoSize = true;
            autoGetCheckBox.Location = new Point(865, 76);
            autoGetCheckBox.Name = "autoGetCheckBox";
            autoGetCheckBox.Size = new Size(212, 19);
            autoGetCheckBox.TabIndex = 5;
            autoGetCheckBox.Text = "Beim Starten automatisch beziehen";
            autoGetCheckBox.UseVisualStyleBackColor = true;
            // 
            // getButton
            // 
            getButton.Anchor = AnchorStyles.None;
            getButton.AutoSize = true;
            getButton.Location = new Point(1083, 73);
            getButton.Name = "getButton";
            getButton.Size = new Size(90, 25);
            getButton.TabIndex = 4;
            getButton.Text = "Jetzt beziehen";
            getButton.UseVisualStyleBackColor = true;
            // 
            // localPathTextBox
            // 
            localPathTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            localPathTextBox.Location = new Point(87, 5);
            localPathTextBox.Name = "localPathTextBox";
            localPathTextBox.ReadOnly = true;
            localPathTextBox.Size = new Size(990, 23);
            localPathTextBox.TabIndex = 2;
            // 
            // remotePathStatusLabel
            // 
            remotePathStatusLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            remotePathStatusLabel.AutoSize = true;
            remotePathStatusLabel.Location = new Point(1083, 43);
            remotePathStatusLabel.Name = "remotePathStatusLabel";
            remotePathStatusLabel.Size = new Size(90, 15);
            remotePathStatusLabel.TabIndex = 6;
            remotePathStatusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // presentGroupBox
            // 
            presentGroupBox.Controls.Add(tableLayoutPanel2);
            presentGroupBox.Dock = DockStyle.Top;
            presentGroupBox.Location = new Point(2, 245);
            presentGroupBox.Name = "presentGroupBox";
            presentGroupBox.Size = new Size(1182, 100);
            presentGroupBox.TabIndex = 3;
            presentGroupBox.TabStop = false;
            presentGroupBox.Text = "Präsentieren";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(vlcPathLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(vlcPathTextBox, 1, 0);
            tableLayoutPanel2.Controls.Add(vlcPathButton, 2, 0);
            tableLayoutPanel2.Controls.Add(stopButton, 2, 1);
            tableLayoutPanel2.Controls.Add(presentButton, 1, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(1176, 78);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // vlcPathLabel
            // 
            vlcPathLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            vlcPathLabel.AutoSize = true;
            vlcPathLabel.Location = new Point(3, 8);
            vlcPathLabel.Name = "vlcPathLabel";
            vlcPathLabel.Size = new Size(58, 15);
            vlcPathLabel.TabIndex = 4;
            vlcPathLabel.Text = "VLC Pfad:";
            vlcPathLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // vlcPathTextBox
            // 
            vlcPathTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            vlcPathTextBox.Location = new Point(67, 4);
            vlcPathTextBox.Name = "vlcPathTextBox";
            vlcPathTextBox.ReadOnly = true;
            vlcPathTextBox.Size = new Size(1023, 23);
            vlcPathTextBox.TabIndex = 5;
            // 
            // vlcPathButton
            // 
            vlcPathButton.Anchor = AnchorStyles.None;
            vlcPathButton.AutoSize = true;
            vlcPathButton.Location = new Point(1097, 3);
            vlcPathButton.Name = "vlcPathButton";
            vlcPathButton.Size = new Size(75, 25);
            vlcPathButton.TabIndex = 6;
            vlcPathButton.Text = "...";
            vlcPathButton.UseVisualStyleBackColor = true;
            vlcPathButton.Click += vlcPathButton_Click;
            // 
            // stopButton
            // 
            stopButton.Anchor = AnchorStyles.None;
            stopButton.AutoSize = true;
            stopButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            stopButton.ForeColor = Color.DarkRed;
            stopButton.Location = new Point(1096, 42);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(77, 25);
            stopButton.TabIndex = 7;
            stopButton.Text = "Stoppen ■";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // presentButton
            // 
            presentButton.Anchor = AnchorStyles.Right;
            presentButton.AutoSize = true;
            presentButton.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            presentButton.ForeColor = Color.DarkGreen;
            presentButton.Location = new Point(925, 35);
            presentButton.Name = "presentButton";
            presentButton.Size = new Size(165, 38);
            presentButton.TabIndex = 8;
            presentButton.Text = "Präsentieren ▶";
            presentButton.UseVisualStyleBackColor = true;
            presentButton.Click += presentButton_Click;
            // 
            // lockSettingsCheckBox
            // 
            lockSettingsCheckBox.AutoSize = true;
            lockSettingsCheckBox.Dock = DockStyle.Bottom;
            lockSettingsCheckBox.Location = new Point(2, 798);
            lockSettingsCheckBox.Name = "lockSettingsCheckBox";
            lockSettingsCheckBox.Size = new Size(1182, 19);
            lockSettingsCheckBox.TabIndex = 4;
            lockSettingsCheckBox.Text = "Sperre Einstellungen";
            lockSettingsCheckBox.UseVisualStyleBackColor = true;
            lockSettingsCheckBox.CheckedChanged += lockSettingsCheckBox_CheckedChanged;
            // 
            // BarMonitorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(lockSettingsCheckBox);
            Controls.Add(presentGroupBox);
            Controls.Add(diashowGroupBox);
            Controls.Add(identifyLabel);
            Controls.Add(displaysGroupBox);
            MenuImageKey = "device-tv";
            MenuItemName = "Kneipe Monitor";
            Name = "BarMonitorControl";
            Size = new Size(1189, 822);
            Load += BarMonitorControl_Load;
            displaysGroupBox.ResumeLayout(false);
            diashowGroupBox.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            presentGroupBox.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox displaysGroupBox;
        private FlowLayoutPanel displaysFlowLayoutPanel;
        private Label identifyLabel;
        private GroupBox diashowGroupBox;
        private TextBox remotePathTextBox;
        private Label label2;
        private Label label3;
        private TextBox localPathTextBox;
        private Button getButton;
        private CheckBox autoGetCheckBox;
        private TableLayoutPanel tableLayoutPanel1;
        private Button localPathButton;
        private GroupBox presentGroupBox;
        private TableLayoutPanel tableLayoutPanel2;
        private Label vlcPathLabel;
        private TextBox vlcPathTextBox;
        private Button vlcPathButton;
        private Button stopButton;
        private Button presentButton;
        private CheckBox lockSettingsCheckBox;
        private Label remotePathStatusLabel;
    }
}
