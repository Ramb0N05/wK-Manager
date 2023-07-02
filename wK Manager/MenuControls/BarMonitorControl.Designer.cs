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
            label1 = new Label();
            displaysGroupBox.SuspendLayout();
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
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(2, 105);
            label1.Name = "label1";
            label1.Size = new Size(1182, 15);
            label1.TabIndex = 1;
            label1.Text = "(Rechtsklick zum identifizieren)";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // BarMonitorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(displaysGroupBox);
            MenuImageKey = "device-tv";
            MenuItemName = "Kneipe Monitor";
            Name = "BarMonitorControl";
            Size = new Size(1189, 822);
            displaysGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox displaysGroupBox;
        private FlowLayoutPanel displaysFlowLayoutPanel;
        private Label label1;
    }
}
