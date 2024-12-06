using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public void Draw(SpriteBatch spriteBatch)
        {
            //eindbaas tekenen
            spriteBatch.Draw(texture, Position, Color.Purple);

            // Teken de HP-balk
            var healthBarWidth = 100;
            var healthBarHeight = 10;
            var healthPercentage = (float)currentHealth / maxHealth;
            spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y - 20, (int)(healthBarWidth * healthPercentage), healthBarHeight), Color.Red);
        }
    }
}
