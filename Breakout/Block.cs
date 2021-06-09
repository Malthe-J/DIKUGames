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
        protected string filePath;
        public int Health{
            get{return health;}
        }

        public Block(StationaryShape shape, string filepath): base(shape,new Image(filepath)) {
            filepath = filePath;
            health = 1;
        }

        /// <summary>
        /// This function is a virtual function since the subclasses 
        /// might need to overide it
        /// </summary>
        public virtual void HealthDown() {
            health--;
    
            if(health <= 0)
            {
                DeleteEntity();
            }
        }


        /// <summary>
        /// Checks if the block's health is below or equal to zero
        /// </summary>
        /// <returns>a boolean statement</returns>
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