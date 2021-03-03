using QEMUManager.Models;

namespace QEMUManager
{
    public class MachineInfoViewModel : ViewModelBase
    {
        public MachineInfoViewModel(QEMUMachine machine)
        {
            this.Machine = machine;
        }

        public QEMUMachine Machine { get; }
    }
}