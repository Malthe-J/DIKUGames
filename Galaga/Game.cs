using DIKUArcade.GUI;
using DIKUArcade.Timers;
using System.Collections.Generic;
using DIKUArcade.Events;
using Galaga.GalagaStates;
using DIKUArcade;


namespace Galaga
{
    public class Game{
        private Window window;
        private GameTimer gameTimer;
        private StateMachine state;
        public Game(){
            window = new Window(new WindowArgs {Title = "Galaga", Width = 500, Height = 500});
            gameTimer = new GameTimer(60, 60);
            GalagaBus.GetBus();
            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.
            InputEvent, GameEventType.GameStateEvent });
            //window.RegisterEventBus();
            state = new StateMachine(ref window);
        }
        public void Run() {
            while (window.IsRunning()){
                gameTimer.MeasureTime();

                while (gameTimer.ShouldUpdate()){
                    window.PollEvents();
                    GalagaBus.GetBus().ProcessEvents();
                    state.ActiveState.UpdateState();
                }
                if (gameTimer.ShouldRender()){
                    window.Clear();

                    state.ActiveState.RenderState();

                    window.SwapBuffers();
                }
// xd omg
                if (gameTimer.ShouldReset()){
                   window.Title = $"Galaga | (UPS, FPS): ({gameTimer.CapturedUpdates}, {gameTimer.CapturedFrames})";
                }
            }
        }
    }
}