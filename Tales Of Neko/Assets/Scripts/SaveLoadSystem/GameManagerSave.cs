using System;
using System.Collections.Generic;

namespace SaveLoadSystem
{
    [Serializable]
    public class GameManagerSave
    {

        public PlayerSave PlayerSave;
        public List<EnemySave> EnemiesSave;
        public int EnemyAttacked;


        public static GameManagerSave Save(GameManager gameManager)
        {
            GameManagerSave save = new GameManagerSave();
            save.PlayerSave = PlayerSave.Save(gameManager.player);
            save.EnemiesSave = new List<EnemySave>();
            foreach (Mob enemy in gameManager.enemies)
            {
                save.EnemiesSave.Add(EnemySave.Save(enemy));
            }

            save.EnemyAttacked = Int32.MaxValue;
            return save;
        }

        public static GameManagerData Load(GameManagerSave save)
        {
            GameManagerData gameManager = new GameManagerData();
            gameManager.player = PlayerSave.Load(save.PlayerSave);
            gameManager.enemies = new List<Mob>();
            foreach (EnemySave enemySave in save.EnemiesSave)
            {
                gameManager.enemies.Add(EnemySave.Load(enemySave));
            }

            gameManager.enemyAttacked = save.EnemyAttacked;
            return gameManager;
        }
    }
}