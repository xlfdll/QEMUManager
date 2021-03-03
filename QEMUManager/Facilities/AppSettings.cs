using System;
using System.IO;

using Newtonsoft.Json;

using Xlfdll.Text;

using QEMUManager.Properties;

namespace QEMUManager
{
    public class AppSettings
    {
        static AppSettings()
        {
            AppSettings.Location = Environment.CurrentDirectory;

            try
            {
                Directory.GetAccessControl(AppSettings.Location);
            }
            catch (UnauthorizedAccessException)
            {
                AppSettings.Location = Path.Combine
                    (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"Xlfdll\QEMUManager");

                if (!Directory.Exists(AppSettings.Location))
                {
                    Directory.CreateDirectory(AppSettings.Location);
                }
            }
        }

        #region Settings

        public String QEMULocation { get; set; }
            = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "qemu");
        public String MachineLocation { get; set; }
            = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "QEMU VMs");
        public String DisplayLanguage { get; set; } = "en-us";

        #region Theme Settings

        // Accent: Light, Dark
        public String ThemeAccent { get; set; } = "Light";
        // Color:
        // Red, Green, Blue,
        // Purple, Orange, Lime,
        // Emerald, Teal, Cyan, Cobalt,
        // Indigo, Violet, Pink, Magenta,
        // Crimson, Amber, Yellow, Brown,
        // Olive, Steel, Mauve, Taupe, Sienna
        public String ThemeColor { get; set; } = "Cyan";
        public Boolean SyncWindowsThemeMode { get; set; } = true;

        #endregion

        #endregion

        #region I/O Methods

        public void Save()
        {
            this.Save(AppSettings.FullPath);
        }

        public void Save(String fileName)
        {
            String contents = JsonConvert.SerializeObject(this, Formatting.Indented);

            File.WriteAllText(fileName, contents, AdditionalEncodings.UTF8WithoutBOM);
        }

        public static void Create()
        {
            if (!File.Exists(AppSettings.FullPath))
            {
                AppSettings appSettings = new AppSettings();

                appSettings.Save();
            }
        }

        public static AppSettings Load()
        {
            return AppSettings.Load(AppSettings.FullPath);
        }

        public static AppSettings Load(String fileName)
        {
            String contents = File.ReadAllText(fileName, AdditionalEncodings.UTF8WithoutBOM);

            return JsonConvert.DeserializeObject<AppSettings>(contents);
        }

        #endregion

        private static String Location { get; }
        private static String FullPath => Path.Combine(AppSettings.Location, Resources.AppSettingsFileName);
    }
}