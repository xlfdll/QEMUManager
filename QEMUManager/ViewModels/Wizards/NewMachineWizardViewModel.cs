using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using QEMUManager.Models;

namespace QEMUManager
{
    public class NewMachineWizardViewModel : ViewModelBase
    {
        public NewMachineWizardViewModel()
        {
            this.Architectures = new ObservableCollection<QEMUArchitecture>();

            this.LoadArchitectures();
        }

        public ObservableCollection<QEMUArchitecture> Architectures { get; }

        private void LoadArchitectures()
        {
            this.Architectures.Clear();

            foreach (KeyValuePair<String, QEMUArchitecture> item in QEMUChecker.SupportedArchitectures)
            {
                if (item.Value != QEMUChecker.UnsupportedArchitecture)
                {
                    this.Architectures.Add(item.Value);
                }
            }
        }
    }
}