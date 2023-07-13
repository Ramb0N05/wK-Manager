using System.Runtime.InteropServices;

namespace wK_Manager.Base.Extensions {
    public static partial class ProgressBarExtensions {
        public const int WM_USER = 0x400;
        public const int PBM_GETSTATE = WM_USER + 17;
        public const int PBM_SETSTATE = WM_USER + 16;

        [LibraryImport("user32.dll", EntryPoint = "SendMessageW", SetLastError = false)]
        private static partial nint SendMessage(nint hWnd, uint Msg, nint wParam, nint lParam);

        public enum ProgressBarState : int {
            Normal = 1,
            Error = 2,
            Pause = 3
        }

        public static ProgressBarState GetState(this ProgressBar progressBar)
            => (ProgressBarState)(int)SendMessage(progressBar.Handle, PBM_GETSTATE, nint.Zero, nint.Zero);

        public static void SetState(this ProgressBar progressBar, ProgressBarState state)
            => SendMessage(progressBar.Handle, PBM_SETSTATE, (nint)state, nint.Zero);
    }
}
