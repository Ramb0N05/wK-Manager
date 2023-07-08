using System.Drawing;
using System.Windows.Forms;

namespace wK_Manager.Plugins.MenuControls {
    partial class BarMonitorControl {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarMonitorControl));
            displaysGroupBox = new GroupBox();
            displaysFlowLayoutPanel = new FlowLayoutPanel();
            reloadMonitorsPictureBox = new PictureBox();
            identifyLabel = new Label();
            diashowGroupBox = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            localPathButton = new Button();
            remotePathTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            autoObtainCheckBox = new CheckBox();
            obtainButton = new Button();
            localPathTextBox = new TextBox();
            remotePathStatusLabel = new Label();
            presentGroupBox = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            shuffleCheckBox = new CheckBox();
            intervalNumericUpDown = new NumericUpDown();
            repeatCheckBox = new CheckBox();
            intervalLabel = new Label();
            stopButton = new Button();
            presentButton = new Button();
            lockSettingsCheckBox = new CheckBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            saveButton = new Button();
            defaultsButton = new Button();
            obtainProgressBar = new ProgressBar();
            tableLayoutPanel4 = new TableLayoutPanel();
            displaysGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)reloadMonitorsPictureBox).BeginInit();
            diashowGroupBox.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            presentGroupBox.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)intervalNumericUpDown).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // displaysGroupBox
            // 
            displaysGroupBox.Controls.Add(displaysFlowLayoutPanel);
            displaysGroupBox.Dock = DockStyle.Top;
            displaysGroupBox.Location = new Point(0, 20);
            displaysGroupBox.Name = "displaysGroupBox";
            displaysGroupBox.Size = new Size(1029, 100);
            displaysGroupBox.TabIndex = 0;
            displaysGroupBox.TabStop = false;
            displaysGroupBox.Text = "Monitor Auswahl";
            // 
            // displaysFlowLayoutPanel
            // 
            displaysFlowLayoutPanel.Dock = DockStyle.Fill;
            displaysFlowLayoutPanel.Location = new Point(3, 19);
            displaysFlowLayoutPanel.Name = "displaysFlowLayoutPanel";
            displaysFlowLayoutPanel.Size = new Size(1023, 78);
            displaysFlowLayoutPanel.TabIndex = 0;
            // 
            // reloadMonitorsPictureBox
            // 
            reloadMonitorsPictureBox.Anchor = AnchorStyles.Right;
            reloadMonitorsPictureBox.BackColor = Color.Transparent;
            reloadMonitorsPictureBox.Cursor = Cursors.Hand;
            reloadMonitorsPictureBox.Image = (Image)resources.GetObject("reloadMonitorsPictureBox.Image");
            reloadMonitorsPictureBox.Location = new Point(1005, 0);
            reloadMonitorsPictureBox.Margin = new Padding(3, 0, 0, 0);
            reloadMonitorsPictureBox.Name = "reloadMonitorsPictureBox";
            reloadMonitorsPictureBox.Size = new Size(24, 20);
            reloadMonitorsPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            reloadMonitorsPictureBox.TabIndex = 6;
            reloadMonitorsPictureBox.TabStop = false;
            reloadMonitorsPictureBox.Click += reloadMonitorsPictureBox_Click;
            // 
            // identifyLabel
            // 
            identifyLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            identifyLabel.AutoSize = true;
            identifyLabel.Location = new Point(828, 5);
            identifyLabel.Name = "identifyLabel";
            identifyLabel.Size = new Size(171, 15);
            identifyLabel.TabIndex = 1;
            identifyLabel.Text = "(Rechtsklick zum identifizieren)";
            identifyLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // diashowGroupBox
            // 
            diashowGroupBox.Controls.Add(tableLayoutPanel1);
            diashowGroupBox.Dock = DockStyle.Top;
            diashowGroupBox.Location = new Point(0, 120);
            diashowGroupBox.Name = "diashowGroupBox";
            diashowGroupBox.Size = new Size(1029, 125);
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
            tableLayoutPanel1.Controls.Add(autoObtainCheckBox, 1, 2);
            tableLayoutPanel1.Controls.Add(obtainButton, 2, 2);
            tableLayoutPanel1.Controls.Add(localPathTextBox, 1, 0);
            tableLayoutPanel1.Controls.Add(remotePathStatusLabel, 2, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(1023, 103);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // localPathButton
            // 
            localPathButton.Anchor = AnchorStyles.None;
            localPathButton.AutoSize = true;
            localPathButton.Location = new Point(937, 4);
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
            remotePathTextBox.Size = new Size(837, 23);
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
            // autoObtainCheckBox
            // 
            autoObtainCheckBox.Anchor = AnchorStyles.Right;
            autoObtainCheckBox.AutoSize = true;
            autoObtainCheckBox.Location = new Point(712, 76);
            autoObtainCheckBox.Name = "autoObtainCheckBox";
            autoObtainCheckBox.Size = new Size(212, 19);
            autoObtainCheckBox.TabIndex = 5;
            autoObtainCheckBox.Text = "Beim Starten automatisch beziehen";
            autoObtainCheckBox.UseVisualStyleBackColor = true;
            autoObtainCheckBox.CheckedChanged += autoObtainCheckBox_CheckedChanged;
            // 
            // obtainButton
            // 
            obtainButton.Anchor = AnchorStyles.None;
            obtainButton.AutoSize = true;
            obtainButton.Location = new Point(930, 73);
            obtainButton.Name = "obtainButton";
            obtainButton.Size = new Size(90, 25);
            obtainButton.TabIndex = 4;
            obtainButton.Text = "Jetzt beziehen";
            obtainButton.UseVisualStyleBackColor = true;
            obtainButton.Click += obtainButton_Click;
            // 
            // localPathTextBox
            // 
            localPathTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            localPathTextBox.Location = new Point(87, 5);
            localPathTextBox.Name = "localPathTextBox";
            localPathTextBox.ReadOnly = true;
            localPathTextBox.Size = new Size(837, 23);
            localPathTextBox.TabIndex = 2;
            // 
            // remotePathStatusLabel
            // 
            remotePathStatusLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            remotePathStatusLabel.AutoSize = true;
            remotePathStatusLabel.Location = new Point(930, 43);
            remotePathStatusLabel.Name = "remotePathStatusLabel";
            remotePathStatusLabel.Size = new Size(90, 15);
            remotePathStatusLabel.TabIndex = 6;
            remotePathStatusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // presentGroupBox
            // 
            presentGroupBox.Controls.Add(tableLayoutPanel2);
            presentGroupBox.Dock = DockStyle.Top;
            presentGroupBox.Location = new Point(0, 245);
            presentGroupBox.Name = "presentGroupBox";
            presentGroupBox.Size = new Size(1029, 100);
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
            tableLayoutPanel2.Controls.Add(shuffleCheckBox, 1, 0);
            tableLayoutPanel2.Controls.Add(intervalNumericUpDown, 1, 1);
            tableLayoutPanel2.Controls.Add(repeatCheckBox, 0, 0);
            tableLayoutPanel2.Controls.Add(intervalLabel, 0, 1);
            tableLayoutPanel2.Controls.Add(stopButton, 2, 0);
            tableLayoutPanel2.Controls.Add(presentButton, 2, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1023, 78);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // shuffleCheckBox
            // 
            shuffleCheckBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            shuffleCheckBox.AutoSize = true;
            shuffleCheckBox.Location = new Point(102, 6);
            shuffleCheckBox.Name = "shuffleCheckBox";
            shuffleCheckBox.Size = new Size(747, 19);
            shuffleCheckBox.TabIndex = 9;
            shuffleCheckBox.Text = "Zufallswiedergabe";
            shuffleCheckBox.UseVisualStyleBackColor = true;
            shuffleCheckBox.CheckedChanged += shuffleCheckBox_CheckedChanged;
            // 
            // intervalNumericUpDown
            // 
            intervalNumericUpDown.Anchor = AnchorStyles.Left;
            intervalNumericUpDown.Location = new Point(102, 34);
            intervalNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            intervalNumericUpDown.Name = "intervalNumericUpDown";
            intervalNumericUpDown.Size = new Size(120, 23);
            intervalNumericUpDown.TabIndex = 13;
            intervalNumericUpDown.TextAlign = HorizontalAlignment.Center;
            intervalNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            intervalNumericUpDown.ValueChanged += intervalNumericUpDown_ValueChanged;
            // 
            // repeatCheckBox
            // 
            repeatCheckBox.Anchor = AnchorStyles.Left;
            repeatCheckBox.AutoSize = true;
            repeatCheckBox.Location = new Point(3, 6);
            repeatCheckBox.Name = "repeatCheckBox";
            repeatCheckBox.Size = new Size(93, 19);
            repeatCheckBox.TabIndex = 11;
            repeatCheckBox.Text = "Wiederholen";
            repeatCheckBox.UseVisualStyleBackColor = true;
            repeatCheckBox.CheckedChanged += repeatCheckBox_CheckedChanged;
            // 
            // intervalLabel
            // 
            intervalLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            intervalLabel.AutoSize = true;
            intervalLabel.Location = new Point(3, 38);
            intervalLabel.Name = "intervalLabel";
            intervalLabel.Size = new Size(93, 15);
            intervalLabel.TabIndex = 12;
            intervalLabel.Text = "Intervall (ms):";
            intervalLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // stopButton
            // 
            stopButton.Anchor = AnchorStyles.None;
            stopButton.AutoSize = true;
            stopButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            stopButton.ForeColor = Color.DarkRed;
            stopButton.Location = new Point(899, 3);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(77, 25);
            stopButton.TabIndex = 7;
            stopButton.Text = "Stoppen ■";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // presentButton
            // 
            presentButton.Anchor = AnchorStyles.Top;
            presentButton.AutoSize = true;
            presentButton.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            presentButton.ForeColor = Color.DarkGreen;
            presentButton.Location = new Point(855, 34);
            presentButton.Name = "presentButton";
            tableLayoutPanel2.SetRowSpan(presentButton, 2);
            presentButton.Size = new Size(165, 38);
            presentButton.TabIndex = 8;
            presentButton.Text = "Präsentieren ▶";
            presentButton.TextAlign = ContentAlignment.TopCenter;
            presentButton.UseVisualStyleBackColor = true;
            presentButton.Click += presentButton_Click;
            // 
            // lockSettingsCheckBox
            // 
            lockSettingsCheckBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lockSettingsCheckBox.AutoSize = true;
            lockSettingsCheckBox.Location = new Point(3, 5);
            lockSettingsCheckBox.Name = "lockSettingsCheckBox";
            lockSettingsCheckBox.Size = new Size(421, 19);
            lockSettingsCheckBox.TabIndex = 4;
            lockSettingsCheckBox.Text = "Sperre Einstellungen";
            lockSettingsCheckBox.UseVisualStyleBackColor = true;
            lockSettingsCheckBox.CheckedChanged += lockSettingsCheckBox_CheckedChanged;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.Controls.Add(saveButton, 3, 0);
            tableLayoutPanel3.Controls.Add(defaultsButton, 2, 0);
            tableLayoutPanel3.Controls.Add(lockSettingsCheckBox, 0, 0);
            tableLayoutPanel3.Controls.Add(obtainProgressBar, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Bottom;
            tableLayoutPanel3.Location = new Point(0, 598);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1029, 30);
            tableLayoutPanel3.TabIndex = 5;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.None;
            saveButton.AutoSize = true;
            saveButton.Location = new Point(950, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 24);
            saveButton.TabIndex = 5;
            saveButton.Text = "Speichern";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // defaultsButton
            // 
            defaultsButton.Anchor = AnchorStyles.None;
            defaultsButton.AutoSize = true;
            defaultsButton.Location = new Point(857, 3);
            defaultsButton.Name = "defaultsButton";
            defaultsButton.Size = new Size(87, 24);
            defaultsButton.TabIndex = 6;
            defaultsButton.Text = "Zurücksetzen";
            defaultsButton.UseVisualStyleBackColor = true;
            defaultsButton.Click += defaultsButton_Click;
            // 
            // obtainProgressBar
            // 
            obtainProgressBar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            obtainProgressBar.Location = new Point(430, 7);
            obtainProgressBar.Name = "obtainProgressBar";
            obtainProgressBar.Size = new Size(421, 15);
            obtainProgressBar.Style = ProgressBarStyle.Continuous;
            obtainProgressBar.TabIndex = 7;
            obtainProgressBar.Visible = false;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.Controls.Add(identifyLabel, 0, 0);
            tableLayoutPanel4.Controls.Add(reloadMonitorsPictureBox, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Top;
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(1029, 20);
            tableLayoutPanel4.TabIndex = 7;
            // 
            // BarMonitorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(tableLayoutPanel3);
            Controls.Add(presentGroupBox);
            Controls.Add(diashowGroupBox);
            Controls.Add(displaysGroupBox);
            Controls.Add(tableLayoutPanel4);
            MenuImageKey = "device-tv";
            MenuItemName = "Kneipe Monitor";
            Name = "BarMonitorControl";
            Size = new Size(1029, 628);
            Load += barMonitorControl_Load;
            Resize += barMonitorControl_Resize;
            displaysGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)reloadMonitorsPictureBox).EndInit();
            diashowGroupBox.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            presentGroupBox.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)intervalNumericUpDown).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
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
        private Button obtainButton;
        private CheckBox autoObtainCheckBox;
        private TableLayoutPanel tableLayoutPanel1;
        private Button localPathButton;
        private GroupBox presentGroupBox;
        private TableLayoutPanel tableLayoutPanel2;
        private Button stopButton;
        private Button presentButton;
        private CheckBox lockSettingsCheckBox;
        private Label remotePathStatusLabel;
        private TableLayoutPanel tableLayoutPanel3;
        private Button saveButton;
        private Button defaultsButton;
        private CheckBox shuffleCheckBox;
        private CheckBox repeatCheckBox;
        private Label intervalLabel;
        private NumericUpDown intervalNumericUpDown;
        private ProgressBar obtainProgressBar;
        private PictureBox reloadMonitorsPictureBox;
        private TableLayoutPanel tableLayoutPanel4;
    }
}
