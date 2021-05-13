using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {
    public class HardenedBlock: Block { 
        public HardenedBlock(StationaryShape shape, IBaseImage image): base(shape,image) {
            health*=2;
        }
        public void HealthDown() {
            health--;
            if(health <= 0)
            {
                DeleteEntity();
            }
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