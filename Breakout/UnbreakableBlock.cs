using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {
    public class UnbreakableBlock : Block { 
        public UnbreakableBlock(StationaryShape shape, string filePath): base(shape,filePath) {
            
        }
        ///<summary>
        /// Overrides the "HealthDown function inherited from blocks, since this block cant lose HP
        ///</summary>
        public override void HealthDown() {
            
        }
        ///<summary>
        /// Checks if block is dead
        ///</summary>
        ///<returns>Either true or false, depending on the health of the block</returns>
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