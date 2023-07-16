using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static wK_Manager.Base.NativeCode.Enumerations;

namespace wK_Manager.Base.NativeCode {

    public static partial class User32 {
        private const string LIB = "user32.dll";

        #region Imports

        [LibraryImport(LIB, EntryPoint = "SendMessageW", SetLastError = false)]
        private static partial nint SendMessage(nint hWnd, uint Msg, nint wParam, nint lParam);

        #endregion Imports

        #region Wrappers

        public static nint? SendMessage(nint hWnd, WindowsMessages Msg, nint wParam, nint lParam)
            => hWnd == nint.Zero ? null : SendMessage(hWnd, (uint)Msg, wParam, lParam);

        #endregion Wrappers
    }
}
