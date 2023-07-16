using SharpRambo.ExtensionsLib;
using System.Security.AccessControl;
using System.Security.Principal;

namespace wK_Manager.Base.Extensions {

    public static class SystemIOExtensions {

        public static async Task<bool> CheckFileSystemRight(this DirectoryInfo srcPath, FileSystemRights fileSystemRight = FileSystemRights.WriteData) {
            if (!srcPath.Exists)
                return false;

            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal currentPrincipal = new(identity);
            string currentSid = identity.User?.Value ?? throw new NullReferenceException(nameof(currentSid));

            if (currentPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
                currentSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null).Value;

            DirectorySecurity srcAcl = srcPath.GetAccessControl(AccessControlSections.Access);
            AuthorizationRuleCollection srcRules = srcAcl.GetAccessRules(true, true, typeof(SecurityIdentifier));
            bool checkRight = false;

            await srcRules.Cast<AuthorizationRule>().ForEachAsync(async (rule) => {
                if (rule.IdentityReference.Value.Equals(currentSid, StringComparison.CurrentCultureIgnoreCase)) {
                    FileSystemAccessRule filesystemAccessRule = (FileSystemAccessRule)rule;

                    if ((filesystemAccessRule.FileSystemRights & fileSystemRight) > 0 && filesystemAccessRule.AccessControlType != AccessControlType.Deny)
                        checkRight = true;
                }

                await Task.CompletedTask;
            });

            return checkRight;
        }

        public static async Task CopyTo(this DirectoryInfo srcPath, string destinationPath) {
            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);

            await srcPath.GetDirectories("*", SearchOption.AllDirectories).ForEachAsync(async (srcInfo) => {
                Directory.CreateDirectory(Path.Combine(destinationPath, srcInfo.Name));
                await Task.CompletedTask;
            });

            await srcPath.GetFiles("*", SearchOption.AllDirectories).ForEachAsync(async (srcInfo) => {
                File.Copy(srcInfo.FullName, Path.Combine(destinationPath, srcInfo.Name), true);
                await Task.CompletedTask;
            });
        }

        public static async Task DeleteContents(this DirectoryInfo srcPath) {
            if (srcPath.Exists) {
                await srcPath.GetDirectories("*", SearchOption.AllDirectories).ForEachAsync(async (srcInfo) => {
                    Directory.Delete(Path.Combine(srcPath.FullName, srcInfo.Name), true);
                    await Task.CompletedTask;
                });

                await srcPath.GetFiles("*", SearchOption.AllDirectories).ForEachAsync(async (srcInfo) => {
                    File.Delete(Path.Combine(srcPath.FullName, srcInfo.Name));
                    await Task.CompletedTask;
                });
            }
        }

        public static async Task DeleteDirectories(this DirectoryInfo srcPath, bool recursive = true) {
            if (srcPath.Exists) {
                await srcPath.GetDirectories("*", SearchOption.AllDirectories).ForEachAsync(async (srcInfo) => {
                    Directory.Delete(Path.Combine(srcPath.FullName, srcInfo.Name), recursive);
                    await Task.CompletedTask;
                });
            }
        }
    }
}
