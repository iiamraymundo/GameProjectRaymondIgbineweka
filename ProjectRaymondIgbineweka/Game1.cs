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

        private Texture2D playerTexture; // Sprite van speler
        private Vector2 playerPosition; // Positie van speler
        private float playerSpeed = 200f; // Snelheid van speler

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

            //Speler aanmaken als een gekleurde rechthoek
            playerTexture = new Texture2D(GraphicsDevice, 50, 50);
            Color[] data = new Color[50 * 50];
            for (int i = 0; i < data.Length; i++) 
            {
                data[i] = Color.Red;
            }
            playerTexture.SetData(data);

            // Startpositie van de speler
            playerPosition = new Vector2(375, 500); // Midden-onderaan het scherm
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
                    //Spelerbewegingen
                    if (state.IsKeyDown(Keys.Left)) //Naar links bewegen
                        playerPosition.X -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (state.IsKeyDown(Keys.Right)) //Naar rechts bewegen
                        playerPosition.X += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (state.IsKeyDown(Keys.Up)) // Naar boven bewegen
                        playerPosition.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (state.IsKeyDown(Keys.Down)) // Naar beneden bewegen
                        playerPosition.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    playerPosition.X = MathHelper.Clamp(playerPosition.X, 0, _graphics.PreferredBackBufferWidth - playerTexture.Width);
                    playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, _graphics.PreferredBackBufferHeight - playerTexture.Height);
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
                    // Speler tekenen
                    _spriteBatch.Draw(playerTexture, playerPosition, Color.White);
                    break;

                case GameState.GameOver:
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
