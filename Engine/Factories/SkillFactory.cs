using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
namespace Engine.Factories
{
     public static class SkillFactory
    {
        private static List<Skill> EntireSkillList;
        static SkillFactory()
        {
            EntireSkillList = new List<Skill>();
            EntireSkillList.Add(new Skill(0,"Double Slash","Quickly strike twice",10,5,WeaponTypes.Sword));
            EntireSkillList.Add(new Skill(1, "Hydra Strike", "Strike your enemy 3 times", 10, 5, WeaponTypes.Fist));
            EntireSkillList.Add(new Skill(2, "Giant Slash", "Spin into a wide slash", 10, 5, WeaponTypes.BroadSword));
        }
        public static Skill GetSkill(int id)
        {
            Skill requestedSkill;
            requestedSkill = EntireSkillList[id];
            return requestedSkill.Clone();
        }
    }
}
