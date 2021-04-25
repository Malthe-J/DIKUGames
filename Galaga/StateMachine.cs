using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.GUI;
using Galaga.GalagaStates;


namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public GalagaStates.GameStateType prevType;

        private Window window;

        public StateMachine(ref Window window) {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            
            ActiveState = MainMenu.GetInstance(ref window);
            prevType = GalagaStates.GameStateType.MainMenu;
            this.window = window;
            window.SetKeyEventHandler(KeyHandler);
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance(prevType);
                    prevType = GalagaStates.GameStateType.GameRunning;
                    break;
                case GameStateType.GamePaused:
                    ActiveState= GamePaused.GetInstance(ref window);
                    break;
                case GameStateType.MainMenu:
                    ActiveState= MainMenu.GetInstance(ref window);
                    prevType = GalagaStates.GameStateType.MainMenu;
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