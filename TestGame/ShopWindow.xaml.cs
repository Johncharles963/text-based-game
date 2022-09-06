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
using Engine.Models;
using Engine.Factories;

namespace TestGame
{
    /// <summary>
    /// Interaction logic for ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        GameWindow gameWindow;
        ShopOpened shop;
        private int currentItemPrice;
        public ShopWindow(GameWindow inComingWindow)
        {
            gameWindow = inComingWindow;
            InitializeComponent();
            shop = gameWindow.selectedShop;
            setShop();
        }
        private void setShop()
        {
            playersGoldLabel.Content = gameWindow.currentPlayer.Gold; 
            switch (shop)
            {
                case ShopOpened.BlackSmith:

                    shopListBox.Items.Add(ItemFactory.GetItem(0));
                    shopListBox.Items.Add(ItemFactory.GetItem(1));
                    shopListBox.Items.Add(ItemFactory.GetItem(2));
                    break;
                case ShopOpened.Alchemist:
                    shopListBox.Items.Add(ItemFactory.GetItem(6));
                    shopListBox.Items.Add(ItemFactory.GetItem(7));
                    shopListBox.Items.Add(ItemFactory.GetItem(8));
                    break;
                case ShopOpened.Armorer:

                    shopListBox.Items.Add(ItemFactory.GetItem(3));
                    shopListBox.Items.Add(ItemFactory.GetItem(4));
                    shopListBox.Items.Add(ItemFactory.GetItem(5));
                    break;
                case ShopOpened.Mage:
                    shopListBox.Items.Add(SpellFactory.GetSpell(0));
                    shopListBox.Items.Add(SpellFactory.GetSpell(1));
                    shopListBox.Items.Add(SpellFactory.GetSpell(2));
                    break;
            }
        }
        private void addWeaponToIventory(int id)
        {
            gameWindow.currentPlayer.Inventory.Add(ItemFactory.GetItem(id));
            gameWindow.weaponComboBox.Items.Add(gameWindow.currentPlayer.Inventory[gameWindow.currentPlayer.Inventory.Count - 1]);
            gameWindow.currentPlayer.Gold -= currentItemPrice;
            playersGoldLabel.Content = gameWindow.currentPlayer.Gold;
        }
        private void addArmorToIventory(int id)
        {
            gameWindow.currentPlayer.Inventory.Add(ItemFactory.GetItem(id));
            gameWindow.armorComboBox.Items.Add(gameWindow.currentPlayer.Inventory[gameWindow.currentPlayer.Inventory.Count - 1]);
            gameWindow.currentPlayer.Gold -= currentItemPrice;
            playersGoldLabel.Content = gameWindow.currentPlayer.Gold;
        }
        private void addPotionToIventory(int id)
        {
            gameWindow.currentPlayer.Inventory.Add(ItemFactory.GetItem(id));
            gameWindow.potionComboBox.Items.Add(gameWindow.currentPlayer.Inventory[gameWindow.currentPlayer.Inventory.Count - 1]);
            gameWindow.currentPlayer.Gold -= currentItemPrice;
            playersGoldLabel.Content = gameWindow.currentPlayer.Gold;
        }
        private void addSpellToIventory(int id)
        {
            bool hasSpell = false; 
            foreach(Spell i in gameWindow.currentPlayer.SpellBook)
            {
                if (i.Id == id)
                    hasSpell = true;
            }
            if (hasSpell == false)
            {
                gameWindow.currentPlayer.SpellBook.Add(SpellFactory.GetSpell(id));
                gameWindow.spellComboBox.Items.Add(gameWindow.currentPlayer.SpellBook[gameWindow.currentPlayer.SpellBook.Count - 1]);
                gameWindow.currentPlayer.Gold -= currentItemPrice;
                playersGoldLabel.Content = gameWindow.currentPlayer.Gold;
            }
            else
                MessageBox.Show("You already have that spell!", "Sorry!");
        }
        private void buyButton_Click(object sender, RoutedEventArgs e)
        {
            if(shop==ShopOpened.BlackSmith&& gameWindow.currentPlayer.Gold>=currentItemPrice)
                switch (shopListBox.SelectedIndex)
                {
                    case 0:
                        addWeaponToIventory(0);
                        break;
                    case 1:
                        addWeaponToIventory(1);
                        break;
                    case 2:
                        addWeaponToIventory(2);
                        break;
                }
            if (shop == ShopOpened.Armorer && gameWindow.currentPlayer.Gold >= currentItemPrice)
                switch (shopListBox.SelectedIndex)
                {
                    case 0:
                        addArmorToIventory(3);
                        break;
                    case 1:
                        addArmorToIventory(4);
                        break;
                    case 2:
                        addArmorToIventory(5);
                        break;
                }
            if (shop == ShopOpened.Alchemist && gameWindow.currentPlayer.Gold >= currentItemPrice)
                switch (shopListBox.SelectedIndex)
                {
                    case 0:
                        addPotionToIventory(6);
                        break;
                    case 1:
                        addPotionToIventory(7);
                        break;
                    case 2:
                        addPotionToIventory(8);
                        break;
                }
            if (shop == ShopOpened.Mage && gameWindow.currentPlayer.Gold >= currentItemPrice)
                switch (shopListBox.SelectedIndex)
                {
                    case 0:
                        addSpellToIventory(0);
                        break;
                    case 1:
                        addSpellToIventory(1);
                        break;
                    case 2:
                        addSpellToIventory(2);
                        break;
                }
            if (gameWindow.currentPlayer.Gold < currentItemPrice)
                MessageBox.Show("Not enough gold", "Sorry!");
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void changeDesctiption(string description,string stats ,string requirements, int price)
        {
            itemDescriptionTextBox.Text = description;
            itemDescriptionTextBox.Text += "\n"+stats;
            itemDescriptionTextBox.Text += "\n"+requirements;
            costLabel.Content = price;
            currentItemPrice = price;
        }
        private void shopListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (shop == ShopOpened.BlackSmith)
            {
                switch (shopListBox.SelectedIndex)
                {
                    case 0:
                        changeDesctiption("Standard iron sword for those new to one handed combat.", "Damage: 5", "Strength Required: 2", 50);
                        break;
                    case 1:
                        changeDesctiption("Sharper more dangerous sword for those more experienced.", "Damage: 10", "Strength required: 5", 100);
                        break;
                    case 2:
                        changeDesctiption("A dense sword crafted with rare imported steel.", "Damage: 15", "Strength required: 10", 150);
                        break;
                }
            }
            if (shop == ShopOpened.Armorer)
            {
                switch (shopListBox.SelectedIndex)
                {
                    case 0:
                        changeDesctiption("Basic armor for your average travel.", "Armor: 10", "No Requirements", 50);
                        break;
                    case 1:
                        changeDesctiption("Armor that provides a little more protection.", "Armor: 11", "Strength Required: 1", 100);
                        break;
                    case 2:
                        changeDesctiption("Metal armor the provides good protection.", "Armor: 12", "Strength required: 2", 150);
                        break;
                }
            }
            if (shop == ShopOpened.Mage)
            {
                switch (shopListBox.SelectedIndex)
                {
                    case 0:
                        changeDesctiption("Basic healing magic.", "Heal: 5", "Wisdom Required: 2", 50);
                        break;
                    case 1:
                        changeDesctiption("Shoots 3 scalding projectiles.", "Fire Damage: 10", "Wisdom Required: 2", 50);
                        break;
                    case 2:
                        changeDesctiption("Launches a heavy water projectile.", "Water Damage: 20", "Wisdom required: 10", 100);
                        break;
                }
            }
            if (shop == ShopOpened.Alchemist)
            {
                switch (shopListBox.SelectedIndex)
                {
                    case 0:
                        changeDesctiption("Refreshes the soul.", "Restore 25 heath", "Consumed on use", 150);
                        break;
                    case 1:
                        changeDesctiption("Refreshes the body.", "Restore 25 stamina", "Consumed on use", 150);
                        break;
                    case 2:
                        changeDesctiption("Resfreshes the mind.", "Restore 25 wisdom", "Consumed on use", 150);
                        break;
                }
            }
        }

        private void sellButton_Click(object sender, RoutedEventArgs e)
        {
            SellWindow sell = new SellWindow(gameWindow);
            sell.ShowDialog();
        }
    }
}
