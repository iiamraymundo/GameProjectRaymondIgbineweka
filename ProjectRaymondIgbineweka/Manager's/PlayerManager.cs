using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectRaymondIgbineweka.Manager_s
{
    internal class PlayerManager
    {
        public Vector2 Position { get; private set; }
        private Texture2D playerTexture;
        private int lives;



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
    }
}
