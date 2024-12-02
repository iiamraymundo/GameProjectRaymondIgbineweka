using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectRaymondIgbineweka
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private enum GameState { StartScreen, Playing, GameOver}
        private GameState currentGameState = GameState.StartScreen;

        private SpriteFont startFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            startFont = Content.Load<SpriteFont>("StartFont"); // Ik maak hier een SpriteFont nadat ik het startscherm heb ingeladen
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            switch (currentGameState)
            {
                case GameState.StartScreen:
                    if (state.IsKeyDown(Keys.Enter))
                    {
                        currentGameState = GameState.Playing;
                    }
                    break;

                case GameState.Playing:
                    break;

                case GameState.GameOver:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (currentGameState)
            {
                case GameState.StartScreen:
                    _spriteBatch.DrawString(startFont, "Press ENTER to start", new Vector2(300, 250), Color.White);
                    break;

                case GameState.Playing:
                    break;

                case GameState.GameOver:
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
