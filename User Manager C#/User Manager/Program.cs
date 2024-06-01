using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace User_Manager
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Check if the application is running as administrator
            if (!IsAdministrator())
            {
                // Create a new process start info
                var startInfo = new ProcessStartInfo(Application.ExecutablePath)
                {
                    Verb = "runas", // Specifies to run the process with administrative permissions
                    UseShellExecute = true
                };

                // Start the new process
                Process.Start(startInfo);

                // Shut down the current application
                Application.Exit();
            }
            else
            {
                // The application is running as administrator
                Application.Run(new Form1());
            }
        }

        // Function to check if the application is running as administrator
        private static bool IsAdministrator()
        {
            // Get the current Windows user
            WindowsIdentity user = WindowsIdentity.GetCurrent();

            // Get the current Windows user principal
            WindowsPrincipal principal = new WindowsPrincipal(user);

            // Return whether the user is part of the administrator role
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}