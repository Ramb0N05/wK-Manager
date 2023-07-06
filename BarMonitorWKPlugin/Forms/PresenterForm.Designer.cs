namespace BarMonitorWKPlugin.Forms
{
    partial class PresenterForm
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
            components = new System.ComponentModel.Container();
            presenterPictureBox = new PictureBox();
            presenterTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)presenterPictureBox).BeginInit();
            SuspendLayout();
            // 
            // presenterPictureBox
            // 
            presenterPictureBox.BackColor = Color.Black;
            presenterPictureBox.Dock = DockStyle.Fill;
            presenterPictureBox.Location = new Point(0, 0);
            presenterPictureBox.Name = "presenterPictureBox";
            presenterPictureBox.Size = new Size(500, 500);
            presenterPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            presenterPictureBox.TabIndex = 0;
            presenterPictureBox.TabStop = false;
            // 
            // presenterTimer
            // 
            presenterTimer.Interval = 1;
            presenterTimer.Tick += presenterTimer_Tick;
            // 
            // PresenterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 500);
            ControlBox = false;
            Controls.Add(presenterPictureBox);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PresenterForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "PresenterFrom";
            FormClosing += PresenterForm_FormClosing;
            Load += PresenterForm_Load;
            ((System.ComponentModel.ISupportInitialize)presenterPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox presenterPictureBox;
        private System.Windows.Forms.Timer presenterTimer;
    }
}