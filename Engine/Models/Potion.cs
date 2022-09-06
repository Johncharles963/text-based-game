using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Potion : Item
    {
        ChangeStat _statHealed;
        private int _healAmount;
        public int HealAmount
        {
            get { return _healAmount; }
        }
        public ChangeStat StatHealed
        {
            get { return _statHealed; }
        }
        public Potion(int inId, string inName,int inBuy,int inSell,int healAmount, ChangeStat statHealed):
            base(inId, inName, inBuy, inSell)
        {
            _healAmount = healAmount;
            _statHealed = statHealed;
        }
        public override Item Clone()
        {
            return new Potion(Id,Name,BuyPrice,SellPrice,_healAmount,_statHealed);
        }
    }
}
