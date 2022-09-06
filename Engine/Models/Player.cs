using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Engine.Factories;
namespace Engine.Models
{
    public enum Races
    {
        Human,
        Pygmy,
        Dragonkin,
        Abyssian
    }

    public enum PlayerClasses 
    {
        Nomand,
        Warrior,
        Savant,
        Neoman
    }
    public class Player : INotifyPropertyChanged
    {
        public List<Item> Inventory;
        public List<Spell> SpellBook;
        public List<Quest> Questbook;
        public List<Skill> Skillbook;

        public event PropertyChangedEventHandler PropertyChanged;
        private bool isAlive=true;
        private int health;
        private int currentHealth = 0;
        private int stamina;
        private int currentStamina=0;
        private int experience;
        private int currentExperience=0;
        private int mana;
        private int currentMana=0;
        private int gold=0;
        private string name="";
        private string sex = "Male";
        private Races race;
        private PlayerClasses playersClass;
        private int level = 1;
        private int strength = 0;
        private int dexerity = 0;
        private int wisdom = 0;
        private int battleStrength;
        private int battleDexerity;
        private int battleWisdom;
        public Player()
        {
            Inventory = new List<Item>();
            SpellBook = new List<Spell>();
            Questbook = new List<Quest>();
            Skillbook = new List<Skill>();
        }
        public bool IsAlive
        {
            get { return isAlive; }
        }
        public int Gold
        {
            get { return gold; }
            set { gold = value;
                OnPropertyChanged("Gold");
                }
        }
        public int BattleStrength
        {
            get { return battleStrength; }
            set { battleStrength = value; }
        }
        public int BattleDexerity
        {
            get { return battleDexerity; }
            set { battleDexerity = value; }
        }
        public int BattleWisdom
        {
            get { return battleWisdom; }
            set { battleWisdom = value; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public int Strength
        {
            get { return strength; }
            set { strength = value;
                OnPropertyChanged("Strength");
            }
        }
        public int Dexerity
        {
            get { return dexerity; }
            set { dexerity = value; }
        }
        public int Wisdom
        {
            get { return wisdom; }
            set { wisdom = value; }
        }
        public string DisplayHealth
        {
            get { return "Hp " + currentHealth + "/" + health; }
        }
        public string DisplayStamina
        {
            get { return "Sp " + currentStamina + "/" + stamina; }
        }
        public string DisplayMana
        {
            get { return "Mp " + currentMana + "/" + mana; }
        }
        public string DisplayExperience
        {
            get { return "Xp " +currentExperience + "/" + experience; }
        }
        public int Health {
            get {return health; }
            set { health = value;
                OnPropertyChanged("Health");
            }
        }
        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }
        public int CurrentHealth 
        { 
            get { return currentHealth; } 
            set { currentHealth = value;
                OnPropertyChanged("CurrentHealth");
            } 
        }
        public int CurrentStamina
        {
            get { return currentStamina; }
            set { currentStamina = value; }
        }
        public int CurrentMana
        {
            get { return currentMana; }
            set { currentMana = value; }
        }
        public int Stamina 
        {
            get { return stamina; }
            set { stamina = value; } 
        }
        public int Mana 
        {
            get { return mana; }
            set { mana = value; }
        }
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        public Races Race
        {
            get { return race; }
            set { race = value; }
        }

        public PlayerClasses PlayersClass
        {
            get { return playersClass; }
            set { playersClass = value; }
        }

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isAlive = false;
            }
        }
        public void Heal(int healAmount, ChangeStat stat)
        {
            if(stat==ChangeStat.Health)
            {
                currentHealth += healAmount;
                if (currentHealth > health)
                    currentHealth = health;
            }
            else if(stat==ChangeStat.Stamina)
            {
                currentStamina += healAmount;
                if (currentStamina > Stamina)
                    currentStamina = Stamina;
            }
            else if(stat==ChangeStat.Mana)
            {
                currentMana += healAmount;
                if (currentMana > Mana)
                    currentMana = Mana;
            }
        }
        public void ResetBattleStats()
        {
            battleDexerity = dexerity;
            battleStrength = strength;
            battleWisdom = wisdom;
        }
        public void SetStats()
        {
            switch(playersClass)
            {
                case PlayerClasses.Nomand:
                    health = 15;
                    stamina = 10;
                    mana = 15;
                    strength = 0;
                    dexerity = 3;
                    wisdom = 2;
                    Skillbook.Add(SkillFactory.GetSkill(1));
                    break;
                case PlayerClasses.Neoman:
                    health = 20;
                    stamina = 10;
                    mana = 10;
                    strength = 3;
                    dexerity = 0;
                    wisdom = 2;
                    Skillbook.Add(SkillFactory.GetSkill(0));
                    break;
                case PlayerClasses.Warrior:
                    health = 25;
                    stamina = 15;
                    mana = 0;
                    strength = 5;
                    dexerity = 0;
                    wisdom = 0;
                    Skillbook.Add(SkillFactory.GetSkill(2));
                    break;
                case PlayerClasses.Savant:
                    health = 15;
                    stamina = 0;
                    mana = 25;
                    strength = 0;
                    dexerity = 0;
                    wisdom = 5;
                    break;
            }
            currentHealth = health;
            currentStamina = stamina;
            currentMana = mana;
            battleStrength = strength;
            battleDexerity = dexerity;
            battleWisdom = wisdom;
            experience = 10;
        }
    }
}
