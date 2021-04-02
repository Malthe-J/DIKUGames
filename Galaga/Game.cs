using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;
using Galaga.GalagaStates;


namespace Galaga
{
    public class Game{
        private Window window;
        private GameTimer gameTimer;
        private StateMachine state;
        public Game(){
            window = new Window("Galaga", 500, 500);
            gameTimer = new GameTimer(60, 60);
            GalagaBus.GetBus();
            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.
            InputEvent, GameEventType.GameStateEvent });
            window.RegisterEventBus(GalagaBus.GetBus());
            state = new StateMachine(ref window);
        }
        public void Run() {
            while (window.IsRunning()){
                gameTimer.MeasureTime();

                while (gameTimer.ShouldUpdate()){
                    window.PollEvents();
                    GalagaBus.GetBus().ProcessEvents();
                    state.ActiveState.UpdateGameLogic();
                }
                if (gameTimer.ShouldRender()){
                    window.Clear();

                    state.ActiveState.RenderState();

                    window.SwapBuffers();
                }

                if (gameTimer.ShouldReset()){
                   window.Title = $"Galaga | (UPS, FPS): ({gameTimer.CapturedUpdates}, {gameTimer.CapturedFrames})";
                }
            }
        }
    }
}