using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.IO;
namespace Breakout.PowerUp{
    public class PowerUpBlock : Block{
        private PowerUp powerUp;
        public PowerUpBlock(StationaryShape shape, string filepath) : base(shape, filepath){
           powerUp = new PowerUp(new DynamicShape(shape.Position, new Vec2F(0.06f, 0.06f)), Path.Combine("Assets", "Images", "LifePickUp.png")); 
        }

        public override void HealthDown()
        {
            base.HealthDown();
            if (Health <= 0)
                powerUp.ShouldShow();
        }
    }
}