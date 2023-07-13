namespace wK_Manager.MenuControls {
    partial class UpdatesControl {
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
            tableLayoutPanel1 = new TableLayoutPanel();
            updatesImagePictureBox = new PictureBox();
            updateStatusLabel = new Label();
            updateButton = new Button();
            updateProgressLabel = new Label();
            updateProgressBar = new ProgressBar();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)updatesImagePictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(updatesImagePictureBox, 0, 0);
            tableLayoutPanel1.Controls.Add(updateStatusLabel, 1, 0);
            tableLayoutPanel1.Controls.Add(updateButton, 2, 0);
            tableLayoutPanel1.Controls.Add(updateProgressLabel, 2, 1);
            tableLayoutPanel1.Controls.Add(updateProgressBar, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(800, 400);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // updatesImagePictureBox
            // 
            updatesImagePictureBox.Anchor = AnchorStyles.Left;
            updatesImagePictureBox.Location = new Point(3, 3);
            updatesImagePictureBox.Name = "updatesImagePictureBox";
            updatesImagePictureBox.Size = new Size(64, 67);
            updatesImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            updatesImagePictureBox.TabIndex = 0;
            updatesImagePictureBox.TabStop = false;
            updatesImagePictureBox.Click += updatesImagePictureBox_Click;
            // 
            // updateStatusLabel
            // 
            updateStatusLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            updateStatusLabel.AutoSize = true;
            updateStatusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            updateStatusLabel.ForeColor = Color.Teal;
            updateStatusLabel.Location = new Point(73, 29);
            updateStatusLabel.Name = "updateStatusLabel";
            updateStatusLabel.Size = new Size(625, 15);
            updateStatusLabel.TabIndex = 1;
            updateStatusLabel.Text = "%Status%";
            updateStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // updateButton
            // 
            updateButton.Anchor = AnchorStyles.Right;
            updateButton.AutoSize = true;
            updateButton.Location = new Point(712, 24);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(85, 25);
            updateButton.TabIndex = 2;
            updateButton.Text = "Aktualisieren";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Visible = false;
            updateButton.Click += updateButton_Click;
            // 
            // updateProgressLabel
            // 
            updateProgressLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            updateProgressLabel.AutoSize = true;
            updateProgressLabel.Location = new Point(704, 76);
            updateProgressLabel.Name = "updateProgressLabel";
            updateProgressLabel.Size = new Size(93, 15);
            updateProgressLabel.TabIndex = 3;
            updateProgressLabel.Text = "%ProgressInfo%";
            updateProgressLabel.TextAlign = ContentAlignment.MiddleLeft;
            updateProgressLabel.Visible = false;
            // 
            // updateProgressBar
            // 
            updateProgressBar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            updateProgressBar.Location = new Point(73, 76);
            updateProgressBar.Maximum = 200;
            updateProgressBar.Name = "updateProgressBar";
            updateProgressBar.Size = new Size(625, 15);
            updateProgressBar.Style = ProgressBarStyle.Continuous;
            updateProgressBar.TabIndex = 4;
            updateProgressBar.Visible = false;
            // 
            // UpdatesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            MenuImageKey = "cloud-sync";
            MenuItemName = "Aktualisierungen";
            MenuItemOrder = 998;
            Name = "UpdatesControl";
            Size = new Size(800, 400);
            Load += updatesControl_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)updatesImagePictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox updatesImagePictureBox;
        private Label updateStatusLabel;
        private Button updateButton;
        private Label updateProgressLabel;
        private ProgressBar updateProgressBar;
    }
}
