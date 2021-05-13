using System;
using System.IO;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.BreakoutStates;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout
{
    public class Game : DIKUGame {
        private StateMachine state;
         
        public Game() : base(new WindowArgs {Title = "Breakout", Width = 500, Height = 500}) {
        state = new StateMachine(window);   
        }
        public override void Update() {
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

    }
}
