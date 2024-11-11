using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectRaymondIgbineweka
{
    public class Game1 : Game
    {
        private Texture2D PlayerImage;
        private GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            PlayerImage = Content.Load<Texture2D>("monkey");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            Rectangle playerDestinationRect = new Rectangle(0, 0, PlayerImage.Width, PlayerImage.Height);
            _spriteBatch.Draw(PlayerImage, playerDestinationRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
