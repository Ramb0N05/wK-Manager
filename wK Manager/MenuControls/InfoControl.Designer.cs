namespace wK_Manager.MenuControls {
    partial class InfoControl {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoControl));
            tableLayoutPanel1 = new TableLayoutPanel();
            logoPictureBox = new PictureBox();
            productInfoLabel = new Label();
            licenseTextBox = new TextBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(logoPictureBox, 1, 0);
            tableLayoutPanel1.Controls.Add(productInfoLabel, 1, 1);
            tableLayoutPanel1.Controls.Add(licenseTextBox, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1233, 702);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            logoPictureBox.Anchor = AnchorStyles.None;
            logoPictureBox.Location = new Point(568, 3);
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.Size = new Size(96, 96);
            logoPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            logoPictureBox.TabIndex = 0;
            logoPictureBox.TabStop = false;
            // 
            // productInfoLabel
            // 
            productInfoLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            productInfoLabel.AutoSize = true;
            productInfoLabel.Location = new Point(414, 102);
            productInfoLabel.Name = "productInfoLabel";
            productInfoLabel.Size = new Size(405, 45);
            productInfoLabel.TabIndex = 1;
            productInfoLabel.Text = "%ProductName%\r\n%ProductVersion%\r\n%Author%";
            productInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // licenseTextBox
            // 
            licenseTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(licenseTextBox, 3);
            licenseTextBox.Location = new Point(3, 170);
            licenseTextBox.Multiline = true;
            licenseTextBox.Name = "licenseTextBox";
            licenseTextBox.ReadOnly = true;
            licenseTextBox.ScrollBars = ScrollBars.Vertical;
            licenseTextBox.Size = new Size(1227, 529);
            licenseTextBox.TabIndex = 3;
            licenseTextBox.Text = resources.GetString("licenseTextBox.Text");
            licenseTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // InfoControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            MenuImageKey = "sign_info";
            MenuItemName = "Info";
            MenuItemOrder = 999;
            Name = "InfoControl";
            Size = new Size(1233, 702);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox logoPictureBox;
        private Label productInfoLabel;
        private TextBox licenseTextBox;
    }
}
