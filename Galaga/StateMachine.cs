// using DIKUArcade.EventBus;
// using DIKUArcade.State;

// namespace Galaga.GalagaStates {
//     public class StateMachine : IGameEventProcessor<object> {
//         public IGameState ActiveState { get; private set; }

//         public StateMachine() {
//             GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
//             GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

//             ActiveState = MainMenu.GetInstance();
//         }

//         private void SwitchState(GameStateType stateType) {
//             //switch (stateType) { ... }
//         }
//     }
// }