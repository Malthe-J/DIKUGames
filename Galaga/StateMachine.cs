using DIKUArcade.EventBus;
using DIKUArcade.State;

namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            
            ActiveState = MainMenu.GetInstance();
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState= GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState= GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState= MainMenu.GetInstance();
                    break;
             }
        }
        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {

        }
    }
}