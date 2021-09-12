using System.Windows;
using System.Windows.Controls;

using Xlfdll.Windows.Presentation;

namespace QEMUManager
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
        }

        private void AddMachineButton_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button)?.ShowDropMenu();
        }
    }
}