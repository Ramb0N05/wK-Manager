namespace wK_Manager
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            mainSplitContainer = new SplitContainer();
            menuListView = new ListView();
            menuImageList = new ImageList(components);
            menuTabControl = new TabControl();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.Location = new Point(0, 0);
            mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(menuListView);
            mainSplitContainer.Panel1MinSize = 150;
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(menuTabControl);
            mainSplitContainer.Panel2MinSize = 200;
            mainSplitContainer.Size = new Size(800, 450);
            mainSplitContainer.SplitterDistance = 266;
            mainSplitContainer.TabIndex = 0;
            // 
            // menuListView
            // 
            menuListView.Dock = DockStyle.Fill;
            menuListView.HeaderStyle = ColumnHeaderStyle.None;
            menuListView.LargeImageList = menuImageList;
            menuListView.Location = new Point(0, 0);
            menuListView.MultiSelect = false;
            menuListView.Name = "menuListView";
            menuListView.Size = new Size(266, 450);
            menuListView.SmallImageList = menuImageList;
            menuListView.TabIndex = 0;
            menuListView.UseCompatibleStateImageBehavior = false;
            menuListView.View = View.List;
            menuListView.ItemSelectionChanged += menuListView_ItemSelectionChanged;
            // 
            // menuImageList
            // 
            menuImageList.ColorDepth = ColorDepth.Depth32Bit;
            menuImageList.ImageStream = (ImageListStreamer)resources.GetObject("menuImageList.ImageStream");
            menuImageList.TransparentColor = Color.Transparent;
            menuImageList.Images.SetKeyName(0, "device-tv");
            // 
            // menuTabControl
            // 
            menuTabControl.Appearance = TabAppearance.FlatButtons;
            menuTabControl.Dock = DockStyle.Fill;
            menuTabControl.ItemSize = new Size(0, 1);
            menuTabControl.Location = new Point(0, 0);
            menuTabControl.Name = "menuTabControl";
            menuTabControl.SelectedIndex = 0;
            menuTabControl.Size = new Size(530, 450);
            menuTabControl.SizeMode = TabSizeMode.Fixed;
            menuTabControl.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(mainSplitContainer);
            MinimumSize = new Size(370, 250);
            Name = "MainForm";
            Text = "wK Manager";
            Load += MainForm_Load;
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer mainSplitContainer;
        private ListView menuListView;
        private TabControl menuTabControl;
        private ImageList menuImageList;
    }
}