using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Skill
    {
        private int _id;
        private int _damage;
        private int _staminaCost;
        private string _name;
        private string _description;
        private WeaponTypes _weaponType;
        public int Id
        {
            get { return _id; }
        }
        public int Damage
        {
            get { return _damage;}
        }
        public int StaminaCost
        {
            get { return _staminaCost; }
        }
        public string Name
        {
            get { return _name; }
        }
        public WeaponTypes WeaponType
        {
            get { return _weaponType; }
        }
        public string Description
        {
            get { return Description; }
        }
        
        public Skill(int id, string name, string description, int damage, int staminaCost, WeaponTypes weaponType)
        {
            _id =id;
            _damage =damage;
            _staminaCost =staminaCost;
            _name =name;
            _description =description;
            _weaponType =weaponType;
        }

        public Skill Clone()
        {
            return new Skill(_id, _name, _description, _damage, _staminaCost, _weaponType);
        }
    }
}
