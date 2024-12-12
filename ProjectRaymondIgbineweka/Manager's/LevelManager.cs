using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace ProjectRaymondIgbineweka.Manager_s
{
    internal class LevelManager
    {
        private List<Enemy> enemies;
        private List<Vector2> coins;

        public LevelManager(Texture2D enemyTexture, Texture2D coinTexture)
        {
            // Initialiseer vijanden en coins
            enemies = new List<Enemy>
            {
                new Enemy(enemyTexture, new Vector2(300,200), 50f), // 3de element (speed) nog aanpassen indien nodig
                new Enemy(enemyTexture, new Vector2(500,150), 50f)
            };

            coins = new List<Vector2>
            {
                new Vector2(400,200),
                new Vector2(600,150)
            };
        }

        public void Update(GameTime gameTime, PlayerManager playerManager)
        {
            for (int i = enemies.Count; i >= 0; i--)
            {
                Enemy enemy = enemies[i];
                enemy.Update(gameTime, playerManager.Position);

                // Als er botsingen zijn tussen spelers en vijanden

                if (Vector2.Distance(playerManager.Position, enemy.Position) < 50)
                {
                    if (playerManager.IsjumpingOn(enemy))
                    {
                        enemies.RemoveAt(i); // Vijand is verslagen
                    }
                    else
                    {
                        playerManager.TakeDamage();
                    }
                }
            }

            // Update coins: check of de speler een coin heeft opgepakt
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                if (Vector2.Distance(playerManager.Position, coins[i] < 30){
                    coins.RemoveAt(i); //verwijder coin
                    
                }
            }
        }


    }
}
