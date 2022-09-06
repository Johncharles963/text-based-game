using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public enum DamageTypes
    {
        Fire,
        Thunder,
        Water,
        Physical
    }
    public enum WeaponTypes
    {
        Sword,
        BroadSword,
        Fist,
        Dagger,
        Staff
    }
    public class Weapon : Item
    {
        private int _minDamage;
        private int _maxDamge;
        private int _requiredStrengthStat;
        private int _requiredDexerityStat;
        private int _requiredWisdomStat;
        private DamageTypes _damageType;
        private WeaponTypes _weaponType;

        public WeaponTypes WeaponType
        {
            get { return _weaponType; }
        }
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
        public int MinDamgage
        {
            get { return _minDamage; }
        }
        public int MaxDamgage
        {
            get { return _maxDamge; }
        }
        public DamageTypes DamgageType
        {
            get { return _damageType; }
        }

        public Weapon(int inId, string inName, int inBuy, int inSell, int minDamage, int maxDamage, DamageTypes inDamage, int strength, int dexerity,int wisdom, WeaponTypes weaponType):
            base(inId, inName, inBuy, inSell)
        {
            _minDamage = minDamage;
            _maxDamge = maxDamage;
            _damageType = inDamage;
            _requiredStrengthStat = strength;
            _requiredDexerityStat = dexerity;
            _requiredWisdomStat = wisdom;
            _weaponType = weaponType;
        }
        public Weapon(int inId, string inName, int inSell, int minDamage, int maxDamage, DamageTypes inDamage, int strength, int dexerity, int wisdom, WeaponTypes weaponType) :
          base(inId, inName, 0, inSell)
        {
            _minDamage = minDamage;
            _maxDamge = maxDamage;
            _damageType = inDamage;
            _requiredStrengthStat = strength;
            _requiredDexerityStat = dexerity;
            _requiredWisdomStat = wisdom;
            _weaponType = weaponType;
        }
        public Weapon(int inId, string inName, int minDamage, int maxDamage, DamageTypes inDamage, int strength, int dexerity, int wisdom, WeaponTypes weaponType) :
  base(inId, inName, 0, 0)
        {
            _minDamage = minDamage;
            _maxDamge = maxDamage;
            _damageType = inDamage;
            _requiredStrengthStat = strength;
            _requiredDexerityStat = dexerity;
            _requiredWisdomStat = wisdom;
            _weaponType = weaponType;
        }

        public override Item Clone()
        {
            return new Weapon(Id, Name, BuyPrice, SellPrice, _minDamage, _maxDamge, _damageType, _requiredStrengthStat,_requiredDexerityStat, _requiredWisdomStat, _weaponType);
        }
    }
}
