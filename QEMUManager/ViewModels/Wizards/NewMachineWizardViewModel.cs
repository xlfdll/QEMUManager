using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using QEMUManager.Models;

namespace QEMUManager
{
    public class NewMachineWizardViewModel : ViewModelBase
    {
        public NewMachineWizardViewModel(QEMUInstallation qemuInstallation)
        {
            this.Architectures = new ObservableCollection<QEMUArchitecture>();
            this.QEMUInstallation = qemuInstallation;

            this.LoadArchitectures();
        }

        #region Wizard States

        public Boolean BeforeGoBack => true;
        public Boolean BeforeGoNext => true;
        public Boolean BeforeCancel => true;
        public Boolean CanGoBack => true;
        public Boolean CanGoNext
            => !String.IsNullOrEmpty(this.MachineName);
        public Boolean CanCancel => true;

        #endregion

        private Int32 _selectedArchitectureIndex;
        private String _machineName;

        public Int32 SelectedArchitectureIndex
        {
            get => _selectedArchitectureIndex;
            set => SetField(ref _selectedArchitectureIndex, value);
        }
        public String MachineName
        {
            get
            {
                return _machineName;
            }
            set
            {
                SetField(ref _machineName, value);

                OnPropertyChanged(nameof(this.CanGoNext));
            }
        }

        public QEMUInstallation QEMUInstallation { get; }
        public ObservableCollection<QEMUArchitecture> Architectures { get; }

        private void LoadArchitectures()
        {
            QEMUArchitecture initialArchitecture = null;

            this.Architectures.Clear();

            foreach (KeyValuePair<String, QEMUArchitecture> item in this.QEMUInstallation.AvailableArchitectures)
            {
                this.Architectures.Add(item.Value);

                if (item.Key == "i386")
                {
                    initialArchitecture = item.Value;
                }
            }

            if (initialArchitecture != null)
            {
                this.SelectedArchitectureIndex = this.Architectures.IndexOf(initialArchitecture);

            }
        }
    }
}