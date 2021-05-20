using System.Collections.Generic;

namespace SaveLoadSystem
{
    public class GameManagerData
    {
        public Player player;
        public List<Mob> enemies;

        public int enemyAttacked;
        public QuestManager QuestManager;

        public override string ToString()
        {
            return $"{nameof(player)}: {player}, {nameof(enemies)}: {enemies}, {nameof(enemyAttacked)}: {enemyAttacked}, {nameof(QuestManager)}: {QuestManager}";
        }
    }
}