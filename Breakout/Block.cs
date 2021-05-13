using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {
    public class Block: Entity { 
        int value;
        protected int health;
        public int Health{
            get{return health;}
        }
        public Block(StationaryShape shape, IBaseImage image): base(shape,image) {
            health = 1;
        }
        public virtual void HealthDown() {
            health--;
            if(health <= 0)
            {
                //ScoreBoard.AddPoint();
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