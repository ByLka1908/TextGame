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
using System.Windows.Shapes;

namespace BOMJ_Simulator.GameWindows
{
    /// <summary>
    /// Логика взаимодействия для LosingWindows.xaml
    /// </summary>
    public partial class LosingWindows : Window
    {
        StartWindows startWindow;
        public LosingWindows(StartWindows startWindow)
        {
            InitializeComponent();
            this.startWindow = startWindow;
        }
        void BtnRestartGame_Click(object sender, RoutedEventArgs e)
        {
            startWindow.Game.BreakCurrentTime();
            startWindow.InitGame();
            startWindow.InitPerson();
            this.Close();
        }

        void BtnEndGame_Click(object sender, RoutedEventArgs e)
        {
            WPF_Misc.OpenNewWindow(this, new MainWindow(), false, false);

            // Вместе с этим окном закроется и окно паузы, т.к. это окно-родитель
            startWindow.MainWindow.Close();
            startWindow.Close();
        }
    }
}