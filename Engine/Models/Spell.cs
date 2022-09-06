using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public enum ChangeStat
    {
        Health,
        Stamina,
        Mana,
        Strength,
        Dexerity,
        Wisdom
    }
    public enum SpellType
    {
        Damage,
        Buff, 
        Heal,
    }
    public class Spell
    {
        private string _name;
        private int _id;
        private int _buyPrice;
        private int _manaCost;
        private SpellType _type;
        private int _requiredWisdomStat;
        public int WisdomRequired
        {
            get { return _requiredWisdomStat; }
            set { _requiredWisdomStat = value; }
        }
        public SpellType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public int Id
        {
            get { return _id; }
        }
        public string Name
        {
            get { return _name; }
        }
        public int BuyPrice
        {
            get { return _buyPrice; }
        }
        public int ManaCost
        {
            get { return _manaCost; }
        }
        public Spell(int id, string name, int buyPrice, int manaCost, int wisdom)
        {
            _id = id;
            _name = name;
            _buyPrice = buyPrice;
            _manaCost = manaCost;
            _requiredWisdomStat = wisdom;
        }
        public virtual Spell Clone()
        {
            return new Spell(_id, _name, _buyPrice, _manaCost,_requiredWisdomStat) ;
        }

    }
}

