using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {

    /// <summary>
    /// This class only change the health value inherited from block and doubles it.
    /// </summary>
    public class HardenedBlock: Block { 

        public HardenedBlock(StationaryShape shape, string filepath): base(shape,filepath) {
            health*=2;
        }
        
    }
}