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
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace TestGame
{
    /// <summary>
    /// Interaction logic for BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        private GameWindow gameWindow;
        private Duration duration;
        private Weapon heldWeapon;
        private Armor heldArmor;
        private Skill heldSkill;
        private Spell heldSpell;
        private DamageSpell damageSpell;
        private HealSpell healSpell;
        private BuffSpell buffSpell;
        private Potion heldPotion;
        private int selectedEnemy=0;
        private bool lastAttacker;
        private Random rand = new Random();
        private int enemiesDead=0;
        private bool skillSelected = false;
        private bool spellSelected = false;
        private bool potionSelected = false;
        private DoubleAnimation doubleAnimation;
        private DoubleAnimation manaAnimation;
        private DoubleAnimation staminaAnimation;
        public BattleWindow(GameWindow incomingWindow)
        {
            gameWindow = incomingWindow;
            duration = new Duration(TimeSpan.FromSeconds(1));
            InitializeComponent();
            setWindow();
            setEquipment();
        }
        //checks if enemies are alive and will have them attack if they are
        private void enemyTurn(int index)
        {
            if(gameWindow.currentEncounter.Enemies[index].IsAlive)
            {
                if (enemyRollToHit(index))
                {
                    gameWindow.currentPlayer.TakeDamage(gameWindow.currentEncounter.Enemies[index].Damage);
                    mainTextBlock.Text += "\n" + gameWindow.currentEncounter.Enemies[index].Name + " hit you for " + gameWindow.currentEncounter.Enemies[index].Damage + " damage!";
                    doubleAnimation = new DoubleAnimation(gameWindow.currentPlayer.CurrentHealth, duration);
                    doubleAnimation.Completed += (s, e) =>
                    {
                        if (gameWindow.currentPlayer.IsAlive == false)
                        {
                            MessageBox.Show("You died!");
                        }
                        else if(index<gameWindow.currentEncounter.EnemyCount-1)
                        {
                            index++;
                            enemyTurn(index);
                        }
                        else
                        {
                            enablePlayersButtons();
                        }
                    };
                    playerProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
                }
                else
                {
                    mainTextBlock.Text += "\n" + gameWindow.currentEncounter.Enemies[index].Name + " missed their attack!";
                    if (index < gameWindow.currentEncounter.EnemyCount - 1)
                    {
                        index++;
                        enemyTurn(index);
                    }
                    else if (index == gameWindow.currentEncounter.EnemyCount - 1)
                    {
                        enablePlayersButtons();
                    }
                }
            }
            else
            {
                if (index < gameWindow.currentEncounter.EnemyCount - 1)
                {
                    index++;
                    enemyTurn(index);
                }
                else 
                {
                    enablePlayersButtons();
                }
            }
            

        }
        //copys the players weapons and armor so they can be augmented in battle
        private void setEquipment()
        {
            gameWindow.currentPlayer.ResetBattleStats();
            heldWeapon = gameWindow.weaponComboBox.SelectedItem as Weapon;
            heldArmor = gameWindow.armorComboBox.SelectedItem as Armor;
            heldArmor.Clone();
            heldWeapon.Clone();
        }
        //checks to see if enemy hits player
        private bool enemyRollToHit(int index)
        {
            int dice;
            dice = rand.Next(1,21);
            dice += gameWindow.currentEncounter.Enemies[index].HitDice;
            if (dice > heldArmor.Stats+gameWindow.currentPlayer.BattleDexerity)
                return true;
            return false;
        }
        //checks to see if player hits enemy
        private bool playerRollToHit(int index)
        {
            int dice;
            dice = rand.Next(1, 21);
            dice += 2;
            switch(heldWeapon.WeaponType)
            {
                case WeaponTypes.Sword:
                case WeaponTypes.BroadSword:
                    dice +=  gameWindow.currentPlayer.BattleStrength;
                    break;
                case WeaponTypes.Fist:
                case WeaponTypes.Dagger:
                    dice +=  gameWindow.currentPlayer.BattleDexerity;
                    break;
                case WeaponTypes.Staff:
                    dice +=  gameWindow.currentPlayer.BattleWisdom;
                    break;
            }
            if (dice > gameWindow.currentEncounter.Enemies[index].Armor)
                return true;
            return false;
        }
        //sets ui based on number of enemies
        private void setWindow()
        {
            enemy1Radio.IsChecked = true;
            enemy2Radio.Visibility = Visibility.Hidden;
            enemy2ProgressBar.Visibility = Visibility.Hidden;
            enemy2TextBlock.Visibility = Visibility.Hidden;
            enemy3TextBlock.Visibility = Visibility.Hidden;
            enemy3Radio.Visibility = Visibility.Hidden;
            enemy3ProgressBar.Visibility = Visibility.Hidden;
            playerProgressBar.Maximum = gameWindow.currentPlayer.Health;
            playerProgressBar.Value = gameWindow.currentPlayer.CurrentHealth;
            staminaProgressBar.Maximum = gameWindow.currentPlayer.Stamina;
            staminaProgressBar.Value = gameWindow.currentPlayer.CurrentStamina;
            manaProgressBar.Maximum = gameWindow.currentPlayer.Mana;
            manaProgressBar.Value = gameWindow.currentPlayer.CurrentMana;
            playerLabel.Content = gameWindow.currentPlayer.Name;
            for (int i = 0; i < gameWindow.currentEncounter.EnemyCount; i++)
            {
                switch (i)
                {
                    case 0:
                        enemy1ProgressBar.Maximum = gameWindow.currentEncounter.Enemies[i].Health;
                        enemy1ProgressBar.Value = gameWindow.currentEncounter.Enemies[i].Health;
                        enemy1Radio.Content = gameWindow.currentEncounter.Enemies[i].Name;
                        break;
                    case 1:
                        enemy2Radio.Visibility = Visibility.Visible;
                        enemy2ProgressBar.Visibility = Visibility.Visible;
                        enemy2TextBlock.Visibility = Visibility.Visible;
                        enemy2ProgressBar.Maximum = gameWindow.currentEncounter.Enemies[i].Health;
                        enemy2ProgressBar.Value = gameWindow.currentEncounter.Enemies[i].Health;
                        enemy2Radio.Content = gameWindow.currentEncounter.Enemies[i].Name;
                        break;
                    case 2:
                        enemy3Radio.Visibility = Visibility.Visible;
                        enemy3ProgressBar.Visibility = Visibility.Visible;
                        enemy2TextBlock.Visibility = Visibility.Visible;
                        enemy3ProgressBar.Maximum = gameWindow.currentEncounter.Enemies[i].Health;
                        enemy3ProgressBar.Value = gameWindow.currentEncounter.Enemies[i].Health;
                        enemy3Radio.Content = gameWindow.currentEncounter.Enemies[i].Name;
                        break;
                }
            }
        }
        //returns damage based on players held weapon type
        private int getWeaponDamage()
        {
            int damage = rand.Next(heldWeapon.MinDamgage, heldWeapon.MaxDamgage + 1);
            switch (heldWeapon.WeaponType)
            {
                case WeaponTypes.Sword:
                case WeaponTypes.BroadSword:
                    damage += gameWindow.currentPlayer.BattleStrength;
                    break;
                case WeaponTypes.Fist:
                    damage += gameWindow.currentPlayer.BattleDexerity * 2;
                    break;
                case WeaponTypes.Dagger:
                    damage += gameWindow.currentPlayer.BattleDexerity;
                    break;
                case WeaponTypes.Staff:
                    damage += gameWindow.currentPlayer.BattleWisdom;
                    break;
            }
            return damage;
        }
        //returns the min and max damage of players weapon for display
        private string getMinMaxString()
        {
            int minDamage = heldWeapon.MinDamgage;
            int maxDamage = heldWeapon.MaxDamgage;
            int damage=0;
                switch (heldWeapon.WeaponType)
                {
                    case WeaponTypes.Sword:
                    case WeaponTypes.BroadSword:
                        damage = gameWindow.currentPlayer.BattleStrength;
                        break;
                    case WeaponTypes.Fist:
                        damage = gameWindow.currentPlayer.BattleDexerity * 2;
                        break;
                    case WeaponTypes.Dagger:
                        damage = gameWindow.currentPlayer.BattleDexerity;
                        break;
                    case WeaponTypes.Staff:
                        damage = gameWindow.currentPlayer.BattleWisdom;
                        break;
                }
                    minDamage += damage;
                    maxDamage += damage;
            string damageString = "Damage: " + minDamage + "-" + maxDamage;
            return damageString;
        }
        //Selected enemey takes damage, updates their hp bar, switches turns
        private void hitEnemy(int damage, DamageTypes damageType)
        {
            gameWindow.currentEncounter.Enemies[selectedEnemy].TakeDamage(damage, damageType);
            mainTextBlock.Text += "\nYou hit " + gameWindow.currentEncounter.Enemies[selectedEnemy].Name + " for " + damage + "!";
            if (gameWindow.currentEncounter.Enemies[selectedEnemy].IsAlive == false)
            {
                enemiesDead++;
                mainTextBlock.Text += "\nYou killed " + gameWindow.currentEncounter.Enemies[selectedEnemy].Name + "!";
                switch (selectedEnemy)
                {
                    case 0:
                        enemy1Radio.IsEnabled = false;
                        enemy1Radio.IsChecked = false;
                        break;
                    case 1:
                        enemy2Radio.IsEnabled = false;
                        enemy2Radio.IsChecked = false;
                        break;
                    case 2:
                        enemy3Radio.IsEnabled = false;
                        enemy3Radio.IsChecked = false;
                        break;
                }
            }
            switch (selectedEnemy)
            {
                case 0:
                    DoubleAnimation doubleAnimation = new DoubleAnimation(gameWindow.currentEncounter.Enemies[0].Health, duration);
                    doubleAnimation.Completed += (s, e) =>
                    {
                        switchTurns();
                    };
                    enemy1ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
                    break;
                case 1:
                    doubleAnimation = new DoubleAnimation(gameWindow.currentEncounter.Enemies[1].Health, duration);
                    doubleAnimation.Completed += (s, e) =>
                    {
                        switchTurns();
                    };
                    enemy2ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
                    break;
                case 2:
                    doubleAnimation = new DoubleAnimation(gameWindow.currentEncounter.Enemies[2].Health, duration);
                    doubleAnimation.Completed += (s, e) =>
                    {
                        switchTurns();
                    };
                    enemy3ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
                    break;
            }
        }
        //stops player from taking a turn
        private void disablePlayersButtons()
        {
            attackButton.IsEnabled = false;
            skillButton.IsEnabled = false;
            magicButton.IsEnabled = false;
            potionButton.IsEnabled = false;
            useButton.IsEnabled = false;
        }
        //enables player to take a turn
        private void enablePlayersButtons()
        {
            attackButton.IsEnabled = true;
            skillButton.IsEnabled = true;
            magicButton.IsEnabled = true;
            potionButton.IsEnabled = true;
            useButton.IsEnabled = true;
        }
        private void switchTurns()
        {
            //makes sure an enemy is always selected
            if(enemy3Radio.IsChecked==false && enemy2Radio.IsChecked==false && enemy1Radio.IsChecked==false)
            {
                for (int i = 0; i < gameWindow.currentEncounter.EnemyCount; i++)
                {
                    if (gameWindow.currentEncounter.Enemies[i].IsAlive == true)
                    {
                        switch (i)
                        {
                            case 0:
                                enemy1Radio.IsChecked = true;
                                break;
                            case 1:
                                enemy2Radio.IsChecked = true;
                                break;
                            case 2:
                                enemy3Radio.IsChecked = true;
                                break;
                        }
                        break;
                    }
                }
            }
            disablePlayersButtons();
            //checks to see if player has won, if not, enemies take their turn
            if (enemiesDead < gameWindow.currentEncounter.EnemyCount)
            {
                for (int i = 0; i < gameWindow.currentEncounter.EnemyCount; i++)
                {
                    if (gameWindow.currentEncounter.Enemies[i].IsAlive)
                    {
                        enemyTurn(i);
                        break;
                    }     
                }
            }
            else
            {
                MessageBox.Show("You won!");
            }
        }
        //player attempts to attack, switches turns
        private void attackButton_Click(object sender, RoutedEventArgs e)
        {
            if (enemy1Radio.IsChecked == false && enemy2Radio.IsChecked == false && enemy3Radio.IsChecked == false)
                MessageBox.Show("No enemy selected");
            else
            {
                if (playerRollToHit(selectedEnemy))
                {
                    disablePlayersButtons();
                    int damage = getWeaponDamage();
                    hitEnemy(damage, heldWeapon.DamgageType);                  
                }
                else
                {
                    mainTextBlock.Text += "\nYou missed your Attack!";
                    switchTurns();
                }
            }
        }
        //displays skills if clicked
        private void skillButton_Click(object sender, RoutedEventArgs e)
        {
            resetSlection();
            skillSelected = true;
            userListBox.Items.Clear();
            foreach(Skill s in gameWindow.currentPlayer.Skillbook)
            {
                userListBox.Items.Add(s);
            }

        }
        //displays magic if clicked
        private void magicButton_Click(object sender, RoutedEventArgs e)
        {
            resetSlection();
            spellSelected = true;
            userListBox.Items.Clear();
            foreach (Spell s in gameWindow.currentPlayer.SpellBook)
            {
                userListBox.Items.Add(s);
            }
        }
        //sets the selected enemy to use as index for lists
        private void enemy1Radio_Checked(object sender, RoutedEventArgs e)
        {
            selectedEnemy = 0;
        }

        private void enemy2Radio_Checked(object sender, RoutedEventArgs e)
        {
            selectedEnemy = 1;
        }

        private void enemy3Radio_Checked(object sender, RoutedEventArgs e)
        {
            selectedEnemy = 2;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            gameWindow.currentEncounter=null;
            this.Close();
        }

        private void mainTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            mainTextBlock.ScrollToEnd();
        }

        private void attackButton_MouseEnter(object sender, MouseEventArgs e)
        {
            attackPopUp.IsOpen = true;
            attackPopText.Text = getMinMaxString();
        }

        private void attackButton_MouseLeave(object sender, MouseEventArgs e)
        {
            attackPopUp.IsOpen = false;
        }

        private void userListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void potionButton_Click(object sender, RoutedEventArgs e)
        {
            resetSlection();
            potionSelected = true;
            userListBox.Items.Clear();
            foreach(Item i in gameWindow.currentPlayer.Inventory)
            {
                if(i is Potion)
                userListBox.Items.Add(i);
            }
        }
        private void resetSlection()
        {
            skillSelected = false;
            spellSelected = false;
            potionSelected = false;
        }
        private int getSkillDamage(int skillDamage)
        {
            int weaponDamge = getWeaponDamage();
            if (heldWeapon.WeaponType is WeaponTypes.Fist)
                weaponDamge += gameWindow.currentPlayer.BattleDexerity;
            skillDamage += weaponDamge;
            return skillDamage;
        }
        private void useButton_Click(object sender, RoutedEventArgs e)
        {
            if (userListBox.SelectedIndex == -1)
                MessageBox.Show("Nothing selected");
            else if (enemy1Radio.IsChecked == false && enemy2Radio.IsChecked == false && enemy3Radio.IsChecked == false)
                MessageBox.Show("No enemy Selected");
            else
            {
                if(skillSelected)
                {
                    heldSkill = gameWindow.currentPlayer.Skillbook[userListBox.SelectedIndex];
                    if (gameWindow.currentPlayer.CurrentStamina >= heldSkill.StaminaCost)
                    {
                        disablePlayersButtons();
                        int damage = getSkillDamage(heldSkill.Damage);         
                        gameWindow.currentPlayer.CurrentStamina -= heldSkill.StaminaCost;
                        staminaAnimation = new DoubleAnimation(gameWindow.currentPlayer.CurrentStamina, duration);
                        staminaAnimation.Completed += (s, e) =>
                        {
                            hitEnemy(damage, heldWeapon.DamgageType);
                        };
                        staminaProgressBar.BeginAnimation(ProgressBar.ValueProperty, staminaAnimation);
                    }
                    else
                        MessageBox.Show("Not enough stamina");
                }
                else if(spellSelected)
                {
                    heldSpell = gameWindow.currentPlayer.SpellBook[userListBox.SelectedIndex];
                    if(gameWindow.currentPlayer.CurrentMana>=heldSpell.ManaCost)
                    {
                        disablePlayersButtons();
                        if (heldSpell is DamageSpell)
                        {
                            damageSpell = heldSpell as DamageSpell;
                            gameWindow.currentPlayer.CurrentMana -= damageSpell.ManaCost;
                            manaAnimation = new DoubleAnimation(gameWindow.currentPlayer.CurrentMana, duration);
                            manaAnimation.Completed += (s, e) =>
                            {
                                hitEnemy(damageSpell.Damgage, damageSpell.DamageType);
                            };
                            manaProgressBar.BeginAnimation(ProgressBar.ValueProperty, manaAnimation);
                        }
                        else if (heldSpell is HealSpell)
                        {
                            healSpell = heldSpell as HealSpell;
                            int amount = healSpell.HealAmount + gameWindow.currentPlayer.BattleWisdom;
                            mainTextBlock.Text += "\n You healed " + amount + "!";
                            gameWindow.currentPlayer.Heal(amount, ChangeStat.Health);
                            doubleAnimation = new DoubleAnimation(gameWindow.currentPlayer.CurrentHealth, duration);
                            doubleAnimation.Completed += (s, e) =>
                            {
                                switchTurns();
                            };
                            playerProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
                            gameWindow.currentPlayer.CurrentMana -= healSpell.ManaCost;
                            manaAnimation = new DoubleAnimation(gameWindow.currentPlayer.CurrentMana, duration);
                            manaProgressBar.BeginAnimation(ProgressBar.ValueProperty, manaAnimation);
                        }
                        else if (heldSpell is BuffSpell)
                        {
                            buffSpell = heldSpell as BuffSpell;
                            ChangeStat stat = buffSpell.BuffedStat;
                            int amount = buffSpell.BuffAmount;
                            mainTextBlock.Text += "\nYou buff your " + stat + " by " + amount + "!";
                            buffSpell.Buff(gameWindow.currentPlayer);
                            gameWindow.currentPlayer.CurrentMana -= buffSpell.ManaCost;
                            manaAnimation = new DoubleAnimation(gameWindow.currentPlayer.CurrentMana, duration);
                            manaAnimation.Completed += (s, e) =>
                            {
                                switchTurns();
                            };
                            manaProgressBar.BeginAnimation(ProgressBar.ValueProperty, manaAnimation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not Enough Mana");
                    }
                }
                else if(potionSelected)
                {
                    disablePlayersButtons();
                    heldPotion = userListBox.SelectedItem as Potion;
                    int amount = heldPotion.HealAmount;
                    gameWindow.currentPlayer.Heal(amount, heldPotion.StatHealed);
                    mainTextBlock.Text += "\nYou healed " + amount + "!";
                    switch(heldPotion.StatHealed)
                    {
                        case ChangeStat.Health:
                            doubleAnimation = new DoubleAnimation(gameWindow.currentPlayer.CurrentHealth, duration);
                            doubleAnimation.Completed += (s, e) =>
                            {
                                switchTurns();
                            };
                            playerProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
                            break;
                        case ChangeStat.Stamina:
                            staminaAnimation = new DoubleAnimation(gameWindow.currentPlayer.CurrentStamina, duration);
                            staminaAnimation.Completed += (s, e) =>
                            {
                                switchTurns();
                            };
                            staminaProgressBar.BeginAnimation(ProgressBar.ValueProperty, staminaAnimation);
                            break;
                        case ChangeStat.Mana:
                            manaAnimation = new DoubleAnimation(gameWindow.currentPlayer.CurrentMana, duration);
                            manaAnimation.Completed += (s, e) =>
                            {
                                switchTurns();
                            };
                            manaProgressBar.BeginAnimation(ProgressBar.ValueProperty, manaAnimation);
                            break;
                    }
                    gameWindow.potionComboBox.Items.Remove(heldPotion);
                    gameWindow.currentPlayer.Inventory.Remove(heldPotion);
                    userListBox.Items.Remove(heldPotion);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameWindow.playerHpLabel.Content = gameWindow.currentPlayer.DisplayHealth;
            gameWindow.staminaLabel.Content = gameWindow.currentPlayer.DisplayStamina;
            gameWindow.manaLabel.Content = gameWindow.currentPlayer.DisplayMana;
        }
    }
}
