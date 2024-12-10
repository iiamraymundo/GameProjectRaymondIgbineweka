using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace ProjectRaymondIgbineweka.Manager_s
{
    internal class GameStateManager
    {
        private GameState currentGameState;
        private Vector2 playerPosition;
        private KeyboardState state;

        public GameStateManager()
        {
            currentGameState = GameState.StartScreen;
        }
        public GameState CurrentGameState => currentGameState;

        public void SetGameState(GameState newState)
        {
            currentGameState = newState;
        }

        public void Update(GameTime gameTime, PlayerManager playerManager, LevelManager levelManager)
        {
            switch (currentGameState)
            {
                case GameState.StartScreen:
                    UpdateStartScreen(state);
                    break;
                case GameState.Playing:
                    levelManager.Update(gameTime, playerManager);
                    break;
                case GameState.GameOver:
                    UpdateGameOver(state);
                    break;
            }
        }

        private void UpdateStartScreen(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Enter))
            {
                currentGameState = GameState.Playing;
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

        
    }
}    
    

