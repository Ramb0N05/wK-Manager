using SharpRambo.ExtensionsLib;
using System.Linq;
using System.Reflection;
using wK_Manager.Base;

namespace wK_Manager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}