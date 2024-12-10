using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace ProjectRaymondIgbineweka.Manager_s
{
    internal class GameStateManager
    {
        private GameState currentGameState;

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
                UpdateStartMenu();
                break;
            case GameState.Playing:
                levelManager.Update(gameTime, playerManager);
                break;
            case GameState.GameOver:
                UpdateGameOver();
                break;
        }
    }
    }
}
