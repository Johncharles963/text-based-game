using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
namespace Engine.Factories
{
    public static class EncounterFactory
    {
        private static List<Encounter> EveryEncounter;
        static EncounterFactory()
        {
            EveryEncounter = new List<Encounter>();
            EveryEncounter.Add(new Encounter(0,"Lone Wolf",1,"A Lone wolf has attacked you!","You deafeated the wolf!",EnemyFactory.GetEnemy(0)));
            EveryEncounter.Add(new Encounter(1, "Wolf Duo", 2, "A pair of wolves has are attacking you!", "You defeated both the wolves!", EnemyFactory.GetEnemy(0), EnemyFactory.GetEnemy(0)));
            EveryEncounter.Add(new Encounter(2, "Pack of Wolves", 3, "A pack of wolves have circled you!", "You defeated all the wolves!", EnemyFactory.GetEnemy(0), EnemyFactory.GetEnemy(0), EnemyFactory.GetEnemy(0)));
        }
        public static Encounter GetEncounter(int id)
        {
            Encounter requestedEncounter;
            requestedEncounter = EveryEncounter[id];
            return requestedEncounter.Clone();
        }
    }
}
