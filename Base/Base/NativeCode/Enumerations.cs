using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace wK_Manager.Base.NativeCode {

    public static class Enumerations {

        #region General

        public enum HRESULT : uint {

            /// <summary>
            /// Vorgang erfolgreich
            /// </summary>
            S_OK = 0x00000000,

            /// <summary>
            /// Vorgang erfolgreich, aber keine Ergebnisse zurückgegeben
            /// </summary>
            S_FALSE = 0x00000001,

            /// <summary>
            /// Nicht implementiert
            /// </summary>
            E_NOTIMPL = 0x80004001,

            /// <summary>
            /// Schnittstelle nicht unterstützt.
            /// </summary>
            E_NOINTERFACE = 0x80004002,

            /// <summary>
            /// Ungültiger Zeiger
            /// </summary>
            E_POINTER = 0x80004003,

            /// <summary>
            /// Vorgang abgebrochen
            /// </summary>
            E_ABORT = 0x80004004,

            /// <summary>
            /// Nicht spezifizierter Fehler
            /// </summary>
            E_FAIL = 0x80004005,

            /// <summary>
            /// Unerwarteter Fehler
            /// </summary>
            E_UNEXPECTED = 0x8000FFFF,

            /// <summary>
            /// Fehler "Allgemeiner Zugriff verweigert"
            /// </summary>
            E_ACCESSDENIED = 0x80070005,

            /// <summary>
            /// Ungültiges Handle
            /// </summary>
            E_HANDLE = 0x80070006,

            /// <summary>
            /// Fehler beim Zuweisen des erforderlichen Arbeitsspeichers
            /// </summary>
            E_OUTOFMEMORY = 0x8007000E,

            /// <summary>
            /// Mindestens ein Argument ist ungültig.
            /// </summary>
            E_INVALIDARG = 0x80070057
        }

        #endregion General

        #region Shell32

        [Flags]
        public enum OFASI {
            None = 0x0000,

            /// <summary>
            /// Select an item and put its name in edit mode. This flag can only be used when a single item is being selected. For multiple item selections, it is ignored.
            /// </summary>
            Edit = 0x0001,

            /// <summary>
            /// Select the item or items on the desktop rather than in a Windows Explorer window. Note that if the desktop is obscured behind open windows, it will not be made visible.
            /// </summary>
            OpenDesktop = 0x0002
        }

        /// <summary>
        /// Bitfeldwerte stellen Attribute dar, die für ein Element (Datei oder Ordner) oder eine Gruppe von Elementen abgerufen werden können. Sie werden mit den IShellFolder- und IShellItem-APIs verwendet, insbesondere mit IShellFolder::GetAttributesOf und IShellItem::GetAttributes.
        /// </summary>
        [Flags]
        public enum SFGAO : ulong {
            None = 0x00000000,

            /// <summary>
            /// Die angegebenen Elemente können kopiert werden.
            /// </summary>
            CanCopy = 0x00000001,

            /// <summary>
            /// Die angegebenen Elemente können verschoben werden.
            /// </summary>
            CanMove = 0x00000002,

            /// <summary>
            /// <para>Verknüpfungen können für die angegebenen Elemente erstellt werden. Dieses Attribut hat den gleichen Wert wie DROPEFFECT_LINK.</para>
            /// <para>Wenn eine Namespaceerweiterung dieses Attribut zurückgibt, wird dem Kontextmenü, das bei Drag-and-Drop-Vorgängen angezeigt wird, der Eintrag Verknüpfung erstellen mit einem Standardhandler hinzugefügt. Die Erweiterung kann auch einen eigenen Handler für das Linkverb anstelle des Standardwerts implementieren. Wenn die Erweiterung dies tut, ist sie für die Erstellung der Verknüpfung verantwortlich.</para>
            /// <para>Außerdem wird dem Menü Windows Explorer Datei und normalen Kontextmenüs ein Element Verknüpfung erstellen hinzugefügt.</para>
            /// <para>Wenn das Element ausgewählt ist, wird die IContextMenu::InvokeCommand-Methode Ihrer Anwendung aufgerufen, wobei das lpVerb-Element der CMINVOKECOMMANDINFO-Struktur auf link festgelegt ist. Ihre Anwendung ist für die Erstellung des Links verantwortlich.</para>
            /// </summary>
            CanLink = 0x00000004,

            /// <summary>
            /// Die angegebenen Elemente können über IShellFolder::BindToObject an ein IStorage-Objekt gebunden werden. Weitere Informationen zu Namespacebearbeitungsfunktionen finden Sie unter IStorage.
            /// </summary>
            Storage = 0x00000008,

            /// <summary>
            /// Die angegebenen Elemente können umbenannt werden. Beachten Sie, dass dieser Wert im Wesentlichen ein Vorschlag ist. Nicht alle Namespaceclients lassen das Umbenennen von Elementen zu. Für diejenigen, für die dies der Fall ist, muss dieses Attribut jedoch festgelegt sein.
            /// </summary>
            CanRename = 0x00000010,

            /// <summary>
            /// Die angegebenen Elemente können gelöscht werden.
            /// </summary>
            CanDelete = 0x00000020,

            /// <summary>
            /// Die angegebenen Elemente verfügen über Eigenschaftenblätter.
            /// </summary>
            HasPropSheet = 0x00000040,

            /// <summary>
            /// Die angegebenen Elemente sind Ablageziele.
            /// </summary>
            DropTarget = 0x00000100,

            /// <summary>
            /// Dieses Flag ist eine Maske für die Funktionsattribute: SFGAO_CANCOPY, SFGAO_CANMOVE, SFGAO_CANLINK, SFGAO_CANRENAME, SFGAO_CANDELETE, SFGAO_HASPROPSHEET und SFGAO_DROPTARGET. Aufrufer verwenden diesen Wert normalerweise nicht.
            /// </summary>
            CapabilityMask = CanCopy | CanMove | CanLink | CanRename | CanDelete | HasPropSheet | DropTarget,

            /// <summary>
            /// Windows 7 und höher. Die angegebenen Elemente sind Systemelemente.
            /// </summary>
            System = 0x00001000,

            /// <summary>
            /// Die angegebenen Elemente sind verschlüsselt und erfordern möglicherweise eine spezielle Präsentation.
            /// </summary>
            Encrypted = 0x00002000,

            /// <summary>
            /// <para>Der Zugriff auf das Element (über IStream oder andere Speicherschnittstellen) wird erwartet, dass es sich um einen langsamen Vorgang handelt. Anwendungen sollten den Zugriff auf Elemente vermeiden, die mit SFGAO_ISSLOW gekennzeichnet sind.</para>
            /// <para>
            /// [!Hinweis]
            /// Das Öffnen eines Datenstroms für ein Element ist in der Regel immer ein langsamer Vorgang. SFGAO_ISSLOW gibt an, dass es besonders langsam ist, z. B. bei langsamen Netzwerkverbindungen oder Offlinedateien (FILE_ATTRIBUTE_OFFLINE). Das Abfragen SFGAO_ISSLOW ist jedoch selbst ein langsamer Vorgang.Anwendungen sollten SFGAO_ISSLOW nur in einem Hintergrundthread abfragen.Anstelle eines Methodenaufrufs, der SFGAO_ISSLOW umfasst, kann eine alternative Methode verwendet werden, z. B.das Abrufen der PKEY_FileAttributes-Eigenschaft und das Testen auf FILE_ATTRIBUTE_OFFLINE.
            /// </para>
            /// </summary>
            IsSlow = 0x00004000,

            /// <summary>
            /// Die angegebenen Elemente werden als abgeblendet angezeigt und sind für den Benutzer nicht verfügbar.
            /// </summary>
            Ghosted = 0x00008000,

            /// <summary>
            /// Die angegebenen Elemente sind Verknüpfungen.
            /// </summary>
            Link = 0x00010000,

            /// <summary>
            /// Die angegebenen Objekte werden freigegeben.
            /// </summary>
            Share = 0x00020000,

            /// <summary>
            /// Die angegebenen Elemente sind schreibgeschützt. Im Fall von Ordnern bedeutet dies, dass in diesen Ordnern keine neuen Elemente erstellt werden können. Dies sollte nicht mit dem Verhalten verwechselt werden, das durch das FILE_ATTRIBUTE_READONLY Flag angegeben wird, das von IColumnProvider::GetItemData in einer SHCOLUMNDATA-Struktur abgerufen wird. FILE_ATTRIBUTE_READONLY hat für Win32-Dateisystemordner keine Bedeutung.
            /// </summary>
            Readonly = 0x00040000,

            /// <summary>
            /// Das Element ist ausgeblendet und sollte nicht angezeigt werden, es sei denn, die Option Ausgeblendete Dateien und Ordner anzeigen ist in den Ordnereinstellungen aktiviert.
            /// </summary>
            Hidden = 0x00080000,

            /// <summary>
            /// Darf nicht verwendet werden.
            /// </summary>
            DisplayAttrMask = IsSlow | Ghosted | Link | Share | Readonly | Hidden,

            /// <summary>
            /// Die Elemente sind nicht aufgezählte Elemente und sollten ausgeblendet werden. Sie werden nicht über einen Enumerator wie den zurückgegeben, der von der IShellFolder::EnumObjects-Methode erstellt wurde.
            /// </summary>
            NonEnumerated = 0x00100000,

            /// <summary>
            /// Die Elemente enthalten neue Inhalte, wie von der jeweiligen Anwendung definiert.
            /// </summary>
            NewContent = 0x00200000,

            /// <summary>
            /// Wird nicht unterstützt.
            /// </summary>
            CanMoniker = 0x00400000,

            /// <summary>
            /// Wird nicht unterstützt.
            /// </summary>
            HasStorage = 0x00400000,

            /// <summary>
            /// Gibt an, dass dem Element ein Stream zugeordnet ist. Auf diesen Stream kann über einen Aufruf von IShellFolder::BindToObject oder IShellItem::BindToHandler mit IID_IStream im riid-Parameter zugegriffen werden.
            /// </summary>
            Stream = 0x00400000,

            /// <summary>
            /// Auf untergeordnete Elemente dieses Elements kann über IStream oder IStorage zugegriffen werden. Diese untergeordneten Elemente werden mit SFGAO_STORAGE oder SFGAO_STREAM gekennzeichnet.
            /// </summary>
            StorageAncestor = 0x00800000,

            /// <summary>
            /// <para>Wenn als Eingabe angegeben, weist SFGAO_VALIDATE den Ordner an, zu überprüfen, ob die in einem Ordner oder Shellelementarray enthaltenen Elemente vorhanden sind. Wenn eines oder mehrere dieser Elemente nicht vorhanden sind, geben IShellFolder::GetAttributesOf und IShellItemArray::GetAttributes einen Fehlercode zurück. Dieses Flag wird nie als [out]-Wert zurückgegeben.</para>
            /// <para>Bei Verwendung mit dem Dateisystemordner weist SFGAO_VALIDATE den Ordner an, zwischengespeicherte Eigenschaften zu verwerfen, die von Clients von IShellFolder2::GetDetailsEx abgerufen wurden, die sich möglicherweise für die angegebenen Elemente angesammelt haben.</para>
            /// </summary>
            Validate = 0x01000000,

            /// <summary>
            /// Die angegebenen Elemente befinden sich auf Wechselmedien oder sind selbst Wechselmedien.
            /// </summary>
            Removable = 0x02000000,

            /// <summary>
            /// Die angegebenen Elemente werden komprimiert.
            /// </summary>
            Compressed = 0x04000000,

            /// <summary>
            /// Die angegebenen Elemente können in einem Webbrowser oder windows Explorer Frame gehostet werden.
            /// </summary>
            Browsable = 0x08000000,

            /// <summary>
            /// Die angegebenen Ordner sind entweder Dateisystemordner oder enthalten mindestens einen Nachfolger (untergeordnetes Kind, Enkelkind oder höher), bei dem es sich um einen Dateisystemordner (SFGAO_FILESYSTEM) handelt.
            /// </summary>
            FileSysAncestor = 0x10000000,

            /// <summary>
            /// Die angegebenen Elemente sind Ordner. Einige Elemente können sowohl mit SFGAO_STREAM als auch mit SFGAO_FOLDER gekennzeichnet werden, z. B. eine komprimierte Datei mit einer .zip Dateinamenerweiterung. Einige Anwendungen können dieses Flag enthalten, wenn sie auf Elemente testen, die sowohl Dateien als auch Container sind.
            /// </summary>
            Folder = 0x20000000,

            /// <summary>
            /// Die angegebenen Ordner oder Dateien sind Teil des Dateisystems (d. a. Es handelt sich um Dateien, Verzeichnisse oder Stammverzeichnisse). Es kann davon ausgegangen werden, dass die analysierten Namen der Elemente gültige Win32-Dateisystempfade sind. Diese Pfade können entweder AUF UNC- oder Laufwerkbuchstaben basieren.
            /// </summary>
            FileSystem = 0x40000000,

            /// <summary>
            /// Dieses Flag ist eine Maske für die Speicherfunktionsattribute: SFGAO_STORAGE, SFGAO_LINK, SFGAO_READONLY, SFGAO_STREAM, SFGAO_STORAGEANCESTOR, SFGAO_FILESYSANCESTOR, SFGAO_FOLDER und SFGAO_FILESYSTEM. Aufrufer verwenden diesen Wert normalerweise nicht.
            /// </summary>
            StorageCapMask = Storage | Link | Readonly | Stream | StorageAncestor | FileSysAncestor | Folder | FileSystem,

            /// <summary>
            /// <para>Die angegebenen Ordner verfügen über Unterordner. Das Attribut SFGAO_HASSUBFOLDER ist nur eine Empfehlung und kann von Shellordnerimplementierungen zurückgegeben werden, auch wenn sie keine Unterordner enthalten. Beachten Sie jedoch, dass umgekehrt – wenn SFGAO_HASSUBFOLDER nicht zurückgegeben wird – definitiv angibt, dass die Ordnerobjekte keine Unterordner aufweisen.</para>
            /// <para>Die Rückgabe von SFGAO_HASSUBFOLDER wird empfohlen, wenn ein erheblicher Zeitaufwand erforderlich ist, um zu bestimmen, ob Unterordner vorhanden sind. Beispielsweise gibt die Shell immer SFGAO_HASSUBFOLDER zurück, wenn sich ein Ordner auf einem Netzlaufwerk befindet.</para>
            /// </summary>
            HasSubFolder = 0x80000000,

            /// <summary>
            /// Dieses Flag ist eine Maske für Inhaltsattribute, derzeit nur SFGAO_HASSUBFOLDER. Aufrufer verwenden diesen Wert normalerweise nicht.
            /// </summary>
            ContentsMask = HasSubFolder,

            /// <summary>
            /// Maske, die von der PKEY_SFGAOFlags-Eigenschaft verwendet wird, um Attribute zu bestimmen, die als Ursache für langsame Berechnungen oder fehlenden Kontext gelten: SFGAO_ISSLOW, SFGAO_READONLY, SFGAO_HASSUBFOLDER und SFGAO_VALIDATE. Aufrufer verwenden diesen Wert normalerweise nicht.
            /// </summary>
            PKeySfgaoMask = IsSlow | Readonly | HasSubFolder | Validate
        }

        #endregion Shell32

        #region User32

        public enum WindowsMessages {
            WM_USER = 0x400,
            PBM_SETSTATE = WM_USER + 16,
            PBM_GETSTATE = WM_USER + 17
        }

        #endregion User32
    }
}
