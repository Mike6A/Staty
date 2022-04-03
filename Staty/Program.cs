using System;
using System.Runtime.InteropServices;
using Autofac;

namespace Staty
{
    internal class Program
    {
        private static readonly IContainer Container = RegisterDependency.Register(new ContainerBuilder());

        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            if (handle != IntPtr.Zero)
            {
                //odstranění resize okna konzole
                var sysMenu = GetSystemMenu(handle, false);
                DeleteMenu(sysMenu, 0xF000, 0x0);
            }

            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<App>();
                app.Run();
            }
        }

        #region Native Mehods

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        #endregion
    }
}
