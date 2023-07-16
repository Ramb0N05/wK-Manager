using SharpRambo.ExtensionsLib;

namespace BarMonitorWKPlugIn.Forms {

    public partial class PresenterForm : Form {
        public const int DEFAULT_INTERVAL = 5000;

        private readonly Dictionary<string, Bitmap> cache = new();
        private readonly DirectoryInfo? diashowDir;
        private uint currentPosition;
        private IEnumerable<string> diashowFiles = Enumerable.Empty<string>();

        public bool CloseOnEnd { get; set; }
        public uint Interval { get; set; }
        public bool Repeat { get; set; } = true;
        public bool Shuffle { get; set; }

        #region Constructor

        public PresenterForm(string diashowPath) : this(diashowPath, DEFAULT_INTERVAL) {
        }

        public PresenterForm(string diashowPath, uint interval) {
            InitializeComponent();

            if (!diashowPath.IsNull())
                diashowDir = new(diashowPath);

            Interval = interval;
        }

        #endregion Constructor

        #region EventHandlers

        private void presenterForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (presenterTimer.Enabled)
                presenterTimer.Stop();

            presenterTimer.Dispose();
            presenterPictureBox.Image?.Dispose();
            presenterPictureBox.Dispose();
            cache.Clear();
        }

        private async void presenterForm_Load(object sender, EventArgs e) {
            if (diashowDir?.Exists == true) {
                await diashowDir.GetFiles().ForEachAsync(async (file) => {
                    string mime = MimeMapping.MimeUtility.GetMimeMapping(file.Extension);

                    if (wK_Manager.PlugIns.BarMonitorWKPlugIn.AcceptedDiashowContentTypes.Contains(mime))
                        diashowFiles = diashowFiles.Append(file.FullName);

                    await Task.CompletedTask;
                });

                presenterTimer.Start();
            } else
                Close();
        }

        private void presenterTimer_Tick(object sender, EventArgs e) {
            if (!diashowFiles.IsNull()) {
                if (diashowFiles.Count() > 1) {
                    if (Shuffle)
                        currentPosition = (uint)new Random().Next(0, diashowFiles.Count());

                    if (currentPosition < diashowFiles.Count()) {
                        string filePath = diashowFiles.ElementAt((int)currentPosition);

                        if (!cache.ContainsKey(filePath))
                            cache.Add(filePath, new Bitmap(filePath));

                        presenterPictureBox.Image = cache[filePath];
                        currentPosition++;
                    }

                    if (currentPosition >= diashowFiles.Count()) {
                        if (!Repeat) {
                            if (CloseOnEnd)
                                Close();

                            presenterTimer.Enabled = false;
                        }

                        currentPosition = 0;
                    }
                } else {
                    presenterPictureBox.Image = new Bitmap(diashowFiles.ElementAt((int)currentPosition));
                    presenterTimer.Enabled = false;
                }
            }

            presenterTimer.Interval = (int)Interval;
        }

        #endregion EventHandlers
    }
}
