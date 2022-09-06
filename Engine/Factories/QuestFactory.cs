using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
namespace Engine.Factories
{
    public static class QuestFactory
    {
        private static List<Quest> QuestList;

        static QuestFactory()
        {
            QuestList = new List<Quest>();
            QuestList.Add(new Quest(0, "Wolf Troubles", "A pack of wolves has been prowling the outskirts of town. They have been attacking people and need to be dealt with. Go to the forest and thin them out.",1,5));
            QuestList.Add(new Quest(1, "Play hero", "Some kids were playing in the forest, but they got spooked and one of them dropped their toy sword. His parent is willing to pay gold for its return.", 1, 10));
            QuestList.Add(new Quest(2, "Pesky Bandits", "Bandits have been raiding caravans passing near the forest. Find their hideout in the forest and take care of them.", 2, 10));

        }

        public static Quest GetQuest(int id)
        {
            Quest requestedQuest;
            requestedQuest = QuestList[id];
            if (requestedQuest != null)
                return requestedQuest;
            return null;
        }
    }
}
