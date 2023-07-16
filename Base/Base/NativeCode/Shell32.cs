using SharpRambo.ExtensionsLib;
using System.Runtime.InteropServices;
using static wK_Manager.Base.NativeCode.Enumerations;

namespace wK_Manager.Base.NativeCode {

    public static partial class Shell32 {
        private const string LIB = "shell32.dll";

        #region Imports

        [LibraryImport(LIB, SetLastError = true)]
        private static partial uint SHOpenFolderAndSelectItems(nint pidlFolder, uint cidl, [MarshalAs(UnmanagedType.LPArray)] nint[] apidl, uint dwFlags);

        [LibraryImport(LIB, SetLastError = true)]
        private static partial uint SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string pszName, nint pbc, out nint ppidl, uint sfgaoIn, out uint psfgaoOut);

        #endregion Imports

        #region Wrappers

        /// <summary>
        /// Öffnet ein Windows-Explorer-Fenster mit angegebenen Elementen in einem bestimmten Ordner, der ausgewählt ist.
        /// </summary>
        /// <param name="pidlFolder">Ein Zeiger auf eine vollqualifizierte Element-ID-Liste, die den Ordner angibt.</param>
        /// <param name="cidl">Eine Anzahl von Elementen im Auswahlarray, apidl. Wenn cidl null ist, muss pidlFolder auf eine vollständig angegebene ITEMIDLIST verweisen, die ein einzelnes Element beschreibt, das ausgewählt werden soll. Diese Funktion öffnet den übergeordneten Ordner und wählt dieses Element aus.</param>
        /// <param name="apidl">Ein Zeiger auf ein Array von PIDL-Strukturen, von denen jede ein Element ist, das im Zielordner ausgewählt werden soll, auf den pidlFolder verwiesen wird.</param>
        /// <param name="dwFlags">Die optionalen Flags. Unter Windows XP wird dieser Parameter ignoriert. In Windows Vista werden die folgenden Flags definiert.</param>
        /// <returns>Wenn diese Funktion erfolgreich verläuft, gibt sie S_OK zurück. Andernfalls wird ein HRESULT-Fehlercode zurückgegeben.</returns>
        public static HRESULT OpenFolderAndSelectItems(nint pidlFolder, uint cidl, nint[] apidl, OFASI dwFlags) {
            if (pidlFolder == nint.Zero)
                return HRESULT.E_POINTER;

            uint result = SHOpenFolderAndSelectItems(pidlFolder, cidl, apidl, (uint)dwFlags);
            return Enum.TryParse(result.ToString(), true, out HRESULT hr) ? hr : HRESULT.E_FAIL;
        }

        /// <summary>
        /// Übersetzt den Anzeigenamen eines Shell-Namespaceobjekts in eine Elementbezeichnerliste und gibt die Attribute des Objekts zurück. Diese Funktion ist die bevorzugte Methode zum Konvertieren einer Zeichenfolge in einen Zeiger auf eine Elementbezeichnerliste (Item Identifier List, PIDL).
        /// </summary>
        /// <param name="pszName">Ein Zeiger auf eine mit Null beendete breite Zeichenfolge, die den zu analysierenden Anzeigenamen enthält.</param>
        /// <param name="pbc">Ein Bindungskontext, der den Analysevorgang steuert. Dieser Parameter ist normalerweise auf NULL festgelegt.</param>
        /// <param name="ppidl">Die Adresse eines Zeigers auf eine Variable vom Typ ITEMIDLIST , welche die Elementbezeichnerliste für das Objekt empfängt. Wenn ein Fehler auftritt, wird dieser Parameter auf NULL festgelegt.</param>
        /// <param name="sfgaoIn">Ein ULONG-Wert, der die abzufragenden Attribute angibt. Um ein oder mehrere Attribute abzufragen, initialisieren Sie diesen Parameter mit den Flags, welche die relevanten Attribute darstellen. Eine Liste der verfügbaren SFGAO-Flags finden Sie unter SFGAO.</param>
        /// <param name="psfgaoOut">Ein Zeiger auf eine ULONG. Bei der Rückgabe werden die Attribute festgelegt, die für das Objekt true sind und in sfgaoIn angefordert wurden. Die Attributflags eines Objekts können null oder eine Kombination aus SFGAO-Flags sein. Eine Liste der verfügbaren SFGAO-Flags finden Sie unter SFGAO.</param>
        /// <returns>Wenn diese Funktion erfolgreich ist, gibt sie S_OK zurück. Andernfalls wird ein Fehlercode HRESULT zurückgegeben.</returns>
        public static HRESULT ParseDisplayName(string pszName, nint? pbc, out nint ppidl, SFGAO sfgaoIn, out SFGAO psfgaoOut) {
            if (pszName.IsNull()) {
                ppidl = nint.Zero;
                psfgaoOut = 0;

                return HRESULT.E_FAIL;
            }

            pbc ??= nint.Zero;
            uint result = SHParseDisplayName(pszName, pbc.Value, out ppidl, (uint)sfgaoIn, out uint psfgaoOutUint);
            psfgaoOut = Enum.TryParse(psfgaoOutUint.ToString(), true, out SFGAO sfgao) ? sfgao : SFGAO.None;

            return Enum.TryParse(result.ToString(), true, out HRESULT hr) ? hr : HRESULT.E_FAIL;
        }

        #endregion Wrappers

        #region Methods

        public static void OpenFolder(string folderPath) => OpenFolderAndSelectFile(folderPath, null);

        public static void OpenFolderAndSelectFile(string folderPath, string? file) {
            ParseDisplayName(folderPath, nint.Zero, out nint nativeFolder, 0, out SFGAO psfgaoOut);

            if (nativeFolder == nint.Zero) {
                // Log error, can't find folder
                return;
            }

            file ??= string.Empty;
            ParseDisplayName(Path.Combine(folderPath, file), nint.Zero, out nint nativeFile, 0, out psfgaoOut);

            nint[] fileArray;
            if (nativeFile == nint.Zero) {
                // Open the folder without the file selected if we can't find the file
                fileArray = Array.Empty<nint>();
            } else {
                fileArray = new nint[] { nativeFile };
            }

            OpenFolderAndSelectItems(nativeFolder, (uint)fileArray.Length, fileArray, OFASI.None);

            Marshal.FreeCoTaskMem(nativeFolder);
            if (nativeFile != nint.Zero) {
                Marshal.FreeCoTaskMem(nativeFile);
            }
        }

        #endregion Methods
    }
}
