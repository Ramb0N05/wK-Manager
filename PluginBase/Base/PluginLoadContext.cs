using System;
using System.Reflection;
using System.Runtime.Loader;

namespace wK_Manager.Base
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver resolver;

        public PluginLoadContext(string pluginPath)
        {
            resolver = new AssemblyDependencyResolver(pluginPath);
        }

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            string? assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);

            return assemblyPath != null
                ? LoadFromAssemblyPath(assemblyPath)
                : null;
        }

        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            string? libraryPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);

            return libraryPath != null
                ? LoadUnmanagedDllFromPath(libraryPath)
                : IntPtr.Zero;
        }
    }
}
