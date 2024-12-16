using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectRaymondIgbineweka
{
    internal class Enemy
    {
        
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }


        
        public Enemy(Texture2D texture, Vector2 startPosition, float speed)
        {
            Texture = texture;
            Position = startPosition;
            Speed = speed;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            Vector2 direction = playerPosition - Position;
            if(direction.Length() > 0)
            {
                direction.Normalize();
            }

            Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        /* 
         // enemies aanmaken
            enemies = new List<Enemy>
            {
                new Enemy(enemyTexture,new Vector2(300,200),90f),
                new Enemy(enemyTexture,new Vector2(500,150),90f)
            };
         */
    }
}
