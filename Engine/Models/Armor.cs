using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Armor : Item
    {
        private int _armor;
        private int _requiredStrengthStat;
        private int _requiredDexerityStat;
        private int _requiredWisdomStat;
        public int StrengthRequired
        {
            get { return _requiredStrengthStat; }
            set { _requiredStrengthStat = value; }
        }
        public int DexerityRequired
        {
            get { return _requiredDexerityStat; }
            set { _requiredDexerityStat = value; }
        }
        public int WisdomRequired
        {
            get { return _requiredWisdomStat; }
            set { _requiredWisdomStat = value; }
        }
        public int Stats
        {
            get { return _armor; }
        }
        public Armor(int inId, string inName, int inBuy, int inSell, int armor, int strength, int dexerity, int wisdom) :
            base(inId, inName, inBuy, inSell)
        {
            _requiredStrengthStat = strength;
            _requiredDexerityStat = dexerity;
            _requiredWisdomStat = wisdom;
            _armor = armor;
        }
        public override Item Clone()
        {
            return new Armor(Id,Name,BuyPrice,SellPrice, _armor,_requiredStrengthStat, _requiredDexerityStat, _requiredWisdomStat);
        }
    }
}
