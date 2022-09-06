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
    /// Interaction logic for QuestWindow.xaml
    /// </summary>
    public partial class QuestWindow : Window
    {
        GameWindow gameWindow;
        public QuestWindow(GameWindow incomingWindow)
        {
            gameWindow = incomingWindow;
            InitializeComponent();
            setQuests();
        }
        private void setQuests()
        {
            questListBox.Items.Add(QuestFactory.GetQuest(0));
            questListBox.Items.Add(QuestFactory.GetQuest(1));
            questListBox.Items.Add(QuestFactory.GetQuest(2));
        }
        private void changeDescrition(Quest quest)
        {
            descriptionTextBox.Text = quest.Description;
            lvlReccomendLabel.Content = quest.ReccomendedLevel;
        }
        private void changeToComplete()
        {
            MessageBox.Show("Quest completed!");
            progressLabel.Content = "Completed";
            Quest holder = (Quest)questListBox.SelectedItem;
            gameWindow.currentPlayer.Gold += holder.RewardGold;
            foreach (Quest q in gameWindow.currentPlayer.Questbook)
            {
                if (q.Id == holder.Id)
                {
                    q.InProgress = false;
                    q.IsCompleted = true;
                }
            }
            if (questListBox.SelectedIndex == 1)
            {
                int index = 0;
                int counter = 0;
                foreach (Item q in gameWindow.currentPlayer.Inventory)
                {
                    if (q.Id == 13)
                    {
                        index = counter;
                    }
                    counter++;
                }
                gameWindow.keyItemComboBox.Items.Remove(gameWindow.currentPlayer.Inventory[index]);
                gameWindow.currentPlayer.Inventory.RemoveAt(index);
            }
        }
        private void setProgressLabel(Quest quest)
        {
            if (gameWindow.currentPlayer.Questbook.Contains((Quest)questListBox.SelectedItem))
            {
                foreach (Quest q in gameWindow.currentPlayer.Questbook)
                {
                    if (q.Id == quest.Id && q.InProgress)
                    {
                        progressLabel.Content = "In progress";
                    }
                    if (q.Id == quest.Id && q.IsCompleted)
                    {
                        progressLabel.Content = "Completed";
                    }
                }
            }
            else
                progressLabel.Content = "Not Accpeted";
        }
        private void questListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changeDescrition((Quest)questListBox.SelectedItem);
            setProgressLabel((Quest)questListBox.SelectedItem);
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (questListBox.SelectedIndex == -1)
            {
                MessageBox.Show("No quest selected");
            }
            else
            {
                Quest questHolder = (Quest)questListBox.SelectedItem;
                if (gameWindow.currentPlayer.Questbook.Contains((Quest)questListBox.SelectedItem))
                {
                    foreach (Quest q in gameWindow.currentPlayer.Questbook)
                    {
                        if (q.Id == questHolder.Id && q.InProgress)
                        {
                            MessageBox.Show("You currently have this quest");
                        }
                        if (q.Id == questHolder.Id && q.IsCompleted)
                        {
                            MessageBox.Show("You have already completed this quest");
                        }
                    }
                }
                else
                {
                    gameWindow.questEncouter[questListBox.SelectedIndex] = true;
                    progressLabel.Content = "In progress";
                    gameWindow.currentPlayer.Questbook.Add(questHolder);
                    gameWindow.currentPlayer.Questbook[gameWindow.currentPlayer.Questbook.Count - 1].InProgress = true;
                }
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void completeButton_Click(object sender, RoutedEventArgs e)
        {
            if (questListBox.SelectedIndex == -1)
                MessageBox.Show("No Quest selected");
            else
            {
                if (gameWindow.questEncouter[questListBox.SelectedIndex] == true)
                {
                    MessageBox.Show("You have not completed the quest yet");
                }
                else if (gameWindow.questEncouter[questListBox.SelectedIndex] == false && progressLabel.Content == "In progress")
                {
                    changeToComplete();
                }
                else if (progressLabel.Content == "Not Accpeted")
                {
                    MessageBox.Show("You havent accepted the quest yet");
                }
                else
                {
                    MessageBox.Show("Already obtained quest reward");
                }
            }
        }
    }
}
