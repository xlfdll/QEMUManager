﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

using Xlfdll.Localization;

using QEMUManager.Properties;

namespace QEMUManager
{
    public class L10n
    {
        public L10n()
        {
            this.DetectedCultures = new ObservableCollection<CultureInfo>();
            this.LanguagePackProcessor = new TextLanguagePackProcessor();

            this.Initialize();
        }

        private void Initialize()
        {
            this.RefreshAvailableLanguages();

            if (this.DetectedCultures.Count > 0
                && this.DetectedCultures.Where(c => c.Name.ToLower() == App.Settings.DisplayLanguage).Count() > 0)
            {
                this.LoadLanguagePack(App.Settings.DisplayLanguage);
            }
        }

        public CultureInfo CurrentCulture { get; private set; }
        public LanguageDictionary CurrentLanguageDictionary { get; private set; }
        public ObservableCollection<CultureInfo> DetectedCultures { get; }

        public void RefreshAvailableLanguages()
        {
            if (Directory.Exists(L10n.LanguagePackDirectoryPath))
            {
                String[] results = Directory.GetFiles(L10n.LanguagePackDirectoryPath,
                    $"{L10n.ProductName}.*.{Resources.LanguagePackFileExtension}");

                if (results.Length > 0)
                {
                    this.DetectedCultures.Clear();

                    for (Int32 i = 0; i < results.Length; i++)
                    {
                        String lang = Path.GetFileName(results[i])
                            .Replace($"{L10n.ProductName}.", String.Empty)
                            .Replace($".{Resources.LanguagePackFileExtension}", String.Empty);

                        this.DetectedCultures.Add(new CultureInfo(lang));
                    }
                }
            }
        }

        public void LoadLanguagePack(String lang)
        {
            this.CurrentLanguageDictionary = this.LanguagePackProcessor.LoadLanguagePack
                (L10n.GetLanguagePackFilePath(L10n.ProductName, lang));

            this.CurrentCulture = new CultureInfo(lang);
        }

        private ILanguagePackProcessor LanguagePackProcessor { get; }

        #region Static Members

        public static String GetLanguagePackFilePath(String productName, String langName)
        {
            return Path.Combine(Resources.LanguagePackDirectoryName,
                $"{productName}.{langName}.{Resources.LanguagePackFileExtension}");
        }

        public static String LanguagePackDirectoryPath => Path.Combine
            (Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
            Resources.LanguagePackDirectoryName);

        private static String ProductName => typeof(App).Namespace;

        #endregion
    }
}