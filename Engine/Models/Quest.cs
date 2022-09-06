using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Quest
    {
        private bool _inProgress = false;
        private bool _isCompleted = false;
        private string _name;
        private string _description;
        private int _id;
        private int _rewardGold;
        private int _reccomendedLevel;
        public int RewardGold
        {
            get { return _rewardGold; }
        }
        public int ReccomendedLevel
        {
            get { return _reccomendedLevel; }
        }
        public int Id
        {
            get { return _id; }
        }
        public string Name
        {
            get { return _name; }
        }
        public string Description
        {
            get { return _description; }
        }
        public bool InProgress
        {
            get { return _inProgress; }
            set { _inProgress = value; }
        }
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; }
        }
        public Quest(int id, string name, string description, int reccomendedLevel, int rewardGold)
        {
            _id = id;
            _name = name;
            _description = description;
            _reccomendedLevel =reccomendedLevel;
            _rewardGold =rewardGold;
        }
        public Quest Clone()
        {
            return new Quest(_id, _name, _description,_reccomendedLevel,_rewardGold);
        }
    }
}
