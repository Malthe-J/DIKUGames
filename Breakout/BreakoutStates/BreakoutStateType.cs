
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