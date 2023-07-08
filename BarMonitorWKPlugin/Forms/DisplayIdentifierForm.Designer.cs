using System.Drawing;
using System.Windows.Forms;

namespace wK_Manager.Forms {
    partial class DisplayIdentifierForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            displayInfoLabel = new Label();
            SuspendLayout();
            // 
            // displayInfoLabel
            // 
            displayInfoLabel.Dock = DockStyle.Fill;
            displayInfoLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            displayInfoLabel.Location = new Point(0, 0);
            displayInfoLabel.Name = "displayInfoLabel";
            displayInfoLabel.Size = new Size(284, 261);
            displayInfoLabel.TabIndex = 0;
            displayInfoLabel.Text = "%DisplayNumber%\r\n%DisplayName%";
            displayInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DisplayIdentifierForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SkyBlue;
            ClientSize = new Size(284, 261);
            ControlBox = false;
            Controls.Add(displayInfoLabel);
            Cursor = Cursors.Cross;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DisplayIdentifierForm";
            Opacity = 0.75D;
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "DisplayIdentifierForm";
            TopMost = true;
            Shown += displayIdentifierForm_Shown;
            ResumeLayout(false);
        }

        #endregion

        private Label displayInfoLabel;
    }
}