using Engine.Factories;
using Engine.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace TestGame
{
    public enum ShopOpened
    {
        BlackSmith,
        Armorer,
        Mage,
        Alchemist
    }
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public ShopOpened selectedShop=ShopOpened.BlackSmith;
        public Player currentPlayer;
        public List<bool> questEncouter = new List<bool>()
        {
            false,
            false,
            false
        };
        public Encounter currentEncounter;     
        private Button playersCurretPos;
        private bool playerAtForest=false;
        private bool playerAtTown = false;
        private bool firstTimeAtForest=true;
        private MainWindow _mainWindow;
        private int i;
        DispatcherTimer timer;
        public GameWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            currentPlayer = new Player();
            DataContext = currentPlayer;
            currentPlayer.Name = _mainWindow.playerChosenName.Trim();
            currentPlayer.Race = _mainWindow.playerChosenRace;
            currentPlayer.PlayersClass = _mainWindow.playerChosenClass;
            _mainWindow.Close();
            currentPlayer.SetStats();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
             i = 0;
            InitializeComponent();
            setInitialInventory();
        }
        //Sets users inventory
        private void setInitialInventory()
        {
            currentPlayer.Inventory.Add(ItemFactory.GetItem(9));
            weaponComboBox.Items.Add(currentPlayer.Inventory[0]);
            currentPlayer.SpellBook.Add(SpellFactory.GetSpell(0));
            spellComboBox.Items.Add(currentPlayer.SpellBook[0]);
            currentPlayer.Inventory.Add(ItemFactory.GetItem(10));
            armorComboBox.Items.Add(currentPlayer.Inventory[1]);
            currentPlayer.Gold = 10000;
            currentPlayer.Inventory.Add(ItemFactory.GetItem(13));
            keyItemComboBox.Items.Add(currentPlayer.Inventory[2]);
            currentPlayer.SpellBook.Add(SpellFactory.GetSpell(3));
            spellComboBox.Items.Add(currentPlayer.SpellBook[1]);
        }
        private void timer_Tick(object sender, EventArgs e)
        { 
            switch (i)
            {
                case 1 :
                    mainTextBox.Text+="";
                    break;
                case 5:
                    mainTextBox.Text+= "\n Welcome to Quest Battler!";
                    break;
                case 10:
                    mainTextBox.Text += "\nUse the map to navigate areas.";
                    break;
                case 15:
                    mainTextBox.Text += "\nHead to the guild hall to pick up quests.";
                    break;
                case 20:
                    mainTextBox.Text += "\nYou can enter the shops to trade with them.";
                    break;
                case 25:
                    mainTextBox.Text += "\nTo exit an area you must go the the entrance.";
                    timer.Stop();
                    i = 0;
                    break;

            }
            mainTextBox.ScrollToEnd();
            i++;
        }
        private void setMapForForest()
        {
            playerAtForest = true;
            playersCurretPos = mapPos42;
            playersCurretPos.Visibility = Visibility.Visible;
            playersCurretPos.IsEnabled = true;
            playersCurretPos.Background = Brushes.Blue;
            mapPos32.IsEnabled = true;
            mapPos32.Visibility = Visibility.Visible;
            mapPos22.Visibility = Visibility.Visible;
            mapPos21.Visibility= Visibility.Visible;
            mapPos20.Visibility = Visibility.Visible;         
            mapPos11.Visibility = Visibility.Visible;
            mapPos01.Visibility = Visibility.Visible;         
            mapPos33.Visibility= Visibility.Visible;
            mapPos34.Visibility= Visibility.Visible;          
            mapPos24.Visibility = Visibility.Visible;
            mapPos14.Visibility= Visibility.Visible;
            mapPos04.Visibility = Visibility.Visible;
            if(questEncouter[0])
            {
                mapPos01.Background = Brushes.Green;
            }
            if (questEncouter[1])
            {
                mapPos20.Background = Brushes.Green;
            }
            if (questEncouter[2])
            {
                mapPos04.Background = Brushes.Green;
            }
        }

        private void setMapForTown()
        {
            playerAtTown = true;
            playersCurretPos = mapPos42;
            playersCurretPos.Visibility = Visibility.Visible;
            changeBackgroundColor("Blue");
            mapPos32.Visibility = Visibility.Visible;
            mapPos32.IsEnabled = true;
            mapPos22.Visibility = Visibility.Visible;
            mapPos12.Visibility = Visibility.Visible;
            mapPos31.Visibility = Visibility.Visible;
            mapPos33.Visibility = Visibility.Visible;
            mapPos21.Visibility = Visibility.Visible;
            mapPos23.Visibility = Visibility.Visible;
            mapPos31.Background = Brushes.Coral;
            mapPos33.Background = Brushes.Coral;
            mapPos21.Background = Brushes.Coral;
            mapPos23.Background = Brushes.Coral;
            mapPos12.Background = Brushes.Coral;
        }
        private void resetMap()
        {
            foreach(Button b in mapGrid.Children)
            {
                b.Visibility = Visibility.Hidden;
                b.Background = Brushes.White;
                disableAllMapButtons();
                playerAtTown = false;
                playerAtForest = false;
            }
        }
        //Used to change color of players current position in the map
        private void changeBackgroundColor(string i)
        {
            switch(i)
            {
                case "White":
                    playersCurretPos.Background = Brushes.White;
                    disableAllMapButtons();
                    break;
                case "Blue":
                    playersCurretPos.Background = Brushes.Blue;
                    playersCurretPos.IsEnabled = true;
                    break;
                case "Green":
                    playersCurretPos.Background = Brushes.Green;
                    break;
                case "Enable":
                    playersCurretPos.IsEnabled = true;
                    break;
            }    
            if(playerAtTown && i=="White")
            {
                if(playersCurretPos==mapPos21|| playersCurretPos == mapPos31|| playersCurretPos == mapPos23|| playersCurretPos == mapPos33||playersCurretPos==mapPos12)
                {
                    playersCurretPos.Background = Brushes.Coral;
                }
            }
        }

        private void disableAllMapButtons()
        {
            foreach(Button i in mapGrid.Children)
            {
                i.IsEnabled = false;
            }
        }

        private void changePlayerPos(Button playerNextPos)
        {
            changeBackgroundColor("White");
            playersCurretPos.IsEnabled = true;
            playersCurretPos = playerNextPos;
            changeBackgroundColor("Blue");
        }
        private void changePlayerPos(Button playerNextPos, Button adjButton, Button adjButton2)
        {
            changeBackgroundColor("White");
            playersCurretPos = playerNextPos;
            changeBackgroundColor("Blue");
            adjButton.IsEnabled = true;
            adjButton2.IsEnabled = true;
        }
        private void changePlayerPos(Button playerNextPos, Button adjButton, Button adjButton2, Button adjButton3)
        {
            changeBackgroundColor("White");
            playersCurretPos = playerNextPos;
            changeBackgroundColor("Blue");
            adjButton.IsEnabled = true;
            adjButton2.IsEnabled = true;
            adjButton3.IsEnabled = true;
        }
        private void changePlayerPos(Button playerNextPos, Button adjButton, Button adjButton2, Button adjButton3, Button adjButton4)
        {
            changeBackgroundColor("White");
            playersCurretPos = playerNextPos;
            changeBackgroundColor("Blue");
            adjButton.IsEnabled = true;
            adjButton2.IsEnabled = true;
            adjButton3.IsEnabled = true;
            adjButton4.IsEnabled = true;
        }
        private void changeLocationLabel(string location)
        {
            characterPosLabel.Content = location;
        }

        private void mapPos40_Click(object sender, RoutedEventArgs e)
        {
    
        }

        private void mapPos41_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mapPos42_Click(object sender, RoutedEventArgs e)
        {
            if(playersCurretPos!=mapPos42 && playerAtForest)
            {
                changePlayerPos(mapPos42);
            }
            if (playersCurretPos != mapPos42 && playerAtTown)
            {
                changePlayerPos(mapPos42);
                changeLocationLabel("Town Entrance");
            }
        }

        private void mapPos43_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mapPos44_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mapPos30_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mapPos32_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos32 && playerAtForest)
            {
                changePlayerPos(mapPos32, mapPos22, mapPos33, mapPos42);
            }
            if (playersCurretPos != mapPos32 && playerAtTown)
            {
                changePlayerPos(mapPos32, mapPos31, mapPos33, mapPos42, mapPos22);
                changeLocationLabel("Main Street");
            }

        }

        private void mapPos31_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos31 && playerAtTown)
            {
                changePlayerPos(mapPos31, mapPos21, mapPos32);
                changeLocationLabel("Welyns Weapons");
            }
        }

        private void mapPos33_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos33 && playerAtForest)
            {
                changePlayerPos(mapPos33, mapPos32, mapPos34);
            }
            if (playersCurretPos != mapPos33 && playerAtTown)
            {
                changePlayerPos(mapPos33, mapPos32, mapPos23);
                changeLocationLabel("Pipers Potions");
            }
        }

        private void mapPos34_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos34 && playerAtForest)
            {
                changePlayerPos(mapPos34, mapPos33, mapPos24);
            }
        }

        private void mapPos20_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos20 && playerAtForest)
            {
                changePlayerPos(mapPos20);
            }
        }

        private void mapPos21_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos21 && playerAtForest)
            {
                changePlayerPos(mapPos21, mapPos20, mapPos22, mapPos11);
            }
            if (playersCurretPos != mapPos21 && playerAtTown)
            {
                changePlayerPos(mapPos21, mapPos22, mapPos31);
                changeLocationLabel("Arnies Armor");
            }
        }

        private void mapPos22_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos22 && playerAtForest)
            {
                changePlayerPos(mapPos22, mapPos21, mapPos32);
            }
            if (playersCurretPos != mapPos22 && playerAtTown)
            {
                changePlayerPos(mapPos22, mapPos12, mapPos32, mapPos21, mapPos23);
                changeLocationLabel("Main Street");
            }
        }

        private void mapPos23_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos23 && playerAtTown)
            {
                changePlayerPos(mapPos23, mapPos22, mapPos33);
                changeLocationLabel("Mages Hand");
            }
        }

        private void mapPos24_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos24 && playerAtForest)
            {
                changePlayerPos(mapPos24, mapPos14, mapPos34);
            }
        }

        private void mapPos10_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mapPos11_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos11 && playerAtForest)
            {
                changePlayerPos(mapPos11, mapPos01, mapPos21);
            }
        }

        private void mapPos12_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos12 && playerAtTown)
            {
                changePlayerPos(mapPos12);
                changeLocationLabel("Guild Hall");
            }
        }

        private void mapPos13_Click(object sender, RoutedEventArgs e)
        {
        }

        private void mapPos14_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos14 && playerAtForest)
            {
                changePlayerPos(mapPos14, mapPos24, mapPos04);
            }
        }

        private void mapPos00_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mapPos01_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos01 && playerAtForest)
            {
                changePlayerPos(mapPos01);
            }
        }

        private void mapPos02_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mapPos03_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mapPos04_Click(object sender, RoutedEventArgs e)
        {
            if (playersCurretPos != mapPos04 && playerAtForest)
            {
                changePlayerPos(mapPos04);
            }
        }
         private void weaponComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                userInputTextBox.Clear();
            }
        }
        private void enterExitButton_Click(object sender, RoutedEventArgs e)
        {
            if(playersCurretPos==mapPos23 && playerAtTown)
            {
                selectedShop = ShopOpened.Mage;
                ShopWindow shop = new ShopWindow(this);
                shop.ShowDialog();
            }
            else if (playersCurretPos == mapPos31 && playerAtTown)
            {
                selectedShop = ShopOpened.BlackSmith;
                ShopWindow shop = new ShopWindow(this);
                shop.ShowDialog();
            }
            else if (playersCurretPos == mapPos21 && playerAtTown)
            {
                selectedShop = ShopOpened.Armorer;
                ShopWindow shop = new ShopWindow(this);
                shop.ShowDialog();
            }
            else if (playersCurretPos == mapPos33 && playerAtTown)
            {
                selectedShop = ShopOpened.Alchemist;
                ShopWindow shop = new ShopWindow(this);
                shop.ShowDialog();
            }
            else if(playersCurretPos==mapPos12 && playerAtTown)
            {
                QuestWindow questBoard = new QuestWindow(this);
                questBoard.ShowDialog();
            }
        }
        private void lookButton_Click(object sender, RoutedEventArgs e)
        {
            currentEncounter= EncounterFactory.GetEncounter(0);
            BattleWindow battle = new BattleWindow(this);
            battle.ShowDialog();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            questEncouter[0] = false;
            questEncouter[1] = false;
            currentEncounter = EncounterFactory.GetEncounter(1);
            BattleWindow battle = new BattleWindow(this);
            battle.ShowDialog();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            resetMap();
            setMapForTown();
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            resetMap();
            setMapForForest();
        }
    }
}
