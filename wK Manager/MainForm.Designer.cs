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
            columnHeader1 = new ColumnHeader();
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
            mainSplitContainer.Panel1MinSize = 175;
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(menuTabControl);
            mainSplitContainer.Panel2MinSize = 300;
            mainSplitContainer.Size = new Size(800, 450);
            mainSplitContainer.SplitterDistance = 175;
            mainSplitContainer.TabIndex = 0;
            // 
            // menuListView
            // 
            menuListView.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            menuListView.Dock = DockStyle.Fill;
            menuListView.HeaderStyle = ColumnHeaderStyle.None;
            menuListView.Location = new Point(0, 0);
            menuListView.MultiSelect = false;
            menuListView.Name = "menuListView";
            menuListView.Scrollable = false;
            menuListView.Size = new Size(175, 450);
            menuListView.SmallImageList = menuImageList;
            menuListView.TabIndex = 0;
            menuListView.UseCompatibleStateImageBehavior = false;
            menuListView.View = View.Details;
            menuListView.ItemSelectionChanged += menuListView_ItemSelectionChanged;
            menuListView.Resize += menuListView_Resize;
            // 
            // menuImageList
            // 
            menuImageList.ColorDepth = ColorDepth.Depth32Bit;
            menuImageList.ImageStream = (ImageListStreamer)resources.GetObject("menuImageList.ImageStream");
            menuImageList.TransparentColor = Color.Transparent;
            menuImageList.Images.SetKeyName(0, "address-book");
            menuImageList.Images.SetKeyName(1, "address-book-alt");
            menuImageList.Images.SetKeyName(2, "bag");
            menuImageList.Images.SetKeyName(3, "basket");
            menuImageList.Images.SetKeyName(4, "beer");
            menuImageList.Images.SetKeyName(5, "bell");
            menuImageList.Images.SetKeyName(6, "bitcoin");
            menuImageList.Images.SetKeyName(7, "book");
            menuImageList.Images.SetKeyName(8, "book-bookmark");
            menuImageList.Images.SetKeyName(9, "box");
            menuImageList.Images.SetKeyName(10, "box-full");
            menuImageList.Images.SetKeyName(11, "box-in");
            menuImageList.Images.SetKeyName(12, "box-out");
            menuImageList.Images.SetKeyName(13, "brick-alt");
            menuImageList.Images.SetKeyName(14, "bubble");
            menuImageList.Images.SetKeyName(15, "bubbles");
            menuImageList.Images.SetKeyName(16, "bubbles-alt");
            menuImageList.Images.SetKeyName(17, "building");
            menuImageList.Images.SetKeyName(18, "bullhorn");
            menuImageList.Images.SetKeyName(19, "calculator");
            menuImageList.Images.SetKeyName(20, "calendar");
            menuImageList.Images.SetKeyName(21, "calendar-clock");
            menuImageList.Images.SetKeyName(22, "carton");
            menuImageList.Images.SetKeyName(23, "cat");
            menuImageList.Images.SetKeyName(24, "clock");
            menuImageList.Images.SetKeyName(25, "cloud");
            menuImageList.Images.SetKeyName(26, "cloud-down");
            menuImageList.Images.SetKeyName(27, "cloud-sync");
            menuImageList.Images.SetKeyName(28, "cloud-up");
            menuImageList.Images.SetKeyName(29, "cog");
            menuImageList.Images.SetKeyName(30, "cogs");
            menuImageList.Images.SetKeyName(31, "compass");
            menuImageList.Images.SetKeyName(32, "cone");
            menuImageList.Images.SetKeyName(33, "dashboard");
            menuImageList.Images.SetKeyName(34, "dashboard-alt");
            menuImageList.Images.SetKeyName(35, "database");
            menuImageList.Images.SetKeyName(36, "device-camera");
            menuImageList.Images.SetKeyName(37, "device-computer");
            menuImageList.Images.SetKeyName(38, "device-drive");
            menuImageList.Images.SetKeyName(39, "device-laptop");
            menuImageList.Images.SetKeyName(40, "device-mobile-phone");
            menuImageList.Images.SetKeyName(41, "device-tablet");
            menuImageList.Images.SetKeyName(42, "device-tv");
            menuImageList.Images.SetKeyName(43, "disc");
            menuImageList.Images.SetKeyName(44, "disc-vinyl");
            menuImageList.Images.SetKeyName(45, "drop");
            menuImageList.Images.SetKeyName(46, "envelope");
            menuImageList.Images.SetKeyName(47, "envelope-letter");
            menuImageList.Images.SetKeyName(48, "file-bookmark");
            menuImageList.Images.SetKeyName(49, "file-code");
            menuImageList.Images.SetKeyName(50, "file-empty");
            menuImageList.Images.SetKeyName(51, "file-excel");
            menuImageList.Images.SetKeyName(52, "file-exe");
            menuImageList.Images.SetKeyName(53, "file-font");
            menuImageList.Images.SetKeyName(54, "file-illustrator");
            menuImageList.Images.SetKeyName(55, "file-indesign");
            menuImageList.Images.SetKeyName(56, "file-link");
            menuImageList.Images.SetKeyName(57, "file-note");
            menuImageList.Images.SetKeyName(58, "file-pdf");
            menuImageList.Images.SetKeyName(59, "file-photoshop");
            menuImageList.Images.SetKeyName(60, "file-picture");
            menuImageList.Images.SetKeyName(61, "file-powerpoint");
            menuImageList.Images.SetKeyName(62, "file-premiere");
            menuImageList.Images.SetKeyName(63, "file-sound");
            menuImageList.Images.SetKeyName(64, "file-text");
            menuImageList.Images.SetKeyName(65, "file-video");
            menuImageList.Images.SetKeyName(66, "file-word");
            menuImageList.Images.SetKeyName(67, "file-zip");
            menuImageList.Images.SetKeyName(68, "flag");
            menuImageList.Images.SetKeyName(69, "flag-alt");
            menuImageList.Images.SetKeyName(70, "flask");
            menuImageList.Images.SetKeyName(71, "floppy");
            menuImageList.Images.SetKeyName(72, "flower");
            menuImageList.Images.SetKeyName(73, "folder");
            menuImageList.Images.SetKeyName(74, "folder-document");
            menuImageList.Images.SetKeyName(75, "folder-house");
            menuImageList.Images.SetKeyName(76, "folder-music");
            menuImageList.Images.SetKeyName(77, "folder-picture");
            menuImageList.Images.SetKeyName(78, "folder-video");
            menuImageList.Images.SetKeyName(79, "funnel");
            menuImageList.Images.SetKeyName(80, "gamepad");
            menuImageList.Images.SetKeyName(81, "gift");
            menuImageList.Images.SetKeyName(82, "globe");
            menuImageList.Images.SetKeyName(83, "handshake");
            menuImageList.Images.SetKeyName(84, "headphone");
            menuImageList.Images.SetKeyName(85, "heart");
            menuImageList.Images.SetKeyName(86, "house");
            menuImageList.Images.SetKeyName(87, "institution");
            menuImageList.Images.SetKeyName(88, "key");
            menuImageList.Images.SetKeyName(89, "keyring");
            menuImageList.Images.SetKeyName(90, "layers");
            menuImageList.Images.SetKeyName(91, "life-buoy");
            menuImageList.Images.SetKeyName(92, "light-bulb");
            menuImageList.Images.SetKeyName(93, "lightning");
            menuImageList.Images.SetKeyName(94, "lock");
            menuImageList.Images.SetKeyName(95, "lock-open");
            menuImageList.Images.SetKeyName(96, "magnify");
            menuImageList.Images.SetKeyName(97, "magnify-less");
            menuImageList.Images.SetKeyName(98, "map");
            menuImageList.Images.SetKeyName(99, "map-map-marker");
            menuImageList.Images.SetKeyName(100, "map-marker");
            menuImageList.Images.SetKeyName(101, "microphone");
            menuImageList.Images.SetKeyName(102, "mixer");
            menuImageList.Images.SetKeyName(103, "money");
            menuImageList.Images.SetKeyName(104, "monitor");
            menuImageList.Images.SetKeyName(105, "news");
            menuImageList.Images.SetKeyName(106, "notepad");
            menuImageList.Images.SetKeyName(107, "paperclip");
            menuImageList.Images.SetKeyName(108, "paper-plane");
            menuImageList.Images.SetKeyName(109, "pencil");
            menuImageList.Images.SetKeyName(110, "phone");
            menuImageList.Images.SetKeyName(111, "pin");
            menuImageList.Images.SetKeyName(112, "post-it");
            menuImageList.Images.SetKeyName(113, "profile");
            menuImageList.Images.SetKeyName(114, "profile-group");
            menuImageList.Images.SetKeyName(115, "puzzle");
            menuImageList.Images.SetKeyName(116, "radio");
            menuImageList.Images.SetKeyName(117, "rss");
            menuImageList.Images.SetKeyName(118, "safe");
            menuImageList.Images.SetKeyName(119, "search");
            menuImageList.Images.SetKeyName(120, "shield");
            menuImageList.Images.SetKeyName(121, "shield-error");
            menuImageList.Images.SetKeyName(122, "shield-ok");
            menuImageList.Images.SetKeyName(123, "shield-warning");
            menuImageList.Images.SetKeyName(124, "shop");
            menuImageList.Images.SetKeyName(125, "sign-add");
            menuImageList.Images.SetKeyName(126, "sign-ban");
            menuImageList.Images.SetKeyName(127, "sign-check");
            menuImageList.Images.SetKeyName(128, "sign-delete");
            menuImageList.Images.SetKeyName(129, "sign-down");
            menuImageList.Images.SetKeyName(130, "sign-error");
            menuImageList.Images.SetKeyName(131, "sign-info");
            menuImageList.Images.SetKeyName(132, "sign-left");
            menuImageList.Images.SetKeyName(133, "sign-question");
            menuImageList.Images.SetKeyName(134, "sign-right");
            menuImageList.Images.SetKeyName(135, "sign-sync");
            menuImageList.Images.SetKeyName(136, "sign-up");
            menuImageList.Images.SetKeyName(137, "sign-warning");
            menuImageList.Images.SetKeyName(138, "skull");
            menuImageList.Images.SetKeyName(139, "snow-flake");
            menuImageList.Images.SetKeyName(140, "social-facebook");
            menuImageList.Images.SetKeyName(141, "social-google-plus");
            menuImageList.Images.SetKeyName(142, "social-twitter");
            menuImageList.Images.SetKeyName(143, "social-youtube");
            menuImageList.Images.SetKeyName(144, "stamp");
            menuImageList.Images.SetKeyName(145, "star");
            menuImageList.Images.SetKeyName(146, "star-alt");
            menuImageList.Images.SetKeyName(147, "sun");
            menuImageList.Images.SetKeyName(148, "switch-off");
            menuImageList.Images.SetKeyName(149, "switch-on");
            menuImageList.Images.SetKeyName(150, "tag");
            menuImageList.Images.SetKeyName(151, "tag-alt");
            menuImageList.Images.SetKeyName(152, "terminal");
            menuImageList.Images.SetKeyName(153, "thumb-up");
            menuImageList.Images.SetKeyName(154, "trashcan");
            menuImageList.Images.SetKeyName(155, "trashcan-full");
            menuImageList.Images.SetKeyName(156, "user-female");
            menuImageList.Images.SetKeyName(157, "user-female-alt");
            menuImageList.Images.SetKeyName(158, "user-id");
            menuImageList.Images.SetKeyName(159, "user-male");
            menuImageList.Images.SetKeyName(160, "user-male-alt");
            menuImageList.Images.SetKeyName(161, "webcam");
            menuImageList.Images.SetKeyName(162, "window");
            menuImageList.Images.SetKeyName(163, "window-layout");
            menuImageList.Images.SetKeyName(164, "window-system");
            menuImageList.Images.SetKeyName(165, "wine");
            menuImageList.Images.SetKeyName(166, "wrench");
            menuImageList.Images.SetKeyName(167, "wrench-screwdriver");
            menuImageList.Images.SetKeyName(168, "brick");
            // 
            // menuTabControl
            // 
            menuTabControl.Appearance = TabAppearance.FlatButtons;
            menuTabControl.Dock = DockStyle.Fill;
            menuTabControl.ItemSize = new Size(0, 1);
            menuTabControl.Location = new Point(0, 0);
            menuTabControl.Name = "menuTabControl";
            menuTabControl.SelectedIndex = 0;
            menuTabControl.Size = new Size(621, 450);
            menuTabControl.SizeMode = TabSizeMode.Fixed;
            menuTabControl.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(mainSplitContainer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(370, 250);
            Name = "MainForm";
            Text = "wK Manager";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
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
        public ImageList menuImageList;
        private ColumnHeader columnHeader1;
    }
}