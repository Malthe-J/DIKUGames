using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {
    public class Block: Entity { 
        int value;
        private int health;
        public int Health{
            get{return health;}
        }
        public Block(StationaryShape shape, IBaseImage image, int helbred): base(shape,image) {
            health = helbred;
        }
        public void HealthDown() {
            health--;
        }
        public bool IsBlockDead() {
            if (Health <= 0) {
                return true;
            }
            return false;
        }
    }
}