using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectRaymondIgbineweka.Manager_s
{
    internal class PlayerManager
    {
        public Vector2 Position { get; private set; }
        private Texture2D playerTexture;
        private int lives;
        private int score;

        private Vector2 velocity;

        public PlayerManager(Texture2D texture)
        {
            playerTexture = texture;
            Position = new Vector2(100, 300);
            lives = 3;
            score = 0;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                velocity.X = -5f;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                velocity.X = 5f;
            }
            else
            {
                velocity.X = 0; // Stop met bewegen
            }
        }

        public bool IsjumpingOn(Enemy enemy)
        {
            return Position.Y + playerTexture.Height < enemy.Position.Y + 10;
        }

        public void TakeDamage()
        {
            lives--;
            if (lives <= 0)
            {
                // Speler is dood
            }
        }

        public void AddScore(int amount)
        {
            score += amount;
        }
    }
}
