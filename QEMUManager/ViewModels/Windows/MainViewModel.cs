using System;
using System.Collections.ObjectModel;
using System.Windows;

using Xlfdll.Windows.Presentation;
using Xlfdll.Windows.Presentation.Dialogs;

using QEMUManager.Models;
using QEMUManager.Properties;

namespace QEMUManager
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            this.Machines = new ObservableCollection<QEMUMachine>();
        }

        #region Data

        public ObservableCollection<QEMUMachine> Machines { get; }
        public MachineInfoViewModel SelectedMachineViewModel { get; private set; }

        public QEMUMachine SelectedMachine
        {
            get
            {
                return this.SelectedMachineViewModel?.Machine;
            }
            set
            {
                this.SelectedMachineViewModel = new MachineInfoViewModel(value);

                OnPropertyChanged(nameof(this.SelectedMachineViewModel));
            }
        }

        private String _status;
        private Boolean _isBusy;

        public String Status
        {
            get => _status;
            set => SetField(ref _status, value);
        }
        public Boolean IsBusy
        {
            get => _isBusy;
            set => SetField(ref _isBusy, value);
        }

        #endregion

        #region Commands

        public RelayCommand<Object> AboutCommand => new RelayCommand<Object>
        (
            delegate
            {
                AboutWindow aboutWindow = new AboutWindow
                    (App.Current.MainWindow,
                    App.Metadata,
                    new ApplicationPackUri($"Images\\{typeof(App).Namespace + ".png"}"),
                    Resources.ExternalSources);

                aboutWindow.ShowDialog();
            }
        );

        public RelayCommand<MainViewModel> NewMachineCommand => new RelayCommand<MainViewModel>
        (
            (MainViewModel mainViewModel) =>
            {
                NewMachineWizardViewModel newMachineWizardViewModel = new NewMachineWizardViewModel(App.QEMUInstallation);

                WizardWindow wizardWindow = new WizardWindow
                    (new Uri[]
                    {
                        new ApplicationPackUri("Views/Pages/NewMachineWizard/NewMachineWelcomePage.xaml")
                    })
                {
                    Owner = App.Current.MainWindow,
                    DataContext = newMachineWizardViewModel
                };

                if (wizardWindow.ShowDialog() == true)
                {

                }
            }
        );

        #endregion
    }
}