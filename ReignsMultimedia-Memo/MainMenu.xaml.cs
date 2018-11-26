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

namespace ReignsMultimedia_Memo {
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl {
        public MainMenu() {
            InitializeComponent();
        }

        private void BtnStartGame_Click(object sender, RoutedEventArgs e) {
            MainWindow.gameState = MainWindow.GameState.Gameplay;
        }

        private void BtnExitGame_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}
