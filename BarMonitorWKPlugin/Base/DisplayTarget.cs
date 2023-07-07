using Microsoft.Win32;
using ScreenInformation;
using SharpRambo.ExtensionsLib;
using System.Text.RegularExpressions;

namespace BarMonitorWKPlugin.Base {
    internal partial class DisplayTarget {
        public const string MonitorEnumRegSubKey = "SYSTEM\\CurrentControlSet\\Enum\\DISPLAY";
        public const string MonitorEnumFriendlyNameRegValue = "FriendlyName";
        public const char MonitorEnumFriendlyNameValueItemSeperator = ';';

        [GeneratedRegex(@"\w+\\([A-Za-z0-9]+)\\\{.+\}\\\d+", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        public static partial Regex MonitorIdentifierRegEx();

        public Rectangle Area { get; set; }
        public string FriendlyName { get; set; }
        public string Identifier { get; set; }
        public bool IsDuplicated { get; set; } = false;
        public bool IsPrimary { get; set; }
        public uint Number { get; set; }
        public uint SourceID { get; set; }
        public uint TargetID { get; set; }
        public Rectangle WorkArea { get; set; }

        #region Constructor
        public DisplayTarget(Rectangle area, string friendlyName, string identifier, bool isPrimary, uint number, uint sourceId, uint targetId, Rectangle workArea) {
            Area = area;
            FriendlyName = !friendlyName.IsNull() ? friendlyName : throw new ArgumentNullException(nameof(friendlyName));
            Identifier = !identifier.IsNull() ? identifier : throw new ArgumentNullException(nameof(identifier));
            IsPrimary = isPrimary;
            Number = number;
            SourceID = sourceId;
            TargetID = targetId;
            WorkArea = workArea;
        }
        #endregion

        #region Methods
        public Point GetLocation() => GetLocation(new Point(0, 0));
        public Point GetLocation(Point relativeLocation) {
            uint displayId = SourceID;

            if (Screen.AllScreens.Length > displayId) {
                Screen screen = Screen.AllScreens[displayId];

                if (relativeLocation.X <= screen.WorkingArea.Width && relativeLocation.X >= 0 &&
                    relativeLocation.Y <= screen.WorkingArea.Height && relativeLocation.Y >= 0)
                    return new Point(Screen.AllScreens[displayId].WorkingArea.Location.X + relativeLocation.X,
                                     Screen.AllScreens[displayId].WorkingArea.Location.Y + relativeLocation.Y);
            }

            return new Point(0, 0);
        }
        #endregion

        #region Statics
        public static async Task<IEnumerable<DisplayTarget>> InitializeFromMonitors(IEnumerable<DisplaySource> monitors) {
            IEnumerable<DisplayTarget> targets = Enumerable.Empty<DisplayTarget>();
            uint numberIterator = 1;

            await monitors.ForEachAsync(async (monitor) => {
                if (targets.Any(d => d.TargetID == monitor.MonitorInformation.TargetId)) {
                    await targets.ForEachAsync(async (target) => {
                        if (target.TargetID == monitor.MonitorInformation.TargetId) {
                            string identifier = parseMonitorIdentifier(monitor.Id) ?? string.Empty;
                            string friendlyName = await parseMonitorFriendlyName(identifier) ?? string.Empty;

                            target.IsDuplicated = true;
                            target.FriendlyName += " & " + (!friendlyName.IsNull() ? friendlyName : "Unknown");
                        }

                        await Task.CompletedTask;
                    });
                } else {
                    string? identifier = parseMonitorIdentifier(monitor.Id);

                    if (identifier != null) {
                        string? friendlyName = await parseMonitorFriendlyName(identifier);

                        if (friendlyName != null) {
                            targets = targets.Append(new DisplayTarget(
                                area: monitor.MonitorInformation.Area,
                                friendlyName: friendlyName,
                                identifier: identifier,
                                isPrimary: monitor.MonitorInformation.IsPrimary,
                                number: numberIterator,
                                sourceId: (uint)monitor.MonitorInformation.SourceId,
                                targetId: (uint)monitor.MonitorInformation.TargetId,
                                workArea: monitor.MonitorInformation.WorkArea
                            ));

                            numberIterator++;
                        }
                    }
                }

                await Task.CompletedTask;
            });

            return targets;
        }

        private static string? parseMonitorIdentifier(string id) {
            if (id.IsNull())
                return null;

            Match match = MonitorIdentifierRegEx().Match(id);

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

                    if (currentRegKey != null && currentRegKey.ValueCount > 0 && currentRegKey.GetValueNames().Contains(MonitorEnumFriendlyNameRegValue)) {
                        string? regFriendlyName = Convert.ToString(
                            currentRegKey.GetValue(MonitorEnumFriendlyNameRegValue, null, RegistryValueOptions.DoNotExpandEnvironmentNames)
                        );

                        if (regFriendlyName != null && !regFriendlyName.IsNull() && regFriendlyName != Convert.ToString(null)) {
                            IEnumerable<string> regFriendlyNameItems = regFriendlyName.Split(
                                MonitorEnumFriendlyNameValueItemSeperator,
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
        #endregion
    }
}
