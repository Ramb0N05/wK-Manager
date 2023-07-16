using System.Runtime.InteropServices;
using wK_Manager.Base.NativeCode;

namespace wK_Manager.Base.Extensions {

    public static class ProgressBarExtensions {

        public enum ProgressBarState {
            Normal = 1,
            Error = 2,
            Pause = 3
        }

        public static ProgressBarState GetState(this ProgressBar progressBar)
            => (ProgressBarState)(int)(User32.SendMessage(progressBar.Handle, Enumerations.WindowsMessages.PBM_GETSTATE, nint.Zero, nint.Zero) ?? 0);

        public static void SetState(this ProgressBar progressBar, ProgressBarState state)
            => User32.SendMessage(progressBar.Handle, Enumerations.WindowsMessages.PBM_SETSTATE, (nint)state, nint.Zero);
    }
}
