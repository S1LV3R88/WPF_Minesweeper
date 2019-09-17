using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using System.Timers;

namespace WPF_Tests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Gameboard Game;

        public MainWindow()
        {
            InitializeComponent();

            StartNewGame(int.Parse(EingabeB.Text), int.Parse(EingabeH.Text), int.Parse(EingabeB.Text));

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Game.Show(ContentCanvas);
        }


        private void Button_ClickNeuesSpielSpiel (object sender, RoutedEventArgs e)
        {
            StartNewGame(int.Parse(EingabeB.Text), int.Parse(EingabeH.Text), int.Parse(EingabeM.Text));
        }
        private void Button_ClickNeuesSpielSpielNoob(object sender, RoutedEventArgs e)
        {
            StartNewGame(8,8,10);
        }
        private void Button_ClickNeuesSpielSpielNormal(object sender, RoutedEventArgs e)
        {
            StartNewGame(16, 16, 40);
        }
        private void Button_ClickNeuesSpielSpielHard(object sender, RoutedEventArgs e)
        {
            StartNewGame(30, 16, 99);
        }

        void StartNewGame(int width, int height, int bombCount)
        {
            Game = new Gameboard(width, height, bombCount);
            Game.Show(ContentCanvas);
            ((MainWindow)System.Windows.Application.Current.MainWindow).lbl_Status.Background = Brushes.Transparent;
            ((MainWindow)System.Windows.Application.Current.MainWindow).lbl_Status.Content = "laufendes Spiel!";

            if (Gametimer.IsRunning()) Gametimer.Stop();
            Gametimer.Init(lbl_Timer);
            Gametimer.Start();
        }

    }
}
