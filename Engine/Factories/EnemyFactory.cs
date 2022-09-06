using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
namespace Engine.Factories
{
    public static class EnemyFactory
    {
        private static List<Enemy> EveryEnemy;
        static EnemyFactory()
        {
            EveryEnemy = new List<Enemy>();
            EveryEnemy.Add(new Enemy(0,"White Wolf",2,10,DamageTypes.Physical,DamageTypes.Fire, ItemFactory.GetItem(11),5,11));
            EveryEnemy.Add(new Enemy(1, "Alpha Wolf", 4, 20, DamageTypes.Physical, DamageTypes.Fire,ItemFactory.GetItem(12),6,12));
            EveryEnemy.Add(new Enemy(2, "Bandit", 3, 15, DamageTypes.Physical, DamageTypes.Fire,ItemFactory.GetItem(14),5,13));
        }
        public static Enemy GetEnemy(int id)
        {
            Enemy requestedEnemy;
            requestedEnemy = EveryEnemy[id];
            return requestedEnemy.Clone();
        }
    }
}
