using System;
using System.Threading;
using System.Windows;

using ControlzEx.Theming;

using Xlfdll.Diagnostics;

namespace QEMUManager
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!App.StartupMutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Another instance of QEMU Manager is already running.",
                    "QEMU Manager",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                Application.Current.Shutdown();
            }

            AppSettings.Create(); // Create settings file if it doesn't exist

            App.Settings = new AppSettings();
            App.L10n = new L10n();

            ThemeManager.Current.ChangeTheme(this, $"{App.Settings.ThemeAccent}.{App.Settings.ThemeColor}");

            if (App.Settings.SyncWindowsThemeMode)
            {
                ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
                ThemeManager.Current.SyncTheme();
            }
        }

        private static Mutex StartupMutex = new Mutex(true, "e0eb0873-8a92-4fa2-9a13-879a6f52d898");

        #region Facilities

        public static AppSettings Settings { get; private set; }
        public static L10n L10n { get; private set; }
        public static AssemblyMetadata Metadata => AssemblyMetadata.EntryAssemblyMetadata;

        #endregion
    }
}