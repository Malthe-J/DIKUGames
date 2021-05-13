using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.GUI;
using Breakout.BreakoutStates;


namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public BreakoutStates.GameStateType prevType;
        private Window window;

        public StateMachine(Window window) {
            // BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            // BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance(window);
            window = window;
            prevType = BreakoutStates.GameStateType.MainMenu;
            window.SetKeyEventHandler(KeyHandler);
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance(prevType);
                    prevType = BreakoutStates.GameStateType.GameRunning;
                    break;
                case GameStateType.GamePaused:
                    ActiveState= GamePaused.GetInstance(window);
                    break;
                case GameStateType.MainMenu:
                    ActiveState= MainMenu.GetInstance(window);
                    prevType = BreakoutStates.GameStateType.MainMenu;
                    break;
             }
        }
        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.StringArg1) {
                case "CHANGE_STATE":
                    SwitchState(StateTransformer.TransFormStringToState(gameEvent.Message));
                    break;
            }
        }

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