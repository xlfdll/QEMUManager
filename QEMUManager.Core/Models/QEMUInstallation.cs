using System;
using System.Collections.Generic;

namespace QEMUManager.Models
{
    public class QEMUInstallation
    {
        internal QEMUInstallation
            (IReadOnlyDictionary<String, QEMUArchitecture> availableArchitectures,
            Boolean isImageToolAvailable)
        {
            this.IsImageToolAvailable = isImageToolAvailable;
            this.AvailableArchitectures = availableArchitectures;
        }

        public Boolean IsImageToolAvailable { get; }
        public IReadOnlyDictionary<String, QEMUArchitecture> AvailableArchitectures { get; }
    }
}