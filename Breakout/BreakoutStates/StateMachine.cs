using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.GUI;
using Breakout.BreakoutStates;
using System;
namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public BreakoutStates.GameStateType prevType;
        private Window window;
        public StateMachine(Window window) {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, GameRunning.GetInstance(BreakoutStates.GameStateType.MainMenu));
            ActiveState = MainMenu.GetInstance(window);
            this.window = window;
            window.SetKeyEventHandler(KeyHandler);
        }
        /// <summary>
        /// Switches the game state to the given statetype
        /// </summary>
        /// <param name="stateType"> GameStateType enum</param>
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance(prevType);
                    prevType = BreakoutStates.GameStateType.GameRunning;
                    break;
                case GameStateType.GamePaused:
                    ActiveState= GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState= MainMenu.GetInstance(window);
                    prevType = BreakoutStates.GameStateType.MainMenu;
                    break;
                case GameStateType.GameLost:
                    ActiveState = GameLost.GetInstance(window);
                    break;
                case GameStateType.GameWon:
                    ActiveState = GameWon.GetInstance(window);
                    break;
             }
        }
        /// <summary>
        /// Handles when the game state should be switched
        /// </summary>
        /// <param name="gameEvent"></param>
        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.StringArg1) {
                case "CHANGE_STATE":
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.Message));
                    break;
            }
            GameRunning.GetInstance(prevType).ProcessEvent(gameEvent);
        }
        /// <summary>
        /// Handles the event when the keyboard key is pressed and released
        /// </summary>
        /// <param name="key"> keyboard key</param>
        /// <param name="action"></param>
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            switch(action){
                case KeyboardAction.KeyPress:
                    ActiveState.HandleKeyEvent(action, key);
                    break;
                case KeyboardAction.KeyRelease:
                    ActiveState.HandleKeyEvent(action, key);
                    break;
            }
        }
    }
}