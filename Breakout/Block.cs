using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {
    public class Block: Entity { 
        private int Value;
        private int Health;
        public Block(StationaryShape shape, IBaseImage image): base(shape,image) {
           
        }
    }
}