using System.Runtime.InteropServices;

namespace wK_Manager.Base.Extensions {

    public static partial class ProgressBarExtensions {
        private const int PBM_GETSTATE = WM_USER + 17;
        private const int PBM_SETSTATE = WM_USER + 16;
        private const int WM_USER = 0x400;

        public enum ProgressBarState {
            Normal = 1,
            Error = 2,
            Pause = 3
        }

        public static ProgressBarState GetState(this ProgressBar progressBar)
            => (ProgressBarState)(int)SendMessage(progressBar.Handle, PBM_GETSTATE, nint.Zero, nint.Zero);

        public static void SetState(this ProgressBar progressBar, ProgressBarState state)
            => SendMessage(progressBar.Handle, PBM_SETSTATE, (nint)state, nint.Zero);

        [LibraryImport("user32.dll", EntryPoint = "SendMessageW", SetLastError = false)]
        private static partial nint SendMessage(nint hWnd, uint Msg, nint wParam, nint lParam);
    }
}
