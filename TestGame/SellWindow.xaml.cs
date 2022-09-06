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
using Engine.Factories;
using Engine.Models;

namespace TestGame
{
    /// <summary>
    /// Interaction logic for SellWindow.xaml
    /// </summary>
    public partial class SellWindow : Window
    {
        int index;
        GameWindow gameWindow;
        Item placeholder;
        List<Item> heldItems;

        public SellWindow(GameWindow inComingWindow)
        {
            gameWindow = inComingWindow;
            InitializeComponent();
            setItems();
        }
        private void setItems()
        {
            heldItems = new List<Item>();
            playerGoldLabel.Content = gameWindow.currentPlayer.Gold;
            foreach (Item i in gameWindow.currentPlayer.Inventory)
            {
                    sellListBox.Items.Add(i);
                    heldItems.Add(i);
            }
        }


        private void sellButton_Click(object sender, RoutedEventArgs e)
        {
            if (sellListBox.SelectedIndex == -1)
                MessageBox.Show("No items selected!");
            else if(heldItems[index].SellPrice==0)
            {
                MessageBox.Show("They dont want it", "Nope");
            }
            else
            {
                if(heldItems[index] is Weapon)
                {
                    indexLabel.Content = index;
                    gameWindow.currentPlayer.Gold += heldItems[index].SellPrice;
                    playerGoldLabel.Content = gameWindow.currentPlayer.Gold;
                    gameWindow.weaponComboBox.Items.Remove(heldItems[index]);
                    gameWindow.currentPlayer.Inventory.Remove(heldItems[index]);
                    heldItems.RemoveAt(index);
                    sellListBox.Items.RemoveAt(index);
                    sellListBox.SelectedIndex = -1;
                }
                else if(heldItems[index] is Armor)
                {
                    indexLabel.Content = index;
                    gameWindow.currentPlayer.Gold += heldItems[index].SellPrice;
                    playerGoldLabel.Content = gameWindow.currentPlayer.Gold;
                    gameWindow.armorComboBox.Items.Remove(heldItems[index]);
                    gameWindow.currentPlayer.Inventory.Remove(heldItems[index]);
                    heldItems.RemoveAt(index);
                    sellListBox.Items.RemoveAt(index);
                    sellListBox.SelectedIndex = -1;
                }
                else if(heldItems[index] is Potion)
                {
                    indexLabel.Content = index;
                    gameWindow.currentPlayer.Gold += heldItems[index].SellPrice;
                    playerGoldLabel.Content = gameWindow.currentPlayer.Gold;
                    gameWindow.potionComboBox.Items.Remove(heldItems[index]);
                    gameWindow.currentPlayer.Inventory.Remove(heldItems[index]);
                    heldItems.RemoveAt(index);
                    sellListBox.Items.RemoveAt(index);
                    sellListBox.SelectedIndex = -1;
                }
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sellListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = sellListBox.SelectedIndex;
            if (sellListBox.SelectedIndex>=0)
            { 
                itemSellLabel.Content = heldItems[index].SellPrice;
            }
        }
    }
}
