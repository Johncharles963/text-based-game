using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class HealSpell :Spell
    {
        private int _healAmount;
        public int HealAmount
        {
            get {return _healAmount; }
        }
        public HealSpell(int id, string name, int buyPrice, int manaCost, int wisdom, int healAmount):
            base(id, name, buyPrice, manaCost, wisdom)
        {
            _healAmount = healAmount;
            Type = SpellType.Heal;
        }

        public override Spell Clone()
        {
            return new HealSpell(Id, Name, BuyPrice, ManaCost,WisdomRequired, _healAmount) ;
        }

        public bool Heal(Player player)
        {
            if(player.CurrentHealth<player.Health)
            {
                player.CurrentHealth += _healAmount+player.BattleWisdom;
                if (player.CurrentHealth > player.Health)
                    player.CurrentHealth = player.Health;
                return true;
            }
            return false;
        }
    }
}
