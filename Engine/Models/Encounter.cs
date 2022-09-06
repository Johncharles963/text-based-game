using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Factories;
namespace Engine.Models
{
    public class Encounter
    {
        private int _enemyCount;
        private int _id;
        private string _name;
        private string _intro;
        private string _outro;
        private Enemy[] _enemies;
        public int EnemyCount
        {
            get { return _enemyCount; }
        }
        private int Id
        {
            get { return _id; }
        }
        public string Name
        {
            get { return _name; }
        }
        public string Outro
        {
            get { return _outro; }
        }
        public string Intro
        {
            get { return _intro; }
        }
        public Enemy[] Enemies
        {
            get { return _enemies; }
        }
        public Encounter(int id, string name, int enemyCount, string intro, string outro, Enemy firstEnemy)
        {
            _enemies = new Enemy[enemyCount];
            _id = id;
            _name = name;
            _enemyCount = enemyCount;
            _intro = intro;
            _outro = outro;
            _enemies[0] = firstEnemy;
        }
        public Encounter(int id, string name, int enemyCount, string intro, string outro, Enemy firstEnemy, Enemy secondEnemy):
            this(id, name, enemyCount, intro, outro, firstEnemy)
        {
            _enemies[1] = secondEnemy;
        }
        public Encounter(int id, string name, int enemyCount, string intro, string outro, Enemy firstEnemy, Enemy secondEnemy, Enemy thirdEnemy) :
          this(id, name, enemyCount, intro, outro, firstEnemy, secondEnemy)
        {
            _enemies[2] = thirdEnemy;
        }

        public Encounter Clone()
        {
            switch(_enemyCount)
            {
                case 1:
                    return new Encounter(_id, _name, _enemyCount, _intro, _outro,EnemyFactory.GetEnemy(_enemies[0].Id));
                    break;
                case 2:
                    return new Encounter(_id, _name, _enemyCount, _intro, _outro, EnemyFactory.GetEnemy(_enemies[0].Id), EnemyFactory.GetEnemy(_enemies[1].Id));
                    break;
                case 3:
                    return new Encounter(_id, _name, _enemyCount, _intro, _outro, EnemyFactory.GetEnemy(_enemies[0].Id), EnemyFactory.GetEnemy(_enemies[1].Id), EnemyFactory.GetEnemy(_enemies[2].Id));
                    break;
                                    default:
                    return null;
            }
        }
    }
}
