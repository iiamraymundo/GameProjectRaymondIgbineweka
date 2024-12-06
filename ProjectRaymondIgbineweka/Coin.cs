using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectRaymondIgbineweka
{
    internal class Coin
    {
        public Vector2 Position { get; private set; }
        private Texture2D texture;
        public bool IsCollected { get; private set; }

        public Coin(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
            this.IsCollected = false;
        }

        public void Collect()
        {
            IsCollected = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsCollected)
            {
                spriteBatch.Draw(texture, Position, Color.Yellow);
            }
        }

    }
}
