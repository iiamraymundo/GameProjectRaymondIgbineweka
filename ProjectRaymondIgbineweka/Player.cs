using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            // komt nog wel later
            this.SpriteImage = root.Content.Load<Texture2D>("");
        }
        public void Update(GameTime gameTime)
        {
            // Get current keyboard state
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Handle any movement input
            HandleInput(currentKeyboardState);
        }
        private void HandleInput (KeyboardState currentKeyboardState)
        {
            // Get all the key states
            bool upKeyPressed = currentKeyboardState.IsKeyDown(Keys.Up);

            bool downKeyPressed = currentKeyboardState.IsKeyDown(Keys.Down);

            bool leftKeyPressed = currentKeyboardState.IsKeyDown(Keys.Left);

            bool rightKeyPressed = currentKeyboardState.IsKeyDown(Keys.Right);

            // If Up is pressed, decrease position Y
            if (upKeyPressed)
            {
                position.Y -= movementSpeed;
            }
            if (downKeyPressed)
            {
                position.Y += movementSpeed;
            }
            if (leftKeyPressed)
            {
                position.X -= movementSpeed;
            }
            if (rightKeyPressed)
            {
                position.Y += movementSpeed;
            }

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteImage, positionRectangle, Color.White);
        }
    }
}
