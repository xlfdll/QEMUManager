using System;
using System.Collections.Generic;
using System.IO;

using QEMUManager.Helpers;
using QEMUManager.Models;

namespace QEMUManager
{
    public static class QEMUChecker
    {
        static QEMUChecker()
        {
            QEMUChecker.SupportedArchitectures = IOHelper.LoadSupportedArchitectures();
        }

        public static IReadOnlyDictionary<String, QEMUArchitecture> SupportedArchitectures { get; }
        public static QEMUArchitecture UnsupportedArchitecture
            => QEMUChecker.SupportedArchitectures["unsupported"];

        public static QEMUInstallation Check(String qemuFolderPath)
        {
            Boolean isImageToolAvailable = File.Exists(Path.Combine(qemuFolderPath, "qemu-img.exe"));
            Dictionary<String, QEMUArchitecture> availableArchitectures = new Dictionary<String, QEMUArchitecture>();

            foreach (String file in Directory.GetFiles(qemuFolderPath, "qemu-system-*.exe", SearchOption.TopDirectoryOnly))
            {
                String systemCode = Path.GetFileNameWithoutExtension(file).Replace("qemu-system-", String.Empty);

                systemCode = systemCode.EndsWith("w") ? systemCode.TrimEnd('w') : systemCode;

                if (QEMUChecker.SupportedArchitectures.ContainsKey(systemCode)
                    && !availableArchitectures.ContainsKey(systemCode))
                {
                    availableArchitectures.Add(systemCode, QEMUChecker.SupportedArchitectures[systemCode]);
                }
                else if (!QEMUChecker.SupportedArchitectures.ContainsKey(systemCode))
                {
                    availableArchitectures.Add(systemCode, QEMUChecker.UnsupportedArchitecture);
                }
            }

            return new QEMUInstallation(availableArchitectures, isImageToolAvailable);
        }
    }
}