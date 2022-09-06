using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
namespace Engine.Factories
{
    public static class ItemFactory
    {
        private static List<Item> EveryGameItem;
       static ItemFactory()
        {
            EveryGameItem = new List<Item>();
            EveryGameItem.Add(new Weapon(0, "Iron Sword", 50, 10, 3, 5, DamageTypes.Physical,2,0,0,WeaponTypes.Sword));
            EveryGameItem.Add(new Weapon(1, "Steel Sword", 100, 20, 8, 10, DamageTypes.Physical,5,0,0,WeaponTypes.Sword));
            EveryGameItem.Add(new Weapon(2, "Black Sword", 150, 30, 13, 15, DamageTypes.Physical,10,0,0,WeaponTypes.Sword));
            EveryGameItem.Add(new Armor(3, "Leather Armor", 50, 10, 10,0,0,0));
            EveryGameItem.Add(new Armor(4, "Studden Armor", 100, 20, 11,1,0,0));
            EveryGameItem.Add(new Armor(5, "Chain Armor", 150, 30, 12,2,0,0));
            EveryGameItem.Add(new Potion(6,"Healing Potion",150,30,25,ChangeStat.Health));
            EveryGameItem.Add(new Potion(7, "Refresh Potion", 150, 30, 25, ChangeStat.Stamina));
            EveryGameItem.Add(new Potion(8, "Intellect Potion", 150, 30, 25, ChangeStat.Mana));
            EveryGameItem.Add(new Weapon(9,"Fists",1,2,DamageTypes.Physical,0,0,0,WeaponTypes.Fist));
            EveryGameItem.Add(new Armor(10,"Unarmored",0,0,5,0,0,0));
            EveryGameItem.Add(new Item(11, "Wolf Claws", 0, 5));
            EveryGameItem.Add(new Item(12, "Wolf Skull", 0, 10));
            EveryGameItem.Add(new Item(13, "Toy Sword", true));
            EveryGameItem.Add(new Weapon(14, "Iron Dagger",10,2,7,DamageTypes.Physical,1,1,0,WeaponTypes.Dagger));

        }

        public static Item GetItem(int id)
        {
            Item requestedItem = EveryGameItem[id];
            if (requestedItem != null)
            {
                return requestedItem.Clone();
            }
             return null;
        }
    }
}
