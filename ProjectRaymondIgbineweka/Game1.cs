using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectRaymondIgbineweka.Manager_s;
using System.Collections.Generic;

namespace ProjectRaymondIgbineweka
{
    public class Game1 : Game
    {
        //deze 3 zeker behouden
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static bool GameOver = false;
        
        // Game state
        private GameState currentGameState = GameState.StartScreen;       

        // Managers        
        private GameStateManager gameStateManager;
        private PlayerManager player;
        private LevelManager levelManager;
        private UIManager uiManager;

        private CollisionManager collisionManager;

        // Game assets/objects
        private SpriteFont startFont;
        private Texture2D playerTexture, enemyTexture, coinTexture, backgroundTexture;
        private List<Enemy> enemies; // Bevat alle vijanden in het spel
        private List<Coin> coins;
        private FinalBoss finalBoss;

        // Background
        private Vector2 backgroundPosition1, backgroundPosition2;
        private float backgroundSpeed = 3f;


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

            gameStateManager = new GameStateManager();
            player = new PlayerManager(playerTexture);
            enemies = new List<Enemy>();
            coins = new List<Coin>();
            

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            startFont = Content.Load<SpriteFont>("StartFont"); // Ik maak hier een SpriteFont nadat ik het startscherm heb ingeladen            
            playerTexture = Content.Load<Texture2D>("player");            
            enemyTexture = Content.Load<Texture2D>("enemy");

            

            // Laad enemies en coins
            LoadInitialEnemiesAndCoins();

            // Final boss setup
            finalBoss = new FinalBoss(enemyTexture, new Vector2(600, 200));

        }

        private void LoadInitialEnemiesAndCoins()
        {
            enemies.Add(new Enemy(enemyTexture, new Vector2(500, 300), 20f));
            coins.Add(new Coin(coinTexture, new Vector2(400, 350)));
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            switch (currentGameState)
            {
                case GameState.StartScreen:
                    UpdateStartScreen(state);
                    break;

                case GameState.Playing:
                    UpdatePlaying(gameTime,state);
                    break;

                case GameState.GameOver:
                    UpdateGameOver(state);
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
                    DrawStartScreen();
                    break;

                case GameState.Playing:
                    DrawPlaying();
                    break;

                case GameState.GameOver:
                    DrawGameOver();
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdateStartScreen(KeyboardState state) // dit ook straks weg
        {
            if (state.IsKeyDown(Keys.Enter))
            {
                currentGameState = GameState.Playing;
            }
        }

        private void UpdatePlaying(GameTime gameTime, KeyboardState keyboardState)
        {
            // Update spelerpositie op basis van toetsenbordinvoer
            Vector2 movement = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Up)) movement.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.Down)) movement.Y += 1;
            if (keyboardState.IsKeyDown(Keys.Left)) movement.X -= 1;
            if (keyboardState.IsKeyDown(Keys.Right)) movement.X += 1;

            if (movement.Length() > 0)
            {
                movement.Normalize();
                playerPosition += movement * playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Limiteer de spelerpositie binnen de schermgrenzen
                playerPosition.X = MathHelper.Clamp(playerPosition.X, 0, _graphics.PreferredBackBufferWidth - playerTexture.Width);
                playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, _graphics.PreferredBackBufferHeight - playerTexture.Height);
            }

            if (isPlayerInvincible)
            {
                invincibilityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibilityTimer >= invincibilityDuration)
                {
                    isPlayerInvincible = false;
                    invincibilityTimer = 0f;
                }
            }

            // Update vijand Type 1
            enemy.Update(gameTime, playerPosition);

            // Vijand Type 2
            enemyTypeTwo.Update(gameTime);


            // Controleer op Game Over (bijvoorbeeld: botsing met vijand)
            if (Vector2.Distance(playerPosition, enemy.Position) < 50)
            {
                currentGameState = GameState.GameOver;
            }

            // Botsing met vijanden
            if (!isPlayerInvincible && (Vector2.Distance(playerPosition, enemy.Position) < 50 || Vector2.Distance(playerPosition, enemyTypeTwo.Position) < 50))
            {
                playerLives--;
                isPlayerInvincible = true;

                if (playerLives <= 0)
                {
                    currentGameState = GameState.GameOver;
                }
            }

            // Check voor coins
            foreach (var coin in coins)
            {
                if (!coin.IsCollected && Vector2.Distance(playerPosition, coin.Position) < 30)
                {
                    coin.Collect();
                    score += 10;

                    if (score >= 50) // Victory bij score 50
                    {
                        currentGameState = GameState.GameOver;
                    }
                }
            }
        }

        private void UpdateGameOver(KeyboardState state) // doe dit straks weg, staat in GameStateManager
        {
            if (state.IsKeyDown(Keys.Enter))
            {
                currentGameState = GameState.StartScreen;
                playerPosition = new Vector2(400, 300); // Reset spelerpositie
            }
        }

        private void DrawStartScreen()
        {
            string startText = "Press Enter to Start!";
            Vector2 startTextSize = startFont.MeasureString(startText);
            _spriteBatch.DrawString(
                startFont,
                startText,
                new Vector2((_graphics.PreferredBackBufferWidth - startTextSize.X) / 2,
                            (_graphics.PreferredBackBufferHeight - startTextSize.Y) / 2),
                Color.White);
        }
        private void DrawPlaying()
        {
            // Teken speler
            _spriteBatch.Draw(playerTexture, playerPosition, Color.White);

            // Teken vijand
            enemy.Draw(_spriteBatch);

            foreach (var coin in coins)
            {
                coin.Draw(_spriteBatch);
            }

            _spriteBatch.DrawString(startFont, $"Score: {score}", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(startFont, $"Lives: {playerLives}", new Vector2(10, 40), Color.White);

            // Vijand Type 2
            enemyTypeTwo.Draw(_spriteBatch);
        }
        private void DrawGameOver()
        {
            string gameOverText = "Game Over! Press Enter to Restart.";
            Vector2 gameOverTextSize = startFont.MeasureString(gameOverText);
            _spriteBatch.DrawString(
                startFont,
                gameOverText,
                new Vector2((_graphics.PreferredBackBufferWidth - gameOverTextSize.X) / 2,
                            (_graphics.PreferredBackBufferHeight - gameOverTextSize.Y) / 2),
                Color.Red);
        }


    }
}
