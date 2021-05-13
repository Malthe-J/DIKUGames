using System;
using System.IO;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using Breakout.BreakoutStates;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout
{
    public class Game : DIKUGame, IGameEventProcessor {
        private StateMachine state;
         
        public Game() : base(new WindowArgs {Title = "Breakout", Width = 500, Height = 500}) {   
        BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {GameEventType.GameStateEvent, GameEventType.InputEvent});
        state = new StateMachine(window);
        }
        public override void Update() {
            BreakoutBus.GetBus().ProcessEvents();
            state.ActiveState.UpdateState();
        }
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if (action==KeyboardAction.KeyPress && key==KeyboardKey.Escape) {
                window.CloseWindow();
            }

        }
        public override void Render() {
            state.ActiveState.RenderState();
        }
        public void ProcessEvent(GameEvent gameevent) {
            state.ProcessEvent(gameevent);
        }
    }
}
