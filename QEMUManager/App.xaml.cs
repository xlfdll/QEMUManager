using System;
using System.IO;
using System.Threading;
using System.Windows;

using ControlzEx.Theming;

using QEMUManager.Models;

using Xlfdll.Diagnostics;
using Xlfdll.Localization;

namespace QEMUManager
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppSettings.Create(); // Create settings file if it doesn't exist

            App.Settings = AppSettings.Load();
            App.L10n = new L10n();

            if (!App.StartupMutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show(App.L10n.CurrentLanguageDictionary.GetTranslatedString
                    ("App.AnotherInstanceRunningWarning",
                    "Another instance of QEMU Manager is already running."),
                    App.L10n.CurrentLanguageDictionary.GetTranslatedString
                    ("App.Title", "QEMU Manager"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                Application.Current.Shutdown();
            }

            try
            {
                App.QEMUInstallation = QEMUChecker.Check(App.Settings.QEMULocation);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(String.Format(App.L10n.CurrentLanguageDictionary.GetTranslatedString
                    ("App.QEMUInstallationNotFoundWarning",
                    "The location of QEMU installation in settings is not found.{0}Please set the correct location in Options dialog."),
                    Environment.NewLine),
                    App.L10n.CurrentLanguageDictionary.GetTranslatedString
                    ("App.Title", "QEMU Manager"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                // TODO: Open Options in MainWindow
            }

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

        public static QEMUInstallation QEMUInstallation { get; internal set; }

        #endregion
    }
}