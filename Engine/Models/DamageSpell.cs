using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class DamageSpell : Spell
    {
        private int _damage;
        private DamageTypes _damageType;
        public int Damgage
        {
            get { return _damage; }
        }
        public DamageTypes DamageType
        {
            get { return _damageType; }
        }
        public DamageSpell(int id, string name, int buyPrice, int manaCost, int wisdom, int damage, DamageTypes damageType) :
            base(id, name, buyPrice, manaCost, wisdom)
        {
            _damage = damage;
            _damageType = damageType;
            Type = SpellType.Damage;
        }
        public override Spell Clone()
        {
                return new DamageSpell(Id, Name, BuyPrice, ManaCost,WisdomRequired, _damage, _damageType);
        }
    }
}
