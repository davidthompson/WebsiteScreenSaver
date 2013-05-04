using System;
using System.Windows.Forms;

namespace DSTMedia.WebsiteScreenSaver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Check for any passed arguments
            if (args.Length > 0)
            {
                switch (args[0].ToLower().Trim().Substring(0, 2))
                {
                    // Preview the screen saver
                    case "/p":
                        // args[1] is the handle to the preview window
                        var screenSaverForm = new ScreenSaverForm(new IntPtr(long.Parse(args[1])));
                        screenSaverForm.ShowDialog();
                        break;

                    // Show the screen saver
                    case "/s":
                        RunScreensaver();
                        break;

                    // Configure the screesaver's settings
                    case "/c":
                        // Show the settings form
                        var settingsForm = new SettingsForm();
                        settingsForm.ShowDialog();
                        break;

                    // Show the screen saver
                    default:
                        RunScreensaver();
                        break;
                }
            }
            else
            {
                // No arguments were passed so we show the screensaver anyway
                RunScreensaver();
            }
        }

        private static void RunScreensaver()
        {
            var screenSaverForm = new ScreenSaverForm();
            screenSaverForm.ShowDialog();
        }
    }
}
