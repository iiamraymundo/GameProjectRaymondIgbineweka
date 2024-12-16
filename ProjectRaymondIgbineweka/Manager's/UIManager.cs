using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectRaymondIgbineweka.Manager_s
{
    internal class UIManager
    {
        private SpriteFont font;
        

        public UIManager(SpriteFont font)
        {
            this.font = font;
        }

        public void Draw(SpriteBatch spriteBatch, int lives, int score)
        {
            spriteBatch.DrawString(font, $"Lives: {lives}", new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, $"Score: {score}", new Vector2(10, 30), Color.White);

            
        }

        public void ShowGameOver()
        {
            // geluid, animaties,.. (is voor later als er tijd is)
        }
    }
}
