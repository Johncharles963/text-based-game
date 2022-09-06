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

namespace TestGame
{
    /// <summary>
    /// Interaction logic for CreateConfirmation.xaml
    /// </summary>
    public partial class CreateConfirmation : Window
    {
        private MainWindow mainWindow;
        public CreateConfirmation(MainWindow inWindow)
        {
            mainWindow = inWindow;
            InitializeComponent();
            confirmationTextBlock.Text = $"Race: {mainWindow.playerChosenRace}\nClass: {mainWindow.playerChosenClass}\nSex: {mainWindow.playerChosenSex}\nName: {mainWindow.playerChosenName}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(mainWindow);
            gameWindow.Show();
            this.Close();
        }
    }
}
