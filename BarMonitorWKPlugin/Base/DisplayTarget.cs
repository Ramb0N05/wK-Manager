using Microsoft.Win32;
using SharpRambo.ExtensionsLib;
using System.Text.RegularExpressions;
using WindowsDisplayAPI;
using wK_Manager.Base;

namespace BarMonitorWKPlugIn.Base {

    public sealed partial class DisplayTarget {
        public const string MonitorEnumFriendlyNameRegValue = "FriendlyName";
        public const char MonitorEnumFriendlyNameValueItemSeparator = ';';
        public const string MonitorEnumRegSubKey = "SYSTEM\\CurrentControlSet\\Enum\\DISPLAY";
        public const string UnknownDeviceName = "<dev:unknown>";
        public const string UnknownDisplayName = "<d:unknown>";
        public const string UnknownIdentifier = "<id:unknown>";
        public static readonly Point FallbackPosition = Screen.PrimaryScreen?.Bounds.Location ?? new(0, 0);
        public static readonly Size FallbackResolution = Screen.PrimaryScreen?.WorkingArea.Size ?? new(800, 400);

        public static DisplayTarget Unknown => new(
            UnknownDisplayName,
            UnknownDeviceName,
            "Unknown",
            UnknownIdentifier,
            false,
            0,
            FallbackPosition,
            FallbackResolution
        );

        public string DeviceName { get; }

        public string DisplayName { get; }

        public string FriendlyName { get; private set; }

        public string Identifier { get; }

        public bool IsDuplicated { get; private set; }

        public bool IsPrimary { get; private set; }

        public uint Number { get; private set; }

        public Point Position { get; }

        public Size Resolution { get; }

        #region Constructor

        private DisplayTarget(string displayName, string deviceName, string friendlyName, string identifier, bool isPrimary, uint number, Point position, Size resolution) {
            DisplayName = !displayName.IsNull() ? displayName : throw new ArgumentNullException(nameof(displayName));
            DeviceName = !deviceName.IsNull() ? deviceName : UnknownDeviceName;
            FriendlyName = !friendlyName.IsNull() ? friendlyName : throw new ArgumentNullException(nameof(friendlyName));
            Identifier = !identifier.IsNull() ? Hashing.SHA1_Simple(identifier) : Hashing.SHA1_Simple(UnknownIdentifier);
            IsPrimary = isPrimary;
            Number = number;
            Position = position;
            Resolution = resolution;
        }

        #endregion Constructor

        #region RegEx

        [GeneratedRegex(@"\\\\\?\\DISPLAY#([A-Za-z0-9]+)#.+#\{.+\}", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        public static partial Regex MonitorIdentifierRegEx();

        #endregion RegEx

        #region Methods

        public Point GetPosition() => GetPosition(new Point(0, 0));

        public Point GetPosition(Point relativeLocation)
            => (relativeLocation.X <= Resolution.Width && relativeLocation.X >= 0 &&
                relativeLocation.Y <= Resolution.Height && relativeLocation.Y >= 0)
                ? new Point(Position.X + relativeLocation.X, Position.Y + relativeLocation.Y)
                : FallbackPosition;

        #endregion Methods

        #region Statics

        public static async Task<IEnumerable<DisplayTarget>> Initialize(IEnumerable<Display> monitors) {
            IEnumerable<DisplayTarget> targets = Enumerable.Empty<DisplayTarget>();
            uint numberIterator = 1;

            await monitors.ForEachAsync(async (monitor) => {
                if (targets.Any(d => d.DisplayName == monitor.DisplayName)) {
                    await targets.ForEachAsync(async (target) => {
                        if (target.DisplayName == monitor.DisplayName) {
                            string identifier = parseMonitorDeviceName(monitor.DevicePath) ?? string.Empty;
                            string friendlyName = await parseMonitorFriendlyName(identifier) ?? string.Empty;

                            target.IsDuplicated = true;
                            target.FriendlyName += " & " + (!friendlyName.IsNull() ? friendlyName : "Unknown");
                        }

                        await Task.CompletedTask;
                    });
                } else {
                    string? deviceName = parseMonitorDeviceName(monitor.DevicePath);

                    if (deviceName != null) {
                        string? friendlyName = await parseMonitorFriendlyName(deviceName);

                        if (friendlyName != null) {
                            targets = targets.Append(new DisplayTarget(
                                displayName: monitor.DisplayName,
                                deviceName: deviceName,
                                friendlyName: friendlyName,
                                identifier: Hashing.SHA1_Simple(monitor.DevicePath),
                                isPrimary: monitor.IsGDIPrimary,
                                number: numberIterator,
                                position: monitor.CurrentSetting.Position,
                                resolution: monitor.CurrentSetting.Resolution
                            ));

                            numberIterator++;
                        }
                    }
                }

                await Task.CompletedTask;
            });

            return targets;
        }

        private static string? parseMonitorDeviceName(string devicePath) {
            if (devicePath.IsNull())
                return null;

            Match match = MonitorIdentifierRegEx().Match(devicePath);

            return match.Success && match.Groups.Count > 1
                ? match.Groups[1].Value
                : null;
        }

        private static async Task<string?> parseMonitorFriendlyName(string identifier) {
            if (identifier.IsNull())
                return null;

            string monitorRegKey = MonitorEnumRegSubKey + "\\" + identifier;
            RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey? subKey = baseKey.OpenSubKey(monitorRegKey, RegistryKeyPermissionCheck.ReadSubTree);

            if (subKey == null || subKey.SubKeyCount < 1)
                return identifier;

            IEnumerable<string> monitorSubs = subKey.GetSubKeyNames();

            string? friendlyName = null;
            await monitorSubs.ForEachAsync(async (key) => {
                if (friendlyName.IsNull()) {
                    string currentSubKey = monitorRegKey + "\\" + key;
                    RegistryKey? currentRegKey = baseKey.OpenSubKey(currentSubKey, RegistryKeyPermissionCheck.ReadSubTree);

                    if (currentRegKey?.ValueCount > 0 && currentRegKey.GetValueNames().Contains(MonitorEnumFriendlyNameRegValue)) {
                        string? regFriendlyName = Convert.ToString(
                            currentRegKey.GetValue(MonitorEnumFriendlyNameRegValue, null, RegistryValueOptions.DoNotExpandEnvironmentNames)
                        );

                        if (regFriendlyName?.IsNull() == false && regFriendlyName != Convert.ToString(null)) {
                            IEnumerable<string> regFriendlyNameItems = regFriendlyName.Split(
                                MonitorEnumFriendlyNameValueItemSeparator,
                                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
                            );

                            if (regFriendlyNameItems.Any())
                                friendlyName = regFriendlyNameItems.Last().Replace("(", string.Empty).Replace(")", string.Empty);
                        }
                    }
                }

                await Task.CompletedTask;
            });

            return !friendlyName.IsNull() ? friendlyName : identifier;
        }

        #endregion Statics
    }
}
