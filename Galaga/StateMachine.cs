using DIKUArcade.EventBus;
using DIKUArcade.State;
using DIKUArcade;

namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }
        public GalagaStates.GameStateType prevType;

        private Window window;

        public StateMachine(ref Window window) {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            
            ActiveState = MainMenu.GetInstance(ref window);
            prevType = GalagaStates.GameStateType.MainMenu;
            this.window = window;
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance(prevType);
                    prevType = GalagaStates.GameStateType.GameRunning;
                    break;
                case GameStateType.GamePaused:
                    //ActiveState= GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState= MainMenu.GetInstance(ref window);
                    prevType = GalagaStates.GameStateType.MainMenu;
                    break;
             }
        }
        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {
            switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    ActiveState.HandleKeyEvent("KEY_PRESS", gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    ActiveState.HandleKeyEvent("KEY_RELEASE", gameEvent.Message);
                    break;
                case "CHANGE_STATE":
                    switch(gameEvent.Message) {
                        case "GAME_RUNNING":
                            SwitchState(GameStateType.GameRunning);
                            break;
                        case "MAIN_MENU":
                            SwitchState(GameStateType.MainMenu);
                            break;
                    }
                    break;
            }
        }
    }
}