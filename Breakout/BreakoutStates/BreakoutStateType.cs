
using System;
namespace Breakout.BreakoutStates{
    public enum GameStateType{
        GameRunning,
        GamePaused,
        MainMenu,
        GameLost,
        GameWon
    }
    public class StateTransformer{
        /// <summary>
        /// Transforms the given string to the GameStateType
        /// </summary>
        /// <param name="state"></param>
        /// <returns> GamestateType</returns>
        public static GameStateType TransformStringToState(string state){
            switch (state){
                case "GameRunning":
                    return GameStateType.GameRunning;
                case "GamePaused":
                    return GameStateType.GamePaused;
                case "MainMenu":
                    return GameStateType.MainMenu;
                case "GameLost":
                    return GameStateType.GameLost;
                case "GameWon":
                    return GameStateType.GameWon;
                default:
                    throw new ArgumentException("Invalid string");
            }
        }
        /// <summary>
        /// Transforms the given state to the corresponding string
        /// </summary>
        /// <param name="state"></param>
        /// <returns> string of the game state</returns>
        public static string TransformStateToString(GameStateType state){
            switch (state){
                case GameStateType.GameRunning:
                    return "GameRunning";
                case GameStateType.GamePaused:
                    return "GamePaused";
                case GameStateType.MainMenu:
                    return "MainMenu";
                case GameStateType.GameLost:
                    return "GameLost";
                case GameStateType.GameWon:
                    return "GameWon";
                default:
                    throw new ArgumentException("Invalid string");                
            }
        }
    }

}