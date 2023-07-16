using DotNet.Basics.SevenZip;
using SharpRambo.ExtensionsLib;
using wK_Manager.Base.Exceptions;

namespace wK_Manager.Base.Extensions {

    public static class SevenZipExtensions {

        public static async Task<bool> ExtractAtLocalPath(this SevenZipExe sevenZipExe, string filePath, DirectoryInfo extractDirectory) => await ExtractAtLocalPath(sevenZipExe, filePath, extractDirectory, false);

        public static async Task<bool> ExtractAtLocalPath(this SevenZipExe sevenZipExe, string filePath, DirectoryInfo extractDirectory, bool deleteIfExists) {
            if (extractDirectory.Exists) {
                if (deleteIfExists)
                    extractDirectory.Delete(true);
                else
                    throw new DirectoryAlreadyExistsException();
            }

            if (!filePath.IsNull() && File.Exists(filePath)) {
                int result = await Task.Run(() => sevenZipExe.ExtractToDirectory(filePath, extractDirectory.FullName));

                if (result == 0) {
                    File.Delete(filePath);
                    return true;
                }
            }

            return false;
        }
    }
}
