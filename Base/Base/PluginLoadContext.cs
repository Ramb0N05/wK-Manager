using System.Reflection;
using System.Runtime.Loader;

namespace wK_Manager.Base {
    public class PlugInLoadContext : AssemblyLoadContext {
        private readonly AssemblyDependencyResolver resolver;

        public PlugInLoadContext(string plugInPath) {
            resolver = new AssemblyDependencyResolver(plugInPath);
        }

        protected override Assembly? Load(AssemblyName assemblyName) {
            string? assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);

            return assemblyPath != null
                ? LoadFromAssemblyPath(assemblyPath)
                : null;
        }

        protected override nint LoadUnmanagedDll(string unmanagedDllName) {
            string? libraryPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);

            return libraryPath != null
                ? LoadUnmanagedDllFromPath(libraryPath)
                : IntPtr.Zero;
        }
    }
}
