using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Enemy
    {
        private int _id;
        private string _name;
        private int _damage;
        private int _health;
        private int _hitDice;
        private int _armor;
        private bool _isAlive=true;
        private DamageTypes _damageType;
        private DamageTypes _weakness;
        private Item _loot;

        public int Armor
        {
            get { return _armor; }
        }
        public int HitDice
        {
            get { return _hitDice; }
        }
        public Item Loot
        {
            get { return _loot; }
        }
        public int Id
        {
            get { return _id; }
        }
        public string Name
        {
            get { return _name; }
        }
        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }
        public int Health
        {
           get {return _health; }
        }
        public bool IsAlive
        {
            get { return _isAlive; }
        }
        public DamageTypes DamageType
        {
            get { return _damageType;}
        }
        public DamageTypes Weakness
        {
            get { return _weakness; }
        }
        public Enemy(int id, string name, int damage, int health, DamageTypes damageType, DamageTypes weakness, Item loot, int hitDice, int armor)
        {
            _id = id;
            _name = name;
            _damage = damage;
            _health = health;
            _damageType = damageType;
            _weakness = weakness;
            _loot = loot;
            _hitDice = hitDice;
            _armor = armor;
        }
        public void TakeDamage(int damage, DamageTypes damageType)
        {
            if(damageType  == _weakness)
            {
                damage *= 2;
            }
            _health -= damage;
            if (_health < 0)
                _health = 0;
            if (_health == 0)
                _isAlive = false;
        }
        public Enemy Clone()
        {
            return new Enemy(_id, _name, _damage, _health, _damageType, _weakness, _loot, _hitDice, _armor);
        }
    }
}
