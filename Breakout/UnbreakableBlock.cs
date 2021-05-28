using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {
    public class UnbreakableBlock: Block { 
        public UnbreakableBlock(StationaryShape shape, string filePath): base(shape,filePath) {
            health = 1;
        }
        public override void HealthDown() {
            
        }
        public bool IsBlockDead() {
            if (Health <= 0) {
                return true;
            }
            return false;
        }

        public Shape GetShape() {
            return Shape;
        }
    }
}