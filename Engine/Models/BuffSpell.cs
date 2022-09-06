using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{

    public class BuffSpell :Spell
    {
        private int _buffAmount;
        private ChangeStat _buffedStat;
        private SpellType spelltype = SpellType.Buff;
        public ChangeStat BuffedStat
        {
            get { return _buffedStat; }
        }
        public int BuffAmount
        {
            get { return _buffAmount; }
        }
        public BuffSpell(int id, string name, int buyPrice, int manaCost, int buffAmount, int wisdom, ChangeStat buffedStat) :
            base(id, name, buyPrice, manaCost, wisdom)
        {
            _buffAmount = buffAmount;
            _buffedStat = buffedStat;
            Type = SpellType.Buff;
        }

        public override Spell Clone()
        {
            return new BuffSpell(Id,Name,BuyPrice,ManaCost,WisdomRequired,_buffAmount,_buffedStat);
        }

        public void Buff(Player player)
        {
            switch(_buffedStat)
            {
                case ChangeStat.Strength:
                    player.BattleStrength += _buffAmount;
                    break;
                case ChangeStat.Dexerity:
                    player.BattleDexerity += _buffAmount;
                    break;
                case ChangeStat.Wisdom:
                    player.BattleWisdom += _buffAmount;
                    break;
            }
        }
    }

}
