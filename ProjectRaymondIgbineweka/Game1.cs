﻿using Microsoft.Xna.Framework;
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

        private Enemy enemy; //1 vijand, Ik ga hiervan een lijst maken wanneer ik meerdere vijanden heb

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
            
            // Vijand aanmaken als een gekleurde rechthoek
            Texture2D enemyTexture = new Texture2D(GraphicsDevice,50,50);
            Color[] enemyData = new Color[50 * 50];
            for (int i = 0; i < enemyData.Length; i++)
            {
                enemyData[i] = Color.Green;                
            }
            enemyTexture.SetData(enemyData);

            // Geef de tekstuur door aan de vijand
            enemy = new Enemy(enemyTexture, new Vector2(375, 50), 100f);

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

        private void UpdateStartScreen(KeyboardState state)
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

            // Update vijand
            enemy.Update(gameTime, playerPosition);

            // Controleer op Game Over (bijvoorbeeld: botsing met vijand)
            if (Vector2.Distance(playerPosition, enemy.Position) < 50)
            {
                currentGameState = GameState.GameOver;
            }
        }

        private void UpdateGameOver(KeyboardState state)
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
