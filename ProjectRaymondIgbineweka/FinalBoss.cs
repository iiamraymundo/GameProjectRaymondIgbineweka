using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRaymondIgbineweka
{
    internal class FinalBoss
    {
        public Vector2 Position { get; private set; }
        private Texture2D texture;
        private int maxHealth;
        private int currentHealth;

        public FinalBoss(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
            this.maxHealth = 100;
            this.currentHealth = 100;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }

        public bool isDefeated()
        {
            return currentHealth <= 0;
        }

        
    }
}
