using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BOMJ_Simulator.GameWindows;

namespace BOMJ_Simulator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Настройки окна
            this.Height = 550;
            this.Width = 1100;
            this.BorderThickness = new Thickness(2, 10, 2, 2);

            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtStartGame_Click(object sender, RoutedEventArgs e)
        {
            WPF_Misc.OpenNewWindow(this, new StartWindows(this));
        }

        private void BtExitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
