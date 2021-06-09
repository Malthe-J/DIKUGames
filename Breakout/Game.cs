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
    public class Game : DIKUGame {
        private StateMachine state;
         
        public Game() : base(new WindowArgs {Title = "Breakout", Width = 500, Height = 500}) {   
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {GameEventType.GameStateEvent, GameEventType.InputEvent});
            state = new StateMachine(window);
        }

        /// <summary>
        /// This function makes sure that every update function is called
        /// </summary>
        public override void Update() {
            BreakoutBus.GetBus().ProcessEvents();
            state.ActiveState.UpdateState();
        }

        /// <summary>
        /// This function makes sure that every render function is called
        /// </summary>
        public override void Render() {
            state.ActiveState.RenderState();
        }
    }
}
