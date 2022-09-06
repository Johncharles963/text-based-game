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
using System.IO;
using Engine.Models;

namespace TestGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string playerChosenName = "";
        public string playerChosenSex = "Male";
        public Races playerChosenRace;
        public PlayerClasses playerChosenClass;
        public MainWindow()
        {
            InitializeComponent();
            mainMenuButton.Visibility = Visibility.Visible;
        }
        //Hides the current menu, make next menu visable
        private void mainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            newGameButton.Visibility= Visibility.Visible;
            loadGameButton.Visibility = Visibility.Visible;
            goBackButton.Visibility = Visibility.Visible;
            mainMenuButton.Visibility = Visibility.Hidden;
        }

        //Makes the character creation elements visible, hides the current menu
        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            selectRaceLabel.Visibility = Visibility.Visible;
            maleRadioButton.Visibility = Visibility.Visible;
            femaleRadioButton.Visibility = Visibility.Visible;
            raceList.Visibility = Visibility.Visible;
            selectPlayerClassLabel.Visibility = Visibility.Visible;
            playersClassList.Visibility = Visibility.Visible;
            createPlayerButton.Visibility = Visibility.Visible;
            userInputNameTextBox.Visibility = Visibility.Visible;
            playerCreationBackButton.Visibility = Visibility.Visible;
            playerClassTextBox.Visibility = Visibility.Visible;
            playerRaceTextBox.Visibility = Visibility.Visible;
          

            newGameButton.Visibility = Visibility.Hidden;
            loadGameButton.Visibility = Visibility.Hidden;
            goBackButton.Visibility = Visibility.Hidden;

        }
        //Hides current menu, goes back to Main menu
        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            newGameButton.Visibility = Visibility.Hidden;
            loadGameButton.Visibility = Visibility.Hidden;
            goBackButton.Visibility = Visibility.Hidden;
            mainMenuButton.Visibility = Visibility.Visible;
        }
        //Load data from save file
        private void loadGameButton_Click(object sender, RoutedEventArgs e)
        {
            TextReader textIn = null;
            try
            {   
                textIn = new StreamReader("PlayerSaveData");
                string newText = textIn.ReadLine();
            }
            catch
            {
                MessageBox.Show("There is no saved data", "Load Failed");
            }
            finally
            {
                if (textIn != null)
                    textIn.Close();
            }
        }
        //Sets the race and outputs race lore to textbox
        private void raceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (raceList.SelectedItem == humanListBoxItem)
            {

                playerChosenRace = Races.Human;
                playerRaceTextBox.Text = "Human race description goes here";
            }
            if (raceList.SelectedItem == pygmyListBoxItem)
            {
                playerChosenRace = Races.Pygmy;
                playerRaceTextBox.Text = "Pygmy race description goes here";
            }
            if (raceList.SelectedItem == dragonkinListBoxItem)
            {
                playerChosenRace = Races.Dragonkin;
                playerRaceTextBox.Text = "Dragonkin race description goes here";
            }
            if (raceList.SelectedItem == abyssianListBoxItem)
            {
                playerChosenRace = Races.Abyssian;
                playerRaceTextBox.Text = "Abyssian race description goes here";
            }
        }

        //Will open confirmation menu and sets the players name
        private void createPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage="";
            if(raceList.SelectedItem==null)
            {
                errorMessage = "Please select a race.";
            }
            if (playersClassList.SelectedItem == null)
            {
                if (errorMessage == "")
                    errorMessage = "Please select a class";
                else
                    errorMessage += "\nPlease select a class";
            }
            if(userInputNameTextBox.Text.Trim()=="")
            {
                if (errorMessage == "")
                    errorMessage = "Please enter a name";
                else
                    errorMessage += "\nPlease enter a name";
            }
            if (errorMessage != "")
                MessageBox.Show(errorMessage, "Error");
            else
            {
                playerChosenName = userInputNameTextBox.Text;
                CreateConfirmation confirmPlayerInfo= new CreateConfirmation(this);
                confirmPlayerInfo.ShowDialog();
            }
        }
        //Hides character creation menu and makes previous menu visable
        private void playerCreationBackButton_Click(object sender, RoutedEventArgs e)
        {
            selectRaceLabel.Visibility = Visibility.Hidden;
            maleRadioButton.Visibility = Visibility.Hidden;
            femaleRadioButton.Visibility = Visibility.Hidden;
            raceList.Visibility = Visibility.Hidden;
            selectPlayerClassLabel.Visibility = Visibility.Hidden;
            playersClassList.Visibility = Visibility.Hidden;
            createPlayerButton.Visibility = Visibility.Hidden;
            userInputNameTextBox.Visibility = Visibility.Hidden;
            playerCreationBackButton.Visibility = Visibility.Hidden;
            playerClassTextBox.Visibility = Visibility.Hidden;
            playerRaceTextBox.Visibility = Visibility.Hidden;

            newGameButton.Visibility = Visibility.Visible;
            loadGameButton.Visibility = Visibility.Visible;
            goBackButton.Visibility = Visibility.Visible;
        }
        //Sets the players class and outputs the class lore in textbox
        private void playerClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                if(playersClassList.SelectedItem==nomandListBoxItem)
            {
                playerChosenClass = PlayerClasses.Nomand;
                playerClassTextBox.Text = "Nomad description goes here";
            }
                if(playersClassList.SelectedItem==warriorListBoxItem)
            {
                playerChosenClass = PlayerClasses.Warrior;
                playerClassTextBox.Text = "Warrior description goes here";
            }
            if (playersClassList.SelectedItem == savantListBoxItem)
            {
                playerChosenClass = PlayerClasses.Savant;
                playerClassTextBox.Text = "Savant description goes here";
            }
            if (playersClassList.SelectedItem == neomanListBoxItem)
            {
                playerChosenClass = PlayerClasses.Neoman;
                playerClassTextBox.Text = "Neoman description goes here";
            }
        }

        private void maleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            playerChosenSex = "Male";
        }

        private void femaleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            playerChosenSex = "Female";
        }

    }
}
