using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectRaymondIgbineweka
{
    internal class Player
    {
        private Game1 root;
        private Vector2 position;
        private Texture2D spriteImage;
        private float spriteWidth;      

       

        public float SpriteHeight
        {
            get
            {
                float scale = spriteWidth / SpriteImage.Width;
                return SpriteImage.Height * scale;
            }
        }

        public Rectangle positionRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
            }
        }

        private float movementSpeed = 4.0f;
        public Texture2D SpriteImage { get => spriteImage; set => spriteImage = value; }

        public Player(Game1 root, Vector2 position)
        {
            this.root = root;
            this.position = position;
            this.spriteWidth = 128.0f;

            LoadContent();

        }

        public void LoadContent()
        {
            this.SpriteImage = root.Content.Load<Texture2D>("Main_Char");
        }
        public void Update(GameTime gameTime)
        {
             
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteImage, positionRectangle, Color.White);
        }
    }
}
