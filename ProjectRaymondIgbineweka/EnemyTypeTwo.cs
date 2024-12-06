using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace ProjectRaymondIgbineweka
{
    internal class EnemyTypeTwo
    {
        public Vector2 Position { get; private set; }
        private Texture2D texture;
        private Vector2 direction;
        private float speed;

        private Random random;

        public EnemyTypeTwo(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            this.Position = startPosition;
            this.speed = 100f; // Beweeg snelheid
            this.random = new Random();

            // Willekeurige start richting
            this.direction = new Vector2((float)(random.NextDouble() * 2 - 1), (float)(random.NextDouble() * 2 - 1));
            this.direction.Normalize();
        }

        public void Update(GameTime gameTime)
        {
            Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Als de vijand de randen raakt, verander richting
            if (Position.X <= 0 || Position.X + texture.Width >= 800) // Verander 800 naar je canvas breedte
                direction.X *= -1;

            if (Position.Y <= 0 || Position.Y + texture.Height >= 480) // Verander 480 naar je canvas hoogte
                direction.Y *= -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.Red);
        }
    }
}
