using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class SpellFactory
    {
        private static List<Spell> EntireSpellList;

        static SpellFactory()
        {
            EntireSpellList = new List<Spell>();
            EntireSpellList.Add(new HealSpell(0, "Mend Wounds",50, 5,2, 5));
            EntireSpellList.Add(new DamageSpell(1, "Boiling Missles", 50, 5,2, 10, DamageTypes.Fire));
            EntireSpellList.Add(new DamageSpell(2, "Waterball", 100, 10,10, 20, DamageTypes.Water));
            EntireSpellList.Add(new BuffSpell(3, "Enhance Dexerity", 0, 5, 2, 2, ChangeStat.Dexerity));
        }
        public static Spell GetSpell(int id)
        {
            Spell requestedSpell;
            requestedSpell=EntireSpellList[id];
            if(requestedSpell != null)
            return requestedSpell.Clone();
            return null;
        }
    }


}
