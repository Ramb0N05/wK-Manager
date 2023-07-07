using SharpRambo.ExtensionsLib;

namespace BarMonitorWKPlugin.Forms {
    public partial class PresenterForm : Form {
        public bool CloseOnEnd { get; set; } = false;
        public uint Interval { get; set; } = 5000;
        public bool Repeat { get; set; } = true;
        public bool Shuffle { get; set; } = false;

        private uint currentPosition = 0;
        private readonly DirectoryInfo? diashowDir = null;
        private IEnumerable<string> diashowFiles = Enumerable.Empty<string>();
        private readonly Dictionary<string, Bitmap> cache = new();

        public PresenterForm(string diashowPath, uint interval = 5000) {
            InitializeComponent();

            if (!diashowPath.IsNull())
                diashowDir = new(diashowPath);

            Interval = interval;
        }

        private void presenterForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (presenterTimer.Enabled)
                presenterTimer.Stop();

            presenterTimer.Dispose();
            presenterPictureBox.Image?.Dispose();
            presenterPictureBox.Dispose();
            cache.Clear();
        }

        private async void presenterForm_Load(object sender, EventArgs e) {
            if (diashowDir != null && diashowDir.Exists) {
                await diashowDir.GetFiles().ForEachAsync(async (file) => {
                    string mime = MimeMapping.MimeUtility.GetMimeMapping(file.Extension);

                    if (wK_Manager.Plugins.BarMonitorWKPlugin.AcceptedDiashowContentTypes.Contains(mime))
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
    }
}
